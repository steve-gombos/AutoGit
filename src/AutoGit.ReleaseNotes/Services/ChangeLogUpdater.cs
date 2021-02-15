using AutoGit.Core.Extensions;
using AutoGit.Core.Interfaces;
using AutoGit.ReleaseNotes.Interfaces;
using AutoGit.ReleaseNotes.Models;
using Microsoft.Extensions.Options;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGit.ReleaseNotes.Services
{
    public class ChangeLogUpdater : IDocumentUpdater
    {
        private readonly IGitHubClientFactory _gitHubClientFactory;
        private readonly IEnumerable<IDocumentFormatter> _documentFormatters;
        private readonly AutoGitReleaseOptions _options;

        public ChangeLogUpdater(IGitHubClientFactory gitHubClientFactory, IEnumerable<IDocumentFormatter> documentFormatters, IOptions<AutoGitReleaseOptions> options)
        {
            _gitHubClientFactory = gitHubClientFactory;
            _documentFormatters = documentFormatters;
            _options = options.Value;
        }

        public async Task Update(Repository repository, Release release, List<GitHubCommit> commits)
        {
            var clients = await _gitHubClientFactory.Create();

            // Get the last registered formatter for the type.
            var formatter = _documentFormatters.LastOrDefault(x => x.Type.HasFlag(FormatterTypes.ChangeLog));

            if (formatter == null)
                return;
            
            var document = formatter.Format(release, commits);

            var changeLog =
                await clients.InstallationClient.Repository.Content.GetAllContentsOrNull(repository.Id,
                    _options.ChangeLogFileName);

            var repositoryContentChangeSet = changeLog != null
                ? await clients.InstallationClient.Repository.Content.UpdateFile(repository.Id,
                    _options.ChangeLogFileName,
                    new UpdateFileRequest($"Update {_options.ChangeLogFileName}",
                        document.Content + ReleaseNotesConstants.VersionDivider + changeLog.Content, changeLog.Sha,
                        true))
                : await clients.InstallationClient.Repository.Content.CreateFile(repository.Id,
                    _options.ChangeLogFileName,
                    new CreateFileRequest($"Create {_options.ChangeLogFileName}", document.Content, true));
        }
    }
}