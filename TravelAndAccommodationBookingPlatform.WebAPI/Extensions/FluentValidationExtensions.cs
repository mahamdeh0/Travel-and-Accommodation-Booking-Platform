using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidationSetup(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
