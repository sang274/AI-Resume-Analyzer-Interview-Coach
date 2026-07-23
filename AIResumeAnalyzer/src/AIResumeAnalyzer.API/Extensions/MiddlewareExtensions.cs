using AIResumeAnalyzer.API.Middlewares;

namespace AIResumeAnalyzer.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder
            UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
