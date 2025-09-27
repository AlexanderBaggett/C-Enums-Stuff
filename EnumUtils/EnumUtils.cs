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

        public static List<int> GetAllPossibleValues<T>() where T : struct, Enum
        {
            var enumType = typeof(T);
            var isFlagsEnum = enumType.IsDefined(typeof(FlagsAttribute), false);

            var values = Enum.GetValues<T>().Select(x => x.ToInt()).ToList();
            if (!isFlagsEnum)
            {
                return values;
            }

            //combine all possible values (bitwise)
            var sum = values.Aggregate(0, (acc, val) => acc | val);

            values.Clear();
            for (int i = 0; i < sum; i++)
            {
                if ((i & sum) == i)
                {
                    values.Add(i);
                }
            }
            return values;
        }

        private static List<int> GetFlagEnumPossibleValues<T>() where T : struct, Enum
        {
            var values = Enum.GetValues<T>().Select(x => x.ToInt()).ToList();
            var sum = values.Aggregate(0, (acc, val) => acc | val);

            values.Clear();
            for (int i = 0; i < sum; i++)
            {
                if ((i & sum) == i)
                {
                    values.Add(i);
                }
            }
            return values;
        }

        public static T ToEnum<T>(this int value) where T : struct, Enum
        {
            var enumType = typeof(T);
            var isFlagsEnum = enumType.IsDefined(typeof(FlagsAttribute), false);

            if (!isFlagsEnum)
            {
                if (!Enum.IsDefined(enumType, value))
                {
                    throw new ArgumentException($"Value {value} is not defined in enum {enumType.Name}");
                }
                return (T)Enum.Parse(enumType, value.ToString());
            }
            else
            {
                if (!GetFlagEnumPossibleValues<T>().Contains(value))
                {
                    throw new ArgumentException($"Value {value} is not a valid combination in enum {enumType.Name}");
                }
                return (T)Enum.Parse(enumType, value.ToString());
            }
        }

        public static string ToText<T>(this int value) where T : struct, Enum
        {
            var enumType = typeof(T);
            var isFlagsEnum = enumType.IsDefined(typeof(FlagsAttribute), false);

            if (!isFlagsEnum)
            {
                if (!Enum.IsDefined(enumType, value))
                {
                    throw new ArgumentException($"Value {value} is not defined in enum {enumType.Name}");
                }
            }
            else
            {
                if (!GetFlagEnumPossibleValues<T>().Contains(value))
                {
                    throw new ArgumentException($"Value {value} is not a valid combination in enum {enumType.Name}");
                }
            }

            var enumeration = (T)Enum.Parse(enumType, value.ToString());
            string input = enumeration.ToString();
            string output = Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
            return output;
        }

        public static List<string> ToTextList<T>() where T : struct, Enum
        {
            return Enum.GetValues<T>().Select(x => x.ToText()).ToList();
        }
        public static List<string> ToTextList<Type>(this Type t) where Type : struct, Enum
        {
            return Enum.GetValues<Type>().Select(x => x.ToText()).ToList();
        }
    }
}
