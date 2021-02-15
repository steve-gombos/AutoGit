using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoGit.Bot.Controllers
{
    [Route("github")]
    [ApiController]
    public class GithubController : ControllerBase
    {
        private readonly IWebHookHandlerRegistry _webHookHandlerRegistry;

        public GithubController(IWebHookHandlerRegistry webHookHandlerRegistry)
        {
            _webHookHandlerRegistry = webHookHandlerRegistry;
        }
        
        [HttpPost("hooks")]
        public async Task<IActionResult> Hooks(WebHookEvent webHookEvent)
        {
            await _webHookHandlerRegistry.Handle(webHookEvent);

            return Ok();
        }
    }
}
