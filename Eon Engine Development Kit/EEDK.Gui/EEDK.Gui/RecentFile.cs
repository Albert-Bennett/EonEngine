/* Created: 02/02/2015
 * Last Updated: 02/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;

namespace EEDK.Gui
{
    /// <summary>
    /// Defines a file that contains info about the most recently used files.
    /// </summary>
    public sealed class RecentFile
    {
        public List<string> Recent =
            new List<string>();

        /// <summary>
        /// Adds a filepath to the list, or reorders the list
        /// if the same filepath has been added.
        /// </summary>
        /// <param name="filepath">The filepath to be added.</param>
        public void Add(string filepath)
        {
            if (!Recent.Contains(filepath))
            {
                if (Recent.Count == 10)
                    Recent.RemoveAt(0);

                Recent.Add(filepath);
            }
            else
                if (Recent.Count > 1)
                {
                    bool found = false;
                    int idx = 0;

                    while (!found && idx < Recent.Count)
                    {
                        if (Recent[idx] == filepath)
                        {
                            Recent.RemoveAt(idx);
                            Recent.Insert(0, filepath);
                        }

                        idx++;
                    }
                }

            EEDK.Crosswalk.InterOperations.ProjectFilepath = filepath;
        }

        /// <summary>
        /// Removes a filepath from the list.
        /// </summary>
        /// <param name="filepath">The filepath to be removed.</param>
        public void Remove(string filepath)
        {
            if (Recent.Contains(filepath))
                Recent.Remove(filepath);
        }
    }
}
