using AutoGit.Core.Interfaces;
using AutoGit.ReleaseNotes.Interfaces;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGit.ReleaseNotes.Services
{
    public class CommitFinder : ICommitFinder
    {
        private readonly IGitHubClientFactory _gitHubClientFactory;

        public CommitFinder(IGitHubClientFactory gitHubClientFactory)
        {
            _gitHubClientFactory = gitHubClientFactory;
        }

        public async Task<List<GitHubCommit>> GetCommits(long repositoryId, int releaseId)
        {
            var clients = await _gitHubClientFactory.Create();

            var releases = await clients.InstallationClient.Repository.Release.GetAll(repositoryId);

            var newRelease = releases.FirstOrDefault(r => r.Id == releaseId);
            var previousRelease = releases.FirstOrDefault(r => r.Id != releaseId);

            var commits = previousRelease != null
                ? await clients.InstallationClient.Repository.Commit.GetAll(repositoryId, new CommitRequest
                {
                    Since = previousRelease.PublishedAt
                })
                : await clients.InstallationClient.Repository.Commit.GetAll(repositoryId);

            return commits.ToList();
        }
    }
}