using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace MultiSearch.API.Controllers
{
    using ViewModels;
    using Infrastructure.Services;
    using Infrastructure.Filters;

    [RoutePrefix("api/search")]
    public class SearchApiController : ApiController
    {
        private readonly ISearchService _searchService;

        public SearchApiController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [ValidateModel]
        [HttpGet(), Route("")]
        public async Task<SearchResultResponse> Search([FromUri] SearchRequest data)
        {
            var result = await _searchService.Search(data.Query);

            return new SearchResultResponse
            {
                Items = result
                    .Select(x => new SearchResultItem { Title = x.Title, Url = x.Url })
                    .ToList()
            };
        }
    }
}
