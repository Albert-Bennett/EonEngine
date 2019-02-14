/* Created 15/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine.Management
{
    public class TileLayerDeffination
    {
        /// <summary>
        /// the amount of columns in 
        /// the tile sheet.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// The amount of rows in the tile sheet.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// The total amount of seperate tile images in the tile sheet.
        /// </summary>
        public int TotalTileImages { get; set; }

        /// <summary>
        /// The square size of each Tile.
        /// </summary>
        public Vector2 TileSize { get; set; }

        /// <summary>
        /// Offset when positioning Tiles.
        /// </summary>
        public Vector2 TileOffset { get; set; }

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
        public int[,] Tiles;
    }
}
