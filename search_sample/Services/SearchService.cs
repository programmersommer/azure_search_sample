using search_sample.Models;
using System.Linq;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using search_sample.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace search_sample.Services
{

    class SearchService : ISearchService
    {
        private readonly IConfiguration _configuration;
        private SearchIndexClient _indexClient;

        public SearchService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private void InitSearch()
        {
            if (_indexClient != null) return;

            string searchServiceName = _configuration["SearchServiceName"];
            string queryApiKey = _configuration["SearchServiceKey"];

            _indexClient = new SearchIndexClient(searchServiceName, "mydemo-index", new SearchCredentials(queryApiKey));
        }

        public async Task<List<Person>> Search(string searchText)
        {

            var parameters = new SearchParameters()
            {
                SearchMode = SearchMode.All,
                QueryType = QueryType.Full,
                //ScoringProfile = "newTexts",
                //Filter = " Key eq 'person'",
                Top = 10
            };

            InitSearch();

            var results = await _indexClient.Documents.SearchAsync<Person>(searchText, parameters);
            return results.Results.Select(r => r.Document).ToList();

        }

        public IActionResult Suggest(string term)
        {
            InitSearch();

            // Call suggest API and return results
            SuggestParameters sp = new SuggestParameters()
            {
                UseFuzzyMatching = true,
                Top = 5
            };

            DocumentSuggestResult resp = _indexClient.Documents.Suggest(term, "sg", sp);

            // Convert the suggest query results to a list that can be displayed in the client.
            List<string> suggestions = resp.Results.Select(x => x.Text).ToList();
            return new JsonResult(suggestions.ToArray());
        }


        public IActionResult Autocomplete(string term)
        {
            InitSearch();
            //Call autocomplete API and return results
            AutocompleteParameters ap = new AutocompleteParameters()
            {
                AutocompleteMode = AutocompleteMode.OneTermWithContext,
                UseFuzzyMatching = false,
                Top = 5
            };
            AutocompleteResult autocompleteResult = _indexClient.Documents.Autocomplete(term, "sg", ap);

            // Conver the Suggest results to a list that can be displayed in the client.
            List<string> autocomplete = autocompleteResult.Results.Select(x => x.Text).ToList();
            return new JsonResult(autocomplete.ToArray());
        }

    }


}