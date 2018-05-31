using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearch.API.Infrastructure.Services
{
    using Models;

    public interface ISearchService
    {
        Task<IEnumerable<SearchResult>> Search(string query);
    }
}
