using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchEngineParser.BusinessLayer
{
    public interface IPatternAnalyseService
    {
        IEnumerable<string> GetSearchResultLinks(string pageContent, string searchEngineRegex);
        IEnumerable<int> LoopLinksForRanks(IEnumerable<string> searchEnginResultLinks, string targetUrl);
    }

    public class PatternAnalyseService : IPatternAnalyseService
    {
        public IEnumerable<string> GetSearchResultLinks(string pageContent, string searchEngineRegex)
        {
            var searchEnginResultLinks = new List<string>();
            Regex.Matches(pageContent, searchEngineRegex).ToList().ForEach(x => searchEnginResultLinks.Add(x.Value));
            return searchEnginResultLinks;
        }

        public IEnumerable<int> LoopLinksForRanks(IEnumerable<string> searchEnginResultLinks, string targetUrl)
        {
            var rank = 0;
            var result = new List<int>();
            searchEnginResultLinks.ToList().ForEach(x => { rank++; if (Regex.IsMatch(x, targetUrl)) { result.Add(rank); }});
            if(result.Count == 0)
            {
                result.Add(0);
            }
            return result;
        }
    }
}
