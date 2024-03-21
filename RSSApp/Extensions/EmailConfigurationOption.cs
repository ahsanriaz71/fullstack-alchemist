namespace RSSApp.Extensions
{
    // Class representing email configuration options to get data from AppSetting
    public class EmailConfigurationOption
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public bool SSL { get; set; }

    }
}
