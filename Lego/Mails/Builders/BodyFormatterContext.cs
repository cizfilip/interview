using System;
using System.Globalization;

namespace Lego.Mails.Builders
{
    public sealed class BodyFormatterContext
    {
        public BodyFormatterContext(DateTime currentTime, CultureInfo culture)
        {
            CurrentTime = currentTime;
            Culture = culture;
        }

        public DateTime CurrentTime { get; }

        public CultureInfo Culture { get; }
    }
}
