using AutoGit.Core.Services;
using System.Threading.Tasks;

namespace AutoGit.Core.Interfaces
{
    public interface IGitHubClientFactory
    {
        Task<GitHubClients> Create();
    }
}