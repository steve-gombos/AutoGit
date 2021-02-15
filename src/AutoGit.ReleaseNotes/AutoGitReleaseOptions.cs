namespace AutoGit.ReleaseNotes
{
    public class AutoGitReleaseOptions
    {
        public bool ManageReleaseNotes { get; set; } = true;
        public bool ManageChangeLog { get; set; } = false;
        public string ChangeLogFileName { get; set; } = "CHANGELOG.md";
        public string VersionTagPrefix { get; set; }
    }
}