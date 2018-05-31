using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using MultiSearch.API.Models;
using MultiSearch.API.Infrastructure.SearchEngines.YandexSearchApi;

namespace MultiSearch.API.Infrastructure.SearchEngines
{
    public class YandexSearchEngine : ISearchEngine
    {
        private readonly YandexSearchServiceApi _api;

        public YandexSearchEngine()
        {
            _api = new YandexSearchServiceApi();
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string query)
        {
            var result = await _api.Search(query);

            return result.Response.Results.Grouping.Groups
                .Select(x => new SearchResult(x.Doc.Title, x.Doc.Url));
        }
    }
}
