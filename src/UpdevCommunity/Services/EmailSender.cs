using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace UpdevCommunity.Server.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string destinationEmail, string subject, string message)
        {
            try
            {
                using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential()
                    {
                        UserName = "updevcommunity@gmail.com",
                        Password = "Admin@243"
                    },
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };

                var mailAddressFrom = new MailAddress("updevcommunity@gmail.com", "Updev Community Support");
                var mailAddressTo = new MailAddress(destinationEmail);
                var mail = new MailMessage(mailAddressFrom, mailAddressTo)
                {
                    Subject = subject,
                    Body = $@"<html>
                                    <body>{message}</body>
                            </html>",
                    IsBodyHtml = true
                };

                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("{0}: {1}", e.ToString(), e.Message);
            }

        }
    }
}
