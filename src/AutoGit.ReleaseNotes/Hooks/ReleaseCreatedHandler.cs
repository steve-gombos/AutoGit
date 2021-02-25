using AutoGit.ReleaseNotes.Interfaces;
using AutoGit.WebHooks.Context;
using AutoGit.WebHooks.Interfaces;
using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.ReleaseNotes.Hooks
{
    public sealed class ReleaseCreatedHandler : IWebHookHandler
    {
        private readonly ICommitFinder _commitFinder;
        private readonly IEnumerable<IDocumentUpdater> _documentUpdaters;

        public ReleaseCreatedHandler(IEnumerable<IDocumentUpdater> documentUpdaters, ICommitFinder commitFinder)
        {
            _documentUpdaters = documentUpdaters;
            _commitFinder = commitFinder;
        }

        public string EventName { get; set; } = "release";
        public List<string> Actions { get; set; } = new List<string> {"created"};
        public bool IncludeBotEvents { get; set; } = false;

        public async Task Handle(EventContext eventContext)
        {
            var payload = eventContext.WebHookEvent.GetPayload<ReleaseEventPayload>();

            var repoId = payload.Repository.Id;
            var releaseId = payload.Release.Id;

            var commits = await _commitFinder.GetCommits(repoId, releaseId);

            foreach (var documentUpdater in _documentUpdaters)
                await documentUpdater.Update(payload.Repository, payload.Release, commits);
        }
    }
}