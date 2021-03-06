﻿using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models.Validators;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace AutoGit.WebHooks
{
    public class WebHookMiddleware
    {
        public WebHookMiddleware(RequestDelegate next)
        {
        }

        public async Task Invoke(HttpContext httpContext, IWebHookHandlerRegistry webHookHandlerRegistry,
            IWebHookEventFactory webHookEventFactory, WebHookEventValidator validator)
        {
            var webHookEvent = await webHookEventFactory.Create(httpContext);

            var result = await validator.ValidateAsync(webHookEvent);

            if (!result.IsValid)
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                return;
            }

            await webHookHandlerRegistry.Handle(webHookEvent);
        }
    }
}