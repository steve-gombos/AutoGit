using AutoGit.Core.Caching;
using AutoGit.Core.Interfaces;
using Microsoft.Extensions.Options;
using Octokit;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGit.Core.Services
{
    public class GitHubClientFactory : IGitHubClientFactory
    {
        private readonly IAccessTokenFactory _accessTokenFactory;
        private readonly AutoGitOptions _options;

        public GitHubClientFactory(IOptions<AutoGitOptions> options, IAccessTokenFactory accessTokenFactory)
        {
            _accessTokenFactory = accessTokenFactory;
            _options = options.Value;
        }

        public async Task<GitHubClients> Create()
        {
            var appClient = CreateAppClient();
            var installationClient = await CreateInstallationClient(appClient);

            return new GitHubClients(appClient, installationClient);
        }

        private GitHubClient CreateAppClient()
        {
            var accessToken = _accessTokenFactory.Create(_options.AppIdentifier.ToString(), _options.PrivateKey);

            return new ResilientGitHubClientFactory().Create(new ProductHeaderValue(_options.AppName),
                new Credentials(accessToken, AuthenticationType.Bearer), _options.Url, new InMemoryCacheProvider());
        }

        private async Task<GitHubClient> CreateInstallationClient(GitHubClient appClient)
        {
            var installations = await appClient.GitHubApps.GetAllInstallationsForCurrent();

            if (installations == null || !installations.Any())
                return null;

            var installation = installations.FirstOrDefault(x => x.AppId == _options.AppIdentifier);

            if (installation == null)
                return null;

            var accessToken = await appClient.GitHubApps.CreateInstallationToken(installation.Id);

            return new ResilientGitHubClientFactory()
                .Create(new ProductHeaderValue($"{_options.AppName}-Installation{installation.Id}"),
                    new Credentials(accessToken.Token), _options.Url, new InMemoryCacheProvider());
        }
    }
}