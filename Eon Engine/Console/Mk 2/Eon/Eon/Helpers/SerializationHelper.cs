using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eon.Helpers
{
    /// <summary>
    /// Used to define a helper of serialization.
    /// </summary>
    public sealed class SerializationHelper
    {
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
        /// Used to check if a file all ready exists at a specific filepath.
        /// </summary>
        /// <param name="filepath">The filepath of file to search for.</param>
        /// <returns>The result of the check.</returns>
        public static bool FileExists(string filepath)
        {
            return File.Exists(filepath);
        }
    }
}
