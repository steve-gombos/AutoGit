using AutoBogus;
using AutoBogus.NSubstitute;
using AutoGit.Core.Services;
using AutoGit.WebHooks.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public sealed class WebHookEventFaker : AutoFaker<WebHookEvent>
    {
        public WebHookEventFaker()
        {
            UseSeed(Constants.DataSeed);
            
            var fakedPayload = new PayloadFaker().Generate();

            CustomInstantiator(f => new WebHookEvent(f.Random.ListItem(Constants.Events), f.Random.Word(),
                GetHubSignature(fakedPayload), fakedPayload, new OctokitSerializer()));
            
            Configure(x =>
            {
                x.WithLocale("en_US");
                x.WithBinder<NSubstituteBinder>();
            });
        }

        private string GetHubSignature(string payload)
        {
            var key = Encoding.UTF8.GetBytes(Constants.ValidWebHookSecret);
            var msg = Encoding.UTF8.GetBytes(payload);

            using var hmac = new HMACSHA1(key);

            var hashValue = hmac.ComputeHash(msg);
            var calcHash = "sha1=" + BitConverter.ToString(hashValue).Replace("-", "").ToLowerInvariant();
            
            return calcHash;
        }
    }
}