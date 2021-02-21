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
        [InlineData("release", "completed", false, 3)]
        [InlineData("release", "completed", true, 1)]
        [InlineData("issue_comment", "deleted", false, 1)]
        [InlineData("issue_comment", "deleted", true, 2)]
        [InlineData("issues", "updated", false, 0)]
        [InlineData("issues", "updated", true, 1)]
        [InlineData("push", "created", false, 0)]
        [InlineData("push", "created", true, 0)]
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