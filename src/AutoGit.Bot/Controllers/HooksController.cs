using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoGit.Bot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HooksController : ControllerBase
    {
        private readonly IWebHookHandlerRegistry _webHookHandlerRegistry;

        public HooksController(IWebHookHandlerRegistry webHookHandlerRegistry)
        {
            _webHookHandlerRegistry = webHookHandlerRegistry;
        }

        [HttpPost("test")]
        public async Task<IActionResult> Hooks(WebHookEvent webHookEvent)
        {
            await _webHookHandlerRegistry.Handle(webHookEvent);

            return Ok();
        }
    }
}