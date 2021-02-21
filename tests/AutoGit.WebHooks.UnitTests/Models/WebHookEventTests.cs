using AutoGit.WebHooks.Models;
using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using Octokit;
using Xunit;

namespace AutoGit.WebHooks.UnitTests.Models
{
    public class WebHookEventTests
    {
        private readonly WebHookEvent _sut;
        private readonly WebHookEvent _fakedWebHookEvent;
        
        public WebHookEventTests()
        {
            _fakedWebHookEvent = new WebHookEventFaker().Generate();
            _sut = _fakedWebHookEvent;
        }

        [Fact]
        public void EventName_Should_Be_Equal()
        {
            // Assert
            _sut.EventName.Should().Be(_fakedWebHookEvent.EventName);
        }
        
        [Fact]
        public void GitHubDelivery_Should_Be_Equal()
        {
            // Assert
            _sut.GitHubDelivery.Should().Be(_fakedWebHookEvent.GitHubDelivery);
        }
        
        [Fact]
        public void IsBot_Should_Be_Equal()
        {
            // Assert
            _sut.IsBot.Should().Be(_fakedWebHookEvent.IsBot);
        }
        
        [Fact]
        public void GenericPayload_Should_Not_Be_Null()
        {
            // Assert
            _sut.GenericPayload.Should().NotBeNull();
        }
        
        [Fact]
        public void GetPayload_Should_Return_Specific_Payload()
        {
            // Act
            var payload = _sut.GetPayload<IssueEventPayload>();
            
            // Assert
            payload.Should().BeOfType<IssueEventPayload>();
        }
        
        [Theory]
        [InlineData(Constants.ValidWebHookSecret, true)]
        [InlineData(Constants.InvalidWebHookSecret, false)]
        public void IsAuthenticated_Should_Validate_HubSignature(string secret, bool expected)
        {
            // Act
            var isAuthenticated = _sut.IsAuthenticated(secret);
            
            // Assert
            isAuthenticated.Should().Be(expected);
        }
    }
}