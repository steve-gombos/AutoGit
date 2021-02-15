using System.Collections.Generic;

namespace AutoGit.WebHooks.Interfaces
{
    public interface IWebHookHandlerResolver
    {
        public List<IWebHookHandler> Resolve(string eventName, string action, bool isBot);
    }
}