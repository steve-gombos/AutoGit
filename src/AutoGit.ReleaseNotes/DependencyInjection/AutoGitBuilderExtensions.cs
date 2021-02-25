using AutoGit.Core.Interfaces;
using AutoGit.ReleaseNotes.Formatters;
using AutoGit.ReleaseNotes.Hooks;
using AutoGit.ReleaseNotes.Interfaces;
using AutoGit.ReleaseNotes.Services;
using AutoGit.WebHooks.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutoGit.ReleaseNotes.DependencyInjection
{
    public static class AutoGitBuilderExtensions
    {
        public static IAutoGitBuilder AddReleaseNoteGenerator(this IAutoGitBuilder builder,
            Action<AutoGitReleaseOptions> setupAction = null)
        {
            var releaseOptions = new AutoGitReleaseOptions();
            setupAction?.Invoke(releaseOptions);

            builder.Services.Configure(setupAction);

            builder.AddWebHookHandlers(options =>
            {
                if (releaseOptions.ManageReleaseNotes) options.AddHandler<ReleaseCreatedHandler>();
            });

            builder.Services.AddTransient<ICommitFinder, CommitFinder>();

            if (releaseOptions.ManageReleaseNotes)
            {
                builder.Services.AddTransient<IDocumentUpdater, ReleaseNoteUpdater>();
                builder.Services.AddTransient<IDocumentFormatter, DefaultFormatter>();
            }

            if (releaseOptions.ManageChangeLog)
            {
                builder.Services.AddTransient<IDocumentUpdater, ChangeLogUpdater>();
                builder.Services.AddTransient<IDocumentFormatter, DefaultFormatter>();
            }

            return builder;
        }
    }
}