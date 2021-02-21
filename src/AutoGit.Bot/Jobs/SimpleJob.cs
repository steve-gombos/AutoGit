using AutoGit.Core.Interfaces;
using AutoGit.Jobs.Attributes;
using AutoGit.Jobs.Interfaces;
using Octokit;
using System;
using System.Threading.Tasks;

namespace AutoGit.Bot.Jobs
{
    [RecurringJob("0/5 * * * *")]
    public class SimpleJob : IAutoGitJob
    {
        private readonly IGitHubClientFactory _gitHubClientFactory;

        public SimpleJob(IGitHubClientFactory gitHubClientFactory)
        {
            _gitHubClientFactory = gitHubClientFactory;
        }

        public string RepositoryOwner { get; set; } = "steve-gombos";
        public string RepositoryName { get; set; } = "test";

        public async Task Execute()
        {
            var clients = await _gitHubClientFactory.Create();

            var issue = await clients.InstallationClient.Issue.Create(RepositoryOwner, RepositoryName,
                new NewIssue($"Issue - {DateTime.Now}"));
        }
    }
}