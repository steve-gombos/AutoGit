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
            
            //TODO: make this configurable
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new List<IDashboardAuthorizationFilter>
                {
                    new NullAuthorizationFilter()
                }
            });

            app.UseEndpoints(endpoints => { endpoints.MapHangfireDashboard(); });

            var optionsAccessor = app.ApplicationServices.GetService<IOptions<AutoGitJobOptions>>();
            
            optionsAccessor?.Value.Jobs.ForEach(j => { j.Invoke(app.ApplicationServices); });

            return app;
        }
    }
}