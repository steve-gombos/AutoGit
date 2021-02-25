using AutoGit.WebHooks.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AutoGit.WebHooks
{
    public class WebHookHandlerResolver : IWebHookHandlerResolver
    {
        private readonly IEnumerable<IWebHookHandler> _handlers;

        public WebHookHandlerResolver(IEnumerable<IWebHookHandler> handlers)
        {
            _handlers = handlers;
        }

        public List<IWebHookHandler> Resolve(string eventName, string action, bool isBot)
        {
            return _handlers.Where(h => h.EventName == eventName &&
                                        h.Actions.Contains(action) &&
                                        h.IncludeBotEvents == isBot).ToList();
        }
    }
}