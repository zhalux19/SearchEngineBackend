using SearchEngineParser.BusinessLayer;
using System.Collections.Generic;
using Xunit;

namespace UnitTestProject
{
    public class RankServiceTests
    {
        private readonly IRankService _rankService;

        public RankServiceTests(){
            _rankService = new RankService();
        }

        [Fact]
        public void LoopLinkForRanks_Should_Return_Expected()
        {
            //Arrange
            var pageLinks = new List<string>() { "https://github.com", "https://google.com", "https://github.com", "https://github.com" };
            var targetUrl = "https://github.com";

            //Act
            var result = _rankService.LoopLinksForTargetRanks(pageLinks, targetUrl);

            //Assert
            var expectedResult = new List<int>() { 1, 3, 4 };
            Assert.Equal(expectedResult, result);
        }

    }
}
