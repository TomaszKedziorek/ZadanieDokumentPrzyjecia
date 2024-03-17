using System.Net;
using System.Text.Json;
using Api.Errors;

namespace Api.Middleware;
public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;
  private readonly IHostEnvironment _environment;

  public ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger,
    IHostEnvironment environment)
  {
    _next = next;
    _logger = logger;
    _environment = environment;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception e)
    {
      _logger.LogError(e, e.Message);
      context.Response.ContentType = "application/json";
      int statusCode = (int)HttpStatusCode.InternalServerError;
      context.Response.StatusCode = statusCode;

      ApiException response = _environment.IsDevelopment()
       ? new ApiException(statusCode, e.Message, e.StackTrace)
       : new ApiException(statusCode);

      JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
      string json = JsonSerializer.Serialize(response, options);
      await context.Response.WriteAsync(json);
    }
  }
}