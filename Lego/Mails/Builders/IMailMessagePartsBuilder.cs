using System.Collections.Generic;
using Lego.Entities;

namespace Lego.Mails.Builders
{
    public interface IMailMessagePartsBuilder
    {
        IMailMessagePartsBuilder AddRecipient(string recipient);

        IMailMessagePartsBuilder AddAttachment(Attachment attachment);

        IMailMessagePartsBuilder IsHighPriority();
    }
}
