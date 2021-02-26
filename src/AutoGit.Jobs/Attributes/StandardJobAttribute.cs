using AutoGit.Jobs.Logging;
using Hangfire.Common;
using Hangfire.Server;
using System;

namespace AutoGit.Jobs.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StandardJobAttribute : JobFilterAttribute, IServerFilter
    {
        private IDisposable _subscription;
        
        public StandardJobAttribute(bool runOnStart = false)
        {
            RunOnStart = runOnStart;
        }

        public bool RunOnStart { get; }

        public void OnPerforming(PerformingContext filterContext)
        {
            _subscription = HangfireConsoleLogger.InContext(filterContext);
        }
        public void OnPerformed(PerformedContext filterContext)
        {
            _subscription?.Dispose();
        }
    }
}