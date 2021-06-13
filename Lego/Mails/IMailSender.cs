using System.Net.Mail;
using System.Threading.Tasks;

namespace Lego.Mails
{
    public interface IMailSender
    {
        Task<bool> SendMail(MailMessage message);
    }
}
