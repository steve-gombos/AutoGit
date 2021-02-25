using AutoGit.Bot.Hooks;
using AutoGit.Bot.Jobs;
using AutoGit.Core;
using AutoGit.Core.DependencyInjection;
using AutoGit.Jobs.DependencyInjection;
using AutoGit.ReleaseNotes.DependencyInjection;
using AutoGit.WebHooks.DependencyInjection;
using Hangfire.Console.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoGit.Bot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var autoGitOptions = Configuration.GetSection("GitHub").Get<AutoGitOptions>();

            var webHookSecret = Configuration.GetValue<string>("GitHub:WebHookSecret");

            services.AddGitHubBot(options =>
                {
                    options.AppIdentifier = autoGitOptions.AppIdentifier;
                    options.AppName = autoGitOptions.AppName;
                    options.PrivateKey = autoGitOptions.PrivateKey;
                })
                .AddWebHookHandlers(options =>
                {
                    options.WebHookSecret = webHookSecret;
                    options.AddHandler<IssueCommentHandler>();
                })
                .AddJobs(options =>
                {
                    options.ConnectionString = Configuration.GetConnectionString("AutoGit");
                    options.AddRecurringJob<SimpleJob>();
                })
                .AddReleaseNoteGenerator(options =>
                {
                    options.VersionTagPrefix = "v";
                    options.ManageReleaseNotes = true;
                    options.ManageChangeLog = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IJobManager jobManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAutoGitScheduler();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAutoGitEndpoints();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("<a href=\"\\jobs\\\">Dashboard</a>");
                });
            });
        }
    }
}