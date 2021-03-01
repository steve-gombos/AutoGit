using AutoGit.Jobs.Attributes;
using AutoGit.Jobs.Enums;
using System;
using System.Threading.Tasks;

namespace AutoGit.Jobs.UnitTests.Fakers
{
    [RecurringJob("0/5 * * * *", TimeZoneSetting.Local)]
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