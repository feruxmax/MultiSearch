using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiSearch.API.Models;

namespace MultiSearch.API.Infrastructure.Repositories
{
    public interface ISearchRequestRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task<SearchRequest> GetAsync(string searchQuery);
        SearchRequest Add(SearchRequest request);
    }
}
