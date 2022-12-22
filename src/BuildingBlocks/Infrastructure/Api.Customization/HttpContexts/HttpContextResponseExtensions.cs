using API.Customization.Exceptions;
using Domain.Seedwork.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace API.Customization.HttpContexts
{
    public static class HttpContextResponseExtensions
    {
        public static async Task WriteHandledExceptionToResponseAsync(
            this HttpContext context,
            Exception exception,
            ExceptionResponse response,
            bool isStackTraceOn)
        {
            response.AppendStackTrace(exception.StackTrace, exception.Source, isStackTraceOn);
            context.Response.StatusCode = response.Status ?? (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response!));
        }

        public static async Task WriteUnhandledExceptionToResponseAsync(
            this HttpContext context,
            Exception exception,
            bool isStackTraceOn)
        {
            var task = exception switch
            {
                HttpException httpRequestException => context.WriteHttpRequestExceptionResponseAsync(httpRequestException, isStackTraceOn),
                DomainException domainException => context.WriteBadRequestExceptionResponseAsync(domainException, isStackTraceOn),
                BadRequestException badRequestException => context.WriteBadRequestExceptionResponseAsync(badRequestException, isStackTraceOn),
                NotFoundException notFoundException => context.WriteNotFoundExceptionResponseAsync(notFoundException, isStackTraceOn),
                ForbiddenException forbiddenException => context.WriteForbiddenRequestExceptionResponseAsync(forbiddenException, isStackTraceOn),
                ConflictException conflictException => context.WriteConflictExceptionResponseAsync(conflictException, isStackTraceOn),
                _ => context.WriteInnerExceptionResponseAsync(exception, isStackTraceOn)
            };

            await task;
        }

        private static async Task WriteHttpRequestExceptionResponseAsync(this HttpContext context, HttpException exception, bool isStackTraceOn)
        {
            var status = exception.StatusCode ?? HttpStatusCode.InternalServerError;

            context.WriteHttpStatusExceptionResponseAsync(status, exception!, isStackTraceOn);
            await context.WriteBaseExceptionResponseAsync(exception!, isStackTraceOn);
        }

        private static async Task WriteInnerExceptionResponseAsync(this HttpContext context, Exception exception, bool isStackTraceOn)
        {
            context.WriteHttpStatusExceptionResponseAsync(HttpStatusCode.InternalServerError, exception, isStackTraceOn);
            await context.WriteExceptionResponseAsync(exception, isStackTraceOn);
        }

        private static async Task WriteBadRequestExceptionResponseAsync(this HttpContext context, DomainException exception, bool isStackTraceOn)
        {
            context.WriteHttpStatusExceptionResponseAsync(HttpStatusCode.BadRequest, exception, isStackTraceOn);
            await context.WriteBaseExceptionResponseAsync(exception, isStackTraceOn);
        }

        private static async Task WriteForbiddenRequestExceptionResponseAsync(this HttpContext context, ForbiddenException exception, bool isStackTraceOn)
        {
            context.WriteHttpStatusExceptionResponseAsync(HttpStatusCode.Forbidden, exception, isStackTraceOn);
            await context.WriteBaseExceptionResponseAsync(exception, isStackTraceOn);
        }

        private static async Task WriteConflictExceptionResponseAsync(this HttpContext context, ConflictException exception, bool isStackTraceOn)
        {
            context.WriteHttpStatusExceptionResponseAsync(HttpStatusCode.Conflict, exception, isStackTraceOn);
            await context.WriteBaseExceptionResponseAsync(exception, isStackTraceOn);
        }

        private static async Task WriteBadRequestExceptionResponseAsync(this HttpContext context, BadRequestException exception, bool isStackTraceOn)
        {
            context.WriteHttpStatusExceptionResponseAsync(HttpStatusCode.BadRequest, exception, isStackTraceOn);
            await context.WriteBaseExceptionResponseAsync(exception, isStackTraceOn);
        }

        private static async Task WriteNotFoundExceptionResponseAsync(this HttpContext context, NotFoundException exception, bool isStackTraceOn)
        {
            context.WriteHttpStatusExceptionResponseAsync(HttpStatusCode.NotFound, exception, isStackTraceOn);
            await context.WriteBaseExceptionResponseAsync(exception, isStackTraceOn);
        }

        private static void WriteHttpStatusExceptionResponseAsync(this HttpContext context, HttpStatusCode status, Exception exception, bool isStackTraceOn)
        {
            var statusCode = (int)status;
            context.Response.StatusCode = statusCode;
        }

        private static async Task WriteExceptionResponseAsync(this HttpContext context, Exception exception, bool isStackTraceOn)
        {
            var exceptionResponse = GetExceptionResponse(exception, context.Response.StatusCode, isStackTraceOn);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionResponse));
        }

        private static async Task WriteBaseExceptionResponseAsync(this HttpContext context, BaseException exception, bool isStackTraceOn)
        {
            var exceptionResponse = GetBaseExceptionResponse(exception, context.Response.StatusCode, isStackTraceOn);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionResponse));
        }

        private static ExceptionResponse GetExceptionResponse(Exception exception, int status, bool isStackTraceOn)
        {
            var exceptionResponse = new ExceptionResponse(
                exception.Message,
                status,
                PrepareError(exception!.Message));
            exceptionResponse.AppendStackTrace(exception.StackTrace, exception.Source, isStackTraceOn);
            return exceptionResponse;
        }

        private static ExceptionResponse GetBaseExceptionResponse(BaseException exception, int status, bool isStackTraceOn)
        {
            var exceptionResponse = new ExceptionResponse(
                exception.Message,
                status,
                PrepareError(exception!.Message),
                exception.Code,
                exception.Parameters);
            exceptionResponse.AppendStackTrace(exception.StackTrace, exception.Source, isStackTraceOn);
            return exceptionResponse;
        }

        private static Dictionary<string, string[]> PrepareError(string message)
        {
            return new Dictionary<string, string[]>
            {
                { "Error", new string[]{ message } }
            };
        }
    }
}
