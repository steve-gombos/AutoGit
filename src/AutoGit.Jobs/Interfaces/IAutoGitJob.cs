using System.Threading.Tasks;

namespace AutoGit.Jobs.Interfaces
{
    public interface IAutoGitJob
    {
        string RepositoryOwner { get; set; }

        string RepositoryName { get; set; }

        Task Execute();
    }
}
