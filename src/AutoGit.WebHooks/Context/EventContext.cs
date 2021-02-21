using AutoGit.Core;
using AutoGit.WebHooks.Models;
using Microsoft.AspNetCore.Http;

namespace AutoGit.WebHooks.Context
{
    public class EventContext
    {
        internal EventContext(HttpContext httpContext, WebHookEvent webHookEvent, GitHubClients clients)
        {
            HttpContext = httpContext;
            WebHookEvent = webHookEvent;
            Clients = clients;
        }

        public HttpContext HttpContext { get; }

        public WebHookEvent WebHookEvent { get; }

        public GitHubClients Clients { get; }
    }
}