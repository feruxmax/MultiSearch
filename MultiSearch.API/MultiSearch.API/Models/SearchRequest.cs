using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearch.API.Models
{
    public class SearchRequest
    {
        public int Id { get; protected set; }
        public string Query { get; protected set; }
        public DateTimeOffset Date { get; protected set; }

        public virtual List<SearchResult> Results { get; protected set; }

        protected SearchRequest() { }
        public SearchRequest(string query, List<SearchResult> results)
        {
            Query = query;
            Results = results;
        }
    }
}
