using AutoGit.Core.Interfaces;
using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.UnitTests.Fakers;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace AutoGit.WebHooks.UnitTests
{
    public class WebHookHandlerRegistryTests
    {
        private readonly IGitHubClientFactory _gitHubClientFactory = Substitute.For<IGitHubClientFactory>();
        private readonly IHttpContextAccessor _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private readonly IWebHookHandlerRegistry _sut;
        private readonly IWebHookHandlerResolver _webHookHandlerResolver = Substitute.For<IWebHookHandlerResolver>();

        public WebHookHandlerRegistryTests()
        {
            _sut = new WebHookHandlerRegistry(_webHookHandlerResolver, _gitHubClientFactory, _httpContextAccessor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void Handle_Should_Be_Called(int handlerCount)
        {
            // Arrange
            var webHookHandlerFaker = new WebHookHandlerFaker();
            var fakedWebHookHandlers = webHookHandlerFaker.Generate(handlerCount);
            var mockedWebHookHandlers = Substitute.For<List<IWebHookHandler>>();
            mockedWebHookHandlers.AddRange(fakedWebHookHandlers);
            var webHookEventFaker = new WebHookEventFaker();
            var fakedWebHookEvent = webHookEventFaker.Generate();

            _webHookHandlerResolver.Resolve(fakedWebHookEvent.EventName, fakedWebHookEvent.GenericPayload.Action,
                    fakedWebHookEvent.IsBot)
                .Returns(mockedWebHookHandlers);

            // Act
            await _sut.Handle(fakedWebHookEvent);

            // Assert
            mockedWebHookHandlers.Received(handlerCount);
        }
    }
}