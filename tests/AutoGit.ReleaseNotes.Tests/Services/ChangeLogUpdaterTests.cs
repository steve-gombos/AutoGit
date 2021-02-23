using AutoGit.Core;
using AutoGit.Core.Interfaces;
using AutoGit.ReleaseNotes.Interfaces;
using AutoGit.ReleaseNotes.Services;
using AutoGit.ReleaseNotes.Tests.Fakers;
using Microsoft.Extensions.Options;
using NSubstitute;
using Octokit;
using System.Collections.Generic;
using Xunit;

namespace AutoGit.ReleaseNotes.Tests.Services
{
    public class ChangeLogUpdaterTests
    {
        private readonly IDocumentUpdater _sut;
        private readonly IGitHubClientFactory _gitHubClientFactory = Substitute.For<IGitHubClientFactory>();
        private readonly IEnumerable<IDocumentFormatter> _documentFormatters =
            Substitute.For<IEnumerable<IDocumentFormatter>>();
        private readonly IOptions<AutoGitReleaseOptions> _options = Substitute.For<IOptions<AutoGitReleaseOptions>>();

        public ChangeLogUpdaterTests()
        {
            _documentFormatters = new DocumentFormatterFaker().Generate(5);
            var test = Substitute.For<GitHubClients>();
            _gitHubClientFactory.Create().Returns(test);
            _sut = new ChangeLogUpdater(_gitHubClientFactory, _documentFormatters, _options);
        }

        // [Fact]
        // public async void Test()
        // {
        //     // Arrange
        //     
        //     // Act
        //     await _sut.Update(new Repository(), new Release(), new List<GitHubCommit>());
        //
        //     // Assert
        //     _gitHubClientFactory.Received(3);
        // }
    }
}