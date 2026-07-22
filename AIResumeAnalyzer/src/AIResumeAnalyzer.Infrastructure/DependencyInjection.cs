using AIResumeAnalyzer.Application.Common.Settings;
using AIResumeAnalyzer.Application.Features.Auth.Commands.Register;
using AIResumeAnalyzer.Application.Interfaces.IAIService;
using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using AIResumeAnalyzer.Application.Interfaces.IFileService;
using AIResumeAnalyzer.Application.Interfaces.IInterviewService;
using AIResumeAnalyzer.Application.Interfaces.IJobService;
using AIResumeAnalyzer.Application.Interfaces.IRepository;
using AIResumeAnalyzer.Application.Interfaces.Persistence;
using AIResumeAnalyzer.Infrastructure.Persistence;
using AIResumeAnalyzer.Infrastructure.Repositories;
using AIResumeAnalyzer.Infrastructure.Services.AIService;
using AIResumeAnalyzer.Infrastructure.Services.AuthenticateService;
using AIResumeAnalyzer.Infrastructure.Services.FileService;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AIResumeAnalyzer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();

            services.Configure<GeminiSettings>(configuration.GetSection("Gemini"));

            services.AddScoped<IAIResumeAnalyzerService, AIResumeAnalyzerService>();

            services.AddScoped<IJobMatchingService, AIJobMatchingService>();

            services.AddScoped<IInterviewCoachService, AIInterviewCoachService>();

            services.AddHttpClient<IGeminiClient, GeminiClient>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IJobMatchRepository, JobMatchRepository>();

            services.AddScoped<IInterviewSessionRepository, InterviewSessionRepository>();

            services.AddScoped<IInterviewQuestionRepository, InterviewQuestionRepository>();

            services.AddScoped<IResumeRepository, ResumeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IFileStorageService, LocalFileStorageService>();

            services.AddScoped<IResumeParserService, ResumeParserService>();

            return services;
        }

        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(
                    Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(typeof(RegisterCommand).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));

            return services;
        }
    }
}
