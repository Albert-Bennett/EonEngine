using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;
using System.Xml.Serialization;

namespace Eon.Helpers
{
    /// <summary>
    /// A helper class used to help with isolated storage.
    /// </summary>
    public static class SerializationHelper
    {
        /// <summary>
        /// Loads in an Xml file using isolated storage.
        /// </summary>
        /// <typeparam name="T">The type of document to be loaded in.</typeparam>
        /// <param name="filename">The file's name.</param>
        /// <param name="document">The object to be retured.</param>
        /// <param name="extraTypes">Extra types to be deserialized.</param>
        public static bool Load<T>(string filename, out T document, Type[] extraTypes)
        {
            try
            {
                using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (iso.FileExists(filename))
                        using (IsolatedStorageFileStream stream = iso.OpenFile(filename, FileMode.Open))
                        {
                            XmlSerializer serial = new XmlSerializer(typeof(T), extraTypes);
                            document = (T)serial.Deserialize(stream);
                            return true;
                        }
                    else
                        throw new ArgumentNullException("File not found: " + filename);
                }
            }
            catch
            {
                document = default(T);
                return false;
            }
        }

        /// <summary>
        /// Saves an Xml document using isolated storage.
        /// </summary>
        /// <typeparam name="T">The type of document to be saved.</typeparam>
        /// <param name="filename">The name of the file to be saved.</param>
        /// <param name="document">The object to be serialized.</param>
        /// <param name="extratypes">The extra types that can be serialized.</param>
        public static void Save<T>(string filename, T document, Type[] extratypes)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            try
            {
                using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (!iso.FileExists(filename))
                        iso.CreateFile(filename);

                    using (IsolatedStorageFileStream stream = iso.OpenFile(filename, FileMode.OpenOrCreate))
                    {
                        XmlSerializer serial = new XmlSerializer(typeof(T), extratypes);

                        using (XmlWriter writer = XmlWriter.Create(stream, settings))
                        {
                            serial.Serialize(writer, document);
                        }
                    }
                }
            }
            catch
            {
                throw new System.ArgumentException("Unable to save file, Filename: " + filename);
            }
        }
    }
}
