using System;

namespace AutoGit.ReleaseNotes.Models
{
    [Flags]
    public enum FormatterTypes
    {
        None = 0,
        Release = 1,
        ChangeLog = 2
    }
}