using FluentValidation;
using Microsoft.Extensions.Options;

namespace AutoGit.WebHooks.Models.Validators
{
    public class WebHookEventValidator : AbstractValidator<WebHookEvent>
    {
        public WebHookEventValidator(IOptions<AutoGitEventOptions> eventOptions)
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x).Must(x => x.IsAuthenticated(eventOptions.Value.WebHookSecret)).WithMessage("Unauthenticated event data.");
        }
    }
}
