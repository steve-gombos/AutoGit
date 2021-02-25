using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace AutoGit.Core.Caching
{
    public class InMemoryCacheProvider : ICacheProvider
    {
        private readonly MemoryCache _cache;

        public InMemoryCacheProvider(MemoryCacheOptions memoryCacheOptions = null)
        {
            _cache = new MemoryCache(memoryCacheOptions ?? new MemoryCacheOptions());
        }

        public Task Add(CacheKey key, CacheEntry entry)
        {
            _cache.Set(key, entry);
            return Task.CompletedTask;
        }

        public Task ClearAll()
        {
            throw new InvalidOperationException("You cannot clear the in-memory cache");
        }

        public Task<bool> Exists(CacheKey key)
        {
            var result = _cache.TryGetValue(key, out _);
            return Task.FromResult(result);
        }

        public Task<CacheEntry> Get(CacheKey key)
        {
            var result = _cache.Get<CacheEntry>(key);
            return Task.FromResult(result);
        }

        public Task Remove(CacheKey key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}