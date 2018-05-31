using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearch.API.Infrastructure.SearchEngines.DuckDuckGoApi
{
    public class DuckDuckGoResult
    {
        public List<QueryResult> RelatedTopics { get; set; }
    }

    public class QueryResult
    {
        public string FirstURL { get; set; }
        public string Text { get; set; }
    }
}
