using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;
using TravelAndAccommodationBookingPlatform.Infrastructure.Auth;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;
using TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repositories;
using TravelAndAccommodationBookingPlatform.Infrastructure.RateLimiting;
using TravelAndAccommodationBookingPlatform.Infrastructure.Repositories;
using TravelAndAccommodationBookingPlatform.Infrastructure.Services;
using TravelAndAccommodationBookingPlatform.Infrastructure.Services.Image;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Extensions
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext(config)
                    .AddAuthServices(config)
                    .AddCustomRateLimiter(config)
                    .AddRepositories()
                    .AddPasswordHashing()
                    .AddServices(config);

            return services;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure(3));
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookingRepository, BookingRepository>()
              .AddScoped<ICityRepository, CityRepository>()
              .AddScoped<IDiscountRepository, DiscountRepository>()
              .AddScoped<IHotelRepository, HotelRepository>()
              .AddScoped<IImageRepository, ImageRepository>()
              .AddScoped<IOwnerRepository, OwnerRepository>()
              .AddScoped<IReviewRepository, ReviewRepository>()
              .AddScoped<IRoleRepository, RoleRepository>()
              .AddScoped<IRoomClassRepository, RoomClassRepository>()
              .AddScoped<IRoomRepository, RoomRepository>()
              .AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection AddPasswordHashing(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var firebaseConfig = new FirebaseConfig
            {
                GoogleCredential = configuration["Firebase:GoogleCredential"],
                Bucket = configuration["Firebase:Bucket"]
            };
            services.AddSingleton(firebaseConfig);

            services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserSession, UserSession>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }

    }
}
