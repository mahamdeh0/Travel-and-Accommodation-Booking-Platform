using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using TravelAndAccommodationBookingPlatform.Application.Extensions;
using TravelAndAccommodationBookingPlatform.Infrastructure.Extensions;
using TravelAndAccommodationBookingPlatform.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddWebApi().AddApplication().AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

app.UseRateLimiter();

app.UseSerilogRequestLogging(options => { options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("FixedWindowPolicy");

app.Run();