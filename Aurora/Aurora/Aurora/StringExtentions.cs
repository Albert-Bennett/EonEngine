/* Created: 28/08/2015
 * Last Updated: 28/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Aurora
{
    /// <summary>
    /// Used to concatenate strings.
    /// </summary>
    internal static class StringExtentions
    {
        public static string Concatenate(this IEnumerable<string> source)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string s in source)
                sb.Append(s);

            return sb.ToString();
        }

        public static string Concatenate<T>(this IEnumerable<T> source,
            Func<T, string> func)
        {
            StringBuilder sb = new StringBuilder();

            foreach (T item in source)
                sb.Append(func(item));

            return sb.ToString();
        }

        public static string Concatenate(this IEnumerable<string> source, string separator)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string s in source)
                sb.Append(s).Append(separator);

            return sb.ToString();
        }

        public static string Concatenate<T>(this IEnumerable<T> source,
            Func<T, string> func, string separator)
        {
            StringBuilder sb = new StringBuilder();

            foreach (T item in source)
                sb.Append(func(item)).Append(separator);

            return sb.ToString();
        }
    }
}
