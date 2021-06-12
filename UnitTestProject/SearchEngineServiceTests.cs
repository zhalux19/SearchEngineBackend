using NSubstitute;
using SearchEngineParser.BusinessLayer;
using SearchEngineParser.DBLayer;
using SearchEngineParser.Dtos;
using SearchEngineParser.EntityModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTestProject
{
    public class SearchEngineServiceTests
    {
        private readonly ISearchEngineService _searchEngineService;
        private readonly ISearchEngineRepository _searchEngineRepository;

        public SearchEngineServiceTests()
        {
            _searchEngineRepository = Substitute.For<ISearchEngineRepository>();
            _searchEngineService = new SearchEngineService(_searchEngineRepository);
        }

        [Fact]
        public void GetSearchEngineById_Should_Return_SearchEngine()
        {

            var google = new SearchEngine() { Id = 1, Name = "Google", UrlPattern = "https://www.google.com/search?q={0}&num=100", RegexPattern = "(?<=kCrYT\"><a href=\")(.*?)(?=\")" };
            var yahoo = new SearchEngine() { Id = 2, Name = "Yahoo", UrlPattern = "https://au.search.yahoo.com/search?p={0}&n=100", RegexPattern = "(?<=kCrYT\"><a href=\")(.*?)(?=\")" };
            var allSearchEngines = new List<SearchEngine>() { google, yahoo };

            _searchEngineRepository.GetAllSearchEngines().Returns(allSearchEngines);

            var googleExpected = new SearchEngineOption() { Id = 1, Name = "Google" };
            var yahooExpected = new SearchEngineOption() { Id = 2, Name = "Yahoo" };
            var expectedResult = new List<SearchEngineOption>() { googleExpected, yahooExpected };

            var result = _searchEngineService.GetAllSearchEnginOptions();

            Assert.Equal(expectedResult, result.ToList());
        }
    }
}
