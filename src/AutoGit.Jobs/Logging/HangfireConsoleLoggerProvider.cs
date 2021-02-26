using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace AutoGit.Jobs.Logging
{
    public class HangfireConsoleLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();

        public void Dispose() => _loggers.Clear();

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new HangfireConsoleLogger());
    }
}