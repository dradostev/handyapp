using System;
using System.Threading.Tasks;
using Handy.Domain.SharedContext.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Handy.App.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private static async Task HandleException(HttpContext context, Exception exception)
        {
            int code = 500;
            string message = "Internal server error";
            switch (exception)
            {
                case NotFoundException e:
                    code = 404;
                    message = e.Message ?? "Resource not found";
                    break;
                case UnauthorizedException e:
                    code = 401;
                    message = e.Message ?? "Unauthorized";
                    break;
                case AccessDeniedException e:
                    code = 403;
                    message = e.Message ?? "Access denied";
                    break;
                case ConflictException e:
                    code = 409;
                    message = e.Message ?? "Resource conflict";
                    break;
                case DomainLogicException e:
                    code = 400;
                    message = e.Message ?? "Business logic error";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            var content = JsonConvert.SerializeObject(new {error = message});
            await context.Response.WriteAsync(content);
        }
    }
}