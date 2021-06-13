using Lego.Entities;

namespace Lego.Mails.Builders
{
    public interface IMailMessageBuilder
    {
        ITemplatedMailMessagePartsBuilder FromTemplate(EmailTemplateType templateType);

        IMailMessagePartsBuilder FromText(string subject, string body);
    }
}
