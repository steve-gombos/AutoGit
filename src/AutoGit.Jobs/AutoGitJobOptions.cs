using AutoGit.Jobs.Interfaces;
using Hangfire;
using System;
using System.Collections.Generic;

namespace AutoGit.Jobs
{
    public class AutoGitJobOptions
    {
        public string ConnectionString { get; set; }
        public bool EnableConsoleLogging { get; set; } = true;
        internal List<Action<IServiceProvider>> Jobs { get; } = new List<Action<IServiceProvider>>();

        public void AddRecurringJob<TJob>(string cronExpression, TimeZoneInfo timeZoneInfo, bool runOnStart = false) where TJob : IAutoGitJob
        {
            Jobs.Add(Schedule<TJob>(cronExpression, timeZoneInfo, runOnStart));
        }

        private Action<IServiceProvider> Schedule<TJob>(string cronExpression, TimeZoneInfo timeZoneInfo, bool runOnStart = false) where TJob : IAutoGitJob
        {
            return provider =>
            {
                if (runOnStart)
                    BackgroundJob.Enqueue<TJob>(job => job.Execute());
                
                RecurringJob.AddOrUpdate<TJob>(job => job.Execute(), cronExpression, timeZoneInfo ?? TimeZoneInfo.Utc);
            };
        }
    }
}