using Microsoft.Extensions.Logging;
using Octokit;
using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoGit.Core.Resiliency
{
    public class ResilientPolicies
    {
        private readonly ILogger _logger;

        public ResilientPolicies(ILogger logger = null)
        {
            _logger = logger;
        }

        public AsyncPolicy DefaultHttpRequestExceptionPolicy => Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryForeverAsync(
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timespan) =>
                {
                    _logger?.LogInformation("A {exception} has occurred. Next try will happen in {time} seconds",
                        "HttpRequestException", timespan.TotalSeconds);
                });

        public AsyncPolicy DefaultTimeoutExceptionPolicy => Policy
            .Handle<TaskCanceledException>(ex => !ex.CancellationToken.IsCancellationRequested)
            .WaitAndRetryForeverAsync(
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timespan) =>
                {
                    _logger?.LogInformation("A {exception} has occurred. Next try will happen in {time} seconds",
                        "TaskCanceledException", timespan.TotalSeconds);
                });

        public AsyncPolicy DefaultRateLimitExceededExceptionPolicy => Policy
            .Handle<RateLimitExceededException>()
            .RetryAsync(
                1,
                async (exception, retryCount) =>
                {
                    var e = exception as RateLimitExceededException;

                    var sleepMilliseconds = (int) (e.Reset.ToLocalTime() - DateTime.Now)
                        .TotalMilliseconds + 5 * 1000; // wait for more 5 seconds to make sure there'll be no problem.

                    _logger?.LogInformation("A {exception} has occurred. Next try will happen in {time} seconds",
                        "RateLimitExceededException", sleepMilliseconds / 1000);

                    await Task.Delay(sleepMilliseconds).ConfigureAwait(false);
                });

        public AsyncPolicy DefaultAbuseExceptionExceptionPolicy => Policy
            .Handle<AbuseException>()
            .RetryAsync(
                1,
                async (exception, retryCount) =>
                {
                    var e = exception as AbuseException;

                    var sleepMilliseconds = (int) TimeSpan.FromSeconds(e.RetryAfterSeconds.GetValueOrDefault(30))
                        .TotalMilliseconds;

                    _logger?.LogInformation("A {exception} has occurred. Next try will happen in {time} seconds",
                        "AbuseException", sleepMilliseconds / 1000);

                    await Task.Delay(sleepMilliseconds).ConfigureAwait(false);
                });

        public AsyncPolicy DefaultOctokitApiExceptionExceptionPolicy => Policy
            .Handle<ApiException>()
            .WaitAndRetryAsync(
                3,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, retryCount) =>
                {
                    _logger?.LogInformation("A {exception} has occurred with {message}. Will try again shortly.",
                        "ApiException", exception.Message);

                    return Task.CompletedTask;
                });

        public IAsyncPolicy[] DefaultResilientPolicies => new IAsyncPolicy[]
        {
            DefaultHttpRequestExceptionPolicy,
            DefaultRateLimitExceededExceptionPolicy,
            DefaultAbuseExceptionExceptionPolicy,
            DefaultTimeoutExceptionPolicy,
            DefaultOctokitApiExceptionExceptionPolicy
        };
    }
}