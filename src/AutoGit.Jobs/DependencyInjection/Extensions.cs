using AutoGit.Core.Interfaces;
using AutoGit.Jobs.Filters;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AutoGit.Jobs.DependencyInjection
{
    public static class Extensions
    {
        public static IAutoGitBuilder AddJobs(this IAutoGitBuilder builder, Action<AutoGitJobOptions> setupAction = null)
        {
            AutoGitJobOptions jobOptions = new AutoGitJobOptions();
            setupAction?.Invoke(jobOptions);

            builder.Services.Configure(setupAction);

            builder.Services.AddHangfire(options =>
            {
                options.UseSqlServerStorage(jobOptions.ConnectionString);
            });

            builder.Services.AddHangfireServer();

            return builder;
        }

        public static IApplicationBuilder UseAutoGitScheduler(this IApplicationBuilder app)
        {
            //TODO: make this configurable
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new List<IDashboardAuthorizationFilter>
                {
                    new NullAuthorizationFilter()
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHangfireDashboard();
            });

            var options = app.ApplicationServices.GetService<IOptions<AutoGitJobOptions>>().Value;

            options.Jobs.ForEach(j =>
            {
                j.Invoke(app.ApplicationServices);
            });

            return app;
        }
    }
}
