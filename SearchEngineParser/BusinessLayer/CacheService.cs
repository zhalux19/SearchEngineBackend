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
        private readonly IKeywordCleaningService _keywordCleaningService;

        public CacheService(IMemoryCache memoryCache, IKeywordCleaningService keywordCleaningService)
        {
            _memoryCache = memoryCache;
            _keywordCleaningService = keywordCleaningService;
        }

        public T ReadFromCache<T>(string key)
        {
            var cleanedKey = _keywordCleaningService.PrepareKeywordForCacheKey(key);
            var result = _memoryCache.Get<T>(cleanedKey);
            return result;
        }

        public bool WriteToCache<T>(string key, T value, TimeSpan time)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            var cleanedKey = _keywordCleaningService.PrepareKeywordForCacheKey(key);

            _memoryCache.Set(cleanedKey, value, new MemoryCacheEntryOptions() { SlidingExpiration = time });

            return true;
        }


    }
}
