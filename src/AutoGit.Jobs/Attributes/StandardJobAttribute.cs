using System;

namespace AutoGit.Jobs.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class StandardJobAttribute : Attribute
    {
        public bool RunOnStart { get; }

        public StandardJobAttribute(bool runOnStart = false)
        {
            RunOnStart = runOnStart;
        }
    }
}
