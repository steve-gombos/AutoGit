using AutoGit.WebHooks.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AutoGit.WebHooks.Interfaces
{
    public interface IWebHookEventFactory
    {
        Task<WebHookEvent> Create(HttpContext context);
    }
}