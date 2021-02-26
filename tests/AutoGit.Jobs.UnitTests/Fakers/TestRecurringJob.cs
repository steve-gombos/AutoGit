using AutoGit.Jobs.Attributes;
using AutoGit.Jobs.Interfaces;
using System;
using System.Threading.Tasks;

namespace AutoGit.Jobs.UnitTests.Fakers
{
    [RecurringJob("0/5 * * * *", true)]
    public class TestRecurringJob : IAutoGitJob
    {
        public string RepositoryOwner { get; }
        public string RepositoryName { get; }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}