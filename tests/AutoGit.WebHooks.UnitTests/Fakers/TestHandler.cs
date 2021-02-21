using AutoGit.WebHooks.Context;
using AutoGit.WebHooks.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public class TestHandler : IWebHookHandler
    {
        public string EventName { get; set; }
        public List<string> Actions { get; set; }
        public bool IncludeBotEvents { get; set; }
        public Task Handle(EventContext eventContext)
        {
            throw new System.NotImplementedException();
        }
    }
}