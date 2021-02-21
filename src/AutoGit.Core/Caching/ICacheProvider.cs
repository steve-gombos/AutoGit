using System.Threading.Tasks;

namespace AutoGit.Core.Caching
{
    public interface ICacheProvider
    {
        Task<CacheEntry> Get(CacheKey key);

        Task Add(CacheKey key, CacheEntry entry);

        Task Remove(CacheKey key);

        Task<bool> Exists(CacheKey key);

        Task ClearAll();
    }
}