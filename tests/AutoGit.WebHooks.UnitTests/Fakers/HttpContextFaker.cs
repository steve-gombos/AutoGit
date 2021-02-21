using AutoBogus;
using AutoBogus.NSubstitute;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public sealed class HttpContextFaker : AutoFaker<HttpContext>
    {
        public HttpContextFaker()
        {
            UseSeed(Constants.DataSeed);
            
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
                    },
                    Body = GenerateBody()
                }
            });
        }

        private Stream GenerateBody()
        {
            var fakedPayload = new PayloadFaker().Generate();
            byte[] byteArray = Encoding.UTF8.GetBytes(fakedPayload);
            MemoryStream stream = new MemoryStream(byteArray);

            return stream;
        }
    }
}