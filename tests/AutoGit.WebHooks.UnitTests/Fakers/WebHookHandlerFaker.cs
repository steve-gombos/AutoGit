using AutoBogus;
using AutoBogus.NSubstitute;
using AutoGit.WebHooks.Interfaces;
using System.Collections.Generic;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public sealed class WebHookHandlerFaker : AutoFaker<IWebHookHandler>
    {
        private readonly List<string> _actions = new List<string>
        {
            "created",
            "completed",
            "deleted",
            "updated"
        };

        private readonly List<string> _events = new List<string>
        {
            "issue_comment",
            "issues",
            "release"
        };

        public WebHookHandlerFaker()
        {
            UseSeed(987123);

            Configure(x => { x.WithBinder<NSubstituteBinder>(); });

            RuleFor(x => x.Actions, f => f.Random.ListItems(_actions, 2));
            RuleFor(x => x.EventName, f => f.Random.ListItem(_events));
            RuleFor(x => x.IncludeBotEvents, f => f.Random.Bool());
        }
    }
}