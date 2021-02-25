using AutoGit.WebHooks.Interfaces;
using AutoGit.WebHooks.Models;
using AutoGit.WebHooks.UnitTests.Fakers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AutoGit.WebHooks.UnitTests.Models
{
    public class WebHookEventBinderTests
    {
        private readonly WebHookEventBinder _sut;
        private readonly IWebHookEventFactory _webHookEventFactory = Substitute.For<IWebHookEventFactory>();

        public WebHookEventBinderTests()
        {
            _sut = new WebHookEventBinder(_webHookEventFactory);
        }

        [Fact]
        public async void BindModel_Should_Return_Success_When_Context_Is_Not_Null()
        {
            // Arrange
            var modelBindingContext = new DefaultModelBindingContext();
            var fakedWebHookEvent = new WebHookEventFaker().Generate();
            _webHookEventFactory.Create(modelBindingContext.HttpContext).Returns(fakedWebHookEvent);

            // Act
            await _sut.BindModelAsync(modelBindingContext);

            // Assert
            _webHookEventFactory.Received(1);
            modelBindingContext.Result.Should().Be(ModelBindingResult.Success(fakedWebHookEvent));
        }

        [Fact]
        public async void BindModel_Should_Throw_Exception_When_Context_Is_Null()
        {
            // Arrange

            // Act
            Func<Task> action = async () => await _sut.BindModelAsync(null);

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}