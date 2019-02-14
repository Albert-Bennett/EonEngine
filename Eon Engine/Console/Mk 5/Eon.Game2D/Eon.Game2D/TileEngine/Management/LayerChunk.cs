/* Created 29/09/2013
 * Last Updated: 21/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Game2D.TileEngine.Management
{
    /// <summary>
    /// Defines a group of Tiles which have a 
    /// total size equal to or slightly greater
    /// than the size of the screen.
    /// </summary>
    public struct LayerChunk
    {
        public Rectangle Bounds;
        public List<int> Indices;
    }
}
