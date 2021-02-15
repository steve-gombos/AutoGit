using AutoGit.Core.Interfaces;
using AutoGit.WebHooks.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace AutoGit.WebHooks.DependencyInjection
{
    public static class Extensions
    {
        public static IAutoGitBuilder AddWebHookHandlers(this IAutoGitBuilder builder, Action<AutoGitEventOptions> setupAction = null)
        {
            AutoGitEventOptions options = new AutoGitEventOptions();
            setupAction?.Invoke(options);

            builder.Services.Configure(setupAction);

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            options.WebHookHandlers.ForEach(h =>
            {
                builder.Services.AddScoped(typeof(IWebHookHandler), h);
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IWebHookHandlerRegistry, WebHookHandlerRegistry>();
            builder.Services.AddScoped<IWebHookHandlerResolver, WebHookHandlerResolver>();

            return builder;
        }

        public static void AddHandler<TEvent>(this AutoGitEventOptions eventOptions) where TEvent : IWebHookHandler
        {
            eventOptions.WebHookHandlers.Add(typeof(TEvent));
        }

        public static IEndpointConventionBuilder MapAutoGitEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var app = endpoints.CreateApplicationBuilder();

            var pipeline = app.UsePathBase("/github/hooks")
                .UseMiddleware<WebHookMiddleware>()
                .Build();

            return endpoints.Map("/github/hooks", pipeline);
        }
    }
}
