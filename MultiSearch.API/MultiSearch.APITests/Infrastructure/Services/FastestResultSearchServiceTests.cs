using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiSearch.API.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace MultiSearch.API.Infrastructure.Services.Tests
{
    using Models;
    using Repositories;
    using SearchEngines;

    [TestClass()]
    public class FastestResultSearchServiceTests
    {
        private readonly ISearchRequestRepository _searchRequestRepository;

        public FastestResultSearchServiceTests()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var requests = new List<SearchRequest>();
            var repoMock = new Mock<ISearchRequestRepository>();
            repoMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                .Returns<string>(query => Task.FromResult(requests.Where(x => x.Query == query).FirstOrDefault()));
            repoMock.Setup(x => x.UnitOfWork)
                .Returns(() =>
                {
                    return unitOfWorkMock.Object;
                });
            repoMock.Setup(x => x.Add(It.IsAny<SearchRequest>()))
                .Callback<SearchRequest>(r =>
                {
                    requests.Add(r);
                });
            _searchRequestRepository = repoMock.Object;
        }

        [TestMethod()]
        public async Task SearchTest()
        {
            // Arrange
            var results = new List<(string Title, string Url)> { ("new_t1", "new_u1"), ("new_t2", "new_u2")};
            var engineMock = CreateSearchEngine(results, 0);

            var searchEnginesFactory = new Mock<ISearchEnginesFactory>();
            searchEnginesFactory.Setup(x => x.Create())
                .Returns(() => new List<ISearchEngine> { engineMock.Object });

            // Act
            var contorller = new FastestResultSearchService(
                    _searchRequestRepository,
                    searchEnginesFactory.Object);
            var result = await contorller.Search("query");

            // Assert
            CollectionAssert.AreEqual(results.Select(x => x.Title).ToList(), result.Select(x => x.Title).ToList());
            CollectionAssert.AreEqual(results.Select(x => x.Url).ToList(), result.Select(x => x.Url).ToList());
        }

        [TestMethod()]
        public async Task Can_Select_Fastest_First()
        {
            // Arrange
            var results1 = new List<(string Title, string Url)> { ("new_t1", "new_u1"), ("new_t2", "new_u2")};
            var engineMock1 = CreateSearchEngine(results1, 200);

            var results2 = new List<(string Title, string Url)> { ("new2_t1", "new2_u1"), ("new2_t2", "new2_u2")};
            var engineMock2 = CreateSearchEngine(results2, 300);

            var searchEnginesFactory = new Mock<ISearchEnginesFactory>();
            searchEnginesFactory.Setup(x => x.Create())
                .Returns(() => new List<ISearchEngine> { engineMock1.Object, engineMock2.Object });

            // Act
            var contorller = new FastestResultSearchService(
                    _searchRequestRepository,
                    searchEnginesFactory.Object);
            var result = await contorller.Search("query");

            // Assert
            CollectionAssert.AreEqual(results1.Select(x => x.Title).ToList(), result.Select(x => x.Title).ToList());
            CollectionAssert.AreEqual(results1.Select(x => x.Url).ToList(), result.Select(x => x.Url).ToList());
        }

        [TestMethod()]
        public async Task Can_Select_Fastest_Second()
        {
            // Arrange
            var results1 = new List<(string Title, string Url)> { ("new_t1", "new_u1"), ("new_t2", "new_u2")};
            var engineMock1 = CreateSearchEngine(results1, 300);

            var results2 = new List<(string Title, string Url)> { ("new2_t1", "new2_u1"), ("new2_t2", "new2_u2")};
            var engineMock2 = CreateSearchEngine(results2, 200);

            var searchEnginesFactory = new Mock<ISearchEnginesFactory>();
            searchEnginesFactory.Setup(x => x.Create())
                .Returns(() => new List<ISearchEngine> { engineMock1.Object, engineMock2.Object });

            // Act
            var contorller = new FastestResultSearchService(
                    _searchRequestRepository,
                    searchEnginesFactory.Object);
            var result = await contorller.Search("query");

            // Assert
            CollectionAssert.AreEqual(results2.Select(x => x.Title).ToList(), result.Select(x => x.Title).ToList());
            CollectionAssert.AreEqual(results2.Select(x => x.Url).ToList(), result.Select(x => x.Url).ToList());
        }

        [TestMethod()]
        public async Task Can_Save_To_Cache()
        {
            // Arrange
            var query = "query";
            var results = new List<(string Title, string Url)> { ("new_t1", "new_u1"), ("new_t2", "new_u2")};
            var engineMock = CreateSearchEngine(results, 0);

            var searchEnginesFactory = new Mock<ISearchEnginesFactory>();
            searchEnginesFactory.Setup(x => x.Create())
                .Returns(() => new List<ISearchEngine> { engineMock.Object });

            // Act
            var contorller = new FastestResultSearchService(
                    _searchRequestRepository,
                    searchEnginesFactory.Object);
            var result = await contorller.Search(query);

            // Assert
            var savedRequest = await _searchRequestRepository.GetAsync(query);
            Assert.AreEqual(query, savedRequest.Query);
            CollectionAssert.AreEqual(results.Select(x => x.Title).ToList(),
                savedRequest.Results.Select(x => x.Title).ToList());
            CollectionAssert.AreEqual(results.Select(x => x.Url).ToList(), 
                savedRequest.Results.Select(x => x.Url).ToList());
        }

        [TestMethod()]
        public async Task Can_Use_Cached_Result()
        {
            // Arrange
            var query = "query";
            var newFoundResults = new List<(string Title, string Url)> { ("new_t1", "new_u1"), ("new_t2", "new_u2")};
            var engineMock = CreateSearchEngine(newFoundResults, 0);

            var searchEnginesFactory = new Mock<ISearchEnginesFactory>();
            searchEnginesFactory.Setup(x => x.Create())
                .Returns(() => new List<ISearchEngine> { engineMock.Object });

            var cachedResults = new List<(string Title, string Url)> { ("q1_t1", "q1_u1"), ("q1_t2", "q1_u2")};
            _searchRequestRepository.Add(
                new SearchRequest
                (
                    query: "query",
                    results: cachedResults.Select(x => new SearchResult(x.Title, x.Url)).ToList()
                ));

            // Act
            var contorller = new FastestResultSearchService(
                    _searchRequestRepository,
                    searchEnginesFactory.Object);
            var result = await contorller.Search(query);

            // Assert
            CollectionAssert.AreNotEqual(newFoundResults.Select(x => x.Title).ToList(), result.Select(x => x.Title).ToList());
            CollectionAssert.AreNotEqual(newFoundResults.Select(x => x.Url).ToList(), result.Select(x => x.Url).ToList());
            CollectionAssert.AreEqual(cachedResults.Select(x => x.Title).ToList(), result.Select(x => x.Title).ToList());
            CollectionAssert.AreEqual(cachedResults.Select(x => x.Url).ToList(), result.Select(x => x.Url).ToList());
        }

        private Mock<ISearchEngine> CreateSearchEngine(List<(string Title, string Url)> results, int delayMs)
        {
            var engineMock = new Mock<ISearchEngine>();
            engineMock.Setup(x => x.SearchAsync(It.IsAny<string>()))
                .Returns<string>(query => Task.Run(async () =>
                    {
                        await Task.Delay(delayMs);
                        return results.Select(x => new SearchResult(x.Title, x.Url));
                    }
                ));

            return engineMock;
        }
    }
}