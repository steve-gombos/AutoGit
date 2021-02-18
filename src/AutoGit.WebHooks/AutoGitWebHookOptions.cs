using System;
using System.Collections.Generic;

namespace AutoGit.WebHooks
{
    public class AutoGitWebHookOptions
    {
        public string WebHookSecret { get; set; }
        public List<Type> WebHookHandlers { get; } = new List<Type>();
    }
}
