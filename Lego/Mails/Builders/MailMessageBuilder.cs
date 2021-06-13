using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Lego.Entities;
using Lego.EntityFramework;

namespace Lego.Mails.Builders
{
    internal class MailMessageBuilder : IMailMessageBuilder, ITemplatedMailMessagePartsBuilder
    {
        private readonly List<string> _recipients;
        private readonly List<Entities.Attachment> _attachments;

        private bool _isHighPriority = false;
        private EmailTemplateType? _templateType = null;
        private Func<string, BodyFormatterContext, string> _bodyFormatter = null;
        private string _subject = null;
        private string _body = null;

        public MailMessageBuilder()
        {
            _recipients = new List<string>();
            _attachments = new List<Entities.Attachment>();
        }

        public ITemplatedMailMessagePartsBuilder FromTemplate(EmailTemplateType templateType)
        {
            _templateType = templateType;
            return this;
        }

        public IMailMessagePartsBuilder FromText(string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentNullException(nameof(subject));
            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException(nameof(body));

            _subject = subject;
            _body = body;

            return this;
        }

        public IMailMessagePartsBuilder AddAttachment(Entities.Attachment attachment)
        {
            _attachments.Add(attachment);
            return this;
        }

        public IMailMessagePartsBuilder AddRecipient(string recipient)
        {
            _recipients.Add(recipient);
            return this;
        }

        public IMailMessagePartsBuilder IsHighPriority()
        {
            _isHighPriority = true;
            return this;
        }

        public IMailMessagePartsBuilder UseBodyFormatter(Func<string, BodyFormatterContext, string> bodyFormatter)
        {
            _bodyFormatter = bodyFormatter;
            return this;
        }

        public async Task<MailMessage> Build(LegoDbContext context, DateTime currentTime)
        {
            var recipients = _recipients.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new MailAddress(x)).ToArray();
            if (!recipients.Any())
                return null;

            var message = new MailMessage
            {
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = false,
                Priority = _isHighPriority ? MailPriority.High : MailPriority.Normal,
            };

            foreach (var recipient in recipients)
            {
                message.To.Add(recipient);
            }

            foreach (var attachment in _attachments)
            {
                message.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(attachment.FileContent), attachment.FileName));
            }

            if (_templateType != null)
            {
                var template = await LoadTemplate(context, _templateType.Value);
                message.Subject = template.Subject;

                if (_bodyFormatter != null)
                {
                    var bodyFormatterContext = new BodyFormatterContext(currentTime, CultureInfo.GetCultureInfo("cs-CZ"));
                    message.Body = _bodyFormatter(template.Body, bodyFormatterContext);
                }
                else
                {
                    message.Body = template.Body;
                }
            }
            else
            {
                message.Subject = _subject;
                message.Body = _body;
            }

            return message;
        }

        private Task<EmailTemplate> LoadTemplate(LegoDbContext context, EmailTemplateType type)
        {
            return Task.FromResult(context.EmailTemplates.Single(x => x.EmailTemplateType == type));
        }
    }
}
