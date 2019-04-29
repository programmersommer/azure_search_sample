using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using search_sample.Interfaces;
using search_sample.Models;

namespace search_sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISearchService _searchService;

        public HomeController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(string query)
        {
            var result = await _searchService.Search(query);

            return View("SearchResult", result);
        }

        public IActionResult Suggest(string term)
        {
            return _searchService.Suggest(term);
        }

        public IActionResult Autocomplete(string term)
        {
            return _searchService.Autocomplete(term);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
