/* Created 05/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Collections;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Collision.Phases.BroadPhase
{
    /// <summary>
    /// Used to define an area in 2D space.
    /// </summary>
    internal struct Block
    {
        EonList<string> objIDs;

        Rectangle bounds;

        /// <summary>
        /// The id's of the objects that are 
        /// contained in this block partially or whoally.
        /// </summary>
        public EonList<string> ObjectIDs
        {
            get { return objIDs; }
        }

        /// <summary>
        /// The bounding area of the Block.
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// Creates a new Block.
        /// </summary>
        public Block(int x, int y, int size)
        {
            bounds = new Rectangle(x * size, y * size, size, size);

            objIDs = new EonList<string>();
        }

        /// <summary>
        /// Clears info out of the Block.
        /// </summary>
        public void ClearBlock()
        {
            objIDs.ClearAll();
        }

        /// <summary>
        /// Adds the ID of an object to this.
        /// </summary>
        /// <param name="id">The id to be added.</param>
        public void AddID(string id)
        {
            objIDs.Add(id);
        }
    }
}
