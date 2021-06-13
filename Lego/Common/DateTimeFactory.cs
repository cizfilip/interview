using System;

namespace Lego.Common
{
    public class DateTimeFactory : IDateTimeFactory
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime Today => DateTime.Today;
    }
}
