using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TravelAndAccommodationBookingPlatform.Application.Extensions
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });

            services.AddAutoMapper(assembly);

            return services;
        }
    }
}
