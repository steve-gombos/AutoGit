using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Octokit.Internal;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoGit.WebHooks.Models
{
    [ModelBinder(BinderType = typeof(WebHookEventBinder))]
    public class WebHookEvent
    {
        private readonly string _gitHubDelivery;
        private readonly string _hubSignature;
        private readonly string _payload;
        private readonly SimpleJsonSerializer _serializer;

        internal WebHookEvent(HttpContext httpContext, string payload)
        {
            _payload = payload;
            _serializer = new SimpleJsonSerializer();

            EventName = httpContext.Request.Headers[WebHookConstants.EventHeader];
            _gitHubDelivery = httpContext.Request.Headers[WebHookConstants.DeliveryHeader];
            _hubSignature = httpContext.Request.Headers[WebHookConstants.HubSignatureHeader];
        }

        public string EventName { get; }

        public long? InstallationId => GetInstallationId();

        public bool IsBot => GenericPayload.Sender.Type == AccountType.Bot;

        public GenericPayload GenericPayload => _serializer.Deserialize<GenericPayload>(_payload);

        public T GetPayload<T>()
        {
            return _serializer.Deserialize<T>(_payload);
        }

        public bool IsAuthenticated(string secret)
        {
            var key = Encoding.UTF8.GetBytes(secret);
            var msg = Encoding.UTF8.GetBytes(_payload);

            using var hmac = new HMACSHA1(key);

            var hashValue = hmac.ComputeHash(msg);
            var calcHash = "sha1=" + BitConverter.ToString(hashValue).Replace("-", "").ToLowerInvariant();
            return calcHash == _hubSignature;
        }

        private long? GetInstallationId()
        {
            if (string.IsNullOrWhiteSpace(_payload))
                return null;

            var payload = _serializer.Deserialize<GenericPayload>(_payload);

            return payload?.Installation?.Id;
        }
    }
}