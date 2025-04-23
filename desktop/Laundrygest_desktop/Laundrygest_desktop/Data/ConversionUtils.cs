using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundrygest_desktop.Data
{
    public static class ConversionUtils
    {
        public static int? TryParseInt(string value)
        {
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)
                ? result
                : (int?)null;
        }

        public static long? TryParseLong(string value)
        {
            return long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)
                ? result
                : (long?)null;
        }

        public static decimal? TryParseDecimal(string value)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var result)
                ? result
                : (decimal?)null;
        }

        public static double? TryParseDouble(string value)
        {
            return double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var result)
                ? result
                : (double?)null;
        }

        public static short? TryParseShort(string value)
        {
            return short.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)
                ? result
                : (short?)null;
        }

        public static byte? TryParseByte(string value)
        {
            return byte.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)
                ? result
                : (byte?)null;
        }

        public static DateTime? TryParseDateTime(string value)
        {
            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result)
                ? result
                : (DateTime?)null;
        }

        public static bool? TryParseBool(string value)
        {
            return bool.TryParse(value, out var result)
                ? result
                : (bool?)null;
        }
    }

}

