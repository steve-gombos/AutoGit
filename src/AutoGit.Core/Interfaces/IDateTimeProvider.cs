using System;

namespace AutoGit.Core.Interfaces
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}