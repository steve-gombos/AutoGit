using System;
using System.Threading.Tasks;

namespace AutoGit.Jobs.UnitTests.Fakers
{
    public class TestRecurringJob : AutoGitJob
    {
        public TestRecurringJob(string repositoryOwner, string repositoryName) : base(repositoryOwner, repositoryName)
        {
        }
        
        public override Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}