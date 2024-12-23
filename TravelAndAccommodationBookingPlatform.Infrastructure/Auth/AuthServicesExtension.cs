using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Auth;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Auth
{
    public static class AuthServicesExtension
    {
        public static IServiceCollection AddAuthServices(
            this IServiceCollection services)
        {
            services.AddScoped<IValidator<JwtSettings>, JwtSettingsValidator>();

            services.AddOptions<JwtSettings>()
                .BindConfiguration(nameof(JwtSettings))
                .Configure(options =>
                {
                    var validator = services.BuildServiceProvider().GetRequiredService<IValidator<JwtSettings>>();
                    var validationResult = validator.Validate(options);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                        throw new Exception($"JWT settings validation failed: {errorMessages}");
                    }
                });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = GetTokenValidationParameters(services);
            });

            services.AddTransient<IJwtTokenGenerator, JwtTokenService>();

            return services;
        }

        private static TokenValidationParameters GetTokenValidationParameters(IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;

            var key = Encoding.UTF8.GetBytes(config.Key);

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config.Issuer,
                ValidAudience = config.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
