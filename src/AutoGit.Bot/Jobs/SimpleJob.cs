using AutoGit.Core.Interfaces;
using AutoGit.Jobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AutoGit.Bot.Jobs
{
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
            _logger.LogDebug("Started job");
            _logger.LogTrace("Started job");
            _logger.LogInformation("Started job");
            _logger.LogWarning("Started job");
            _logger.LogError("Started job");
            _logger.LogCritical("Started job");
            
            var clients = await _gitHubClientFactory.Create();

            // var issue = await clients.InstallationClient.Issue.Create(RepositoryOwner, RepositoryName,
            //     new NewIssue($"Issue - {DateTime.Now}"));

            _logger.LogInformation("Completed job");
        }
    }
}