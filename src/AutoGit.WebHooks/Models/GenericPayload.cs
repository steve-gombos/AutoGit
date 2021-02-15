using Octokit;

namespace AutoGit.WebHooks.Models
{
    public class GenericPayload : ActivityPayload
    {
        public string Action { get; set; }
    }
}