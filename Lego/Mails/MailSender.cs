using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Lego.Mails
{
    public class MailSender : IMailSender
    {
        private readonly MailOptions _options;
        private readonly ILogger<MailSender> _logger;

        public MailSender(ILogger<MailSender> logger, MailOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendMail(MailMessage message)
        {
            if (message is null)
                throw new ArgumentNullException(nameof(message));

            var senderAddress = new MailAddress(_options.SenderEmail, _options.SenderName);

            try
            {
                message.From = senderAddress;

                using var client = new SmtpClient(_options.Server, _options.Port)
                {
                    UseDefaultCredentials = _options.UseDefaultCredentials,
                };

                if (_options.UsePickupFolder && !string.IsNullOrEmpty(_options.PickupFolder))
                {
                    Directory.CreateDirectory(_options.PickupFolder);
                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = _options.PickupFolder;
                }

                await client.SendMailAsync(message);

                return true;
            }
            catch (SmtpException ex)
            {
                var recipients = string.Join(", ", message.To.Select(o => o.Address));
                _logger.LogError(ex, $"Error thrown while trying to send an email message ('{message.Subject}' to {recipients})");
                return false;
            }
        }
    }
}
