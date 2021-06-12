
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchEngineParser.BusinessLayer
{
    public interface IPageFetchService
    {
        Task<string> FetchPageContentByUrl(string url);
    }

    public class PageFetchService : IPageFetchService
    {
        public async Task<string> FetchPageContentByUrl(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
