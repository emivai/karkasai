using FluentValidation;
using HappyPaws.Core.Exceptions.Common;
using System.Net;
using System.Text.Json;

namespace HappyPaws.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    ResourceNotFoundException => (int)HttpStatusCode.NotFound,
                    BadRequestException => (int)HttpStatusCode.UnprocessableEntity,
                    UserTypeException => (int)HttpStatusCode.UnprocessableEntity,
                    ValidationException => (int)HttpStatusCode.UnprocessableEntity,
                    ForbiddenException => (int)HttpStatusCode.Forbidden,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
