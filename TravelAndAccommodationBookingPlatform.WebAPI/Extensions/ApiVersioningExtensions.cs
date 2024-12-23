using Asp.Versioning;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Extensions
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection AddApiVersioningSetup(this IServiceCollection services)
        {
            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.ReportApiVersions = true;
                setupAction.DefaultApiVersion = new ApiVersion(1, 0);
                setupAction.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                setupAction.UnsupportedApiVersionStatusCode = StatusCodes.Status406NotAcceptable;
            }).AddApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });

            return services;
        }
    }
}
