using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace MultiSearch.API.Infrastructure.Services
{
    using Models;
    using SearchEngines;
    using Repositories;

    public class FastestResultSearchService : ISearchService
    {
        private readonly IEnumerable<ISearchEngine> _searchEngines;
        private readonly ISearchRequestRepository _searchRequetRepository;
        public FastestResultSearchService(ISearchRequestRepository searchRequetRepository,
            ISearchEnginesFactory searchEnginesFactory)
        {
            _searchRequetRepository = searchRequetRepository;
            _searchEngines = searchEnginesFactory.Create();
        }

        public async Task<IEnumerable<SearchResult>> Search(string query)
        {
            IEnumerable<SearchResult> result;
            var cachedResult = await _searchRequetRepository.GetAsync(query);

            if (cachedResult != null)
            {
                result = cachedResult.Results;
            }
            else
            {
                var tasks = _searchEngines.Select(x => x.SearchAsync(query))
                    .ToArray();

                var completedTask = await Task.WhenAny(tasks);
                result = await completedTask;

                if (result.Count() != 0)
                {
                    var request = new SearchRequest(query, result.ToList());
                    _searchRequetRepository.Add(request);
                    await _searchRequetRepository.UnitOfWork.SaveChangesAsync();
                }
            }

            return result;
        }
    }
}