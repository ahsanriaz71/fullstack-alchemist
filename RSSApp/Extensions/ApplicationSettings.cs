
namespace RSSApp.Extensions
{
    // Class representing Application Settings options to get Required Data from AppSetting to use in the application

    public class ApplicationSettings
    {
        public const string SectionKey = "Application";
        public string ReloadTime { get; set; }
        public string MobileNo { get; set; }
        public string ToEmail { get; set; }
        public string EmailSubject { get; set; }
        public bool IsSendMessage { get; set; }
        public  string KeyWords { get; set; }
        public string Lebels { get; set; }
    }
}
