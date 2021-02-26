using AutoGit.Core.Interfaces;
using AutoGit.Jobs.Logging;
using Hangfire;
using Hangfire.Console;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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
                options.UseConsole();
                // options.UseSimpleAssemblyNameTypeSerializer();
                // options.UseRecommendedSerializerSettings();
                
                if (!string.IsNullOrWhiteSpace(jobOptions.ConnectionString))
                {
                    options.UseSqlServerStorage(jobOptions.ConnectionString);    
                }
                else
                {
                    options.UseMemoryStorage();
                }
            });

            builder.Services.AddSingleton<ILoggerProvider, HangfireConsoleLoggerProvider>();
            //builder.Services.AddHangfireConsoleExtensions();

            builder.Services.AddHangfireServer();

            return builder;
        }
    }
}