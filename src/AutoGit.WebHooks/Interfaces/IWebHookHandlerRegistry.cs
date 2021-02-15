using AutoGit.WebHooks.Models;
using System.Threading.Tasks;

namespace AutoGit.WebHooks.Interfaces
{
    public interface IWebHookHandlerRegistry
    {
        Task Handle(WebHookEvent webHookEvent);
    }
}
