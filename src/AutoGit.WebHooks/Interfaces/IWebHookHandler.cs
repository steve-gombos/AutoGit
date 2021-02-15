using AutoGit.WebHooks.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.WebHooks.Interfaces
{
    public interface IWebHookHandler
    {
        string EventName { get; }
        
        List<string> Actions { get; }
        
        bool IncludeBotEvents { get; }

        Task Handle(EventContext eventContext);
    }
}
