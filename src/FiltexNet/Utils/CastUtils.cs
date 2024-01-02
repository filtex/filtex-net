using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using FiltexNet.Exceptions;

namespace FiltexNet.Utils
{
    public static class CastUtils
    {
        public static bool IsArray(object value)
        {
            try
            {
                _ = Array(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static object[] Array(object value)
        {
            if (value is null)
            {
                return new object[] { };
            }

            if (value is not string && value is IEnumerable enumerable)
            {
                return enumerable.Cast<object>().ToArray();
            }

            throw CastException.NewCouldNotBeCastedError();
        }

        public static bool IsString(object value)
        {
            try
            {
                _ = String(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static string String(object value)
        {
            var type = value?.GetType();

            if (type is { IsPrimitive: false, IsEnum: false } && type != typeof(string) && type != typeof(decimal) && type != typeof(DateTime))
            {
                throw CastException.NewCouldNotBeCastedError();
            }

            try
            {
                return Convert.ToString(value?.ToString());
            }
            catch (Exception)
            {
                throw CastException.NewCouldNotBeCastedError();
            }
        }

        public static bool IsNumber(object value)
        {
            try
            {
                _ = Number(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static double Number(object value)
        {
            try
            {
                var str = value?.ToString();
                return str.ToLowerInvariant() switch
                {
                    "true" => 1,
                    "false" => 0,
                    _ => Convert.ToDouble(str)
                };
            }
            catch (Exception)
            {
                throw CastException.NewCouldNotBeCastedError();
            }
        }

        public static bool IsBoolean(object value)
        {
            try
            {
                _ = Boolean(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool Boolean(object value)
        {
            try
            {
                var str = value?.ToString();
                return str.ToLowerInvariant() switch
                {
                    "1" => true,
                    "0" => false,
                    _ => Convert.ToBoolean(str)
                };
            }
            catch (Exception)
            {
                throw CastException.NewCouldNotBeCastedError();
            }
        }

        public static bool IsDate(object value)
        {
            try
            {
                _ = Date(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static DateTime Date(object value)
        {
            try
            {
                return DateTimeOffset.Parse(value.ToString()).Date;
            }
            catch (Exception)
            {
                throw CastException.NewCouldNotBeCastedError();
            }
        }

        public static bool IsTime(object value)
        {
            try
            {
                _ = Time(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static int Time(object value)
        {
            try
            {
                var str = value?.ToString();

                var m = Regex.Match(str, @"(?i)^((?<hours>\d+)h)?((?<minutes>\d+)m)?((?<seconds>\d+)s)?$");
                if (m.Success)
                {
                    var hs = m.Groups["hours"].Success ? int.Parse(m.Groups["hours"].Value) : 0;
                    var ms = m.Groups["minutes"].Success ? int.Parse(m.Groups["minutes"].Value) : 0;
                    var ss = m.Groups["seconds"].Success ? int.Parse(m.Groups["seconds"].Value) : 0;

                    return hs * 60 * 60 + ms * 60 + ss;
                }

                if (Regex.Match(str, @"^([0-1]?\d|2[0-3])(:([0-5]?\d))(?::([0-5]?\d))?$").Success &&
                    TimeSpan.TryParse(str, out var ts))
                {
                    return Convert.ToInt32(ts.TotalSeconds);
                }

                if (int.TryParse(str, out var secs))
                {
                    return secs;
                }

                throw new Exception("Could not be parsed to time");
            }
            catch (Exception)
            {
                throw CastException.NewCouldNotBeCastedError();
            }
        }

        public static bool IsDateTime(object value)
        {
            try
            {
                _ = DateTime(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static DateTime DateTime(object value)
        {
            try
            {
                return DateTimeOffset.Parse(value.ToString()).DateTime;
            }
            catch (Exception)
            {
                throw CastException.NewCouldNotBeCastedError();
            }
        }
    }
}