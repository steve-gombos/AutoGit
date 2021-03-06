﻿using AutoGit.Core.Interfaces;
using Hangfire;
using Hangfire.Console;
using Hangfire.Console.Extensions;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
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
                if (jobOptions.EnableConsoleLogging)
                {
                    options.UseConsole();
                }

                options.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
                options.UseSimpleAssemblyNameTypeSerializer();
                options.UseRecommendedSerializerSettings();
                
                if (!string.IsNullOrWhiteSpace(jobOptions.ConnectionString))
                {
                    options.UseSqlServerStorage(jobOptions.ConnectionString,new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    });
                    return;
                }
                
                options.UseMemoryStorage();
            });

            if (jobOptions.EnableConsoleLogging)
            {
                builder.Services.AddHangfireConsoleExtensions();
            }

            builder.Services.AddHangfireServer();

            return builder;
        }
    }
}