using Microsoft.AspNetCore.Mvc.Filters;
using TravelAndAccommodationBookingPlatform.WebAPI.Controllers;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Filters
{
    public class ExecutionLoggerFilter(ILogger<ExecutionLoggerFilter> logger) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerType = context.Controller.GetType();
            var controllerName = controllerType.Name;
            var methodName = context.ActionDescriptor.DisplayName;

            if (controllerType == typeof(AuthenticationController))
            {
                logger.LogInformation("Starting execution of method: {MethodName} on controller: {ControllerName}", methodName, controllerName);
                await next();

                logger.LogInformation("Finished execution of method: {MethodName} on controller: {ControllerName}", methodName, controllerName);
                return;
            }

            logger.LogInformation("Executing {MethodName} on controller {ControllerName} with arguments {@ActionArguments}", methodName, controllerName, context.ActionArguments);
            await next();

            logger.LogInformation("Action {MethodName} finished execution on controller {ControllerName} with arguments {@ActionArguments}", methodName, controllerName, context.ActionArguments);
        }
    }
}
