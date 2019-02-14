/* Created 06/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Eon.ContentPipeline
{
    /// <summary>
    /// A helper for serialization.
    /// </summary>
    public static class SerializationHelper
    {
        public static void GetParameters(string key, XmlDocument input, out ParameterCollection parameters)
        {
            string id = "";
            int paramCount = 0;

            foreach (XmlNode node in input.GetElementsByTagName(key)[0].ChildNodes)
                switch (node.Name)
                {
                    case "ID":
                        id = node.InnerText;
                        break;

                    case "Parameters":
                        int.TryParse(node.InnerText, out paramCount);
                        break;
                }

            parameters = new ParameterCollection(id);

            if (paramCount > 0)
                for (int i = 0; i < paramCount; i++)
                {
                    foreach (XmlNode node in input.GetElementsByTagName(key + "Param" + i)[0].ChildNodes)
                    {
                        string text = "";

                        switch (node.Name)
                        {
                            case "Value":
                                text = node.InnerText;
                                break;
                        }

                        object obj = null;

                        GetTypeValue(text, out obj);
                        parameters.Add(obj);
                    }
                }
        }

        public static void GetTypeValue(string text, out object output)
        {
            string[] values = text.Split(new[]
			{
				'_', ' '
			}, StringSplitOptions.RemoveEmptyEntries);

            output = null;

            if (values.Length >= 2)
            {
                switch (values[0])
                {
                    case "List":
                        {
                            Type t = GetType(values[1]);
                            IList objectList = null;

                            if (t != null)
                            {
                                var list = typeof(List<>).MakeGenericType(t);
                                objectList = (IList)Activator.CreateInstance(list);

                                string[] vals = Extract(values[2]);

                                foreach (string s in vals)
                                {
                                    object obj = null;

                                    string v = s.Replace('#', '_');
                                    SerializationHelper.GetTypeValue(v, out obj);

                                    objectList.Add(Convert.ChangeType(obj, t));
                                }
                            }

                            output = objectList;
                        }
                        break;

                    case "LLInt":
                        {
                            List<List<int>> llInt = new List<List<int>>();

                            for (int i = 1; i < values.Length; i++)
                            {
                                List<int> tileKeys = new List<int>();

                                string[] sKeys = values[i].Split(new[]
                                    {
                                        ',',' '
                                    }, StringSplitOptions.RemoveEmptyEntries);

                                for (int k = 0; k < sKeys.Length; k++)
                                    tileKeys.Add(Convert.ToInt32(sKeys[k]));

                                llInt.Add(tileKeys);
                            }

                            output = llInt;
                        }
                        break;

                    case "FDS2":
                        {
                            EonDictionary<string, string> dict =
                                new EonDictionary<string, string>();

                            string[] keys = text.Split(new[]
                                {
                                    ',', ' '
                                }, StringSplitOptions.RemoveEmptyEntries);

                            int count = 0;
                            int.TryParse(keys[1], out count);

                            int range = (count - 2) / 2;

                            if (range > 0)
                                for (int i = 0; i < range; i++)
                                    dict.Add(keys[i + 2], keys[i + 1 + range]);

                            output = dict;
                        }
                        break;

                    case "TimeSpan":
                        {
                            TimeSpan time = TimeSpan.Zero;
                            GetTimeSpan(values[1], out time);
                            output = time;
                        }
                        break;

                    case "Rectangle":
                        {
                            Rectangle rect = new Rectangle();
                            GetRectangle(values[1], out rect);
                            output = rect;
                        }
                        break;

                    case "Colour":
                        {
                            Color colour = new Color();
                            GetColour(values[1], out colour);
                            output = colour;
                        }
                        break;

                    case "Vector2":
                        {
                            Vector2 vec = Vector2.Zero;
                            GetVector2(values[1], out vec);
                            output = vec;
                        }
                        break;

                    case "Vector3":
                        {
                            Vector3 vec = Vector3.Zero;
                            GetVector3(values[1], out vec);
                            output = vec;
                        }
                        break;

                    case "Vector4":
                        {
                            Color col = Color.White;
                            GetColour(values[1], out col);

                            output = col.ToVector4();
                        }
                        break;

                    case "int":
                        {
                            int i;
                            int.TryParse(values[1], out i);

                            output = i;
                        }
                        break;

                    case "float":
                        {
                            float f;
                            float.TryParse(values[1], out f);

                            output = f;
                        }
                        break;

                    case "string":
                        {
                            output = values[1];
                        }
                        break;

                    default:
                        {
                            output = GetParameterCollection(values);
                        }
                        break;
                }
            }
        }

        static ParameterCollection GetParameterCollection(string[] values)
        {
            ParameterCollection parameters = new ParameterCollection(values[0]);

            for (int i = 1; i < values.Length; i += 2)
            {
                string val = values[i] + "_" + values[i + 1];

                object obj = null;

                GetTypeValue(val, out obj);

                parameters.Add(obj);
            }

            return parameters;
        }

        static Type GetType(string name)
        {
            Type t = null;

            switch (name)
            {
                case "string":
                    t = typeof(string);
                    break;

                case "int":
                    t = typeof(int);
                    break;

                case "float":
                    t = typeof(float);
                    break;

                case "Vector2":
                    t = typeof(Vector2);
                    break;

                case "Vector3":
                    t = typeof(Vector3);
                    break;

                case "Vector4":
                    t = typeof(Vector4);
                    break;

                case "Colour":
                    t = typeof(Color);
                    break;

                case "TimeSpan":
                    t = typeof(TimeSpan);
                    break;

                case "Rectangle":
                    t = typeof(Rectangle);
                    break;
            }

            return t;
        }

        public static XmlDocument LoadXmlFile(string filename, ContentImporterContext context)
        {
            XmlDocument file = new XmlDocument();

            try
            {
                file.Load(filename);
            }
            catch (Exception e)
            {
                context.Logger.LogImportantMessage
                    ("The file " + filename + " is not valid: " + e.Message);

                throw e;
            }

            return file;
        }

        /// <summary>
        /// Finds an enum value from a string.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="text">The text to be converted.</param>
        /// <returns>The value found</returns>
        public static T GetEnumValue<T>(string text)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), text);
            }
            catch
            {
                throw new ArgumentException("Invalid type given as an enum.");
            }
        }

        /// <summary>
        /// Returns a Rectangle from a string.
        /// </summary>
        /// <param name="text">The string to get the infomation from.</param>
        /// <param name="rect">The Rectangle to be set.</param>
        public static void GetRectangle(string text, out Rectangle rect)
        {
            rect = Rectangle.Empty;

            string[] values = Extract(text);

            if (values.Length == 4)
                rect = new Rectangle(Convert.ToInt32(values[0]),
                    Convert.ToInt32(values[1]), Convert.ToInt32(values[2]),
                    Convert.ToInt32(values[3]));
        }

        /// <summary>
        /// Returns a TimeSpan from a string.
        /// </summary>
        /// <param name="text">The string to get the infomation from.</param>
        /// <param name="time">The TimeSpan to use.</param>
        public static void GetTimeSpan(string text, out TimeSpan time)
        {
            string[] values = Extract(text);

            switch (values.Length)
            {
                case 2:
                    time = new TimeSpan(0, Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                    break;
                case 3:
                    time = new TimeSpan(Convert.ToInt32(values[0]),
                        Convert.ToInt32(values[1]), Convert.ToInt32(values[2]));
                    break;
                case 4:
                    time = new TimeSpan(Convert.ToInt32(values[0]),
                        Convert.ToInt32(values[1]), Convert.ToInt32(values[2]),
                        Convert.ToInt32(values[3]));
                    break;
                default:
                    time = TimeSpan.FromMilliseconds(Convert.ToDouble(values[0]));
                    break;
            }
        }

        /// <summary>
        /// Returns a Matrix from a string.
        /// </summary>
        /// <param name="text">The string to get the infomation from.</param>
        /// <param name="rect">The Matrix to be set.</param>
        public static void GetMatrix(string text, out Matrix matrix)
        {
            matrix = Matrix.Identity;

            string[] values = Extract(text);

            if (values.Length == 16)
                matrix = new Matrix(Convert.ToSingle(values[0]),
                    Convert.ToSingle(values[1]),
                    Convert.ToSingle(values[2]),
                    Convert.ToSingle(values[3]),
                    Convert.ToSingle(values[4]),
                    Convert.ToSingle(values[5]),
                    Convert.ToSingle(values[6]),
                    Convert.ToSingle(values[7]),
                    Convert.ToSingle(values[8]),
                    Convert.ToSingle(values[9]),
                    Convert.ToSingle(values[10]),
                    Convert.ToSingle(values[11]),
                    Convert.ToSingle(values[12]),
                    Convert.ToSingle(values[13]),
                    Convert.ToSingle(values[14]),
                    Convert.ToSingle(values[15]));
        }

        /// <summary>
        /// Returns a Vector2 from a given string.
        /// </summary>
        /// <param name="text">The text to get the values from.</param>
        /// <param name="vec">The generated Vector2.</param>
        public static void GetVector2(string text, out Vector2 vec)
        {
            vec = Vector2.Zero;

            string[] values = Extract(text);

            if (values.Length == 2)
                vec = new Vector2(Convert.ToSingle(values[0]),
                    Convert.ToSingle(values[1]));
        }

        /// <summary>
        /// Converts a string into a Vector2 that 
        /// can have negetive values but no decimals.
        /// </summary>
        /// <param name="text">The text ot be converted.</param>
        public static Vector2 GetPoint(string text)
        {
            string[] values = Extract(text);

            if (values.Length >= 2)
                return new Vector2(int.Parse(values[0]),
                    int.Parse(values[1]));

            return Vector2.Zero;
        }

        /// <summary>
        /// Returns a Vector3 from a given string.
        /// </summary>
        /// <param name="text">The text to get the values from.</param>
        /// <param name="vec">The generated Vector3.</param>
        public static void GetVector3(string text, out Vector3 vec)
        {
            vec = Vector3.Zero;

            string[] values = Extract(text);

            if (values.Length == 3)
                vec = new Vector3(Convert.ToSingle(values[0]),
                    Convert.ToSingle(values[1]), Convert.ToSingle(values[2]));
        }

        /// <summary>
        /// Returns a Vector4 from a given string.
        /// </summary>
        /// <param name="text">The text to get the values from.</param>
        /// <param name="vec">The generated Vector4.</param>
        public static void GetVector4(string text, out Vector4 vec)
        {
            vec = Vector4.Zero;

            string[] values = Extract(text);

            if (values.Length == 4)
                vec = new Vector4(Convert.ToSingle(values[0]),
                    Convert.ToSingle(values[1]), Convert.ToSingle(values[2]),
                    Convert.ToSingle(values[3]));
        }

        /// <summary>
        /// Returns a Color from a given string.
        /// </summary>
        /// <param name="text">The text to get the values from.</param>
        /// <param name="colour">The generated Color.</param>
        public static void GetColour(string text, out Color colour)
        {
            colour = Color.White;

            string[] values = Extract(text);

            if (values.Length == 4)
                colour = new Color(Convert.ToByte(values[0]),
                    Convert.ToByte(values[1]),
                    Convert.ToByte(values[2]),
                    Convert.ToByte(values[3]));
            else if (values.Length == 3)
                colour = new Color(Convert.ToByte(values[0]),
                    Convert.ToByte(values[1]),
                    Convert.ToByte(values[2]));
        }

        public static string[] Extract(string text)
        {
            text = text.Replace("(", "");
            text = text.Replace(")", "");
            text = text.Replace("{", "");
            text = text.Replace("}", "");

            string[] values = text.Split(new[]
			{
				',', ' '
			},
                StringSplitOptions.RemoveEmptyEntries);

            return values;
        }

        /// <summary>
        /// Converts a string to a number.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        /// <param name="value">The generated number.</param>
        public static void StringToNumber(string text, out int value)
        {
            value = int.Parse(text, CultureInfo.InvariantCulture);
        }
    }
}
