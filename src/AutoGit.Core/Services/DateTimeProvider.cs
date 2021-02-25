using AutoGit.Core.Interfaces;
using System;

namespace AutoGit.Core.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}