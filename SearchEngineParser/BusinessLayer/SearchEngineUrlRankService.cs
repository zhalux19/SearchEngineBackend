using SearchEngineParser.DBLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngineParser.BusinessLayer
{
    public interface ISearchEngineUrlRankService
    {
        Task <IEnumerable<int>> FindUrlRankFromSearchEngine(int searchEngineId, string targetUrl, string keyword);
    }

    public class SearchEngineUrlRankService : ISearchEngineUrlRankService
    {
        private readonly ISearchEngineRepository _searchEngineRepo;
        private readonly IPageFetchService _pageFetchService;
        private readonly IPatternAnalyseService _patternAnalyseService;
        private readonly ICacheService _cacheService;
        private readonly IKeywordCleaningService _keywordCleaningService;
        private readonly IRankService _rankService;

        public SearchEngineUrlRankService(ISearchEngineRepository searchEngineRepo, IPageFetchService pageFetchService, IPatternAnalyseService patternAnalyseService, ICacheService cacheService, IKeywordCleaningService keywordCleaningService, IRankService rankService)
        {
            _searchEngineRepo = searchEngineRepo;
            _pageFetchService = pageFetchService;
            _patternAnalyseService = patternAnalyseService;
            _cacheService = cacheService;
            _keywordCleaningService = keywordCleaningService;
            _rankService = rankService;
        }

        public async Task<IEnumerable<int>> FindUrlRankFromSearchEngine(int searchEngineId, string targetUrl, string keyword)
        {
            var cacheKey = $"{keyword}{searchEngineId}";
            var searchResultLinks = _cacheService.ReadFromCache<IEnumerable<string>>(cacheKey);

            if(searchResultLinks == null)
            {
                var searchEngine = _searchEngineRepo.GetSearchEngineById(searchEngineId);
                var pageContent = await _pageFetchService.FetchPageContentFromSearchEngineForKeyword(searchEngine.UrlPattern, keyword);
                searchResultLinks = _patternAnalyseService.GetSearchResultLinks(pageContent, searchEngine.RegexPattern);

                _cacheService.WriteToCache(cacheKey, searchResultLinks, TimeSpan.FromMinutes(60));
            }

            return _rankService.LoopLinksForTargetRanks(searchResultLinks, targetUrl);
        }
    }
}
