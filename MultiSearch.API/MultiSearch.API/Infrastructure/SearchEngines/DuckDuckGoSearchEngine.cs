using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MultiSearch.API.Models;
using MultiSearch.API.Infrastructure.SearchEngines.DuckDuckGoApi;

namespace MultiSearch.API.Infrastructure.SearchEngines
{
    public class DuckDuckGoSearchEngine : ISearchEngine
    {
        private readonly int ResultsCount = 10;
        private readonly int MaxTextLenght = 80;
        private readonly DuckDuckGoServiceApi _api = new DuckDuckGoServiceApi();

        public async Task<IEnumerable<SearchResult>> SearchAsync(string query)
        {
            var result = await _api.Search(query);

            return result?.RelatedTopics
                ?.Where(x => x.Text != null && x.FirstURL != null)
                ?.Take(ResultsCount)
                ?.Select(x => new SearchResult
                {
                    Title = $"{x.Text.Substring(0, Math.Min(x.Text.Length, MaxTextLenght))}...",
                    Url = x.FirstURL,
                })
                ?.ToList();
        }
    }
}
