using AutoBogus;
using AutoBogus.NSubstitute;
using AutoGit.Core.Services;
using AutoGit.WebHooks.Models;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public sealed class WebHookEventFaker : AutoFaker<WebHookEvent>
    {
        public WebHookEventFaker()
        {
            UseSeed(Constants.DataSeed);
            
            var fakedPayload = new PayloadFaker().Generate();

            CustomInstantiator(f => new WebHookEvent(f.Random.ListItem(Constants.Events), f.Random.Word(),
                f.Random.Word(), fakedPayload, new OctokitSerializer()));
            
            Configure(x =>
            {
                x.WithLocale("en_US");
                x.WithBinder<NSubstituteBinder>();
            });
        }
    }
}