using AutoGit.Core.Services;
using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using Xunit;

namespace AutoGit.WebHooks.UnitTests
{
    public class WebHookEventFactoryTests
    {
        private readonly IWebHookEventFactory _sut;

        public WebHookEventFactoryTests()
        {
            _sut = new WebHookEventFactory(new OctokitSerializer());
        }

        [Fact]
        public async void Create_Should_Return_WebHookEvent()
        {
            // Arrange
            var fakedHttpContext = new HttpContextFaker().Generate();
            var fakedWebHookEvent = new WebHookEventFaker().Generate();

            // Act
            var webHookEvent = await _sut.Create(fakedHttpContext);

            // Assert
            webHookEvent.Should().NotBeNull();
            webHookEvent.EventName.Should().Be(fakedWebHookEvent.EventName);
        }
    }
}