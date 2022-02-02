using Microsoft.Extensions.DependencyInjection;
using ScoreCalculationEngine.Application.Services;

namespace ScoreCalculationEngine.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ScoreCalculationService>();
        }
    }
}
