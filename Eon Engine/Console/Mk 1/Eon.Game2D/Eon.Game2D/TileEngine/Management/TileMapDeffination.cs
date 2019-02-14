/* Created 15/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using System.Collections.Generic;

namespace Eon.Game2D.TileEngine.Management
{
    /// <summary>
    /// Used to define a TileMap.
    /// </summary>
    public class TileMapDeffination
    {
        /// <summary>
        /// Wheater or not the generated 
        /// TileMap should be post rendered.
        /// </summary>
        public bool PostRender { get; set; }

        /// <summary>
        /// The TileLayerDeffinations that
        /// will make up the generate TileMap.
        /// </summary>
        public List<TileLayerDeffination> TileLayers =
            new List<TileLayerDeffination>();
    }
}
