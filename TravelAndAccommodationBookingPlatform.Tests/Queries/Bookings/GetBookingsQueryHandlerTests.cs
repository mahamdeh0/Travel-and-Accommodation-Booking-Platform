using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers.QueryHandlers;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Tests.Queries.Bookings
{
    public class GetBookingsQueryHandlerTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly Mock<IUserSession> _mockUserSession;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetBookingsQueryHandler _handler;

        public GetBookingsQueryHandlerTests()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _mockUserSession = new Mock<IUserSession>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetBookingsQueryHandler(_mockBookingRepository.Object, _mockMapper.Object, _mockUserSession.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPaginatedResult_WhenUserIsGuest()
        {
            var userId = Guid.NewGuid();
            var query = new GetBookingsQuery
            {
                SortColumn = "CheckInDate",
                PageNumber = 1,
                PageSize = 10,
                OrderDirection = OrderDirection.Ascending
            };

            _mockUserSession.Setup(s => s.GetUserId()).Returns(userId);
            _mockUserSession.Setup(s => s.GetUserRole()).Returns("Guest");

            var items = new List<Booking>
            {
                new Booking { Id = Guid.NewGuid(), GuestId = userId },
                new Booking { Id = Guid.NewGuid(), GuestId = userId }
            };

            var paginationMetadata = new PaginationMetadata(
                TotalItemCount: items.Count,
                CurrentPage: 1,
                PageSize: 10
            );

            var paginatedBookings = new PaginatedResult<Booking>(items, paginationMetadata);

            _mockBookingRepository.Setup(repo => repo.GetBookingsAsync(It.IsAny<PaginatedQuery<Booking>>())).ReturnsAsync(paginatedBookings);

            var mappedItems = new List<BookingResponseDto>
            {
                new BookingResponseDto { Id = Guid.NewGuid() },
                new BookingResponseDto { Id = Guid.NewGuid() }
            };

            var mappedMetadata = new PaginationMetadata(
                TotalItemCount: mappedItems.Count,
                CurrentPage: 1,
                PageSize: 10
            );

            var mappedResult = new PaginatedResult<BookingResponseDto>(mappedItems, mappedMetadata);

            _mockMapper.Setup(m => m.Map<PaginatedResult<BookingResponseDto>>(paginatedBookings)).Returns(mappedResult);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.PaginationMetadata.TotalItemCount.Should().Be(2);
            result.PaginationMetadata.CurrentPage.Should().Be(1);
            result.PaginationMetadata.PageSize.Should().Be(10);
            result.PaginationMetadata.TotalPageCount.Should().Be(1);
            _mockBookingRepository.Verify(repo => repo.GetBookingsAsync(It.IsAny<PaginatedQuery<Booking>>()), Times.Once);
            _mockMapper.Verify(m => m.Map<PaginatedResult<BookingResponseDto>>(paginatedBookings), Times.Once);
        }

    }
}
