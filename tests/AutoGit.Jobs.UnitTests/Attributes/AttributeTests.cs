using AutoGit.Jobs.Attributes;
using AutoGit.Jobs.UnitTests.Fakers;
using FluentAssertions;
using Xunit;

namespace AutoGit.Jobs.UnitTests.Attributes
{
    public class AttributeTests
    {
        [Fact]
        public void Job_Should_Be_Decorated_With_StandardAttribute()
        {
            // Arrange
            var job = new TestStandardJob("test", "test");

            // Act

            // Assert
            job.GetType().Should().BeDecoratedWith<StandardJobAttribute>();
        }

        [Fact]
        public void Job_Should_Be_Decorated_With_RecurringAttribute()
        {
            // Arrange
            var job = new TestRecurringJob("test", "test");

            // Act

            // Assert
            job.GetType().Should().BeDecoratedWith<RecurringJobAttribute>();
        }
    }
}