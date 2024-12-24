using AutoMapper;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers.CommandHandlers;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Tests.Commands.Hotels
{
    public class CreateHotelCommandHandlerTests
    {
        private readonly Mock<IHotelRepository> _mockHotelRepository;
        private readonly Mock<IOwnerRepository> _mockOwnerRepository;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        private readonly CreateHotelCommandHandler _handler;

        public CreateHotelCommandHandlerTests()
        {
            _mockHotelRepository = new Mock<IHotelRepository>();
            _mockOwnerRepository = new Mock<IOwnerRepository>();
            _mockCityRepository = new Mock<ICityRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateHotelCommandHandler(_mockHotelRepository.Object, _mockOwnerRepository.Object, _mockCityRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateHotel_WhenAllIsValid()
        {
            // Arrange
            var command = new CreateHotelCommand
            {
                OwnerId = Guid.NewGuid(),
                CityId = Guid.NewGuid(),
                StarRating = 5,
                Geolocation = "Lat,Long",
                Name = "Awesome Hotel",
                PhoneNumber = "123456789"
            };

            _mockOwnerRepository.Setup(r => r.OwnerExistsAsync(It.IsAny<Expression<Func<Owner, bool>>>())).ReturnsAsync(true);
            _mockCityRepository.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<City, bool>>>())).ReturnsAsync(true);
            _mockHotelRepository.Setup(r => r.ExistsByPredicateAsync(It.IsAny<Expression<Func<Hotel, bool>>>())).ReturnsAsync(false);

            var mappedHotel = new Hotel
            {
                Id = Guid.NewGuid(),
                OwnerId = command.OwnerId,
                CityId = command.CityId,
                StarRating = command.StarRating,
                Geolocation = command.Geolocation,
                Name = command.Name,
                PhoneNumber = command.PhoneNumber
            };

            _mockMapper.Setup(m => m.Map<Hotel>(command)).Returns(mappedHotel);

            var createdHotel = new Hotel
            {
                Id = Guid.NewGuid(),
                OwnerId = mappedHotel.OwnerId,
                CityId = mappedHotel.CityId,
                StarRating = mappedHotel.StarRating,
                Geolocation = mappedHotel.Geolocation,
                Name = mappedHotel.Name,
                PhoneNumber = mappedHotel.PhoneNumber
            };

            _mockHotelRepository.Setup(r => r.AddHotelAsync(mappedHotel)).ReturnsAsync(createdHotel);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeEmpty();
            result.Should().Be(createdHotel.Id);
            _mockHotelRepository.Verify(r => r.AddHotelAsync(mappedHotel), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
