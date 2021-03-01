using AutoGit.Core;
using AutoGit.WebHooks.Context;
using AutoGit.WebHooks.Models;
using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Octokit;
using Xunit;

namespace AutoGit.WebHooks.UnitTests.Context
{
    public class EventContextTests
    {
        private readonly EventContext _sut;
        private readonly HttpContext _httpContext;
        private readonly WebHookEvent _webHookEvent;
        private readonly GitHubClients _gitHubClients;

        public EventContextTests()
        {
            _httpContext = Substitute.For<HttpContext>();
            _webHookEvent = new WebHookEventFaker().Generate();
            var gitHubClientFake = Substitute.For<GitHubClient>(new ProductHeaderValue("test"));
            _gitHubClients = Substitute.For<GitHubClients>(gitHubClientFake, gitHubClientFake);
            
            _sut = new EventContext(_httpContext, _webHookEvent, _gitHubClients);
        }

        [Fact]
        public void HttpContext_Should_Be_Equal()
        {
            _sut.HttpContext.Should().Be(_httpContext);
        }
        
        [Fact]
        public void WebHookEvent_Should_Be_Equal()
        {
            _sut.WebHookEvent.Should().Be(_webHookEvent);
        }
        
        [Fact]
        public void Clients_Should_Be_Equal()
        {
            _sut.Clients.Should().Be(_gitHubClients);
        }
    }
}