using AutoGit.WebHooks.Context;
using AutoGit.WebHooks.Interfaces;
using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.Bot.Hooks
{
    public class Test
    {
        private readonly IWebHookHandler _handler;

        public Test(IWebHookHandler handler)
        {
            _handler = handler;
        }
    }
    public class IssueCommentHandler : IWebHookHandler
    {
        public string EventName { get; set; } = "issue_comment";
        public List<string> Actions { get; } = new List<string> {"created"};
        public bool IncludeBotEvents { get; } = false;

        public async Task Handle(EventContext eventContext)
        {
            var payload = eventContext.WebHookEvent.GetPayload<IssueCommentPayload>();

            if (payload.Sender.Type == AccountType.Bot)
                return;
            
            await eventContext.Clients.InstallationClient.Issue.Comment.Create(payload.Repository.Id,
                payload.Issue.Number, "Hello World");
        }
    }
}