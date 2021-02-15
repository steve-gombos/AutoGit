using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.ReleaseNotes.Interfaces
{
    public interface ICommitFinder
    {
        Task<List<GitHubCommit>> GetCommits(long repositoryId, int releaseId);
    }
}