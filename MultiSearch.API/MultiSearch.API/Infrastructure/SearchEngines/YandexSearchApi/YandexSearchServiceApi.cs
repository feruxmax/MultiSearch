using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MultiSearch.API.Infrastructure.SearchEngines.YandexSearchApi
{
    class YandexSearchServiceApi
    {
        private readonly string _baseUrl;
        private readonly string _searchResourse;
        private readonly string _user;
        private readonly string _key;

        public YandexSearchServiceApi()
        {
            _baseUrl = "https://yandex.com";
            _searchResourse = "search/xml";
            _user = ConfigurationManager.AppSettings["yandexSearch.User"];
            _key = ConfigurationManager.AppSettings["yandexSearch.Key"];
        }

        public async Task<YandexSearchResult> Search(string query)
        {
            var restClient = new RestClient(_baseUrl);
            var request = new RestRequest(_searchResourse);
            request.AddQueryParameter("user", _user);
            request.AddQueryParameter("key", _key);
            request.AddQueryParameter("query", query);
            request.AddQueryParameter("maxpassages", "1"); // One anatation per search result

            var result = await restClient.ExecuteTaskAsync<YandexSearchResult>(request);

            return result.Data;
        }
    }
}
