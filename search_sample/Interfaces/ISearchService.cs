using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using search_sample.Models;

namespace search_sample.Interfaces
{
    public interface ISearchService
    {
        Task<List<Person>> Search(string query);
        IActionResult Suggest(string term);
        IActionResult Autocomplete(string term);
    }

}