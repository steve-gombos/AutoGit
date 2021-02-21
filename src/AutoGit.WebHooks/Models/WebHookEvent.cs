using AutoGit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoGit.WebHooks.Models
{
    [ModelBinder(BinderType = typeof(WebHookEventBinder))]
    public class WebHookEvent
    {
        private readonly string _hubSignature;
        private readonly string _payload;
        private readonly ISerializer _serializer;

        public WebHookEvent(string eventName, string gitHubDelivery, string hubSignature, string payload,
            ISerializer serializer)
        {
            EventName = eventName;
            GitHubDelivery = gitHubDelivery;
            _hubSignature = hubSignature;
            _payload = payload;
            _serializer = serializer;
        }

        public string EventName { get; }

        public string GitHubDelivery { get; }

        public bool IsBot => GenericPayload.Sender.Type == AccountType.Bot;

        public GenericPayload GenericPayload => _serializer.Deserialize<GenericPayload>(_payload);

        public T GetPayload<T>() where T : ActivityPayload
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
    }
}