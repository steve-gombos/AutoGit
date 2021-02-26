using AutoGit.WebHooks.Context;
using AutoGit.WebHooks.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.WebHooks
{
    public abstract class WebHookHandler : IWebHookHandler
    {
        protected WebHookHandler(string eventName, List<string> actions, bool includeBotEvents = false)
        {
            EventName = eventName;
            Actions = actions;
            IncludeBotEvents = includeBotEvents;
        }

        public string EventName { get; set; }
        public List<string> Actions { get; set; }
        public bool IncludeBotEvents { get; set; }
        public abstract Task Handle(EventContext eventContext);
    }
}