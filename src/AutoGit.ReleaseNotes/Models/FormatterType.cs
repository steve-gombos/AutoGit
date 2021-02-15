using System;

namespace AutoGit.ReleaseNotes.Models
{
    [Flags]
    public enum FormatterType
    {
        None = 0,
        Release = 1,
        ChangeLog = 2
    }
}