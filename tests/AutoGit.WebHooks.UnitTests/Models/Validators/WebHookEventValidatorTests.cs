using AutoGit.WebHooks.Models.Validators;
using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace AutoGit.WebHooks.UnitTests.Models.Validators
{
    public class WebHookEventValidatorTests
    {
        private readonly IOptions<AutoGitWebHookOptions> _options = Substitute.For<IOptions<AutoGitWebHookOptions>>();
        private readonly WebHookEventValidator _sut;

        public WebHookEventValidatorTests()
        {
            _options.Value.Returns(new AutoGitWebHookOptions {WebHookSecret = Constants.ValidWebHookSecret});
            _sut = new WebHookEventValidator(_options);
        }

        [Fact]
        public void Validator_Should_Not_Have_Error_When_Authenticated()
        {
            // Arrange
            var fakedWebHookEvent = new WebHookEventFaker().Generate();

            // Act
            var result = _sut.TestValidate(fakedWebHookEvent);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Not_Authenticated()
        {
            // Arrange
            var fakedWebHookEvent = new WebHookEventFaker().Generate();
            _options.Value.Returns(new AutoGitWebHookOptions {WebHookSecret = Constants.InvalidWebHookSecret});

            // Act
            var result = _sut.TestValidate(fakedWebHookEvent);

            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}