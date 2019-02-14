/* Created 09/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using System.Collections.Generic;

namespace Eon.Helpers
{
    /// <summary>
    /// Defines a helper class that has methods 
    /// specifically used for sorting non generic lists.
    /// </summary>
    public static class SortHelper
    {
        /// <summary>
        /// Used to sort a list of items.
        /// </summary>
        /// <typeparam name="T">The type of list to be sorted where 
        /// aech item is a predicesor of ISortable.</typeparam>
        /// <param name="toBeSorted">The list to be sorted.</param>
        /// <param name="sortedList">The sorted list.</param>
        public static void SortList<T>(ref List<T> toBeSorted, out List<T> sortedList) where T : ISortable
        {
            sortedList = new List<T>();

            if (toBeSorted.Count > 1)
            {
                int swapNum = 0;
                int swaps = 0;

                while (swaps < toBeSorted.Count)
                {
                    for (int i = 0; i < toBeSorted.Count; i++)
                        if (toBeSorted[i].Priority == swapNum)
                        {
                            sortedList.Add(toBeSorted[i]);
                            swaps++;
                        }

                    swapNum++;
                }

                sortedList.Reverse();
            }
            else
                sortedList = toBeSorted;
        }
    }
}
