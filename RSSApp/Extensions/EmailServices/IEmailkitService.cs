// This is Interface for EmailServices which is used to send out Emails ti Clients

namespace RSSApp.Extensions.EmailServices
{
    public interface IEmailkitService
    {

        Task SendAsync(string recipients, List<string> ccRecipients, string subject, string mailBody,
            Dictionary<string, Stream> attachments);


        Task SendAsync(string recipients, List<string> ccRecipients, string subject, string mailBody);

        Task SendAsync(string recipients, List<string> ccRecipients, string subject, string mailBody,
            Dictionary<string, Stream> attachments, string senderName, string senderEmail);
    }
}
