using AutoGit.Core.DependencyInjection;
using AutoGit.Core.Interfaces;
using AutoGit.Jobs.DependencyInjection;
using AutoGit.Jobs.UnitTests.Fakers;
using FluentAssertions;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Xunit;

namespace AutoGit.Jobs.UnitTests.DependencyInjection
{
    public class AutoGitBuilderExtensionsTests
    {
        private readonly IAutoGitBuilder _sut;

        public AutoGitBuilderExtensionsTests()
        {
            var services = new ServiceCollection();
            _sut = services.AddGitHubBot(options => { })
                .AddJobs(options => { });
        }

        [Theory]
        [InlineData(typeof(IOptions<AutoGitJobOptions>))]
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
        public void ServiceProvider_Should_Have_Jobs_When_Added_With_Generic()
        {
            // Arrange
            _sut.AddJobs(options => { options.AddRecurringJob<TestRecurringJob>("* * * * * *", TimeZoneInfo.Local); });
            var provider = _sut.Services.BuildServiceProvider();
            var appBuilder = new ApplicationBuilder(provider);
            appBuilder.UseAutoGitScheduler();

            // Act
            var service = provider.GetService<JobStorage>();

            // Assert
            service.GetConnection().GetRecurringJobs().Count().Should().Be(1);
        }
    }
}