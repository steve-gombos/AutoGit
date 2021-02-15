using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AutoGit.WebHooks
{
    public class WebHookMiddleware
    {
        private readonly RequestDelegate _next;

        public WebHookMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next.Invoke(httpContext);
        }
    }
}