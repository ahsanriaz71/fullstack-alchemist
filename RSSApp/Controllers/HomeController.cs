using Microsoft.AspNetCore.Mvc;
using RSSApp.Extensions;
using RSSApp.Models;
using System.Diagnostics;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RSSApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Action method for setting session data
        public IActionResult SetSession()
        {
            // Session keys for last record date and its comparison date
            string _session_Key_LastRecordDatetime = "LastRecordDatetime";
            string _session_Key_LastRecordCompersionDatetime = "LastRecordCompersionDatetime";

            // Retrieve last record date from session
            var lastRecordDate = HttpContext.Session.GetString(_session_Key_LastRecordDatetime);

            if (!String.IsNullOrEmpty(lastRecordDate))
            {
                DateTimeOffset lastdatetimeoff = DateTimeOffsetHelper.FromString(lastRecordDate);

                // Set last record compersion date in session by subtracting 2 years
                HttpContext.Session.SetString(_session_Key_LastRecordCompersionDatetime, lastdatetimeoff.AddYears(-2).ToString());
            }
            return View();
        }

        // Action method for the Feeds view
        public IActionResult Feeds()
        {
            return View();
        }

        // Action method for reloading news feeds list
        public IActionResult ReloadNewsFeedsList()
        {
            return ViewComponent("NewsFeeds");
        }

        // Action method for handling errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
