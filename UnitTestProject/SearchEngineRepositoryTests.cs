using SearchEngineParser.DBLayer;
using Xunit;

namespace UnitTestProject
{
    public class SearchEngineRepositoryTests
    {
        private readonly ISearchEngineRepository _searchEngineRepository;
        public SearchEngineRepositoryTests()
        {
            _searchEngineRepository = new SearchEngineRepository();
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        public void CheckSearchEngineExists_Should_Return_Expected_Results(int searchEngineId, bool expectedResult)
        {
            var result = _searchEngineRepository.CheckSearchEngineExists(searchEngineId);
            Assert.Equal(expectedResult, result);
        }
    }
}
