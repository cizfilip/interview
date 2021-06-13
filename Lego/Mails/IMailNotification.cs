using Lego.Mails.Builders;

namespace Lego.Mails
{
    public interface IMailNotification
    {
        public bool CanSend();

        public void BuildMessage(IMailMessageBuilder builder);
    }
}
