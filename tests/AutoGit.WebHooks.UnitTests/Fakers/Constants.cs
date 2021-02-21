using System.Collections.Generic;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public static class Constants
    {
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