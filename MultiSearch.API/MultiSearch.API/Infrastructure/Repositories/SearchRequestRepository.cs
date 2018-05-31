using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using MultiSearch.API.Models;

namespace MultiSearch.API.Infrastructure.Repositories
{
    public class SearchRequestRepository : ISearchRequestRepository
    {
        private readonly MultiSearchContext _dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        public SearchRequestRepository(MultiSearchContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SearchRequest> GetAsync(string searchQuery)
        {
            return await _dbContext.Request
                .Where(x => x.Query == searchQuery)
                .Include(x => x.Results)
                .FirstOrDefaultAsync();
        }

        public SearchRequest Add(SearchRequest request)
        {
            return _dbContext.Request.Add(request);
        }
    }
}