using Lego.Mails;

namespace Lego.Entities
{
    public class EmailTemplate
    {
        public EmailTemplateType EmailTemplateType { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
