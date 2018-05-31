using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiSearch.API.ViewModels
{
    public class SearchResultResponse : ApiBaseResponse
    {
        public List<SearchResultItem> Items { get; set; }
    }
}