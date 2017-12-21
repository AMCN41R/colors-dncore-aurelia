using System;
using System.Collections.Generic;
using System.Linq;

namespace ColorsTest.Core
{
    public static class Extensions
    {
        public static bool IsPalindrome(this string value, bool respectWhitespace = false)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            if (!respectWhitespace)
            {
                value = value.RemoveWhitespace();
            }

            return value.ToLower() == new string(value.ToLower().ToCharArray().Reverse().ToArray());
        }

        public static string RemoveWhitespace(this string str)
        {
            return new string(str.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        public static void RemoveWhere<T>(this IList<T> list, Func<T, bool> predicate)
        {
            list.Remove(list.FirstOrDefault(predicate));
        }
    }
}