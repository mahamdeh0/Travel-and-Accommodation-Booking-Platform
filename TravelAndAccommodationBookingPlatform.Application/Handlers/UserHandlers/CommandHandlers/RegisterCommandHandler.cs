using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.UserHandlers.CommandHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IMapper mapper, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.UserExistsByEmailAsync(request.Email))
                throw new UserWithEmailAlreadyExistsException(UserMessages.EmailAlreadyExists);

            var role = await _roleRepository.GetRoleByNameAsync(request.Role) ?? throw new InvalidRoleException(UserMessages.InvalidRole);
            var user = _mapper.Map<User>(request);
            user.Roles.Add(role);
            await _userRepository.AddUserAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
