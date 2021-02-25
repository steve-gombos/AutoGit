using Octokit;

namespace AutoGit.Core.Interfaces
{
    public interface ISerializer
    {
        string Serialize<T>(T payload) where T : ActivityPayload;
        T Deserialize<T>(string payload) where T : ActivityPayload;
    }
}