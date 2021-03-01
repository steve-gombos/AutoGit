using AutoGit.WebHooks.Context;
using AutoGit.WebHooks.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public class TestHandler : WebHookHandler
    {
        public TestHandler() : base("test", new List<string>(){"test"})
        {
        }
        
        public TestHandler(string eventName, List<string> actions, bool includeBotEvents) : base(eventName, actions, includeBotEvents)
        {
        }

        
        public override Task Handle(EventContext eventContext)
        {
            throw new NotImplementedException();
        }
    }
}