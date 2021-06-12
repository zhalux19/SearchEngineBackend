using SearchEngineParser.BusinessLayer;
using System.Collections.Generic;
using Xunit;

namespace UnitTestProject
{
    public class PatternAnalyseServiceTests
    {
        private readonly IPatternAnalyseService patternAnalyseService;

        public PatternAnalyseServiceTests(){
            patternAnalyseService = new PatternAnalyseService();
        }

        [Fact]
        public void GetSearchResultsLinks_Should_Return_Expected()
        {
            //Arrange
            var pageContent = "wefdsdfab123cwefdwfdsab456csdfsdf";
            var regexPattern = "(?<=ab)(.*?)(?=c)";
            var expectedResult = new List<string>() { "123", "456" };

            //Act
            var result = patternAnalyseService.GetSearchResultLinks(pageContent, regexPattern);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void LoopLinkForRanks_Should_Return_Expected()
        {
            //Arrange
            var pageLinks = new List<string>() { "https://github.com", "https://google.com", "https://github.com", "https://github.com" };
            var targetUrl = "https://github.com";

            //Act
            var result = patternAnalyseService.LoopLinksForRanks(pageLinks, targetUrl);

            //Assert
            var expectedResult = new List<int>() { 1, 3, 4 };
            Assert.Equal(expectedResult, result);
        }

    }
}
