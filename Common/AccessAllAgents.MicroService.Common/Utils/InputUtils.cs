using System;
using System.Globalization;

namespace AccessAllAgents.MicroService.Common.Utils
{
    public static class InputUtils
    {
        private static readonly string[] DateFormats = {
            "yyyy-MM-dd",
            "dd-mmm-yyyy",
            "dd-MM-yyyy HH:mm:ss",
            "dd-MM-yy HH:mm",
            "dd-MM-yy H:mm",
            "dd-MM-yy h:mm",
            "dd-MM-yy K:mm"
        };

        public static bool IsDate(string s)
        {
            return DateTime.TryParse(s, out _);
        }

        public static bool IsNumeric(object expression, out double d)
        {
            if (expression == null)
            {
                d = double.NaN;
                return false;
            }

            return double.TryParse(Convert.ToString(expression, CultureInfo.InvariantCulture),
                NumberStyles.Any,
                NumberFormatInfo.InvariantInfo,
                out d);
        }

        public static DateTime? ConvertDate(string field)
        {
            foreach (string dateFormat in DateFormats)
            {
                if (DateTime.TryParseExact(field, dateFormat, null, DateTimeStyles.AssumeLocal, out DateTime value))
                {
                    return value;
                }
            }

            return null;
        }

        public static double? ConvertDouble(string field)
        {
            if (double.TryParse(field, out double d))
            {
                return d;
            }

            return null;
        }
    }
}