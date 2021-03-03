using AutoGit.Core.Models;
using FluentAssertions;
using Xunit;

namespace AutoGit.Core.UnitTests.Models
{
    public class GenericPayloadTests
    {
        private GenericPayload _sut;

        [Fact]
        public void Action_Should_Be_Equal()
        {
            // Arrange
            var action = "test";
            _sut = new GenericPayload {Action = action};

            // Assert
            _sut.Action.Should().Be(action);
        }
    }
}