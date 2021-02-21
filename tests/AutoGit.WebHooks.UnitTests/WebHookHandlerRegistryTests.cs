using AutoGit.Core.Interfaces;
using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Xunit;

namespace AutoGit.WebHooks.UnitTests
{
    public class WebHookHandlerRegistryTests
    {
        private readonly IWebHookHandlerRegistry _sut;
        private readonly IWebHookHandlerResolver _webHookHandlerResolver = Substitute.For<IWebHookHandlerResolver>();
        private readonly IGitHubClientFactory _gitHubClientFactory = Substitute.For<IGitHubClientFactory>();
        private readonly IHttpContextAccessor _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        
        public WebHookHandlerRegistryTests()
        {
            _sut = new WebHookHandlerRegistry(_webHookHandlerResolver, _gitHubClientFactory, _httpContextAccessor);
        }
        
        // [Theory]
        // public async void Handle_Should_Be_Called()
        // {
        //     // Arrange
        //
        //     // Act
        //     var handlers = await _sut.Handle()
        //
        //     // Assert
        // }
    }
}