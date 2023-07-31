using InfoTracksApp.Models;

namespace InfoTracksApp.Interface
{
    public interface ISearchEngine
    {
        SearchModel GetSearchResults(string searchKeyword, string searchEngineUrl);
        SearchEngineResults ResultsFromSearchEngine(string searchText);
    }
}
