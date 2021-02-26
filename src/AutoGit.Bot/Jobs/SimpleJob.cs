using AutoGit.Core.Interfaces;
using AutoGit.Jobs;
using AutoGit.Jobs.Attributes;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AutoGit.Bot.Jobs
{
    [RecurringJob("0/5 * * * *")]
    public class SimpleJob : AutoGitJob
    {
        private readonly IGitHubClientFactory _gitHubClientFactory;
        private readonly ILogger<SimpleJob> _logger;

        public SimpleJob(IGitHubClientFactory gitHubClientFactory, ILogger<SimpleJob> logger) : base("steve-gombos",
            "test")
        {
            _gitHubClientFactory = gitHubClientFactory;
            _logger = logger;
        }

        public override async Task Execute()
        {
            _logger.LogInformation("Started job");

            var clients = await _gitHubClientFactory.Create();

            // var issue = await clients.InstallationClient.Issue.Create(RepositoryOwner, RepositoryName,
            //     new NewIssue($"Issue - {DateTime.Now}"));

            _logger.LogInformation("Completed job");
        }
    }
}