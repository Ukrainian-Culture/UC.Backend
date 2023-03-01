using Microsoft.Extensions.Caching.Memory;

namespace Ukranian_Culture.Backend.Services;

public class CachingHelperService
{
    private readonly string _cacheKey;
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions _cachingOptions
        = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));

    public CachingHelperService(string cacheKey, IMemoryCache memoryCache)
    {
        _cacheKey = cacheKey;
        _memoryCache = memoryCache;
    }

    public async Task<T?> GetCachedResultAsync<T>
        (string cacheItemId, Func<Task<T?>> getUncachedResultAsync) where T : class
    {
        string key = $"{_cacheKey}-{cacheItemId}";
        return (await _memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetOptions(_cachingOptions);
            return getUncachedResultAsync();
        }))!;
    }
}