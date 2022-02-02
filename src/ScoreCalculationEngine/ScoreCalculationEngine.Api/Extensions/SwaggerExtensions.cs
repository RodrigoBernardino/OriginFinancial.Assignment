#pragma warning disable 1591
using Microsoft.OpenApi.Models;

namespace ScoreCalculationEngine.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Origin Score Calculation Engine Api",
                    Version = "v1",
                    Description = "Origin backend home assignment",
                    Contact = new OpenApiContact
                    {
                        Name = "Rodrigo Bernardino",
                        Email = "rodrigobernardino1@gmail.com",
                        Url = new Uri("https://github.com/RodrigoBernardino")
                    }
                });

                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "ApiKey",
                    Description = "API access key token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                        },
                        new string[] { }
                    }
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "ScoreCalculationEngineApi.xml");
                options.IncludeXmlComments(filePath);
            });
        }
    }
}
