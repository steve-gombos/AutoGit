using AutoGit.Jobs.Interfaces;
using System.Threading.Tasks;

namespace AutoGit.Jobs
{
    public abstract class AutoGitJob : IAutoGitJob
    {
        protected AutoGitJob(string repositoryOwner, string repositoryName)
        {
            RepositoryOwner = repositoryOwner;
            RepositoryName = repositoryName;
        }
        public string RepositoryOwner { get; }
        public string RepositoryName { get; }
        public abstract Task Execute();
    }
}