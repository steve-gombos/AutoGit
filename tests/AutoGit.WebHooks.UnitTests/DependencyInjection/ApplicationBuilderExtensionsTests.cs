using AutoGit.Core.DependencyInjection;
using AutoGit.WebHooks.DependencyInjection;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace AutoGit.WebHooks.UnitTests.DependencyInjection
{
    public class ApplicationBuilderExtensionsTests
    {
        private readonly IApplicationBuilder _sut;

        public ApplicationBuilderExtensionsTests()
        {
            var services = new ServiceCollection();
            services.AddGitHubBot(options => { }).AddWebHookHandlers(options => { });
            _sut = new ApplicationBuilder(services.BuildServiceProvider());
        }

        [Fact]
        public void AppBuilder_Should_Have_Endpoints_When_AppBuilder_Extension_Is_Used()
        {
            // Arrange
            _sut.UseAutoGitEndpoints();

            // Act
            _sut.Properties.TryGetValue("__EndpointRouteBuilder", out var endpoint);
            var endpointBuilder = endpoint as IEndpointRouteBuilder;

            // Assert
            endpointBuilder.DataSources.Any(x => x.Endpoints.Any(e => e.DisplayName == "/hooks")).Should().BeTrue();
        }

        [Fact]
        public void AppBuilder_Should_Have_Endpoints_When_EndpointRouteBuilder_Extension_Is_Used()
        {
            // Arrange
            _sut.UseRouting();
            _sut.UseEndpoints(endpoints => { endpoints.MapAutoGitEndpoints(); });

            // Act
            _sut.Properties.TryGetValue("__EndpointRouteBuilder", out var endpoint);
            var endpointBuilder = endpoint as IEndpointRouteBuilder;

            // Assert
            endpointBuilder.DataSources.Any(x => x.Endpoints.Any(e => e.DisplayName == "/hooks")).Should().BeTrue();
        }
    }
}