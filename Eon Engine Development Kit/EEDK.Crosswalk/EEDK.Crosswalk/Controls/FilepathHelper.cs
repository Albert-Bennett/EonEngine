/* Created: 16/01/2015
 * Last Updated: 16/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;

namespace EEDK.Crosswalk.Controls
{
    /// <summary>
    /// defines a helper for geting loaded filepaths.
    /// </summary>
    public static class FilepathHelper
    {
        /// <summary>
        /// Gets the filepath for a content asset.
        /// </summary>
        /// <param name="filepath">The asset's actual filepath.</param>
        /// <param name="exclude">The end of the exclusion path.</param>
        /// <returns></returns>
        public static string GetFilePath(string filepath, string exclude)
        {
            string[] txt = filepath.Split(new char[]
            {
                '.'
            }, StringSplitOptions.RemoveEmptyEntries);

            string res = txt[0];
            string[] final = res.Split(new string[]
            {
                exclude
            }, StringSplitOptions.RemoveEmptyEntries);

            return final[1];
        }

        /// <summary>
        /// Gets the name of a file that their is a filepath for.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns>The name of the file.</returns>
        public static string GetFileName(string filepath)
        {
            string[] s = filepath.Split(new char[]
            {
                '\\','.'
            }, StringSplitOptions.RemoveEmptyEntries);

            return s[s.Length - 2];
        }
    }
}
