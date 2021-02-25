using AutoGit.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AutoGit.WebHooks.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAutoGitEndpoints(this IApplicationBuilder app,
            string hookEndpoint = "/hooks")
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapAutoGitEndpoints(hookEndpoint); });

            return app;
        }

        public static IEndpointConventionBuilder MapAutoGitEndpoints(this IEndpointRouteBuilder endpoints,
            string hookEndpoint = "/hooks")
        {
            var pipeline = endpoints.CreateApplicationBuilder()
                .UseMiddleware<WebHookMiddleware>()
                .Build();

            return endpoints.Map(hookEndpoint, pipeline);
        }
    }
}