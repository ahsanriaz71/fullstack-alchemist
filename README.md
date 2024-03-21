# RSS Feed Management System
# Overview
This repository contains the source code for a .NET application that manages RSS feeds, allowing users to retrieve and view news articles from various sources. The application includes features such as automatic reloading of feeds, keyword-based filtering, and email notifications for new articles.

# Features
**RSS Feed Retrieval:** The application fetches RSS feeds from specified URLs using the System.ServiceModel.Syndication namespace.

**Automatic Reloading:** Feeds are automatically reloaded at regular intervals defined in the application settings to ensure up-to-date content.

**Keyword-Based Filtering:** Users can specify keywords in the application settings, and only articles containing these keywords in specified labels (e.g., title) are displayed.

**Email Notifications:** Users can receive email notifications for new articles matching the specified keywords. Email configuration is provided in the application settings.

# Setup
To set up and run the application locally, follow these steps:

**Clone the Repository:** Clone this repository to your local machine using Git.

```
git clone https://github.com/yourusername/rss-feed-management.git

```

**Configure Application Settings:** Update the appsettings.json file in the RSSApp project with your desired configuration settings. You may need to provide URLs for RSS feeds, email addresses, keywords, etc.

```
{
  "Application": {
    "ReloadTime": "05",
    "IsSendMessage": true,
    "MobileNo": "03001234567",
    "ToEmail": "client@gmail.com",
    "EmailSubject": "News Feed Updates",
    "KeyWords": "Small,Things",
    "Lebels": "Title"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "EmailConfiguration": {
    "Host": "smtp.gmail.com",
    "Port": 465,
    "Username": "Rss.Dummy@gmail.com",
    "Password": "HashPassword123",
    "From": "Rss.Dummy@gmail.com",
    "SSL": true
  },
  "AllowedHosts": "*"
}

```
**Build and Run:** Build the solution in Visual Studio and run the RSSApp project. Ensure that you have the necessary dependencies installed.

**Access the Application:** Once the application is running, access it through your web browser. You should see the main page displaying the fetched RSS feeds and any relevant information.

# Usage
**View Feeds:** Browse through the displayed feeds to view the latest articles.

**Refresh Feeds:** Click the "Refresh" button to manually reload the feeds.

**Email Notifications:** If enabled in the settings, you will receive email notifications for new articles matching the specified keywords.

# Contributing
Contributions to this project are welcome! If you find any issues or have suggestions for improvements, please open an issue or create a pull request.
