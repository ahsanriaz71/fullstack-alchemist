using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RSSApp.Extensions;
using RSSApp.Extensions.EmailServices;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RSSApp.ViewsComponents
{
    [ViewComponent(Name = "NewsFeeds")]
    public class NewsFeedsViewComponent : ViewComponent
    {
        private IEmailkitService _emailkitManager;
        private readonly ApplicationSettings _settings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor to inject dependencies
        public NewsFeedsViewComponent(IOptions<ApplicationSettings> settings,
                                       IHttpContextAccessor httpContextAccessor,
                                       IEmailkitService emailkitService)
        {
            _settings = settings.Value; // Getting configuration settings
            _httpContextAccessor = httpContextAccessor; // Injecting HttpContextAccessor
            _emailkitManager = emailkitService; // Injecting email service
        }

        // Method to asynchronously invoke the view component
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Splitting keywords and labels from settings
            string[] KeyWordsArray = _settings.KeyWords.Split(",");
            string[] LebelsArray = _settings.Lebels.Split(",");

            // Setting session keys
            string _session_Key_RequestDatetime = "RequestDatetime";
            string _session_Key_LastRecordDatetime = "LastRecordDatetime";
            string _session_Key_LastRecordCompersionDatetime = "LastRecordCompersionDatetime";

            // Setting current request datetime in session
            _httpContextAccessor.HttpContext.Session.SetString(_session_Key_RequestDatetime, DateTimeOffset.Now.ToString());

            var lastRecordDate = _httpContextAccessor.HttpContext.Session.GetString(_session_Key_LastRecordDatetime);

            // Fetching RSS feed data
            var url = "https://feeds.fireside.fm/bibleinayear/rss";
            using var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            List<SyndicationItem> items = feed.Items.ToList();

            // Getting the latest item from the feed
            SyndicationItem item = items.OrderByDescending(a => a.PublishDate).FirstOrDefault();

            if (!String.IsNullOrEmpty(lastRecordDate))
            {
                // Comparing last record date with the latest item's publish date
                DateTimeOffset lastdatetimeoff = DateTimeOffsetHelper.FromString(lastRecordDate);

                if (lastdatetimeoff < item.PublishDate)
                {
                    // Updating session with latest record date and comparison date
                    _httpContextAccessor.HttpContext.Session.SetString(_session_Key_LastRecordDatetime, item.PublishDate.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString(_session_Key_LastRecordCompersionDatetime, lastdatetimeoff.ToString());
                }
            }
            else
            {
                // Setting session with latest record date if it's empty
                _httpContextAccessor.HttpContext.Session.SetString(_session_Key_LastRecordDatetime, item.PublishDate.ToString());
            }

            // Returning the view with the syndication feed data
            return View(feed);
        }
    }
}
