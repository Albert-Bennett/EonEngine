/* Created: 09/06/2013
 * Last Updated: 16/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Eon.Helpers
{
    /// <summary>
    /// Used to define a helper for serialization.
    /// </summary>
    public sealed class SerializationHelper
    {
        static readonly string RootFileName = "Content\\";

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
        /// <typeparam name="T">The type of object to be serialized.</typeparam>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="filepath">The place to save the object.</param>
        /// <param name="fileExtention">The ending of the filepath ie .xml.</param>
        /// <param name="extraTypes">Other types to be serialized.</param>
        public static void Serialize<T>(object obj, string filepath, string fileExtention, Type[] extraTypes)
        {
            using (FileStream stream = File.Open(filepath + fileExtention, FileMode.Create))
            {
                XmlSerializer serial = new XmlSerializer(typeof(T), extraTypes);
                serial.Serialize(stream, obj);
            }
        }

        /// <summary>
        /// Used to serialize data. Adds .xml
        /// extention to the serialized file.
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
        /// <param name="filepath">The filepath where the xml file is in.</param>
        /// <param name="fileExtention">The file extention.</param>
        /// <param name="content">Wheater or not it is content being de-serialized.</param>
        /// <param name="extraTypes">Any extra types that are contained in the 
        /// file that also need de-serializing.</param>
        /// <returns>The loaded in data.</returns>
        public static T Deserialize<T>(string filepath, bool content,
            string fileExtention, Type[] extraTypes)
        {
            if (content)
                filepath = RootFileName + filepath;

            filepath += fileExtention;

            using (XmlReader reader = XmlReader.Create(filepath))
            {
                XmlSerializer serial = new XmlSerializer(typeof(T), extraTypes);
                return (T)serial.Deserialize(reader);
            }
        }

        /// <summary>
        /// Used to de-serialize data.
        /// </summary>
        /// <param name="filepath">The filepath where the xml file is in.</param>
        /// <param name="fileExtention">The file extention.</param>
        /// <param name="content">Wheater or not it is content being de-serialized.</param>
        /// <returns>The loaded in data.</returns>
        public static T Deserialize<T>(string filepath, bool content, string fileExtention)
        {
            if (content)
                filepath = RootFileName + filepath;

            using (XmlReader reader = XmlReader.Create(filepath + fileExtention))
            {
                XmlSerializer serial = new XmlSerializer(typeof(T));
                return (T)serial.Deserialize(reader);
            }
        }

        /// <summary>
        /// Used to de-serialize data.
        /// </summary>
        /// <param name="filePath">The filepath where the file is.</param>
        /// <returns>The loaded in data.</returns>
        public static T Deserialize<T>(string filepath)
        {
            return Deserialize<T>(filepath, false, "");
        }

        /// <summary>
        /// Used to check if a folder exists.
        /// </summary>
        /// <param name="folderFilepath">The folders filepath.</param>
        /// <returns>The result opf the check.</returns>
        public static bool FolderExists(string folderFilepath)
        {
            return Directory.Exists(folderFilepath);
        }

        /// <summary>
        /// Creates a new folder.
        /// </summary>
        /// <param name="filepath">The folder filepath.</param>
        /// <returns>The result of the check.</returns>
        public static bool CreateFolder(string filepath)
        {
            if (!Directory.Exists(filepath))
                try
                {
                    Directory.CreateDirectory(filepath);
                    return true;
                }
                catch
                {
                    return false;
                }

            return true;
        }

        /// <summary>
        /// A check to see if a folder contains files.
        /// </summary>
        /// <param name="filepath">The folder's filepath.</param>
        /// <param name="extention">The file extention to search for.</param>
        /// <returns>The result of the check.</returns>
        public static IEnumerable<string> GetFiles(string filepath, string extention)
        {
            if (!Directory.Exists(filepath))
                return null;

            return Directory.EnumerateFiles(filepath, "*" + extention,
                SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Gets file paths for all files located at a certain directory.
        /// </summary>
        /// <param name="filepath">The root filepath.</param>
        /// <param name="extention">The extent for tjhe files to search for.</param>
        /// <returns>The result of the search.</returns>
        public static IEnumerable<string> GetAllFiles(string filepath, string extention)
        {
            if (!Directory.Exists(filepath))
                return null;

            return Directory.EnumerateFiles(filepath, "*" + extention,
                SearchOption.AllDirectories);
        }

        /// <summary>
        /// Coppies a file from one directory to another.
        /// </summary>
        /// <param name="sourceDirectory">Where the file is comming from.</param>
        /// <param name="destination">Where the file is to be coppied to.</param>
        /// <param name="createFolder">Should a destination folder be created if none exists.</param>
        /// <returns>Was the selected file coppied.</returns>
        public static bool CopyFile(string sourceDirectory, string destination, bool createFolder)
        {
            if (File.Exists(sourceDirectory))
            {
                if (!Directory.Exists(destination) && createFolder)
                    CreateFolder(destination);

                string[] split = sourceDirectory.Split(
                    new char[] { '\\', '/' }, 
                    StringSplitOptions.RemoveEmptyEntries);

                File.Copy(sourceDirectory, destination + "\\" + split[split.Length - 1], true);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Coppies files from one directory to another.
        /// </summary>
        /// <param name="currentDirectory">The directory where the files are.</param>
        /// <param name="destination">The directory where the files should be copied to.</param>
        /// <param name="extention">The file extention that is used to identify which files should be copied.</param>
        /// <param name="identifier">A string that is used to identify which files should be copied.</param>
        /// <param name="createFolder">Should a destination folder be creted if one doesn't exist.</param>
        /// <returns>True if the files were copied succesfully.</returns>
        public static bool CopyFiles(string currentDirectory, string destination,
            string extention, string identifier, bool createFolder)
        {
            if (Directory.Exists(currentDirectory))
            {
                if (!Directory.Exists(destination) && createFolder)
                    CreateFolder(destination);

                string[] files = Directory.GetFiles(currentDirectory);

                List<string> actual = (from s in files
                                       where s.Contains(identifier) &
                                       s.EndsWith(extention)
                                       select s).ToList<string>();

                for (int i = 0; i < actual.Count; i++)
                    File.Copy(actual[i], destination + "/" + (actual[i].Remove(0, currentDirectory.Length)));

                return true;
            }

            return false;
        }

        /// <summary>
        /// Copies all files from a directory and it's sub directories.
        /// </summary>
        /// <param name="currentDirectory">The directory where the files are.</param>
        /// <param name="destination">The directory where the files should be copied to.</param>
        /// <param name="extention">The file extention that is used to identify which files should be copied.</param>
        /// <param name="createFolders">Should the destination folder and any other sub
        /// folders be created in they don't exist</param>
        /// <returns>True if the files were copied succesfully.</returns>
        public static bool CopyFiles(string currentDirectory, string destination,
            string extention, bool createFolders)
        {
            if (Directory.Exists(currentDirectory))
            {
                if (!Directory.Exists(destination) && createFolders)
                    CreateFolder(destination);

                string[] files = Directory.GetFiles(currentDirectory, "*" + extention, SearchOption.AllDirectories);
                string[] subFolders = Directory.GetDirectories(currentDirectory, "*", SearchOption.AllDirectories);

                foreach (string s in subFolders)
                {
                    string path = destination + (s.Remove(0, currentDirectory.Length));

                    if (!Directory.Exists(path))
                        SerializationHelper.CreateFolder(path);
                }

                foreach (string s in files)
                    File.Copy(s, destination + (s.Remove(0, currentDirectory.Length)));

                return true;
            }

            return false;
        }

        /// <summary>
        /// Used to check if a file all ready exists at a specific filepath.
        /// </summary>
        /// <param name="filepath">The filepath of file to search for.</param>
        /// <returns>The result of the check.</returns>
        public static bool FileExists(string filepath)
        {
            return File.Exists(filepath);
        }

        /// <summary>
        /// Deletes a file at a directory. 
        /// </summary>
        /// <param name="filepath">The file to be deleted.</param>
        /// <returns>True if the file was deleted or there was no file found.</returns>
        public static bool DeleteFile(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                    File.Delete(filepath);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the name of a folder at the given filepath.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns>The name of the folder.</returns>
        public static string GetFolderName(string filepath)
        {
            string[] text = filepath.Split(new char[]
            {
                '\\','/'
            });

            return text[text.Length - 1];
        }
    }
}
