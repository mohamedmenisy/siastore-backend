
using e_commerce.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace e_commerce.Services
{
    public class MailingService : IMailingService
    {
        private readonly MailSettings _mailSettings;
        public MailingService(IOptions<MailSettings> mailsettings)
        {
            _mailSettings = mailsettings.Value;
        }
        public async Task SendEmailAsync(string mailto, string subject, List<string> body, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = subject
            };
            email.To.Add(MailboxAddress.Parse(mailto));
            var builder = new BodyBuilder();
            if (attachments !=null)
            {
                byte[] fileBytes;
                foreach (var file in attachments)
                {
                    if (file.Length > 0 )
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        fileBytes=ms.ToArray();
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            var title = "<b>Products You Ordered:</b>";
            string result = title + "<br>" + String.Join("<br>", body)+ "<br>";


            builder.HtmlBody = result;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName ,_mailSettings.Email));
            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
                 smtp.Send(email);

            }
            finally
            {
                smtp.Disconnect(true);
                smtp.Dispose();
            }
           
            

        }
    }
}
