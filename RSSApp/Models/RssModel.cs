namespace RSSApp.Models
{
    // Necessary Models which are required for application
    // you can customize according to the need
    public class RssModel
    {
        public Rss rss { get; set; }
    }

    public class Rss
    {
        public Channel channel { get; set; }
    }

    public class Channel
    {
        public Title title { get; set; }
        public Description description { get; set; }
        public string link { get; set; }
        public Image image { get; set; }
        public string generator { get; set; }
        public string lastBuildDate { get; set; }
        public string pubDate { get; set; }
        public Copyright copyright { get; set; }
        public Language language { get; set; }
        public int ttl { get; set; }
        public Item[] item { get; set; }
    }

    public class Title
    {
        public string __cdata { get; set; }
    }

    public class Description
    {
        public string __cdata { get; set; }
    }

    public class Image
    {
        public string url { get; set; }
        public string title { get; set; }
        public string link { get; set; }
    }

    public class Copyright
    {
        public string __cdata { get; set; }
    }

    public class Language
    {
        public string __cdata { get; set; }
    }

    public class Item
    {
        public Title1 title { get; set; }
        public string link { get; set; }
        public string guid { get; set; }
        public string pubDate { get; set; }
        public Group group { get; set; }
        public Description1 description { get; set; }
    }

    public class Title1
    {
        public string __cdata { get; set; }
    }

    public class Group
    {
        public string[] content { get; set; }
    }

    public class Description1
    {
        public string __cdata { get; set; }
    }

}
