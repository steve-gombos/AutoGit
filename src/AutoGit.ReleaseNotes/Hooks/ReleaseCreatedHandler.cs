using AutoGit.ReleaseNotes.Interfaces;
using AutoGit.WebHooks.Context;
using AutoGit.WebHooks.Interfaces;
using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGit.ReleaseNotes.Hooks
{
    public class ReleaseCreatedHandler : IWebHookHandler
    {
        private readonly IEnumerable<IDocumentUpdater> _documentUpdaters;
        private readonly ICommitFinder _commitFinder;

        public string EventName { get; } = "release";
        public string Action { get; } = "created";
        public bool IncludeBotEvents { get; } = false;
        
        public ReleaseCreatedHandler(IEnumerable<IDocumentUpdater> documentUpdaters, ICommitFinder commitFinder)
        {
            _documentUpdaters = documentUpdaters;
            _commitFinder = commitFinder;
        }

        public async Task Handle(EventContext eventContext)
        {
            var payload = eventContext.WebHookEvent.GetPayload<ReleaseEventPayload>();

            var repoId = payload.Repository.Id;
            var releaseId = payload.Release.Id;

            var commits = await _commitFinder.GetCommits(repoId, releaseId);

            foreach (var documentUpdater in _documentUpdaters)
            {
                await documentUpdater.Update(payload.Repository, payload.Release, commits);
            }
        }
    }
}