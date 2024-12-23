using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.RateLimiting
{
    public static class RateLimiterConfiguration
    {
        public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services, IConfiguration configuration)
        {
            var rateLimiterOptions = configuration.GetSection(nameof(RateLimiterOptions)).Get<RateLimiterOptions>();

            services.Configure<RateLimiterOptions>(configuration.GetSection(nameof(RateLimiterOptions)));

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddFixedWindowLimiter("FixedWindowPolicy", config =>
                {
                    config.PermitLimit = rateLimiterOptions.MaxRequests;
                    config.Window = TimeSpan.FromSeconds(rateLimiterOptions.WindowDurationInSeconds);
                });
            });

            return services;
        }

    }
}
