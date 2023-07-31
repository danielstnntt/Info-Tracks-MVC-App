using System.Text.RegularExpressions;

namespace InfoTracksApp.Models
{
    public class SearchEngineModel
    {
        public string SearchEngineUrl { get; set; }
        public string SearchCriteriaUrl { get; set; }
        public Regex ResultExtractionExpression { get; set; }
    }
}
