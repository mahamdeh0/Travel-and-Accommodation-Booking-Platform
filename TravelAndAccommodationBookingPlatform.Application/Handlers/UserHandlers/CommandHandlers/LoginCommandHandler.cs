using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Auth;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.UserHandlers.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IMapper mapper, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var authenticatedUser = await AuthenticateUserAsync(request.Email, request.Password);

            var jwtToken = _jwtTokenGenerator.CreateTokenForUser(authenticatedUser);

            return _mapper.Map<LoginResponseDto>(jwtToken);
        }

        private async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.AuthenticateUserAsync(email, password);
            if (user == null)
                throw new CredentialsNotValidException(UserMessages.InvalidCredentials);

            return user;
        }
    }
}
