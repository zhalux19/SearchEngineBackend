﻿using NSubstitute;
using SearchEngineParser.BusinessLayer;
using SearchEngineParser.DBLayer;
using SearchEngineParser.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestProject
{
    public class SearchEngineUrlRankServiceTests
    {
        private readonly ISearchEngineUrlRankService _searchEngineUrlRankService;
        private readonly ISearchEngineRepository _searchEngineRepo;
        private readonly IPageFetchService _pageFetchService;
        private readonly IPatternAnalyseService _patternAnalyseService;
        private readonly ICacheService _cacheService;
        private readonly IKeywordCleaningService _keywordCleaningService;
        private readonly IRankService _rankService;


        public SearchEngineUrlRankServiceTests()
        {
            _searchEngineRepo = Substitute.For<ISearchEngineRepository>();
            _pageFetchService = Substitute.For<IPageFetchService>();
            _patternAnalyseService = Substitute.For<IPatternAnalyseService>();
            _cacheService = Substitute.For<ICacheService>();
            _keywordCleaningService = Substitute.For<IKeywordCleaningService>();
            _rankService = Substitute.For<IRankService>();
            _searchEngineUrlRankService = new SearchEngineUrlRankService(_searchEngineRepo, _pageFetchService, _patternAnalyseService, _cacheService, _keywordCleaningService, _rankService);
        }

        //Test all paths

        [Fact]
        public async void SearchEngineUrlRankService_Should_Not_Need_To_Mock_PageFetchService_When_Cache_Available()
        {
            //Arrange
            var cachedData = new List<string>() {};
            _cacheService.ReadFromCache<IEnumerable<string>>(Arg.Any<string>())
                .Returns(cachedData);

            var regexServiceResult = new List<int>() { 1, 2 };
            _rankService.LoopLinksForTargetRanks(Arg.Any<IEnumerable<string>>(), Arg.Any<string>())
                .Returns(regexServiceResult);

            //Act
            var result = await _searchEngineUrlRankService.FindUrlRankFromSearchEngine(1, "http://abc.com", "some keyword");

            //Assert
            Assert.Equal(regexServiceResult, result);
        }

        [Fact]
        public async void SearchEngineUrlRankService_Should_Mock_PageFetchService_When_Cache_Not_Available()
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
            _rankService.LoopLinksForTargetRanks(Arg.Any<IEnumerable<string>>(), Arg.Any<string>())
                .Returns(regexServiceResult);

            //Act
            var result = await _searchEngineUrlRankService.FindUrlRankFromSearchEngine(1, "http://abc.com", "some keyword");

            //Assert
            Assert.Equal(regexServiceResult, result);
        }
    }
}
