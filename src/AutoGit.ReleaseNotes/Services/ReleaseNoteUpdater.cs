using AutoGit.Core.Interfaces;
using AutoGit.ReleaseNotes.Interfaces;
using AutoGit.ReleaseNotes.Models;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGit.ReleaseNotes.Services
{
    public class ReleaseNoteUpdater : IDocumentUpdater
    {
        private readonly IGitHubClientFactory _gitHubClientFactory;
        private readonly IEnumerable<IDocumentFormatter> _documentFormatters;

        public ReleaseNoteUpdater(IGitHubClientFactory gitHubClientFactory, IEnumerable<IDocumentFormatter> documentFormatters)
        {
            _gitHubClientFactory = gitHubClientFactory;
            _documentFormatters = documentFormatters;
        }

        public async Task Update(Repository repository, Release release, List<GitHubCommit> commits)
        {
            var clients = await _gitHubClientFactory.Create();

            // Get the last registered formatter for the type.
            var formatter = _documentFormatters.LastOrDefault(x => x.Type == FormatterType.Release);

            if (formatter == null)
                return;
            
            var document = formatter.Format(release, commits);

            await clients.InstallationClient.Repository.Release.Edit(repository.Id, release.Id, 
                new ReleaseUpdate
                {
                    Name = document.Name,
                    Body = document.Content
                });
        }
    }
}