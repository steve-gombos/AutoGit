using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AutoGit.WebHooks.Models
{
    public class WebHookEventBinder : IModelBinder
    {        
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            await BindModelInternalAsync(bindingContext);
        }

        private async Task BindModelInternalAsync(ModelBindingContext bindingContext)
        {
            string payload;
            using(var sr = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                payload = await sr.ReadToEndAsync();
            }

            var webHookEvent = new WebHookEvent(bindingContext, payload);

            bindingContext.Result = ModelBindingResult.Success(webHookEvent);
        }
    }
}
