using Microsoft.AspNetCore.Mvc;
using SearchEngineParser.BusinessLayer;
using System.Threading.Tasks;

namespace SearchEngineParser.Controllers
{
    [ApiController]
    [Route("keywordrank")]
    public class KeywordRankController : Controller
    {
        private readonly ISearchEngineService _searchEngineService;
        private readonly ISearchEngineUrlRankService _searchEngineUrlRankService;

        public KeywordRankController(ISearchEngineService searchEngineService,  ISearchEngineUrlRankService searchEngineUrlRankService)
        {
            _searchEngineService = searchEngineService;
            _searchEngineUrlRankService = searchEngineUrlRankService;
        }

        [HttpGet("{searchEngineId}")]
        public async Task<IActionResult> CountKeyWord(int searchEngineId, string keyword, string targetUrl)
        {
            if(string.IsNullOrWhiteSpace(keyword) || string.IsNullOrWhiteSpace(targetUrl) )
            {
                return BadRequest("Request not valid");
            }

            if (!_searchEngineService.ValidateSearchEngineId(searchEngineId))
            {
                return NotFound("Invalid search engine");
            }

            var result = await _searchEngineUrlRankService.FindUrlRankFromSearchEngine(searchEngineId, targetUrl, keyword);

            return Ok(result);
        }
    }
}
