using AutoGit.WebHooks.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace AutoGit.WebHooks.Models
{
    public class WebHookEventBinder : IModelBinder
    {
        private readonly IWebHookEventFactory _webHookEventFactory;

        public WebHookEventBinder(IWebHookEventFactory webHookEventFactory)
        {
            _webHookEventFactory = webHookEventFactory;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            await BindModelInternalAsync(bindingContext);
        }

        private async Task BindModelInternalAsync(ModelBindingContext bindingContext)
        {
            var webHookEvent = await _webHookEventFactory.Create(bindingContext.HttpContext);

            bindingContext.Result = ModelBindingResult.Success(webHookEvent);
        }
    }
}