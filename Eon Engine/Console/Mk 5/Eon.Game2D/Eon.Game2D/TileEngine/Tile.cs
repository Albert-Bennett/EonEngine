/* Created 15/09/2013
 * Last Updated: 21/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Game2D.TileEngine
{
    /// <summary>
    /// Defines a Tile in a TileLayer.
    /// </summary>
    public struct Tile
    {
        public int Index;
        public int Column;
        public int Row;

        public Rectangle Bounds;
    }
}
