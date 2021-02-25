using AutoGit.Core.Interfaces;
using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace AutoGit.WebHooks
{
    public class WebHookEventFactory : IWebHookEventFactory
    {
        private readonly ISerializer _serializer;

        public WebHookEventFactory(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public async Task<WebHookEvent> Create(HttpContext context)
        {
            string payload;
            using (var sr = new StreamReader(context.Request.Body))
            {
                payload = await sr.ReadToEndAsync();
            }

            var webHookEvent = new WebHookEvent(context.Request.Headers[WebHookConstants.EventHeader],
                context.Request.Headers[WebHookConstants.DeliveryHeader],
                context.Request.Headers[WebHookConstants.HubSignatureHeader],
                payload,
                _serializer);

            return webHookEvent;
        }
    }
}