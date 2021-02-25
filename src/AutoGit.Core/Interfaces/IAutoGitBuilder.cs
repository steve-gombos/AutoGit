using Microsoft.Extensions.DependencyInjection;

namespace AutoGit.Core.Interfaces
{
    public interface IAutoGitBuilder
    {
        IServiceCollection Services { get; }
    }
}