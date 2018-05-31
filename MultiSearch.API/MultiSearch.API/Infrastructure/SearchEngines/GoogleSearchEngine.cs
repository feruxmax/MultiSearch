using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Customsearch;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using MultiSearch.API.Models;

namespace MultiSearch.API.Infrastructure.SearchEngines
{
    public class GoogleSearchEngine : ISearchEngine
    {
        private readonly CustomsearchService service;

        public GoogleSearchEngine()
        {
            service = new CustomsearchService(new BaseClientService.Initializer
            {
                ApplicationName = "MultiSearch",
                ApiKey = ConfigurationManager.AppSettings["googleSearch.ApiKey"]
            });
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string query)
        {
            var listRequest = service.Cse.List(query);
            listRequest.Cx = ConfigurationManager.AppSettings["googleSearch.SearchEngineId"];

            var result = await listRequest.ExecuteAsync();

            return result.Items.Select(x => new SearchResult { Title = x.Title, Url = x.Link });
        }
    }
}
