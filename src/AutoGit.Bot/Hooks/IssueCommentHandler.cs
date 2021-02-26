using AutoGit.WebHooks;
using AutoGit.WebHooks.Context;
using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.Bot.Hooks
{
    public class IssueCommentHandler : WebHookHandler
    {
        public IssueCommentHandler() : base("issue_comment", new List<string> {"created"})
        {
        }

        public override async Task Handle(EventContext eventContext)
        {
            var payload = eventContext.WebHookEvent.GetPayload<IssueCommentPayload>();

            if (payload.Sender.Type == AccountType.Bot)
                return;

            await eventContext.Clients.InstallationClient.Issue.Comment.Create(payload.Repository.Id,
                payload.Issue.Number, "Hello World");
        }
    }
}