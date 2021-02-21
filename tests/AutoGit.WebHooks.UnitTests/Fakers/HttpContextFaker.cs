using AutoBogus;
using AutoBogus.NSubstitute;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public sealed class HttpContextFaker : AutoFaker<HttpContext>
    {
        public HttpContextFaker()
        {
            UseSeed(1);
            
            Configure(x =>
            {
                x.WithLocale("en_US");
                x.WithBinder<NSubstituteBinder>();
            });

            CustomInstantiator(f => new DefaultHttpContext
            {
                Request =
                {
                    Headers =
                    {
                        new KeyValuePair<string, StringValues>(WebHookConstants.EventHeader,
                            f.Random.ListItem(Constants.Events)),
                        new KeyValuePair<string, StringValues>(WebHookConstants.DeliveryHeader, f.Random.Word()),
                        new KeyValuePair<string, StringValues>(WebHookConstants.HubSignatureHeader, f.Random.Word())
                    }
                }
            });
        }
    }
}