using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using Xunit;

namespace AutoGit.WebHooks.UnitTests
{
    public class WebHookHandlerResolverTests
    {
        private readonly IWebHookHandlerResolver _sut;

        public WebHookHandlerResolverTests()
        {
            var webHookHandlerFaker = new WebHookHandlerFaker();
            var handlers = webHookHandlerFaker.Generate(10);

            _sut = new WebHookHandlerResolver(handlers);
        }

        [Theory]
        [InlineData("release", "completed", false, 0)]
        [InlineData("release", "completed", true, 0)]
        [InlineData("release", "created", false, 0)]
        [InlineData("release", "created", true, 0)]
        [InlineData("issue_comment", "completed", false, 0)]
        [InlineData("issue_comment", "completed", true, 0)]
        [InlineData("issue_comment", "created", false, 0)]
        [InlineData("issue_comment", "created", true, 1)]
        [InlineData("issues", "completed", false, 2)]
        [InlineData("issues", "completed", true, 1)]
        [InlineData("issues", "created", false, 0)]
        [InlineData("issues", "created", true, 1)]
        [InlineData("push", "completed", false, 0)]
        [InlineData("push", "completed", true, 0)]
        [InlineData("push", "created", false, 0)]
        [InlineData("push", "created", true, 0)]
        [InlineData("pull_request", "completed", false, 1)]
        [InlineData("pull_request", "completed", true, 1)]
        [InlineData("pull_request", "created", false, 2)]
        [InlineData("pull_request", "created", true, 1)]
        public void Resolve_Should_Return_Valid_Handlers_When_Parameters_Are_Met(string eventName, string action, bool isBot,
            int expected)
        {
            // Arrange

            // Act
            var handlers = _sut.Resolve(eventName, action, isBot);

            // Assert
            handlers.Should().HaveCount(expected);
        }
    }
}