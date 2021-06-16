using SearchEngineParser.BusinessLayer;
using System.Collections.Generic;
using Xunit;

namespace UnitTestProject
{
    public class PatternAnalyseServiceTests
    {
        private readonly IPatternAnalyseService _patternAnalyseService;

        public PatternAnalyseServiceTests(){
            _patternAnalyseService = new PatternAnalyseService();
        }

        [Fact]
        public void GetSearchResultsLinks_Should_Return_Expected()
        {
            //Arrange
            var pageContent = "wefdsdfab123cwefdwfdsab456csdfsdf";
            var regexPattern = "(?<=ab)(.*?)(?=c)";
            var expectedResult = new List<string>() { "123", "456" };

            //Act
            var result = _patternAnalyseService.GetSearchResultLinks(pageContent, regexPattern);

            //Assert
            Assert.Equal(expectedResult, result);
        }

    }
}
