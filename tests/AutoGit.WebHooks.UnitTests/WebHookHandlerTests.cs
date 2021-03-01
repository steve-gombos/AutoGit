using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace AutoGit.WebHooks.UnitTests
{
    public class WebHookHandlerTests
    {
        private WebHookHandler _sut;

        [Theory]
        [InlineData("test")]
        [InlineData("issue")]
        public void EventName_Should_Be_Equal(string eventName)
        {
            // Arrange
            _sut = new TestHandler(eventName, new List<string>(), false);

            // Assert
            _sut.EventName.Should().Be(eventName);
        }
        
        [Theory]
        [InlineData("test")]
        [InlineData("issue")]
        public void Action_Should_Be_Equal(string action)
        {
            // Arrange
            _sut = new TestHandler(null, new List<string>(){action}, false);

            // Assert
            _sut.Actions.Should().Contain(action);
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IncludeBotEvents_Should_Be_Equal(bool includeBotEvents)
        {
            // Arrange
            _sut = new TestHandler(null, new List<string>(), includeBotEvents);

            // Assert
            _sut.IncludeBotEvents.Should().Be(includeBotEvents);
        }
    }
}