using SearchEngineParser.BusinessLayer;
using Xunit;

namespace UnitTestProject
{
    public class KeywordCleaningServiceTests
    {
        private readonly IKeywordCleaningService _keywordCleaningService;

        public KeywordCleaningServiceTests()
        {
            _keywordCleaningService = new KeywordCleaningService();
        }

        [Fact]
        public void PrepareKeywordForSearchEngineUrl_Should_Replace_Space_With_Plus()
        {
            var keyword = "hello world";

            var result = _keywordCleaningService.PrepareKeywordForSearchEngineUrl(keyword);

            Assert.Equal("hello+world", result);
        }

        [Fact]
        public void PrepareKeywordForCacheKey_Should_Replace_Space_With_Plus()
        {
            var keyword = "hello world";

            var result = _keywordCleaningService.PrepareKeywordForCacheKey(keyword);

            Assert.Equal("hello_world", result);
        }
    }
}
