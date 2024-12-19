using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.RateLimiting
{
    public static class RateLimiterConfiguration
    {
        public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
        {
            services.Configure<YourNamespace.RateLimiting.RateLimiterOptions>(options =>
            {
                options.MaxRequests = 100;
                options.WindowDurationInSeconds = 60;
            });

            services.AddRateLimiter(options =>
            {
                var provider = services.BuildServiceProvider();
                var rateLimiterSettings = provider.GetRequiredService<IOptions<YourNamespace.RateLimiting.RateLimiterOptions>>().Value;

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddFixedWindowLimiter("FixedWindowPolicy", config =>
                {
                    config.PermitLimit = rateLimiterSettings.MaxRequests;
                    config.Window = TimeSpan.FromSeconds(rateLimiterSettings.WindowDurationInSeconds);
                });
            });

            return services;
        }
    }
}
