using AutoGit.Core;
using AutoGit.Core.Interfaces;
using AutoGit.WebHooks.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;

namespace AutoGit.WebHooks.DependencyInjection
{
    public static class Extensions
    {
        public static IAutoGitBuilder AddWebHookHandlers(this IAutoGitBuilder builder,
            Action<AutoGitWebHookOptions> setupAction = null)
        {
            var options = new AutoGitWebHookOptions();
            setupAction?.Invoke(options);

            builder.Services.Configure(setupAction);

            builder.Services.AddControllers().AddFluentValidation(f =>
            {
                f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            //builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            options.WebHookHandlers.ForEach(h => { builder.Services.AddScoped(typeof(IWebHookHandler), h); });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IWebHookHandlerRegistry, WebHookHandlerRegistry>();
            builder.Services.AddScoped<IWebHookHandlerResolver, WebHookHandlerResolver>();
            builder.Services.AddScoped<IWebHookEventFactory, WebHookEventFactory>();

            return builder;
        }

        public static void AddHandler<TEvent>(this AutoGitWebHookOptions webHookOptions) where TEvent : IWebHookHandler
        {
            webHookOptions.WebHookHandlers.Add(typeof(TEvent));
        }
        
        public static void AddHandler(this AutoGitWebHookOptions webHookOptions, Type handlerType)
        {
            var hasInterface = handlerType.GetInterface(nameof(IWebHookHandler));

            if (hasInterface == null)
                return;
            
            webHookOptions.WebHookHandlers.Add(handlerType);
        }

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
            var app = endpoints.CreateApplicationBuilder();

            var options = app.ApplicationServices.GetService<IOptions<AutoGitOptions>>().Value;

            var pipeline = endpoints.CreateApplicationBuilder()
                .UseMiddleware<WebHookMiddleware>()
                .Build();

            return endpoints.Map(hookEndpoint, pipeline);
        }
    }
}