﻿/* Created: 09/06/2013
 * Last Updated: 12/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Eon.Helpers
{
    /// <summary>
    /// Defines an extention for string.
    /// </summary>
    public static class StringHelper
    {
        #region Number to string

        /// <summary>
        /// Converts a string to a type.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The string as the specified type.</returns>
        public static T StringToType<T>(string value, T defaultValue)
        {
            if (defaultValue != null && value != null)
                if (defaultValue is float)
                    return (T)(Convert.ToSingle(value, CultureInfo.InvariantCulture) as object);
                else if (defaultValue is decimal)
                    return (T)(Convert.ToDecimal(value) as object);
                else if (defaultValue is double)
                    return (T)(Convert.ToDouble(value) as object);
                else if (defaultValue is int)
                    return (T)(Convert.ToInt32(value) as object);
                else if (defaultValue is DateTime)
                    return (T)(Convert.ToDateTime(value) as object);
                else if (defaultValue is string)
                    return (T)(Convert.ToString(value) as object);
                else if (defaultValue is char)
                    return (T)(Convert.ToChar(value) as object);
                else if (defaultValue is bool)
                    return (T)(Convert.ToBoolean(value) as object);
                else
                    throw new ArgumentException(string.Format("The value {0} couldn't be converted to {1}", value, typeof(T)));
            else
                throw new ArgumentException(string.Format("The value {0} can't be null or the default value can't be null {1}", value, typeof(T)));
        }

        /// <summary>
        /// Converts a float to a string.
        /// </summary>
        /// <param name="value">The float to convert.</param>
        /// <returns>The float as a string.</returns>
        public static string NumberToString(float value)
        {
            return value.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a float to a string.
        /// </summary>
        /// <param name="value">The float to convert.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>The float as a string.</returns>
        public static string NumberToString(float value, string format)
        {
            return value.ToString(format, NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a double to a string.
        /// </summary>
        /// <param name="value">The double to convert.</param>
        /// <returns>The double as a string.</returns>
        public static string NumberToString(double value)
        {
            return value.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a double to a string.
        /// </summary>
        /// <param name="value">The double to convert.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>The double as a string.</returns>
        public static string NumberToString(double value, string format)
        {
            return value.ToString(format, NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a decimal to a string.
        /// </summary>
        /// <param name="value">The decimal to convert.</param>
        /// <returns>The decimal as a string.</returns>
        public static string NumberToString(decimal value)
        {
            return value.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a decimal to a string.
        /// </summary>
        /// <param name="value">The decimal to convert.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>The decimal as a string.</returns>
        public static string NumberToString(decimal value, string format)
        {
            return value.ToString(format, NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a byte to a string.
        /// </summary>
        /// <param name="value">The byte to convert.</param>
        /// <returns>The byte as a string.</returns>
        public static string NumberToString(byte value)
        {
            return value.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a byte to a string.
        /// </summary>
        /// <param name="value">The byte to convert.</param>
        /// <param name="format">The format to use.</param>
        /// <returns>The byte as a string.</returns>
        public static string NumberToString(byte value, string format)
        {
            return value.ToString(format, NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a number object to a string.
        /// </summary>
        /// <param name="value">The number object to convert.</param>
        /// <returns>The number object as a string.</returns>
        public static string NumberToString(object value)
        {
            if (value != null)
                if (value is float)
                    return NumberToString((float)value);
                else if (value is decimal)
                    return NumberToString((decimal)value);
                else if (value is double)
                    return NumberToString((double)value);
                else if (value is byte)
                    return NumberToString((byte)value);

            return "";
        }

        #endregion
        #region Split remove
        /// <summary>
        /// Splits up the given string and removes and empty spaces.
        /// </summary>
        /// <param name="text">The text to eddited.</param>
        /// <param name="seperator">The seperator to use.</param>
        /// <returns>The editted string.</returns>
        public static string[] SplitRemove(string text, char seperator)
        {
            List<string> res = new List<string>();

            string[] edited = text.Split(new[] { seperator });

            foreach (string s in edited)
                if (s.Length > 0)
                    res.Add(s.Trim());

            return res.ToArray();
        }

        /// <summary>
        /// Splits up the given string and removes and empty spaces.
        /// </summary>
        /// <param name="text">The text to eddited.</param>
        /// <param name="seperator">The seperator to use.</param>
        /// <returns>The editted string.</returns>
        public static string[] SplitRemove(string text, string seperator)
        {
            List<string> res = new List<string>();

            string[] edited = text.Split(new[] { seperator }, StringSplitOptions.None);

            foreach (string s in edited)
                if (s.Length > 0)
                    res.Add(s.Trim());

            return res.ToArray();
        }

        /// <summary>
        /// Splits up the given string and removes and empty spaces.
        /// </summary>
        /// <param name="text">The text to eddited.</param>
        /// <param name="seperator">The seperator to use.</param>
        /// <returns>The editted string.</returns>
        public static string[] SplitRemove(string text, char[] seperators)
        {
            List<string> res = new List<string>();

            string[] edited = text.Split(seperators);

            foreach (string s in edited)
                if (s.Length > 0)
                    res.Add(s.Trim());

            return res.ToArray();
        }

        #endregion
        #region Conversions

        /// <summary>
        /// Converts a char array into a string.
        /// </summary>
        /// <param name="charArray">The array of char that hold the information.</param>
        /// <param name="formedString">The newly formed string.</param>
        public static void StringFromArray(ref char[] charArray, out string formedString)
        {
            int idx = 0;
            formedString = "";

            while (idx < charArray.Length)
            {
                formedString += charArray[idx];
                idx++;
            }
        }

        /// <summary>
        /// Converts a char list into a string.
        /// </summary>
        /// <param name="charList">The list of char that hold the information.</param>
        /// <param name="formedString">The newly formed string.</param>
        public static void StringFromList(ref List<char> charList, out string formedString)
        {
            int idx = 0;
            formedString = "";

            while (idx < charList.Count)
            {
                formedString += charList[idx];
                idx++;
            }
        }

        /// <summary>
        /// Converts a string to a bool.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        /// <returns>The converted string.</returns>
        public static bool BoolFromString(string text)
        {
            bool b = false;

            if (bool.TryParse(text, out b))
                return b;

            return false;
        }

        /// <summary>
        /// Converts a string to a float.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        /// <returns>The converted string.</returns>
        public static float FloatFromString(string text)
        {
            float f = 0.0f;

            if (float.TryParse(text, out f))
                return f;

            return 0f;
        }

        #endregion
    }
}