using AutoGit.Core.Interfaces;
using AutoGit.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutoGit.Core.DependencyInjection
{
    public static class Extensions
    {
        public static IAutoGitBuilder AddGitHubBot(this IServiceCollection services, Action<AutoGitOptions> setupAction)
        {
            if(services == null)
                throw new ArgumentNullException(nameof(services));

            if (setupAction == null)
                throw new ArgumentNullException(nameof(setupAction));

            services.Configure(setupAction);

            services.AddTransient<IAccessTokenFactory, AccessTokenFactory>();
            services.AddTransient<IGitHubClientFactory, GitHubClientFactory>();

            return new AutoGitBuilder(services);
        }
    }
}
