using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserRepository(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task AddUserAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            user.Password = _passwordHasher.HashPassword(user.Password);

            await _context.Users.AddAsync(user);
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == email);

            if (user is null) return null;

            var passwordVerification = _passwordHasher.VerifyHashedPassword(user.Password, password);

            return passwordVerification == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
