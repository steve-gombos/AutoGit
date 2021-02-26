using AutoGit.Jobs.Filters;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace AutoGit.Jobs.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAutoGitScheduler(this IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //TODO: Make this configurable
                endpoints.MapHangfireDashboard("/jobs", new DashboardOptions
                {
                    Authorization = new List<IDashboardAuthorizationFilter>
                    {
                        new NullAuthorizationFilter()
                    }
                });
            });

            var optionsAccessor = app.ApplicationServices.GetService<IOptions<AutoGitJobOptions>>();

            optionsAccessor?.Value.Jobs.ForEach(j => { j.Invoke(app.ApplicationServices); });

            return app;
        }
    }
}