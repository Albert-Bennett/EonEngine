/* Created: 24/09/2015
 * Last Updated: 24/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision.Phases.BroadPhase;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Collision
{
    /// <summary>
    /// Defines an object that is used to set the size of the world.
    /// </summary>
    public sealed class WorldLimiter
    {
        /// <summary>
        /// Creates a new WorldLimiter.
        /// </summary>
        /// <param name="worldSize">The size of the world.</param>
        /// <param name="blockSize">The size of each block.</param>
        public WorldLimiter(Vector2 worldSize, int blockSize)
        {
            worldSize *= Common.UpScale;

            BlockManager.SetWorldSize(worldSize, blockSize);
        }

        /// <summary>
        /// Creates a new WorldLimiter.
        /// </summary>
        /// <param name="blockSize">The size of each block.</param>
        public WorldLimiter(int blockSize)
        {
            BlockManager.SetWorldSize(Common.TextureQuality, blockSize);
        }
    }
}
