using AutoGit.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutoGit.Core
{
    public class AutoGitBuilder : IAutoGitBuilder
    {
        public IServiceCollection Services { get; }

        public AutoGitBuilder(IServiceCollection services)
        {
            if(services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            Services = services;
        }
    }
}
