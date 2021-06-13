using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Lego.Mails
{
    public class ConsoleMailSender : IMailSender
    {
        public Task<bool> SendMail(MailMessage message)
        {
            Console.WriteLine($"Recipients: {string.Join(", ", message.To)}");
            Console.WriteLine($"Subject: {message.Subject}");
            Console.WriteLine("Body:");
            Console.WriteLine(message.Body);

            return Task.FromResult(true);
        }
    }
}
