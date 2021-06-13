using System.Threading.Tasks;

namespace Lego.Mails
{
    public interface IMailNotificationService
    {
        Task<bool> SendNotification(IMailNotification notification);
    }
}
