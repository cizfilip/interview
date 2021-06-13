using System;
using System.Threading.Tasks;
using Lego.Common;
using Lego.EntityFramework;
using Lego.Mails.Builders;

namespace Lego.Mails
{
    public class MailNotificationService : IMailNotificationService
    {
        private readonly Func<LegoDbContext> _contextFactory;
        private readonly IDateTimeFactory _dateTimeFactory;
        private readonly IMailSender _mailSender;
        private Func<LegoDbContext> _p;

        public MailNotificationService(Func<LegoDbContext> p)
        {
            _p = p;
        }

        public MailNotificationService(
            Func<LegoDbContext> contextFactory,
            IDateTimeFactory dateTimeFactory,
            IMailSender mailSender)
        {
            _contextFactory = contextFactory;
            _dateTimeFactory = dateTimeFactory;
            _mailSender = mailSender;
        }

        public async Task<bool> SendNotification(IMailNotification notification)
        {
            if (notification is null)
                throw new ArgumentNullException(nameof(notification));

            if (!notification.CanSend())
                return false;

            var currentTime = _dateTimeFactory.Now;
            using var context = _contextFactory();

            var result = false;

            var builder = new MailMessageBuilder();
            notification.BuildMessage(builder);

            var message = await builder.Build(context, currentTime);
            if (message != null)
            {
                result = await _mailSender.SendMail(message);
            }

            return result;
        }
    }
}
