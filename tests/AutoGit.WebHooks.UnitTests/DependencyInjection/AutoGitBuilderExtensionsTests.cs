using AutoGit.Core.DependencyInjection;
using AutoGit.Core.Interfaces;
using AutoGit.WebHooks.DependencyInjection;
using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models.Validators;
using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Xunit;

namespace AutoGit.WebHooks.UnitTests.DependencyInjection
{
    public class AutoGitBuilderExtensionsTests
    {
        private readonly IAutoGitBuilder _sut;

        public AutoGitBuilderExtensionsTests()
        {
            var services = new ServiceCollection();
            _sut = services.AddGitHubBot(options => { })
                .AddWebHookHandlers(options => { });
        }

        [Theory]
        [InlineData(typeof(IOptions<AutoGitWebHookOptions>))]
        [InlineData(typeof(IWebHookHandlerRegistry))]
        [InlineData(typeof(IWebHookEventFactory))]
        [InlineData(typeof(IWebHookHandlerResolver))]
        [InlineData(typeof(IHttpContextAccessor))]
        [InlineData(typeof(WebHookEventValidator))]
        public void ServiceProvider_Should_Have_Types(Type serviceType)
        {
            // Arrange
            var provider = _sut.Services.BuildServiceProvider();

            // Act
            var service = provider.GetRequiredService(serviceType);

            // Assert
            service.Should().BeAssignableTo(serviceType);
        }

        [Fact]
        public void ServiceProvider_Should_Have_Web_Hook_Handlers_When_Added_With_Type()
        {
            // Arrange
            var fakedWebHookHandlers = new WebHookHandlerFaker().Generate(5);
            _sut.AddWebHookHandlers(options =>
            {
                foreach (var handler in fakedWebHookHandlers) options.AddHandler(handler.GetType());
            });
            var provider = _sut.Services.BuildServiceProvider();

            // Act
            var services = provider.GetServices<IWebHookHandler>();

            // Assert
            services.Count().Should().Be(fakedWebHookHandlers.Count());
        }

        [Fact]
        public void ServiceProvider_Should_Have_Web_Hook_Handlers_When_Added_With_Generic()
        {
            // Arrange
            _sut.AddWebHookHandlers(options => { options.AddHandler<TestHandler>(); });
            var provider = _sut.Services.BuildServiceProvider();

            // Act
            var services = provider.GetServices<IWebHookHandler>();

            // Assert
            services.Count().Should().Be(1);
        }

        [Fact]
        public void ServiceProvider_Should_Not_Have_Web_Hook_Handlers_When_Added_With_Invalid_Type()
        {
            // Arrange
            _sut.AddWebHookHandlers(options => { options.AddHandler(typeof(WebHookMiddleware)); });
            var provider = _sut.Services.BuildServiceProvider();

            // Act
            var services = provider.GetServices<IWebHookHandler>();

            // Assert
            services.Count().Should().Be(0);
        }
    }
}