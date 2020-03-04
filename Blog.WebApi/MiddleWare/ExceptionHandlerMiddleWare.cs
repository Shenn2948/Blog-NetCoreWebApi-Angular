using System;
using System.Net;
using System.Threading.Tasks;
using Blog.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Blog.WebApi.MiddleWare
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _requestDelegate;

        private readonly ILogger<ExceptionHandlerMiddleWare> _logger;

        public ExceptionHandlerMiddleWare(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleWare> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is RequestedResourceNotFoundException notFound)
            {
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;

                return context.Response.WriteAsync(new ErrorDetails { StatusCode = context.Response.StatusCode, Message = $"{notFound.Message}" }.ToString());
            }

            if (exception is RequestedResourceHasConflictException conflict)
            {
                context.Response.StatusCode = (int) HttpStatusCode.Conflict;

                return context.Response.WriteAsync(new ErrorDetails { StatusCode = context.Response.StatusCode, Message = $"{conflict.Message}" }.ToString());
            }

            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails { StatusCode = context.Response.StatusCode, Message = $"{exception.Message}" }.ToString());
        }
    }
}