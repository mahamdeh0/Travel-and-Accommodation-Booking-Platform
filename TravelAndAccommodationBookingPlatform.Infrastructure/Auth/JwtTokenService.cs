using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Auth;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Auth
{
    public class JwtTokenService : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtAuthToken CreateTokenForUser(User user)
        {
            var claims = new List<Claim>
        {
            new("sub", user.Id.ToString()),
            new("firstName", user.FirstName),
            new("lastName", user.LastName),
            new("email", user.Email)
        };

            claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(_jwtSettings.LifetimeMinutes),
                signingCredentials
            );

            var token = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return new JwtAuthToken(token);
        }
    }
}
