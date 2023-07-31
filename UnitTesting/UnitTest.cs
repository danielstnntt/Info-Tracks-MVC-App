using InfoTracksApp;
using InfoTracksApp.Models;

namespace UnitTesting
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test_GetSearchResults_NoData()
        {
            SearchModel testSearchModel = new SearchModel();

            InfoTracksApp.Controllers.SearchEngineController searchEngineController = new InfoTracksApp.Controllers.SearchEngineController();
            testSearchModel = searchEngineController.GetSearchResults("", "");

            Assert.AreEqual(testSearchModel.SearchPhase, null);
            Assert.AreEqual(testSearchModel.SearchURL, null);
        }

        [TestMethod]
        public void Test_GetSearchResults_NoSearchEngine()
        {
            SearchModel testSearchModel = new SearchModel();

            InfoTracksApp.Controllers.SearchEngineController searchEngineController = new InfoTracksApp.Controllers.SearchEngineController();
            testSearchModel = searchEngineController.GetSearchResults("infotrack", "");

            Assert.AreEqual(testSearchModel.SearchURL, null);
        }

        [TestMethod]
        public void Test_GetSearchResults_NoSearchCriteria()
        {
            SearchModel testSearchModel = new SearchModel();

            InfoTracksApp.Controllers.SearchEngineController searchEngineController = new InfoTracksApp.Controllers.SearchEngineController();
            testSearchModel = searchEngineController.GetSearchResults("", "www.google.co.uk");

            Assert.AreEqual(testSearchModel.SearchPhase, null);
        }

        [TestMethod]
        public void Test_ResultsFromSearchEngine_CheckNoPositions()
        {
            SearchEngineResults searchEngineResults = new SearchEngineResults();

            InfoTracksApp.Controllers.SearchEngineController searchEngineController = new InfoTracksApp.Controllers.SearchEngineController();
            searchEngineResults = searchEngineController.ResultsFromSearchEngine("fdfdsdffsdfds");

            Assert.AreEqual(searchEngineResults.SearchEngineRanks.Count, 0);
        }

        [TestMethod]
        public void Test_ResultsFromSearchEngine_CheckForValidPositions()
        {
            SearchEngineResults searchEngineResults = new SearchEngineResults();

            InfoTracksApp.Controllers.SearchEngineController searchEngineController = new InfoTracksApp.Controllers.SearchEngineController();
            searchEngineResults = searchEngineController.ResultsFromSearchEngine("infotrack");

            Assert.IsTrue(searchEngineResults.SearchEngineRanks.Count > 0);
        }
    }
}