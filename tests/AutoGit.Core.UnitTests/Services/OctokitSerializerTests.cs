using AutoGit.Core.Interfaces;
using AutoGit.Core.Models;
using AutoGit.Core.Services;
using Octokit;
using Xunit;

namespace AutoGit.Core.UnitTests.Services
{
    public class OctokitSerializerTests
    {
        private readonly ISerializer _sut;

        public OctokitSerializerTests()
        {
            _sut = new OctokitSerializer();
        }

        // [Fact]
        // public void Serializer_Should_Return_String()
        // {
        //     // Arrange
        //     var payload =
        //         "{{\"action\": \"test\",\"issue\": {{\"url\": \"https://api.github.com/repos/octocat/Hello-World/issues/1347\",\"number\": 1347}},\"repository\" : {{\"id\": 1296269,\"full_name\": \"octocat/Hello-World\",\"owner\": {{\"login\": \"octocat\",\"id\": 1}}}},\"sender\": {{\"login\": \"octocat\", \"id\": 1, \"type\": \"test\" }}}}";
        //     var expected = new GenericPayload(new Repository())
        //     {
        //         Action = "created",
        //     };
        //
        //     // Act
        //     //_sut.Serialize()
        //
        //     // Assert
        // }
        //
        // [Fact]
        // public void Deserializer_Should_Return_GenericPayload()
        // {
        //     // Arrange
        //     var payload =
        //         "{\"action\": \"created\",\"issue\": {\"url\": \"https://api.github.com/repos/octocat/Hello-World/issues/1347\",\"number\": 1347},\"repository\" : {\"id\": 1296269,\"full_name\": \"octocat/Hello-World\",\"owner\": {\"login\": \"octocat\",\"id\": 1}},\"sender\": {\"login\": \"octocat\", \"id\": 1, \"type\": \"1\" }}";
        //     
        //     // Act
        //     var actual = _sut.Deserialize<GenericPayload>(payload);
        //
        //     // Assert
        //     
        // }
    }
}