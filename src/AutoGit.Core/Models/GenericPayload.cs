using Octokit;

namespace AutoGit.Core.Models
{
    public class GenericPayload : ActivityPayload
    {
        public GenericPayload()
        {
            
        }
        
        public GenericPayload(Repository repository, User user, Installation installation) : base(repository, user,
            installation)
        {
            
        }
        
        public string Action { get; set; }
    }
}