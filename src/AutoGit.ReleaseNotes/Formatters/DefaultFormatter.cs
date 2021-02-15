using AutoGit.ReleaseNotes.Interfaces;
using AutoGit.ReleaseNotes.Models;
using Microsoft.Extensions.Options;
using Octokit;
using System.Collections.Generic;
using System.Text;

namespace AutoGit.ReleaseNotes.Formatters
{
    public class DefaultFormatter : IDocumentFormatter
    {
        private readonly AutoGitReleaseOptions _options;

        public FormatterType Type { get; } = FormatterType.ChangeLog | FormatterType.Release;

        public DefaultFormatter(IOptions<AutoGitReleaseOptions> options)
        {
            _options = options.Value;
        }

        public DocumentDetails Format(Release release, List<GitHubCommit> commits)
        {
            var sb = new StringBuilder();

            var name = !string.IsNullOrWhiteSpace(release.Name)
                ? release.Name
                : release.TagName.Replace(_options.VersionTagPrefix, "");
            sb.AppendLine($"# {name} ({release.CreatedAt:yyyy-MM-dd})");

            foreach (var commit in commits)
            {
                sb.AppendLine($"- [{commit.Commit.Message}]({commit.HtmlUrl}) - @{commit.Author.Login}");
            }

            return new DocumentDetails
            {
                Name = name,
                Content = sb.ToString()
            };
        }
    }
}