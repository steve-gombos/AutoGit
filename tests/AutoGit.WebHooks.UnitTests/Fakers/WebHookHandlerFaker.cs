using AutoBogus;
using AutoBogus.NSubstitute;
using AutoGit.WebHooks.Interfaces;
using System.Collections.Generic;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public sealed class WebHookHandlerFaker : AutoFaker<IWebHookHandler>
    {
        public WebHookHandlerFaker()
        {
            UseSeed(Constants.DataSeed);

            Configure(x => { x.WithBinder<NSubstituteBinder>(); });

            RuleFor(x => x.EventName, f => f.Random.ListItem(Constants.Events));
            RuleFor(x => x.Actions, f => f.Random.ListItems(Constants.Actions, 1));
            RuleFor(x => x.IncludeBotEvents, f => f.Random.Bool());
        }
    }
}