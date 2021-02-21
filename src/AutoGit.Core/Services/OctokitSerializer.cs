using AutoGit.Core.Interfaces;
using Octokit;
using Octokit.Internal;

namespace AutoGit.Core.Services
{
    public class OctokitSerializer : ISerializer
    {
        private readonly SimpleJsonSerializer _serializer;

        public OctokitSerializer()
        {
            _serializer = new SimpleJsonSerializer();
        }

        public string Serialize<T>(T payload) where T : ActivityPayload
        {
            return _serializer.Serialize(payload);
        }

        public T Deserialize<T>(string payload) where T : ActivityPayload
        {
            return _serializer.Deserialize<T>(payload);
        }
    }
}