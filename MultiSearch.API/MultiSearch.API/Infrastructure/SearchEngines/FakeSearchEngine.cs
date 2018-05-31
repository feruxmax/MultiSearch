using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiSearch.API.Models;

namespace MultiSearch.API.Infrastructure.SearchEngines
{
    public class FakeSearchEngine : ISearchEngine
    {
        public async Task<IEnumerable<SearchResult>> SearchAsync(string query)
        {
            return await Task.FromResult(new List<SearchResult>
            {
                new SearchResult("Google", "http://www.google.com"),
                new SearchResult("Yandex", "http://yandex.ru"),
                new SearchResult("Bing", "http://bing.com"),
            });
        }
    }
}
