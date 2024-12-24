using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers.QueryHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;

namespace TravelAndAccommodationBookingPlatform.Tests.Queries.Cities
{
    public class GetTrendingCitiesQueryHandlerTests
    {
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetTrendingCitiesQueryHandler _handler;

        public GetTrendingCitiesQueryHandlerTests()
        {
            _mockCityRepository = new Mock<ICityRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetTrendingCitiesQueryHandler(_mockCityRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedCities_WhenCountIsValid()
        {
            // Arrange
            var query = new GetTrendingCitiesQuery { Count = 3 };

            var mockCities = new List<City>
            {
                new City { Id = Guid.NewGuid(), Name = "City1" },
                new City { Id = Guid.NewGuid(), Name = "City2" }
            };

            var mappedCities = new List<TrendingCityResponseDto>
            {
                new TrendingCityResponseDto { Name = "City1" },
                new TrendingCityResponseDto { Name = "City2" }
            };

            _mockCityRepository.Setup(r => r.GetTopMostVisitedAsync(3)).ReturnsAsync(mockCities);

            _mockMapper.Setup(m => m.Map<IEnumerable<TrendingCityResponseDto>>(mockCities)).Returns(mappedCities);

            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull().And.HaveCount(2);
            result.Should().BeEquivalentTo(mappedCities);
            _mockCityRepository.Verify(r => r.GetTopMostVisitedAsync(3), Times.Once);
            _mockMapper.Verify(m => m.Map<IEnumerable<TrendingCityResponseDto>>(mockCities), Times.Once);
        }
    }
}
