using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models.Validators;
using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Net;
using Xunit;

namespace AutoGit.WebHooks.UnitTests
{
    public class WebHookMiddlewareTests
    {
        private readonly RequestDelegate _delegate = Substitute.For<RequestDelegate>();
        private readonly WebHookMiddleware _sut;

        public WebHookMiddlewareTests()
        {
            _sut = new WebHookMiddleware(_delegate);
        }

        [Theory]
        [InlineData(Constants.ValidWebHookSecret, 1, HttpStatusCode.OK)]
        [InlineData(Constants.InvalidWebHookSecret, 0, HttpStatusCode.Forbidden)]
        public async void Invoke_Should_Return_Return_Correct_Status_Code(string secret, int registryCalls,
            HttpStatusCode statusCode)
        {
            // Arrange
            var fakedWebHookEvent = new WebHookEventFaker().Generate();
            var fakedHttpContext = new HttpContextFaker().Generate();
            var mockedWebHookHandlerRegistry = Substitute.For<IWebHookHandlerRegistry>();
            var mockedWebHookEventFactory = Substitute.For<IWebHookEventFactory>();
            var mockedWebHookEventValidator = new WebHookEventValidator(Options.Create(new AutoGitWebHookOptions
                {WebHookSecret = secret}));

            mockedWebHookEventFactory.Create(fakedHttpContext).Returns(fakedWebHookEvent);

            // Act
            await _sut.Invoke(fakedHttpContext, mockedWebHookHandlerRegistry, mockedWebHookEventFactory,
                mockedWebHookEventValidator);

            // Assert
            mockedWebHookEventFactory.Received(1);
            mockedWebHookHandlerRegistry.Received(registryCalls);
            fakedHttpContext.Response.StatusCode.Should().Be((int) statusCode);
        }
    }
}