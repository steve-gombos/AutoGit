using System;

namespace AutoGit.Jobs.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class JobAttribute : Attribute
    {
        public bool RunOnStart { get; set; }

        public JobAttribute(bool runOnStart = false)
        {
            RunOnStart = runOnStart;
        }
    }
}
