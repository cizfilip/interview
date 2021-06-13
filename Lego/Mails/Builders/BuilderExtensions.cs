using Lego.Entities;
using System.Collections.Generic;

namespace Lego.Mails.Builders
{
    public static class BuilderExtensions
    {
        public static IMailMessagePartsBuilder AddRecipients(this IMailMessagePartsBuilder builder, IEnumerable<string> recipients)
        {
            foreach (var recipient in recipients)
            {
                builder.AddRecipient(recipient);
            }
            
            return builder;
        }

        public static IMailMessagePartsBuilder AddRecipients(this IMailMessagePartsBuilder builder, IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                builder.AddRecipient(user);
            }

            return builder;
        }

        public static IMailMessagePartsBuilder AddRecipient(this IMailMessagePartsBuilder builder, User user)
        {
            builder.AddRecipient(user.Email);

            return builder;
        }
    }
}
