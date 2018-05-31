using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearch.API.Models
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public int RequestId { get; set; }
        public virtual SearchRequest Request { get; set; }

        public SearchResult() { }
        public SearchResult(string title, string url)
        {
            Title = title;
            Url = url;
        }
    }
}
