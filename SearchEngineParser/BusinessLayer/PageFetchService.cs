
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchEngineParser.BusinessLayer
{
    public interface IPageFetchService
    {
        Task<string> FetchPageContentFromSearchEngineForKeyword(string urlPattern, string keyword);
    }

    public class PageFetchService : IPageFetchService
    {
        private readonly IKeywordCleaningService _keywordCleaningService;

        public PageFetchService(IKeywordCleaningService keywordCleaningService)
        {
            _keywordCleaningService = keywordCleaningService;
        }

        public async Task<string> FetchPageContentFromSearchEngineForKeyword(string urlPattern, string keyword)
        {
            var cleanedKeyword = _keywordCleaningService.PrepareKeywordForSearchEngineUrl(keyword);
            var url = string.Format(urlPattern, cleanedKeyword);
            return await FetchPageContentByUrl(url);
        }

        private async Task<string> FetchPageContentByUrl(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

    }
}
