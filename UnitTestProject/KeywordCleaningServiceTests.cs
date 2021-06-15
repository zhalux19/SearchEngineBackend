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
            //Arrange
            var keyword = "Hello world";

            //Act
            _keywordCleaningService.PrepareKeywordForSearchEngineUrl(ref keyword);

            //Assert
            Assert.Equal("Hello+world", keyword);
        }
    }
}
