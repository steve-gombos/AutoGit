using System.Threading.Tasks;

namespace AutoGit.ReleaseNotes.Interfaces
{
    public interface IReleaseNoteGenerator
    {
        Task Generate(long repositoryId, int releaseId);
    }
}