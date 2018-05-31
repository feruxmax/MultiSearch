using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MultiSearch.API.Infrastructure.SearchEngines
{
    public class SearchEnginesFactory : ISearchEnginesFactory
    {
        public IEnumerable<ISearchEngine> Create()
        {
            var result = new List<ISearchEngine>
            {
                new DuckDuckGoSearchEngine(),
                new GoogleSearchEngine(),
            };

            if (Boolean.Parse(ConfigurationManager.AppSettings["yandexSearch.Enabled"]) == true)
            {
                result.Add(new YandexSearchEngine());
            }

            return result;
        }
    }
}