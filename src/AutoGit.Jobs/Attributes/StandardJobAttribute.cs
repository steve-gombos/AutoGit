using System;

namespace AutoGit.Jobs.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StandardJobAttribute : Attribute
    {
        public StandardJobAttribute(bool runOnStart = false)
        {
            RunOnStart = runOnStart;
        }

        public bool RunOnStart { get; }
    }
}