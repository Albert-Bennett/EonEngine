/* Created 05/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Math.Shapes;
using System.Collections.Generic;

namespace Eon.Physics2D.Collision.Phases.BroadPhase
{
    /// <summary>
    /// Used to define an area in 2D space.
    /// </summary>
    internal struct Block
    {
        List<CollisionComponent> collides;

        Rectangle bounds;

        /// <summary>
        /// The id's of the objects that are 
        /// contained in this block partially or whoally.
        /// </summary>
        public List<CollisionComponent> Collides
        {
            get { return collides; }
        }

        /// <summary>
        /// The amount of objects in the current Block.
        /// </summary>
        public int Count
        {
            get { return collides.Count; }
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
            collides = new List<CollisionComponent>();
        }

        /// <summary>
        /// Clears info out of the Block.
        /// </summary>
        public void ClearBlock()
        {
            collides.Clear();
        }

        /// <summary>
        /// Adds the ID of an object to this.
        /// </summary>
        /// <param name="id">The id to be added.</param>
        public void Add(CollisionComponent collide)
        {
            collides.Add(collide);
        }
    }
}
