using ApplicationCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieShopAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;

        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                //read info from the HttpContext object and log it somewhere
                _logger.LogInformation("Inside the Middlware.");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //First catch the exception
                //Check the exception type, message
                //Check stacktrace, where the exception happened
                //When the exception happened
                //for which URL and which HTTP method (controller, action method)
                //for which user, if user is logged in

                var exceptionDetails = new ErrorModel
                {
                    Message = ex.Message,
                    ExceptionDateTime = DateTime.UtcNow,
                    //ExceptionType = ex.GetType().ToString(),
                    Path = httpContext.Request.Path,
                    HttpRequestType = httpContext.Request.Method,
                    User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null
                };

                //Save all this information somewhere, text files, json files or Database
                //asp.net core has built-in logging mechanism, (ILogger) which can be used by any 3rd party log provider
                //SeriLog (most popular) and NLog

                //Send email to Dev Team when exception happens
                _logger.LogError("Exception happened, log this to text or json files using SeriLog.");

                //return http status code 500
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize<ErrorModel>(exceptionDetails);
                await httpContext.Response.WriteAsync(result);
                //for MVC
                //httpContext.Response.Redirect("/home/error");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}
