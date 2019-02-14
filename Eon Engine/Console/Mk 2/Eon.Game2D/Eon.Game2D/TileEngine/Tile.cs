/* Created 15/09/2013
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
        internal Vector2 _Position { get; set; }

        public Vector2 Size { get; set; }
        public int RefferenceNumber { get; set; }
        public int Number { get; set; }

        /// <summary>
        /// Returns the bounds of the Tile. 
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)_Position.X,
                    (int)_Position.Y, (int)Size.X, (int)Size.Y);
            }
        }
    }
}
