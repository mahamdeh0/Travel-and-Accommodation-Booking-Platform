﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Services
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedException(UserMessages.UserNotAuthenticated);

            return userId;
        }

        public string GetUserRole()
        {
            var roleClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(roleClaim))
                throw new UnauthorizedException(UserMessages.UserNotAuthenticated);

            return roleClaim;
        }

        public string GetUserEmail()
        {
            var emailClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(emailClaim))
                throw new UnauthorizedException(UserMessages.UserNotAuthenticated);

            return emailClaim;
        }
    }
}
