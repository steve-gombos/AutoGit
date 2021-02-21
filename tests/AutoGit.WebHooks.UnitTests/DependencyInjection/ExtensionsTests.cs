using AutoGit.Core.DependencyInjection;
using AutoGit.WebHooks.DependencyInjection;
using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models.Validators;
using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoGit.WebHooks.UnitTests.DependencyInjection
{
    public class ExtensionsTests
    {
        private readonly IServiceProvider _sut;
        private readonly IEnumerable<IWebHookHandler> _handlers = new WebHookHandlerFaker().Generate(5);
        private readonly int _manualHandlers = 1;

        public ExtensionsTests()
        {
            var provider = new ServiceCollection();
            provider.AddHttpContextAccessor();
            provider.AddGitHubBot(options => { })
                .AddWebHookHandlers(options =>
                {
                    options.WebHookSecret = Constants.ValidWebHookSecret;
                    foreach (var handler in _handlers)
                    {
                        options.AddHandler(handler.GetType());
                    }
                    options.AddHandler<TestHandler>();
                });
            _sut = provider.BuildServiceProvider();
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

            // Act
            var service = _sut.GetRequiredService(serviceType);

            // Assert
            service.Should().BeAssignableTo(serviceType);
        }

        [Fact]
        public void ServiceProvider_Should_Have_Web_Hook_Handlers()
        {
            // Arrange
            
            // Act
            var services = _sut.GetServices<IWebHookHandler>();

            // Assert
            services.Count().Should().Be(_handlers.Count() + _manualHandlers);
        }
        
        [Fact]
        public void AppBuilder_Should_Have_Endpoints_When_AppBuilder_Extension_Is_Used()
        {
            // Arrange
            var appBuilder = new ApplicationBuilder(_sut);
            appBuilder.UseAutoGitEndpoints();
            
            // Act
            appBuilder.Properties.TryGetValue("__EndpointRouteBuilder", out object endpoint);
            var endpointBuilder = endpoint as IEndpointRouteBuilder;

            // Assert
            endpointBuilder.DataSources.Any(x => x.Endpoints.Any(e => e.DisplayName == "/hooks")).Should().BeTrue();
        }
        
        [Fact]
        public void AppBuilder_Should_Have_Endpoints_When_EndpointRouteBuilder_Extension_Is_Used()
        {
            // Arrange
            var appBuilder = new ApplicationBuilder(_sut);
            appBuilder.UseRouting();
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapAutoGitEndpoints();
            });
            
            // Act
            appBuilder.Properties.TryGetValue("__EndpointRouteBuilder", out object endpoint);
            var endpointBuilder = endpoint as IEndpointRouteBuilder;

            // Assert
            endpointBuilder.DataSources.Any(x => x.Endpoints.Any(e => e.DisplayName == "/hooks")).Should().BeTrue();
        }
    }
}