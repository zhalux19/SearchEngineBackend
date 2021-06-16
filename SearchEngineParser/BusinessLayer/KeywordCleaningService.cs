namespace SearchEngineParser.BusinessLayer
{
    public interface IKeywordCleaningService
    {
        string PrepareKeywordForSearchEngineUrl(string keyword);
        string PrepareKeywordForCacheKey(string keyword);
    }

    public class KeywordCleaningService : IKeywordCleaningService
    {
        public string PrepareKeywordForSearchEngineUrl(string keyword)
        {
            return keyword.Trim().Replace(" ", "+"); 
        }

        public string PrepareKeywordForCacheKey(string keyword)
        {
            return keyword.Trim().Replace(" ", "_");
        }
    }
}
