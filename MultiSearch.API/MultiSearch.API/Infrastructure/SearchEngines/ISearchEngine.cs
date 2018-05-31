using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiSearch.API.Models;

namespace MultiSearch.API.Infrastructure.SearchEngines
{
    public interface ISearchEngine
    {
        Task<IEnumerable<SearchResult>> SearchAsync(string query);
    }
}
