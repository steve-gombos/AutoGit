using AutoGit.Core.Interfaces;
using AutoGit.WebHooks.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AutoGit.WebHooks.DependencyInjection
{
    public static class AutoGitBuilderExtensions
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

            options.WebHookHandlers.ForEach(h => { builder.Services.AddScoped(typeof(IWebHookHandler), h); });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRouting();

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
    }
}