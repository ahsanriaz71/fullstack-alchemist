using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Tls.Crypto.Impl;

//This is the Email Services for sending Emails, it uses MailKit Nuget Package to send emails 
// you can coustomise it according to your requirements

namespace RSSApp.Extensions.EmailServices
{
    public class EmailkitService : IEmailkitService
    {
        private readonly EmailConfigurationOption _emailConfiguration;
        public EmailkitService(IOptions<EmailConfigurationOption> emailConfiguration)
        {

            _emailConfiguration = emailConfiguration.Value ?? throw new ArgumentNullException(nameof(emailConfiguration));
        }
        private async Task SendAsync(MimeMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.CheckCertificateRevocation = false;
                await smtpClient.ConnectAsync(_emailConfiguration.Host, _emailConfiguration.Port, _emailConfiguration.SSL);
                smtpClient.AuthenticationMechanisms.Clear();
                smtpClient.AuthenticationMechanisms.Add("PLAIN");
                await smtpClient.AuthenticateAsync(_emailConfiguration.Username, _emailConfiguration.Password);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);

            }
        }


        public async Task SendAsync(string recipients, List<string> ccRecipients, string subject, string mailBody, Dictionary<string, Stream> attachments)
        {
            var mimeMessage = new MimeMessage();


            mimeMessage.To.Add(new MailboxAddress("", recipients));

            if (ccRecipients != null)
            {
                mimeMessage.Cc.AddRange(ccRecipients.Select(x => new MailboxAddress("", x)));
            }

            mimeMessage.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = mailBody
            };

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    builder.Attachments.Add(attachment.Key, attachment.Value);
                }
            }

            mimeMessage.Body = builder.ToMessageBody();
            mimeMessage.From.Add(new MailboxAddress(_emailConfiguration.Username, _emailConfiguration.Username));

            try
            {


                await SendAsync(mimeMessage);

            }
            catch (Exception exception)
            {
                throw exception;

            }
        }
        public async Task SendAsync(string recipients, List<string> ccRecipients, string subject, string mailBody)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.To.Add(new MailboxAddress("", recipients));

            if (ccRecipients != null)
            {
                mimeMessage.Cc.AddRange(ccRecipients.Select(x => new MailboxAddress("", x)));
            }

            mimeMessage.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = mailBody
            };

            mimeMessage.Body = builder.ToMessageBody();

            mimeMessage.From.Add(new MailboxAddress(_emailConfiguration.Username, _emailConfiguration.Username));

            try
            {
                await SendAsync(mimeMessage);
            }
            catch (Exception exception)
            {
                throw exception;

            }
        }


        public async Task SendAsync(string recipients, List<string> ccRecipients, string subject, string mailBody, Dictionary<string, Stream> attachments, string senderName, string senderEmail)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.To.Add(new MailboxAddress("", recipients));

            if (ccRecipients != null)
            {
                mimeMessage.Cc.AddRange(ccRecipients.Select(x => new MailboxAddress("", x)));
            }

            mimeMessage.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = mailBody
            };

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    builder.Attachments.Add(attachment.Key, attachment.Value);
                }
            }

            mimeMessage.Body = builder.ToMessageBody();

            mimeMessage.From.Add(new MailboxAddress(senderName, senderEmail ?? _emailConfiguration.Username));
            try
            {
                await SendAsync(mimeMessage);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
