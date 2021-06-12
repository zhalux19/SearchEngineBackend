using SearchEngineParser.EntityModels;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngineParser.DBLayer
{
    public interface ISearchEngineRepository
    {
        IEnumerable<SearchEngine> GetAllSearchEngines();
        SearchEngine GetSearchEngineById(int id);
        bool CheckSearchEngineExists(int id);

    }
    public class SearchEngineRepository:ISearchEngineRepository
    {
        private IEnumerable<SearchEngine> _allSearchEngines;

        public SearchEngineRepository()
        {
            var google = new SearchEngine() { Id = 1, Name = "Google", UrlPattern = "https://www.google.com/search?q={0}&num=100", RegexPattern = "(?<=kCrYT\"><a href=\"/url\\?q=)(.*?)(?=\")" };
            var yahoo = new SearchEngine() { Id = 2, Name = "Yahoo", UrlPattern = "https://au.search.yahoo.com/search?p={0}&n=100", RegexPattern = "(?<=hu\" href=\")(.*?)(?=\")" };
            _allSearchEngines = new List<SearchEngine>() { google, yahoo };
        }

        public IEnumerable<SearchEngine> GetAllSearchEngines()
        {
            return _allSearchEngines;
        }

        public SearchEngine GetSearchEngineById(int id)
        {
            return _allSearchEngines.FirstOrDefault(x => x.Id == id);
        }

        public bool CheckSearchEngineExists(int id)
        {
            return _allSearchEngines.Any(x => x.Id == id);
        }
    }
}
