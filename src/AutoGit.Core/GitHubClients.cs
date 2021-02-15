using Octokit;

namespace AutoGit.Core
{
    public class GitHubClients
    {
        public GitHubClients(GitHubClient appClient, GitHubClient installationClient)
        {
            AppClient = appClient;
            InstallationClient = installationClient;
        }
        
        public GitHubClient AppClient { get; }
        
        public GitHubClient InstallationClient { get; }
    }
}