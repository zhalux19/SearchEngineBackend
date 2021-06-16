﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchEngineParser.BusinessLayer
{
    public interface IRankService
    {
        IEnumerable<int> LoopLinksForTargetRanks(IEnumerable<string> searchEnginResultLinks, string targetUrl);
    }
    public class RankService : IRankService
    {
        public IEnumerable<int> LoopLinksForTargetRanks(IEnumerable<string> searchEnginResultLinks, string targetUrl)
        {
            var rank = 0;
            var result = new List<int>();
            searchEnginResultLinks.ToList().ForEach(x => { rank++; if (Regex.IsMatch(x, targetUrl)) { result.Add(rank); } });
            if (result.Count == 0)
            {
                result.Add(0);
            }
            return result;
        }
    }
}
