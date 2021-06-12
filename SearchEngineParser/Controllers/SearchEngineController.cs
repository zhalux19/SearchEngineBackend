using Microsoft.AspNetCore.Mvc;
using SearchEngineParser.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineParser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchEngineController : Controller
    {
        private ISearchEngineService _searchEngineService;

        public SearchEngineController(ISearchEngineService searchEngineService)
        {
            this._searchEngineService = searchEngineService;
        }
        public IActionResult GetAllSearchEngineOptions()
        {
            return Ok(_searchEngineService.GetAllSearchEnginOptions());
        }
    }
}
