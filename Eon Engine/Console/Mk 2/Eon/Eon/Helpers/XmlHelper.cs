/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Eon.Helpers
{
    public sealed class XmlHelper
    {
        static readonly string RootFileName = "Content/";

        /// <summary>
        /// Used to serialize data.
        /// </summary>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="filepath">The desired filpath for the file to reside in.</param>
        /// <param name="fileExtention">The ending of the filepath ie .xml.</param>
        public static void Serialize<T>(object obj, string filepath, string fileExtention)
        {
            using (FileStream stream = File.Open(filepath + fileExtention, FileMode.Create))
            {
                XmlSerializer serial = new XmlSerializer(typeof(T));
                serial.Serialize(stream, obj);
            }
        }

        /// <summary>
        /// Used to serialize data.
        /// </summary>
        /// <typeparam name="T">The type of object to be serialized.</typeparam>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="filepath">The place to save the object.</param>
        /// <param name="extraTypes">Other types to be serialized.</param>
        public static void Serialize<T>(object obj, string filepath, Type[] extraTypes)
        {
            using (FileStream stream = File.Open(filepath, FileMode.Create))
            {
                XmlSerializer serial = new XmlSerializer(typeof(T), extraTypes);
                serial.Serialize(stream, obj);
            }
        }

        /// <summary>
        /// Used to serialize data.
        /// </summary>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="filePath">The desired filpath for the file to reside in.</param>
        public static void Serialize<T>(object obj, string filePath)
        {
            Serialize<T>(obj, filePath, ".xml");
        }

        /// <summary>
        /// Used to de-serialize data.
        /// </summary>
        /// <param name="filePath">The filepath where the xml file is in.</param>
        /// <param name="fileExtention">The file extention.</param>
        /// <param name="content">Wheater or not it is content being de-serialized.</param>
        /// <param name="extraTypes">Extra types to be de-serialized.</param>
        /// <returns>The loaded in data.</returns>
        public static T Deserialize<T>(string filePath, string fileExtention, bool content, Type[] extraTypes)
        {
            if (content)
                filePath = RootFileName + filePath;

            using (XmlReader reader = XmlReader.Create(filePath + fileExtention))
            {
                XmlSerializer serial = new XmlSerializer(typeof(T), extraTypes);
                return (T)serial.Deserialize(reader);
            }
        }

        /// <summary>
        /// Used to de-serialize data.
        /// </summary>
        /// <param name="filePath">The filepath where the xml file is in.</param>
        /// <param name="fileExtention">The file extention.</param>
        /// <param name="content">Wheater or not it is content being de-serialized.</param>
        /// <returns>The loaded in data.</returns>
        public static T Deserialize<T>(string filePath, string fileExtention, bool content)
        {
            if (content)
                filePath = RootFileName + filePath;

            using (XmlReader reader = XmlReader.Create(filePath + fileExtention))
            {
                XmlSerializer serial = new XmlSerializer(typeof(T));
                return (T)serial.Deserialize(reader);
            }
        }

        /// <summary>
        /// Used to deserialize data.
        /// Adds .xml to file path.
        /// </summary>
        /// <param name="filePath">The filepath where the xml file is in.</param>
        /// <returns>The loaded in data.</returns>
        public static T Deserialize<T>(string filepath)
        {
            return Deserialize<T>(filepath, ".xml", false);
        }

        /// <summary>
        /// Used to deserialize content data.
        /// </summary>
        /// <param name="filePath">The filepath where the xml file is in.</param>
        /// <returns>The loaded in data.</returns>
        public static T DeserializeContent<T>(string filepath)
        {
            return Deserialize<T>(filepath, "", true);
        }
    }
}
