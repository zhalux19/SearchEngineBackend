using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchEngineParser.BusinessLayer
{
    public interface IPatternAnalyseService
    {
        IEnumerable<string> GetSearchResultLinks(string pageContent, string searchEngineRegex);
    }

    public class PatternAnalyseService : IPatternAnalyseService
    {
        public IEnumerable<string> GetSearchResultLinks(string pageContent, string searchEngineRegex)
        {
            var searchEnginResultLinks = new List<string>();
            Regex.Matches(pageContent, searchEngineRegex).ToList().ForEach(x => searchEnginResultLinks.Add(x.Value));
            return searchEnginResultLinks;
        }
    }
}
