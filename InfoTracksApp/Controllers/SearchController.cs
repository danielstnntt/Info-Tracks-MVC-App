using Microsoft.AspNetCore.Mvc;
using InfoTracksApp.Interface;
using InfoTracksApp.Models;

namespace InfoTracksApp.Controllers
{
    public class SearchController : Controller
    {
        public ISearchEngine _searchEngine;

        public SearchController(ISearchEngine searchEngine)
        {
            _searchEngine = searchEngine;
        }

        public IActionResult Search(SearchModel model)
        {
            model = _searchEngine.GetSearchResults(model.SearchPhase, model.SearchURL);
            return View("Search", model);
        }

    }
}
