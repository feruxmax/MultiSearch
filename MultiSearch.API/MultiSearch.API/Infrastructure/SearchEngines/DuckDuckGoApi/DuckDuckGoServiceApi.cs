using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace MultiSearch.API.Infrastructure.SearchEngines.DuckDuckGoApi
{
    class DuckDuckGoServiceApi
    {
        private readonly string _baseUrl = "https://api.duckduckgo.com";

        public async Task<DuckDuckGoResult> Search(string query)
        {
            var restClient = new RestClient(_baseUrl);
            var request = new RestRequest();
            request.AddQueryParameter("q", query);
            request.AddQueryParameter("format", "json");
            request.AddQueryParameter("no_html", "1");
            request.AddQueryParameter("pretty", "1");
            request.AddQueryParameter("skip_disambig", "1");

            var result = await restClient.ExecuteTaskAsync(request);
            var resultObj = JsonConvert.DeserializeObject<DuckDuckGoResult>(result.Content);

            return resultObj;
        }
    }
}
