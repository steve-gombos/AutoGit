using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models;
using AutoGit.WebHooks.Models.Validators;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;

namespace AutoGit.WebHooks
{
    public class WebHookMiddleware
    {
        private readonly RequestDelegate _next;

        public WebHookMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IWebHookHandlerRegistry webHookHandlerRegistry, WebHookEventValidator validator)
        {
            string payload;
            using(var sr = new StreamReader(httpContext.Request.Body))
            {
                payload = await sr.ReadToEndAsync();
            }

            var webHookEvent = new WebHookEvent(httpContext, payload);

            var result = await validator.ValidateAsync(webHookEvent);

            if (!result.IsValid)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            await webHookHandlerRegistry.Handle(webHookEvent);
        }
    }
}