using AutoGit.Jobs.Attributes;
using AutoGit.Jobs.Interfaces;
using System.Threading.Tasks;

namespace AutoGit.Jobs.UnitTests.Fakers
{
    [StandardJob]
    public class TestStandardJob : IAutoGitJob
    {
        public string RepositoryOwner { get; }
        public string RepositoryName { get; }
        public Task Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}