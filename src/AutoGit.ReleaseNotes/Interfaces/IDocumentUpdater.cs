using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.ReleaseNotes.Interfaces
{
    public interface IDocumentUpdater
    {
        Task Update(Repository repository, Release release, List<GitHubCommit> commits);
    }
}