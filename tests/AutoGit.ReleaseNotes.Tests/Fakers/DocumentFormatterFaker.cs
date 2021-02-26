using AutoBogus;
using AutoBogus.NSubstitute;
using AutoGit.ReleaseNotes.Interfaces;
using AutoGit.ReleaseNotes.Models;

namespace AutoGit.ReleaseNotes.Tests.Fakers
{
    public sealed class DocumentFormatterFaker : AutoFaker<IDocumentFormatter>
    {
        public DocumentFormatterFaker()
        {
            UseSeed(Constants.DataSeed);

            Configure(x => { x.WithBinder<NSubstituteBinder>(); });

            RuleFor(x => x.Type, f => f.Random.Enum<FormatterTypes>());
        }
    }
}