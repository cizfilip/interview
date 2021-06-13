using Lego.Common;
using Lego.Entities;
using Lego.EntityFramework;
using Lego.Mails;
using System;
using System.Threading.Tasks;

namespace Lego
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var generatedFileInfo = new GeneratedFileInfo
            {
                FileName = "Test",
                DestinationPath = "C:\\Temp",
                WasSavedSuccessfully = false,
            };

            var user = new User { Email = "test@aa.cz" };

            var notification = new FileGenerationFailedNotification(generatedFileInfo, user);

            var service = CreateService();

            var result = await service.SendNotification(notification);

            Console.WriteLine($"Notification send status: {result}");

            Console.ReadKey();
        }

        private static IMailNotificationService CreateService()
        {
            var context = new LegoDbContext
            {
                EmailTemplates = new[]
                {
                    new EmailTemplate
                    {
                        EmailTemplateType = EmailTemplateType.FileGenerationFailed,
                        Subject = "Test",
                        Body = "An error occurred while generating the file '{0}' on path '{1}'",
                    },
                },
            };

            return new MailNotificationService(() => context, new DateTimeFactory(), new ConsoleMailSender());
        }
    }
}
