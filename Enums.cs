using System;
using System.Text.RegularExpressions;

namespace Enums
{
    public static class EnumExtensions
    {
        //Break the enum apart based on changes in case.
        public static string ToText(this Enum value)
        {
            string input = value.ToString();
            string output = Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
            return output;
        }

        public static int ToInt(this Enum value)
        {
            return Convert.ToInt32(value);
        }
        public static long ToLong(this Enum value)
        {
            return Convert.ToInt64(value);
        }

        public static T ToEnum<T>(this int value)
        {
            return (T)Enum.Parse(typeof(T), value.ToString());
        }

        public static string ToText<T>(this int value)
        {
            var enumeration = (T)Enum.Parse(typeof(T), value.ToString());
            string input = enumeration.ToString();
            string output = Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
            return output;
        }
    }
}
