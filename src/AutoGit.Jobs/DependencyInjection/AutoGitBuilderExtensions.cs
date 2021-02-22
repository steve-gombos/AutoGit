using AutoGit.Core.Interfaces;
using AutoGit.Jobs.Filters;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AutoGit.Jobs.DependencyInjection
{
    public static class AutoGitBuilderExtensions
    {
        public static IAutoGitBuilder AddJobs(this IAutoGitBuilder builder,
            Action<AutoGitJobOptions> setupAction = null)
        {
            var jobOptions = new AutoGitJobOptions();
            setupAction?.Invoke(jobOptions);

            builder.Services.Configure(setupAction);
            
            builder.Services.AddRouting();

            builder.Services.AddHangfire(options =>
            {
                if (!string.IsNullOrWhiteSpace(jobOptions.ConnectionString))
                {
                    options.UseSqlServerStorage(jobOptions.ConnectionString);    
                }
                else
                {
                    options.UseMemoryStorage();
                }

            });

            builder.Services.AddHangfireServer();

            return builder;
        }
    }
}