using AutoGit.WebHooks.Context;
using System.Threading.Tasks;

namespace AutoGit.WebHooks.Interfaces
{
    public interface IWebHookHandler
    {
        string EventName { get; }
        
        string Action { get; }
        
        bool IncludeBotEvents { get; }

        Task Handle(EventContext eventContext);
    }
}
