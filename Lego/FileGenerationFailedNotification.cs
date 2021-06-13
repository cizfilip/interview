using System;
using Lego.Entities;
using Lego.Mails;
using Lego.Mails.Builders;

namespace Lego
{
    public class FileGenerationFailedNotification : IMailNotification
    {
        private readonly GeneratedFileInfo _generatedFileInfo;
        private readonly User _user;

        public FileGenerationFailedNotification(GeneratedFileInfo generatedFileInfo, User user)
        {
            _generatedFileInfo = generatedFileInfo;
            _user = user;
        }

        protected virtual EmailTemplateType TemplateType => EmailTemplateType.FileGenerationFailed;

        public bool CanSend() => !_generatedFileInfo.WasSavedSuccessfully;

        public void BuildMessage(IMailMessageBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.FromTemplate(TemplateType)
                .UseBodyFormatter(FormatBody)
                .AddRecipient(_user);
        }

        private string FormatBody(string bodyTemplate, BodyFormatterContext context)
        {
            return string.Format(
                context.Culture,
                bodyTemplate,
                _generatedFileInfo.FileName,
                _generatedFileInfo.DestinationPath);
        }
    }
}
