using System.Threading.Tasks;

namespace AutoGit.Jobs.Interfaces
{
    public interface IAutoGitJob
    {
        string RepositoryOwner { get; }

        string RepositoryName { get; }

        Task Execute();
    }
}