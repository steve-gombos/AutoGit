using AutoGit.Core.Interfaces;
using AutoGit.WebHooks.Context;
using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGit.WebHooks
{
    public class WebHookHandlerRegistry : IWebHookHandlerRegistry
    {
        private readonly IGitHubClientFactory _gitHubClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHookHandlerResolver _webHookHandlerResolver;

        public WebHookHandlerRegistry(IWebHookHandlerResolver webHookHandlerResolver,
            IGitHubClientFactory gitHubClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _webHookHandlerResolver = webHookHandlerResolver;
            _gitHubClientFactory = gitHubClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(WebHookEvent webHookEvent)
        {
            var clients = await _gitHubClientFactory.Create();

            var handlers = _webHookHandlerResolver.Resolve(webHookEvent.EventName, 
                webHookEvent.GenericPayload.Action, webHookEvent.IsBot);

            if (!handlers.Any())
                return;

            var context = new EventContext(_httpContextAccessor.HttpContext, webHookEvent, clients);

            handlers.ForEach(h => h.Handle(context));
        }
    }
}