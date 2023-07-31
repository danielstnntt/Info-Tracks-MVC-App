using System.ComponentModel.DataAnnotations;

namespace InfoTracksApp.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            //Initalise Load to the Search Page
            SearchResults = new SearchEngineResults();
            SearchPhase = "Please enter Specific Keyword";
            SearchURL = "www.google.co.uk";
        }

        public SearchEngineResults SearchResults { get; set; }

        [Display(Name = "Search Phase")]
        public string SearchPhase { get; set; }

        [Display(Name = "Search URL")]
        public string SearchURL { get; set; }

    }
}
