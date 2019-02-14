/* Created 15/09/2013
 * Last Updated: 18/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine.Management
{
    /// <summary>
    /// Defines a TileLayer.
    /// </summary>
    public sealed class TileLayerDeffination
    {
        /// <summary>
        /// The order of the TileLayer.
        /// </summary>
        public int Order = 0;

        /// <summary>
        /// the amount of columns in 
        /// the tile sheet.
        /// </summary>
        public int Columns;

        /// <summary>
        /// The amount of rows in the tile sheet.
        /// </summary>
        public int Rows;

        /// <summary>
        /// The square size of each Tile.
        /// </summary>
        public Vector2 TileSize;

        /// <summary>
        /// Offset when positioning Tiles.
        /// </summary>
        public Vector2 TileOffset = new Vector2(-2, -2);

        /// <summary>
        /// The filepath for the tile sheet.
        /// </summary>
        public string TileSheetFilepath = "None";

        /// <summary>
        /// The filepath for the TileLayer's normal map.
        /// </summary>
        public string NormalMapFilepath = "None";

        /// <summary>
        /// The filepath for the TileLayer's distortion map.
        /// </summary>
        public string DistortionMapFilepath = "None";

        /// <summary>
        /// An list of list of int that defines what 
        /// row/column the Tiles lies on.
        /// </summary>
        public List<int[]> Tiles;

        /// <summary>
        /// Updates the Tiles in the TileLayerDeffination.
        /// </summary>
        /// <param name="value">The value new number of either columns or rows.</param>
        /// <param name="columns">Is the value related to columns.</param>
        public void UpdateTiles(int value, bool columns)
        {
            if (value < 1)
                value = 1;

            if (columns)
            {
                if (value - 1 > Tiles[0].Length)
                {
                    for (int i = 0; i < Tiles.Count; i++)
                    {
                        List<int> t = new List<int>(Tiles[i]);

                        int len = t.Count;

                        for (int j = len; j < value; j++)
                            t.Add(-1);

                        Tiles[i] = t.ToArray();
                    }
                }
                else if(value - 1 < Tiles[0].Length)
                {
                    int dif = Tiles[0].Length - value;

                    for (int i = 0; i < Tiles.Count; i++)
                    {
                        List<int> t = new List<int>(Tiles[i]);
                        t.RemoveRange(t.Count - dif, dif);

                        Tiles[i] = t.ToArray();
                    }
                }
            }
            else
            {
                if (value > Tiles.Count)
                {
                    int len = Tiles[0].Length;
                    int count = Tiles.Count;

                    for (int i = count; i < value; i++)
                    {
                        int[] row = new int[len];

                        for (int j = 0; j < len; j++)
                            row[j] = -1;

                        Tiles.Add(row);
                    }
                }
                else
                {
                    int dif = Tiles.Count - value;

                    Tiles.RemoveRange(Tiles.Count - dif, dif);
                }
            }
        }

        /// <summary>
        /// Sets the source index of a tile.
        /// </summary>
        /// <param name="index">The index of the image to be used.</param>
        /// <param name="row">Tile row.</param>
        /// <param name="column">Tile column.</param>
        public void SetIndex(int index, int row, int column)
        {
            Tiles[row][column] = index;
        }
    }
}
