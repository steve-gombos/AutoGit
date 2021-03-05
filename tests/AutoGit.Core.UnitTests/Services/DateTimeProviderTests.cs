using AutoGit.Core.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace AutoGit.Core.UnitTests.Services
{
    public class DateTimeProviderTests
    {
        private readonly IDateTimeProvider _sut;

        public DateTimeProviderTests()
        {
            _sut = Substitute.For<IDateTimeProvider>();
        }

        [Fact]
        public void UtcNow_Should_Returns_Date()
        {
            // Arrange
            var date = DateTime.UtcNow;
            _sut.UtcNow.Returns(date);
            
            // Assert
            _sut.UtcNow.Should().Be(date);
        }
    }
}