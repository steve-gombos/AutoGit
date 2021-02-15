﻿using AutoGit.ReleaseNotes.Models;
using Octokit;
using System.Collections.Generic;

namespace AutoGit.ReleaseNotes.Interfaces
{
    public interface IDocumentFormatter
    {
        FormatterTypes Type { get; }
        DocumentDetails Format(Release release, List<GitHubCommit> commits);
    }
}