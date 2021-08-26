using System;
using System.Globalization;
using System.IO;

namespace AccessAllAgents.MicroService.Common.Extensions
{
    public static class StringExtensions
    {
        public static Stream ToMemoryStream(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static DateTime? ToDate(this string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            // example: 2 Aug 2014 14:38:08
            if (DateTime.TryParseExact(dateString,
                new[] { "dd MMM yyyy HH:mm:ss", "d MMM yyyy H:mm:ss", "dd MMM yyyy HH:mm:ss tt", "yyyy-MM-dd" },
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime date))
            {
                return date;
            }

            if (DateTime.TryParse(dateString, out date))
            {
                return date;
            }

            return null;
        }
    }
}
