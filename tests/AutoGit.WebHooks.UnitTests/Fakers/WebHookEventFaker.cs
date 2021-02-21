using AutoBogus;
using AutoBogus.NSubstitute;
using AutoGit.WebHooks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NSubstitute;
using System.Collections.Generic;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public sealed class WebHookEventFaker : AutoFaker<WebHookEvent>
    {
        public WebHookEventFaker()
        {
            UseSeed(1);

            var httpContextFaker = new HttpContextFaker();
            var fakeHttpContext = httpContextFaker.Generate();
            
            var mockedHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
            mockedHttpContextAccessor.HttpContext.Returns(fakeHttpContext);
            string payload =
                "{\"action\": \"opened\",\"issue\": {\"url\": \"https://api.github.com/repos/octocat/Hello-World/issues/1347\",\"number\": 1347},\"repository\" : {\"id\": 1296269,\"full_name\": \"octocat/Hello-World\",\"owner\": {\"login\": \"octocat\",\"id\": 1}},\"sender\": {\"login\": \"octocat\", \"id\": 1 }}";
            

            CustomInstantiator(x => new WebHookEvent(mockedHttpContextAccessor.HttpContext, payload));
            
            Configure(x =>
            {
                x.WithLocale("en_US");
                x.WithBinder<NSubstituteBinder>();
            });
            
            RuleFor(x => x.EventName, f => f.Random.ListItem(Constants.Events));
            RuleFor(x => x.IsBot, f => f.Random.Bool());
            
        }
    }
}