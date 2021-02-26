using Hangfire.Common;
using System;

namespace AutoGit.Jobs.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StandardJobAttribute : JobFilterAttribute
    {
        private IDisposable _subscription;
        
        public StandardJobAttribute(bool runOnStart = false)
        {
            RunOnStart = runOnStart;
        }

        public bool RunOnStart { get; }
    }
}