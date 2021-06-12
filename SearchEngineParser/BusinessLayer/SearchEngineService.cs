using SearchEngineParser.DBLayer;
using SearchEngineParser.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngineParser.BusinessLayer
{
    public interface ISearchEngineService
    {
        IEnumerable<SearchEngineOption> GetAllSearchEnginOptions();
        bool ValidateSearchEngineId(int id);
    }

    public class SearchEngineService : ISearchEngineService
    {
        private readonly ISearchEngineRepository _searchEngineRepository;

        public SearchEngineService(ISearchEngineRepository searchEngineRepository)
        {
            this._searchEngineRepository = searchEngineRepository;
        }

        public IEnumerable<SearchEngineOption> GetAllSearchEnginOptions()
        {
            var allSearchEngines = _searchEngineRepository.GetAllSearchEngines();
            return allSearchEngines.Select(x => new SearchEngineOption() { Id = x.Id, Name = x.Name });
        }

        public bool ValidateSearchEngineId(int id)
        {
            return _searchEngineRepository.CheckSearchEngineExists(id);
        }
    }
}
