using FluentValidation;
using Microsoft.Extensions.Options;

namespace AutoGit.WebHooks.Models.Validators
{
    public class WebHookEventValidator : AbstractValidator<WebHookEvent>
    {
        public WebHookEventValidator(IOptions<AutoGitWebHookOptions> eventOptions)
        {
            RuleFor(x => x).NotNull();
            if (!string.IsNullOrWhiteSpace(eventOptions.Value.WebHookSecret))
            {
                RuleFor(x => x).Must(x => x.IsAuthenticated(eventOptions.Value.WebHookSecret))
                    .WithMessage("Unauthenticated event data.");
            }
        }
    }
}
