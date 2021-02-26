using AutoGit.Core.DependencyInjection;
using AutoGit.Core.Interfaces;
using AutoGit.ReleaseNotes.DependencyInjection;
using AutoGit.ReleaseNotes.Interfaces;
using AutoGit.WebHooks.DependencyInjection;
using AutoGit.WebHooks.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace AutoGit.ReleaseNotes.Tests.DependencyInjection
{
    public class AutoGitBuilderExtensionsTests
    {
        private readonly IAutoGitBuilder _sut;

        public AutoGitBuilderExtensionsTests()
        {
            var services = new ServiceCollection();
            _sut = services.AddGitHubBot(options => { })
                .AddWebHookHandlers(options => { })
                .AddReleaseNoteGenerator(options =>
                {
                    options.ManageChangeLog = true;
                    options.ManageReleaseNotes = true;
                });
        }

        [Theory]
        [InlineData(typeof(IOptions<AutoGitReleaseOptions>))]
        [InlineData(typeof(ICommitFinder))]
        [InlineData(typeof(IWebHookHandler))]
        [InlineData(typeof(IDocumentUpdater))]
        [InlineData(typeof(IDocumentFormatter))]
        public void ServiceProvider_Should_Have_Types(Type serviceType)
        {
            // Arrange
            var provider = _sut.Services.BuildServiceProvider();

            // Act
            var service = provider.GetRequiredService(serviceType);

            // Assert
            service.Should().BeAssignableTo(serviceType);
        }
    }
}