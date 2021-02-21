using AutoBogus;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSubstitute;

namespace AutoGit.WebHooks.UnitTests.Fakers
{
    public sealed class ModelBindingContextFaker : AutoFaker<ModelBindingContext>
    {
        public ModelBindingContextFaker()
        {
            UseSeed(Constants.DataSeed);

            var fakedHttpContext = new HttpContextFaker().Generate();

            var mockedModelBindingContext = Substitute.For<ModelBindingContext>();
            mockedModelBindingContext.HttpContext.Returns(fakedHttpContext);

            CustomInstantiator(f => mockedModelBindingContext);
        }
    }
}