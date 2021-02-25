using AutoGit.WebHooks.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.WebHooks.Interfaces
{
    public interface IWebHookHandler
    {
        string EventName { get; set; }

        List<string> Actions { get; set; }

        bool IncludeBotEvents { get; set; }

        Task Handle(EventContext eventContext);
    }
}