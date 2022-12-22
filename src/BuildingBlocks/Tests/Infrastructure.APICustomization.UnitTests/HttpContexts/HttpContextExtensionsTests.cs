using API.Customization.Exceptions;
using API.Customization.HttpContexts;
using Domain.Seedwork.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Tests.Shared;
using Xunit;

namespace Infrastructure.APICustomization.UnitTests.HttpContexts
{
    public class HttpContextExtensionsTests
    {
        [Theory, AutoMoqData]
        public async Task WriteHandledExceptionToResponse_ShouldAddHandledExceptionToResponse(
            string exceptionTitle,
            int exceptionStatusCode,
            Dictionary<string, string[]> errors,
            Exception exception,
            bool isStackTraceOn)
        {
            // Arrange
            HttpContext context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exceptionResponse = new ExceptionResponse(exceptionTitle, exceptionStatusCode, errors);

            // Act
            await context.WriteHandledExceptionToResponseAsync(exception, exceptionResponse, isStackTraceOn);
            context.Response.Body.Position = 0;
            string responseString = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var response = JsonConvert.DeserializeObject<ExceptionResponse>(responseString);

            // Assert
            Assert.Equal(exceptionStatusCode, response.Status);
            Assert.Equal(exceptionTitle, response.Title);
            Assert.Equal(errors.Count, response.Errors.Count);
        }

        [Theory, AutoMoqData]
        public async Task WriteUnhandledExceptionToResponse_ShouldAddHandledExceptionToResponse_WhenHttpRequestExceptionOccurs(
            string exceptionTitle,
            int exceptionStatusCode,
            bool isStackTraceOn,
            string exceptionCode,
            string[] exceptionParameters)
        {
            // Arrange
            var expectedErrorCount = 1;
            HttpContext context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new HttpException(exceptionTitle, (HttpStatusCode)exceptionStatusCode, exceptionCode, exceptionParameters);

            // Act
            await context.WriteUnhandledExceptionToResponseAsync(exception, isStackTraceOn);
            context.Response.Body.Position = 0;
            string responseString = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var response = JsonConvert.DeserializeObject<ExceptionResponse>(responseString);

            // Assert
            Assert.Equal(exceptionStatusCode, response.Status);
            Assert.Equal(exceptionTitle, response.Title);
            Assert.Equal(expectedErrorCount, response.Errors.Count);
            Assert.Equal(exceptionCode, response.Code);
            Assert.Equal(exceptionParameters, response.Parameters);
        }

        [Theory, AutoMoqData]
        public async Task WriteUnhandledExceptionToResponse_ShouldAddHandledExceptionToResponse_WhenDomainExceptionOccurs(
            string exceptionTitle,
            bool isStackTraceOn,
            string exceptionCode,
            string[] exceptionParameters)
        {
            // Arrange
            var expectedErrorCount = 1;
            var expectedStatusCode = (int)HttpStatusCode.BadRequest;
            HttpContext context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new DomainException(exceptionTitle, exceptionCode, exceptionParameters);

            // Act
            await context.WriteUnhandledExceptionToResponseAsync(exception, isStackTraceOn);
            context.Response.Body.Position = 0;
            string responseString = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var response = JsonConvert.DeserializeObject<ExceptionResponse>(responseString);

            // Assert
            Assert.Equal(expectedStatusCode, response.Status);
            Assert.Equal(exceptionTitle, response.Title);
            Assert.Equal(expectedErrorCount, response.Errors.Count);
            Assert.Equal(exceptionCode, response.Code);
            Assert.Equal(exceptionParameters, response.Parameters);
        }

        [Theory, AutoMoqData]
        public async Task WriteUnhandledExceptionToResponse_ShouldAddHandledExceptionToResponse_WhenBadRequestExceptionOccurs(
            string exceptionTitle,
            bool isStackTraceOn,
            string exceptionCode,
            string[] exceptionParameters)
        {
            // Arrange
            var expectedErrorCount = 1;
            var expectedStatusCode = (int)HttpStatusCode.BadRequest;
            HttpContext context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new BadRequestException(exceptionTitle, exceptionCode, exceptionParameters);

            // Act
            await context.WriteUnhandledExceptionToResponseAsync(exception, isStackTraceOn);
            context.Response.Body.Position = 0;
            string responseString = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var response = JsonConvert.DeserializeObject<ExceptionResponse>(responseString);

            // Assert
            Assert.Equal(expectedStatusCode, response.Status);
            Assert.Equal(exceptionTitle, response.Title);
            Assert.Equal(expectedErrorCount, response.Errors.Count);
            Assert.Equal(exceptionCode, response.Code);
            Assert.Equal(exceptionParameters, response.Parameters);
        }

        [Theory, AutoMoqData]
        public async Task WriteUnhandledExceptionToResponse_ShouldAddHandledExceptionToResponse_WhenNotFoundExceptionOccurs(
            string exceptionTitle,
            bool isStackTraceOn,
            string exceptionCode,
            string[] exceptionParameters)
        {
            // Arrange
            var expectedErrorCount = 1;
            var expectedStatusCode = (int)HttpStatusCode.NotFound;
            HttpContext context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new NotFoundException(exceptionTitle, exceptionCode, exceptionParameters);

            // Act
            await context.WriteUnhandledExceptionToResponseAsync(exception, isStackTraceOn);
            context.Response.Body.Position = 0;
            string responseString = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var response = JsonConvert.DeserializeObject<ExceptionResponse>(responseString);

            // Assert
            Assert.Equal(expectedStatusCode, response.Status);
            Assert.Equal(exceptionTitle, response.Title);
            Assert.Equal(expectedErrorCount, response.Errors.Count);
            Assert.Equal(exceptionCode, response.Code);
            Assert.Equal(exceptionParameters, response.Parameters);
        }

        [Theory, AutoMoqData]
        public async Task WriteUnhandledExceptionToResponse_ShouldAddHandledExceptionToResponse_WhenUnexpectedExceptionOccurs(
            string exceptionTitle,
            bool isStackTraceOn)
        {
            // Arrange
            var expectedErrorCount = 1;
            var expectedStatusCode = (int)HttpStatusCode.InternalServerError;
            HttpContext context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new Exception(exceptionTitle);

            // Act
            await context.WriteUnhandledExceptionToResponseAsync(exception, isStackTraceOn);
            context.Response.Body.Position = 0;
            string responseString = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var response = JsonConvert.DeserializeObject<ExceptionResponse>(responseString);

            // Assert
            Assert.Equal(expectedStatusCode, response.Status);
            Assert.Equal(exceptionTitle, response.Title);
            Assert.Equal(expectedErrorCount, response.Errors.Count);
            Assert.Empty(response.Parameters);
            Assert.Null(response.Code);
        }

        [Theory, AutoMoqData]
        public async Task WriteUnhandledExceptionToResponse_ShouldAddHandledExceptionToResponse_WhenForbiddenExceptionOccurs(
            string exceptionTitle,
            bool isStackTraceOn,
            string exceptionCode,
            string[] exceptionParameters)
        {
            // Arrange
            var expectedErrorCount = 1;
            var expectedStatusCode = (int)HttpStatusCode.Forbidden;
            HttpContext context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new ForbiddenException(exceptionTitle, exceptionCode, exceptionParameters);

            // Act
            await context.WriteUnhandledExceptionToResponseAsync(exception, isStackTraceOn);
            context.Response.Body.Position = 0;
            string responseString = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var response = JsonConvert.DeserializeObject<ExceptionResponse>(responseString);

            // Assert
            Assert.Equal(expectedStatusCode, response.Status);
            Assert.Equal(exceptionTitle, response.Title);
            Assert.Equal(expectedErrorCount, response.Errors.Count);
            Assert.Equal(exceptionCode, response.Code);
            Assert.Equal(exceptionParameters, response.Parameters);
        }
    }
}
