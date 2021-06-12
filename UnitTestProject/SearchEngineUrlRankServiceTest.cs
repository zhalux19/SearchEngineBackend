using NSubstitute;
using SearchEngineParser.BusinessLayer;
using SearchEngineParser.DBLayer;
using SearchEngineParser.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestProject
{
    public class SearchEngineUrlRankServiceTest
    {
        private readonly ISearchEngineUrlRankService _searchEngineUrlRankService;
        private readonly ISearchEngineRepository _searchEngineRepo;
        private readonly IPageFetchService _pageFetchService;
        private readonly IPatternAnalyseService _patternAnalyseService;
        private readonly ICacheService _cacheService;

        public SearchEngineUrlRankServiceTest()
        {
            _searchEngineRepo = Substitute.For<ISearchEngineRepository>();
            _pageFetchService = Substitute.For<IPageFetchService>();
            _patternAnalyseService = Substitute.For<IPatternAnalyseService>();
            _cacheService = Substitute.For<ICacheService>();
            _searchEngineUrlRankService = new SearchEngineUrlRankService(_searchEngineRepo, _pageFetchService, _patternAnalyseService, _cacheService);
        }

        //Test all paths

        [Fact]
        public async void  SearchEngineUrlRankService_When_Cache_Available_Should_Not_Need_To_Mock_PageFetchService()
        {
            //Arrange
            var cachedData = new List<string>() {};
            _cacheService.ReadFromCache<IEnumerable<string>>(Arg.Any<string>())
                .Returns(cachedData);

            var regexServiceResult = new List<int>() { 1, 2 };
            _patternAnalyseService.LoopLinksForRanks(Arg.Any<IEnumerable<string>>(), Arg.Any<string>())
                .Returns(regexServiceResult);

            //Act
            var result = await _searchEngineUrlRankService.FindUrlRankFromSearchEngine(1, "http://abc.com", "some keyword");

            //Assert
            Assert.Equal(regexServiceResult, result);
        }

        [Fact]
        public async void SearchEngineUrlRankService_When_Cache_Not_Available_Should_Mock_PageFetchService()
        {
            //Arrange
            var cachedData = new List<string>() { };
            _cacheService.ReadFromCache<IEnumerable<string>>(Arg.Any<string>()).Returns(x => null);

            var searchEngine = new SearchEngine() { Id = 1, Name = "Google", RegexPattern = "", UrlPattern = "urlPattern{0}" };
            _searchEngineRepo.GetSearchEngineById(Arg.Any<int>()).Returns(searchEngine);

            _pageFetchService.FetchPageContentByUrl(Arg.Any<string>()).Returns(Task.FromResult("page content"));
            _patternAnalyseService.GetSearchResultLinks(Arg.Any<string>(), Arg.Any<string>()).Returns(new List<string>());
            _cacheService.WriteToCache(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<TimeSpan>()).Returns(true);

            var regexServiceResult = new List<int>() { 1, 2 };
            _patternAnalyseService.LoopLinksForRanks(Arg.Any<IEnumerable<string>>(), Arg.Any<string>())
                .Returns(regexServiceResult);

            //Act
            var result = await _searchEngineUrlRankService.FindUrlRankFromSearchEngine(1, "http://abc.com", "some keyword");

            //Assert
            Assert.Equal(regexServiceResult, result);
        }
    }
}
