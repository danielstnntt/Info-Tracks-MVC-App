using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using InfoTracksApp.Interface;
using InfoTracksApp.Models;

namespace InfoTracksApp.Controllers
{
    public class SearchEngineController : ISearchEngine
    {
        public SearchModel GetSearchResults(string searchKeyword, string searchEngineUrl)
        {
            SearchModel search = new SearchModel();
            if (!String.IsNullOrEmpty(searchKeyword) && !String.IsNullOrEmpty(searchEngineUrl))
            {
                search.SearchPhase = searchKeyword;
                search.SearchURL = searchEngineUrl;
                search.SearchResults = ResultsFromSearchEngine(searchKeyword.Trim());
            }
            return search;
        }

        public SearchEngineResults ResultsFromSearchEngine(string searchText)
        {
            try
            {
                SearchEngineResults searchEngineResults = new SearchEngineResults();

                SearchEngineModel searchEngineModel = GetSearchEngine();

                var searchUrl = searchEngineModel.SearchCriteriaUrl.Replace("#SearchCriteria#", HttpUtility.UrlEncode(searchText)).Replace("#TotalNumberToReturn#", "100");


                var request = WebRequest.Create($"{searchEngineModel.SearchEngineUrl}/{searchUrl}");
                request.Method = "GET";

                var webResponse = request.GetResponse();
                var webStream = webResponse.GetResponseStream();

                var reader = new StreamReader(webStream);
                var data = reader.ReadToEnd();

                var links = RetrieveLinksFromResponse(data, searchEngineModel.ResultExtractionExpression);
                var rankingList = (from link in links where link.Contains("www.infotrack.co.uk") select links.IndexOf(link)).Distinct().ToList();

                var newRankList = ExcludeSearchResults(rankingList, 100);
                searchEngineResults.SearchEngineRanks = newRankList;
                searchEngineResults.SearchDate = DateTime.Now;

                return searchEngineResults;
            }
            catch (Exception ex)
            {
                SearchEngineResults searchEngineResults = null;
                return searchEngineResults;
            }
        }

        private SearchEngineModel GetSearchEngine()
        {
            string searchEngine = "google"; 
            SearchEngineModel engineSearch = new SearchEngineModel();

            switch (searchEngine)
            {
                case "google":
                    engineSearch.SearchEngineUrl = "http://www.google.co.uk";
                    engineSearch.ResultExtractionExpression = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    engineSearch.SearchCriteriaUrl = "search?q=#SearchCriteria#&num=#TotalNumberToReturn#";
                    break;
                //Add Another Search Engine Source if needed. i.e bing
                //case "bing":
                    //engineSearch.SearchEngineUrl = "http://www.bing.co.uk";
                    //engineSearch.ResultExtractionExpression = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    //engineSearch.SearchCriteriaUrl = "searchCriteriaAndQueryFilterDetails";
                    //break;
                default:
                    engineSearch.SearchEngineUrl = "http://www.google.co.uk";
                    engineSearch.ResultExtractionExpression = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    engineSearch.SearchCriteriaUrl = "search?q=#SearchCriteria#&num=#TotalNumberToReturn#";
                    break;
            }

            return engineSearch;
        }

        private static List<string> RetrieveLinksFromResponse(string responseBody, Regex regexToExtractLinks)
        {
            var matchesBodyAndRegex = Regex.Matches(responseBody, $"{regexToExtractLinks}");
            var websiteMatchOnly = matchesBodyAndRegex.Cast<Match>().Select(m => m.Value).ToList();
            return websiteMatchOnly;
        }

        private List<int> ExcludeSearchResults(List<int> currentSearchRankings, int numResultsToSearchIn)
        {
            List<int> newSearchRanking = new List<int>();
            foreach (int rank in currentSearchRankings)
            {
                if (rank <= numResultsToSearchIn)
                {
                    newSearchRanking.Add(rank);
                }
            }
            return newSearchRanking;
        }
 
    }
}
