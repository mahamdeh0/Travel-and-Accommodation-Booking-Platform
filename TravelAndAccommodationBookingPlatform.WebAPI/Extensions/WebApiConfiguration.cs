using System.Text.Json.Serialization;
using TravelAndAccommodationBookingPlatform.WebAPI.Filters;
using TravelAndAccommodationBookingPlatform.WebAPI.Middlewares;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Extensions
{
    public static class WebApiConfiguration
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddApiVersioningSetup();
            services.AddSwaggerSetup();
            services.AddProblemDetails().AddExceptionHandler<GlobalExceptionHandler>();
            services.AddControllers(options => options.Filters.Add<ExecutionLoggerFilter>()).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddDateOnlyTimeOnlyStringConverters();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddFluentValidationSetup();
            services.AddAuthentication();
            services.AddAuthorization();

            return services;
        }
    }
}
