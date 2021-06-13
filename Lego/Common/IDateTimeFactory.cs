using System;

namespace Lego.Common
{
    public interface IDateTimeFactory
    {
        public DateTime Now { get; }

        public DateTime UtcNow { get; }

        public DateTime Today { get; }
    }
}
