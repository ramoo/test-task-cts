using CTSTestApplication;
using System;

namespace TradeProcessor
{
    public static class Extensions
    {
        public static Direction GetDirection(this string source)
        {
            switch (source)
            {
                case "B":
                    return Direction.Buy;
                case "S":
                    return Direction.Sell;
                default:
                    throw new InvalidOperationException($"Unknown direction string '{source}'.");
            }
        }

        public static string GetElapsedTimeString(this TimeSpan timespan)
        {
            return string.Format(
                    "{0:00}:{1:00}:{2:00}.{3:00}",
                    timespan.Hours,
                    timespan.Minutes,
                    timespan.Seconds,
                    timespan.Milliseconds / 10);
        }

        public static T GuardNotNull<T>(this T instance, string name)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(name);
            }

            return instance;
        }

        public static string GuardNotEmpty(this string text, string name)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"Expecting not empty string in '{name}'.");
            }

            return text;
        }
    }
}
