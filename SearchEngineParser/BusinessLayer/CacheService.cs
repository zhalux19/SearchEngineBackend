using Microsoft.Extensions.Caching.Memory;
using System;

namespace SearchEngineParser.BusinessLayer
{
    public interface ICacheService
    {
        T ReadFromCache<T>(string key);
        bool WriteToCache<T>(string key, T value, TimeSpan time);
    }

    public class CacheService: ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T ReadFromCache<T>(string key)
        {
            CleanCacheKey(ref key);
            var result = _memoryCache.Get<T>(key);
            return result;
        }

        public bool WriteToCache<T>(string key, T value, TimeSpan time)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            CleanCacheKey(ref key);

            _memoryCache.Set(key, value, new MemoryCacheEntryOptions() { SlidingExpiration = time });

            return true;
        }

        private void CleanCacheKey(ref string key) => key = key.Replace("+", "_");
    }
}
