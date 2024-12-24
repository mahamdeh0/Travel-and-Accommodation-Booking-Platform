using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers.CommandHandlers;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Tests.Commands.Cities
{
    public class CreateCityCommandHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateCityCommandHandler _handler;

        public CreateCityCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Register<DateOnly>(() => DateOnly.FromDateTime(DateTime.UtcNow.Date));

            _mockCityRepository = _fixture.Freeze<Mock<ICityRepository>>();
            _mockUnitOfWork = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _handler = new CreateCityCommandHandler(_mockCityRepository.Object, _mockMapper.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateCity_WhenPostOfficeNotExists()
        {
            // Arrange
            var command = _fixture.Build<CreateCityCommand>()
                .With(c => c.PostOffice, "54321")
                .With(c => c.Name, "Jeddah")
                .With(c => c.Country, "Saudi Arabia")
                .With(c => c.Region, "Makkah Region")
                .Create();

            _mockCityRepository
                .Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<City, bool>>>()))
                .ReturnsAsync(false);

            var mappedCity = _fixture.Build<City>()
                .With(c => c.Name, command.Name)
                .With(c => c.Country, command.Country)
                .With(c => c.Region, command.Region)
                .With(c => c.PostOffice, command.PostOffice)
                .Create();

            _mockMapper
                .Setup(m => m.Map<City>(command))
                .Returns(mappedCity);

            var addedCity = _fixture.Build<City>()
                .With(c => c.Id, Guid.NewGuid())
                .With(c => c.Name, command.Name)
                .With(c => c.Country, command.Country)
                .With(c => c.Region, command.Region)
                .With(c => c.PostOffice, command.PostOffice)
                .Create();

            _mockCityRepository.Setup(r => r.AddAsync(mappedCity)).ReturnsAsync(addedCity);

            var cityDto = _fixture.Build<CityResponseDto>()
                .With(dto => dto.Id, addedCity.Id)
                .With(dto => dto.Name, addedCity.Name)
                .With(dto => dto.PostOffice, addedCity.PostOffice)
                .Create();

            _mockMapper.Setup(m => m.Map<CityResponseDto>(addedCity)).Returns(cityDto);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(addedCity.Id);
            result.Name.Should().Be(addedCity.Name);
            result.PostOffice.Should().Be(addedCity.PostOffice);
            _mockCityRepository.Verify(r => r.AddAsync(mappedCity), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
