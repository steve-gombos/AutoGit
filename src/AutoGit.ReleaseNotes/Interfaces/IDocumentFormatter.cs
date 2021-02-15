using AutoGit.ReleaseNotes.Models;
using Octokit;
using System.Collections.Generic;

namespace AutoGit.ReleaseNotes.Interfaces
{
    public interface IDocumentFormatter
    {
        FormatterType Type { get; }
        DocumentDetails Format(Release release, List<GitHubCommit> commits);
    }
}