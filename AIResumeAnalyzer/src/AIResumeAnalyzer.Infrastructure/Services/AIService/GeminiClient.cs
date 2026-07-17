using AIResumeAnalyzer.Application.Common.Settings;
using AIResumeAnalyzer.Application.Features.AI.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Infrastructure.Services.AIService
{
    public interface IGeminiClient
    {
        Task<T> SendPromptAsync<T>(string prompt, CancellationToken cancellationToken);
    }

    public class GeminiClient : IGeminiClient
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiSettings _settings;

        public GeminiClient(HttpClient httpClient, IOptions<GeminiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<T> SendPromptAsync<T>(string prompt, CancellationToken cancellationToken)
        {
            var requestBody = new GeminiRequest
            {
                contents = new[] { new GeminiContent { parts = new[] { new GeminiPart { text = prompt } } } }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent?key={_settings.ApiKey}";

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Remove("Authorization");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new Exception($"Google API Error ({response.StatusCode}): {errorBody}");
            }

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var aiResult = geminiResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.text;
            if (string.IsNullOrWhiteSpace(aiResult)) throw new Exception("Gemini returned an empty response.");

            var cleanedJson = CleanJsonString(aiResult);

            var result = JsonSerializer.Deserialize<T>(cleanedJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result ?? throw new Exception($"Unable to deserialize Gemini response to {typeof(T).Name}.");
        }

        private string CleanJsonString(string rawJson)
        {
            var cleaned = rawJson.Trim();
            if (cleaned.StartsWith("```json")) cleaned = cleaned.Substring(7);
            if (cleaned.StartsWith("```")) cleaned = cleaned.Substring(3);
            if (cleaned.EndsWith("```")) cleaned = cleaned.Substring(0, cleaned.Length - 3);
            return cleaned.Trim();
        }
    }
}
