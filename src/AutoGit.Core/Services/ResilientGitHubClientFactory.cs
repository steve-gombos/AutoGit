using AutoGit.Core.Caching;
using AutoGit.Core.Resiliency;
using Microsoft.Extensions.Logging;
using Octokit;
using Octokit.Internal;
using Polly;
using System;
using System.Net.Http;

namespace AutoGit.Core.Services
{
    public class ResilientGitHubClientFactory
    {
        private readonly ILogger _logger;

        public ResilientGitHubClientFactory(ILogger logger = null)
        {
            _logger = logger;
        }

        public GitHubClient Create(ProductHeaderValue productHeaderValue, Credentials credentials, Uri gitHubUrl = null,
            ICacheProvider cacheProvider = null, params IAsyncPolicy[] policies)
        {
            if (policies is null || policies.Length == 0)
                policies = new ResilientPolicies(_logger).DefaultResilientPolicies;

            var policy = policies.Length > 1 ? Policy.WrapAsync(policies) : policies[0];

            var githubConnection = new Connection(productHeaderValue, gitHubUrl ?? GitHubClient.GitHubApiUrl,
                new InMemoryCredentialStore(credentials),
                new HttpClientAdapter(() => GetHttpHandlerChain(_logger, policy, cacheProvider)),
                new SimpleJsonSerializer());

            var githubClient = new GitHubClient(githubConnection);

            return githubClient;
        }

        public GitHubClient Create(ProductHeaderValue productHeaderValue, Uri gitHubUrl = null,
            ICacheProvider cacheProvider = null, params IAsyncPolicy[] policies)
        {
            return Create(productHeaderValue, Credentials.Anonymous, gitHubUrl, cacheProvider, policies);
        }

        private HttpMessageHandler GetHttpHandlerChain(ILogger logger, IAsyncPolicy policy, ICacheProvider cacheProvider)
        {
            var handler = HttpMessageHandlerFactory.CreateDefault();

            handler = new GitHubResilientHandler(handler, policy, _logger);

            if (cacheProvider != null)
            {
                handler = new HttpCacheHandler(handler,cacheProvider,logger); 
            }

            return handler;
        }
    }
}
