/* Created: 05/11/2013
 * Last Updated: 26/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Maths;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Physics2D.Collision.Phases.BroadPhase
{
    /// <summary>
    /// Used to define an area in 2D space.
    /// </summary>
    internal struct Block : IHoldReferences
    {
        List<SimplePhysicsComponent> collides;

        Rectangle bounds;
        Vector2 center;

        /// <summary>
        /// The id's of the objects that are 
        /// contained in this block partially or whoally.
        /// </summary>
        public List<SimplePhysicsComponent> Collides
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

        public Vector2 Center
        {
            get { return center; }
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
            bounds = new Rectangle(x, y, size, size);
            collides = new List<SimplePhysicsComponent>();

            center = new Vector2(bounds.Center.X, bounds.Center.Y);
        }

        /// <summary>
        /// Adds the ID of an object to this.
        /// </summary>
        /// <param name="id">The id to be added.</param>
        public void Add(SimplePhysicsComponent collide)
        {
            SimplePhysicsComponent phy = (from p in collides
                                          where p.ID == collide.ID
                                          select p).FirstOrDefault();

            if (phy == null)
            {
                collide.AddReference(this);
                collides.Add(collide);
            }
        }

        /// <summary>
        /// Resolves all collision detection.
        /// </summary>
        public void Resolve()
        {
            if (collides.Count > 1)
            {
                int count = collides.Count;

                while (collides.Count > 1 || count > 1)
                {
                    for (int i = 1; i < collides.Count; i++)
                    {
                        MTV result = MTV.Initial;

                        if (!collides[0].Collides.Contains(collides[i].Owner.ID))
                        {
                            if (collides[i].Bounds is BoundingCircle)
                                result = collides[0].Bounds.Contains(
                                    collides[i].Bounds as BoundingCircle);
                            else
                                result = collides[0].Bounds.Contains(
                                    collides[i].Bounds as BoundingRectangle);

                            if (result != MTV.Initial)
                                collides[0].Collided(result, collides[i].Owner);
                        }
                    }

                    if (collides.Count > 0)
                    {
                        collides[0].RemoveReference(this);
                        collides.Remove(collides[0]);
                    }

                    count--;
                }

                collides.Clear();
            }
        }

        public void Remove(object obj)
        {
            if (obj is SimplePhysicsComponent)
                collides.Remove(obj as SimplePhysicsComponent);
        }
    }
}
