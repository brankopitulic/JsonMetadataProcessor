using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Presentation.Filters;

namespace Presentation;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });

        // Add Swagger configuration
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Clinical Trials API",
                Version = "v1"
            });

            // Enable support for file uploads
            c.OperationFilter<FileUploadOperationFilter>();

            c.MapType<TrialStatus>(() => new OpenApiSchema
            {
                Type = "string",
                Enum = Enum.GetNames(typeof(TrialStatus))
                            .Select(e => new Microsoft.OpenApi.Any.OpenApiString(e) as Microsoft.OpenApi.Any.IOpenApiAny)
                            .ToList()
            });
        });

        return services;
    }
}