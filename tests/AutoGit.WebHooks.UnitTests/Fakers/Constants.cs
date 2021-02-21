using System.Collections.Generic;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public static class Constants
    {
        public const int DataSeed = 1;
        
        public static List<string> Actions = new List<string>
        {
            "created",
            "completed"
        };

        public static List<string> Events = new List<string>
        {
            "issue_comment",
            "issues",
            "release",
            "pull_request",
            "push"
        };
    }
}