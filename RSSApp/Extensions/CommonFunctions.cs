using System.ServiceModel.Syndication; // Importing the namespace for SyndicationItem

namespace RSSApp.Extensions
{
    // Class containing common utility functions
    public class CommonFunctions
    {
        // Method to check if keywords match any labels in SyndicationItem
        public static bool KeyWordsMatches(SyndicationItem syndicationItem, string[] KeyWordsArray, string[] LebelsArray)
        {
            bool isMatch = false;

            foreach (var itemOut in KeyWordsArray)
            {
                foreach (var item in LebelsArray)
                {
                    // Check if the label is "Title"
                    if (item == "Title")
                    {
                        if (syndicationItem.Title.Text.Contains(itemOut))
                        {
                            isMatch = true;
                            break;
                        }
                    }
                }
            }
            return isMatch;
        }

        // Method to generate HTML body for a single SyndicationItem
        public static string HtmlBody(SyndicationItem syndicationItem)
        {
            string htmlBody = string.Empty;

            // Append title to the HTML body
            htmlBody += "Title: " + syndicationItem.Title.Text.ToString();
            htmlBody += " " + Environment.NewLine;

            // Append links to the HTML body
            htmlBody += "Links: " + syndicationItem.Links.FirstOrDefault().Uri.ToString();
            htmlBody += " " + Environment.NewLine;

            // Append publish date to the HTML body
            htmlBody += "Publish Date: " + syndicationItem.PublishDate;

            return htmlBody;
        }

        // Method to generate HTML body for a list of SyndicationItems
        public static string HtmlBody(List<SyndicationItem> syndicationItemList)
        {
            string htmlBody = string.Empty;

            int rowCount = 0;

            foreach (var syndicationItem in syndicationItemList)
            {
                rowCount = rowCount + 1;

                // Append item number to the HTML body
                htmlBody += "Item No: " + rowCount;
                htmlBody += " " + Environment.NewLine;

                // Append title to the HTML body
                htmlBody += "Title: " + syndicationItem.Title.Text.ToString();
                htmlBody += " " + Environment.NewLine;

                // Append links to the HTML body
                htmlBody += "Links: " + syndicationItem.Links.FirstOrDefault().Uri.ToString();
                htmlBody += " " + Environment.NewLine;

                // Append publish date to the HTML body
                htmlBody += "Publish Date: " + syndicationItem.PublishDate;

                htmlBody += "</br>";
            }

            return htmlBody;
        }
    }
}
