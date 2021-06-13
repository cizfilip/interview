using System;

namespace Lego.Mails.Builders
{
    public interface ITemplatedMailMessagePartsBuilder : IMailMessagePartsBuilder
    {
        IMailMessagePartsBuilder UseBodyFormatter(Func<string, BodyFormatterContext, string> formatter);
    }
}
