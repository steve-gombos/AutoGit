using System;

namespace AutoGit.Core
{
    public class AutoGitOptions
    {
        public Uri Url { get; set; }
        public string AppName { get; set; }
        public long AppIdentifier { get; set; }
        public string PrivateKey { get; set; }
    }
}