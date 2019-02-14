/* Created: 05/11/2013
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Maths.Shapes;
using Eon.System.Interfaces;
using System.Collections.Generic;

namespace Eon.Physics2D.Collision.Phases.BroadPhase
{
    /// <summary>
    /// Used to define an area in 2D space.
    /// </summary>
    internal struct Block:IHoldReferences
    {
        List<PhysicsComponent> collides;

        Rectangle bounds;

        /// <summary>
        /// The id's of the objects that are 
        /// contained in this block partially or whoally.
        /// </summary>
        public List<PhysicsComponent> Collides
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
            collides = new List<PhysicsComponent>();
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
        public void Add(PhysicsComponent collide)
        {
            collides.Add(collide);
        }

        public void Remove(object obj)
        {
            collides.Remove(obj as PhysicsComponent);
        }
    }
}
