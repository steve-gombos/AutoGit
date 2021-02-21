using AutoGit.Jobs.Attributes;
using AutoGit.Jobs.Interfaces;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoGit.Jobs
{
    public class AutoGitJobOptions
    {
        public string ConnectionString { get; set; }
        internal List<Action<IServiceProvider>> Jobs { get; set; } = new List<Action<IServiceProvider>>();

        public void AddRecurringJob<TJob>() where TJob : IAutoGitJob
        {
            Jobs.Add(Schedule<TJob>());
        }

        private Action<IServiceProvider> Schedule<TJob>() where TJob : IAutoGitJob
        {
            return provider =>
            {
                var instance = ActivatorUtilities.CreateInstance<TJob>(provider);

                var attributes = instance.GetType().GetCustomAttributes(true).OfType<StandardJobAttribute>();

                foreach (var attribute in attributes)
                {
                    if (attribute.RunOnStart)
                        BackgroundJob.Enqueue(() => instance.Execute());
                    //BackgroundJob.Enqueue<TJob>(job => job.Execute());

                    switch (attribute)
                    {
                        case RecurringJobAttribute recurrentJobAttribute:
                            RecurringJob.AddOrUpdate(() => instance.Execute(), recurrentJobAttribute.CronExpression);
                            //RecurringJob.AddOrUpdate<TJob>(job => job.Execute(), recurrentJobAttribute.CronExpression);
                            break;
                    }
                }
            };
        }
    }
}