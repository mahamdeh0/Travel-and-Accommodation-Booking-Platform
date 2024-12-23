using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (exception is CustomException)
            {
                _logger.LogWarning(exception, "A known custom exception occurred: {Message}", exception.Message);
            }
            else
            {
                _logger.LogError(exception, "An unexpected error occurred: {Message}", exception.Message);
            }

            var (statusCode, title, detail) = GetProblemDetailsForException(exception);
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            var result = Results.Problem(statusCode: statusCode, title: title, detail: detail,
                         extensions: new Dictionary<string, object?> { ["traceId"] = traceId });
            await result.ExecuteAsync(httpContext);
            return true;
        }

        private static (int statusCode, string title, string detail) GetProblemDetailsForException(Exception exception)
        {
            if (exception is not CustomException customException)
                return (StatusCodes.Status500InternalServerError, "Internal Server Error", "An internal server error occurred. Please try again later.");

            var statusCode = customException switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException => StatusCodes.Status400BadRequest,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            return (statusCode, customException.Title, customException.Message);
        }
    }
}
