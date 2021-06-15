namespace SearchEngineParser.BusinessLayer
{
    public interface IKeywordCleaningService
    {
        void PrepareKeywordForSearchEngineUrl(ref string keyword);
    }

    public class KeywordCleaningService:IKeywordCleaningService
    {
        public void PrepareKeywordForSearchEngineUrl(ref string keyword) => keyword = keyword.Replace(" ", "+");
    }
}
