using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TargetTracker
{
    public static class UniFormatterExtensions
    {
        public static string ToStringUniform(this decimal num)
        {
            return num.ToString(CultureProvider.Common);
        }

        public static string ToStringUniform(this double num)
        {
            return num.ToString(CultureProvider.Common);
        }

        public static string ToStringUniform(this decimal num, int precision)
        {
            var fmt = "f" + precision;
            return num.ToString(fmt, CultureProvider.Common);
        }

        public static string ToStringUniform(this decimal? num)
        {
            return num.HasValue ? num.Value.ToString(CultureProvider.Common) : "";
        }

        public static string ToStringUniform(this double num, int precision)
        {
            var fmt = "f" + precision;
            return num.ToString(fmt, CultureProvider.Common);
        }

        public static string ToStringUniform(this IEnumerable <int> numbers, string delimiter)
        {
            var res = new StringBuilder();
            var startFlag = true;
            foreach(var number in numbers)
            {
                if (!startFlag)
                {
                    res.Append(delimiter);
                    
                }
                startFlag = false;
                res.Append(number.ToString());
            }
            return res.ToString();
        }

        public static string ToStringUniform(this IEnumerable<decimal> numbers, string delimiter)
        {
            var res = new StringBuilder();
            var startFlag = true;
            foreach (var number in numbers)
            {
                if (!startFlag)
                {
                    res.Append(delimiter);

                }
                startFlag = false;
                res.Append(number.ToStringUniform());
            }
            return res.ToString();
        }

        public static string ToStringUniform(this IEnumerable<double> numbers, string delimiter)
        {
            var res = new StringBuilder();
            var startFlag = true;
            foreach (var number in numbers)
            {
                if (!startFlag)
                {
                    res.Append(delimiter);

                }
                startFlag = false;
                res.Append(number.ToStringUniform());
            }
            return res.ToString();
        }

        public static int ToInt(this string numStr)
        {
            return int.Parse(numStr);
        }

        public static bool ToBool(this string boolStr)
        {
            return bool.Parse(boolStr);            
        }

        public static bool? ToBoolSafe(this string boolStr)
        {
            bool result;
            if (Boolean.TryParse(boolStr, out result)) return result;
            return null;
        }

        public static int? ToIntSafe(this string numStr)
        {
            int val;
            if (!int.TryParse(numStr, out val)) return null;
            return val;
        }

        public static int ToInt(this string numStr, int defaultValue)
        {
            if (string.IsNullOrEmpty(numStr)) return defaultValue;
            var digitStr = new StringBuilder();
            foreach (var c in numStr)
                if ((c >= '0' && c <= '9') || c == '-') digitStr.Append(c);

            int result = defaultValue;
            if (!int.TryParse(digitStr.ToString(), out defaultValue))
                result = defaultValue;

            return result;
        }

        public static decimal ToDecimalUniform(this string numStr)
        {
            return decimal.Parse(numStr, CultureProvider.Common);
        }

        public static decimal? ToDecimalUniformSafe(this string numStr)
        {
            decimal result;
            if (decimal.TryParse(numStr.Replace(',', '.'), NumberStyles.Any, CultureProvider.Common, out result))
                return result;
            return null;
        }

        public static double? ToDoubleUniformSafe(this string numStr)
        {
            double result;
            if (double.TryParse(numStr.Replace(',', '.'), NumberStyles.Any, CultureProvider.Common, out result))
                return result;
            return null;
        }

        /// <summary>
        /// выбрать все числа, содержащиеся в строке
        /// </summary>        
        public static decimal[] ToDecimalArrayUniform(this string numStr)
        {
            var numbers = new List<decimal>();

            if (string.IsNullOrEmpty(numStr)) return new decimal[0];
            var numPart = "";
            decimal num;
            for (var i = 0; i < numStr.Length; i++)
            {
                if (numStr[i] == '.' || numStr[i] == '-' ||
                    (numStr[i] >= '0' && numStr[i] <= '9'))
                {
                    numPart = numPart + numStr[i];
                    continue;
                }

                if (decimal.TryParse(numPart, NumberStyles.Float,
                    CultureInfo.InvariantCulture, out num))
                    numbers.Add(num);
                numPart = "";
            }
            if (decimal.TryParse(numPart, NumberStyles.Float,
                    CultureInfo.InvariantCulture, out num))
                numbers.Add(num);
            return numbers.ToArray();
        }

        public static int[] ToIntArrayUniform(this string numStr)
        {
            var numbers = new List<int>();

            if (string.IsNullOrEmpty(numStr)) return new int[0];
            var numPart = "";
            int num;
            for (var i = 0; i < numStr.Length; i++)
            {
                if (numStr[i] == '-' || (numStr[i] >= '0' && numStr[i] <= '9'))
                {
                    numPart = numPart + numStr[i];
                    continue;
                }

                if (int.TryParse(numPart, out num)) numbers.Add(num);
                numPart = "";
            }
            if (int.TryParse(numPart, out num)) numbers.Add(num);
            return numbers.ToArray();
        }

        public static double ToDoubleUniform(this string numStr)
        {
            return double.Parse(numStr, CultureInfo.InvariantCulture);
        }

        public static string[] CastToStringArrayUniform<T>(this IEnumerable<T> coll)
            where T : IFormattable
        {
            var outLst = new List<string>();
            foreach (IFormattable item in coll)
                outLst.Add(item.ToString(null, CultureInfo.InvariantCulture));
            return outLst.ToArray();
        }

        public static List<string> CastToStringListUniform<T>(this IEnumerable<T> coll)
            where T : IFormattable
        {
            var outLst = new List<string>();
            foreach (IFormattable item in coll)
                outLst.Add(item.ToString(null, CultureInfo.InvariantCulture));
            return outLst;
        }


        public static DateTime ToDateTimeUniform(this string str)
        {
            return DateTime.ParseExact(str, "dd.MM.yyyy HH:mm:ss", CultureProvider.Common);
        }

        public static DateTime? ToDateTimeUniformSafe(this string str)
        {
            DateTime result;
            return DateTime.TryParseExact(str, "dd.MM.yyyy HH:mm:ss", CultureProvider.Common, DateTimeStyles.None,
                out result) ? (DateTime?)result : null;            
        }

        public static DateTime ToDateTimeDefault(this string str, DateTime defaultDate)
        {
            DateTime result;
            return DateTime.TryParseExact(str, "dd.MM.yyyy HH:mm:ss", CultureProvider.Common, DateTimeStyles.None,
                out result) ? result : defaultDate;
        }

        public static string ToStringUniform(this DateTime time)
        {
            return time.ToString("dd.MM.yyyy HH:mm:ss", CultureProvider.Common);
        }
    }  

    public static class CultureProvider
    {
        public static CultureInfo Common = CultureInfo.InvariantCulture;
    }
}
