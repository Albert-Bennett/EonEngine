/* Created 10/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.Collision;
using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Physics2D.Math;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Physics2D.Forces.Volumes
{
    /// <summary>
    /// Defines an area that applies a force on objects in it.
    /// </summary>
    public abstract class PhysicsVolume : IVolumetricForce
    {
        CollisionObject collide;

        List<string> collisionIds = new List<string>();

        string id;

        /// <summary>
        /// The ID of this PhysicsVolume.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a new PhysicsVolume.
        /// </summary>
        /// <param name="id">The ID of the PhysicsVolume.</param>
        /// <param name="area">The area that when something 
        /// is in, will be affected by the PhysicsVolume.</param>
        public PhysicsVolume(string id, Rectangle area)
        {
            this.id = id;

            collide = new CollisionObject(id + "Collide", area);
            collide.OnCollided += new CollisionEvent(Collide);

            ForceManager.AddForce(this);
        }

        /// <summary>
        /// Don't call!!
        /// This is used to manage already calculated collisions.
        /// </summary>
        /// <param name="info">Information about the collision that occoured.</param>
        protected virtual void Collide(CollisionInfo info)
        {
            if (info.Instigator.ID != collide.ID)
                collisionIds.Add(info.Instigator.ID);
            else
                collisionIds.Add(info.Collider.ID);
        }

        /// <summary>
        /// Gets the objects that get to have a force applied to them.
        /// </summary>
        /// <returns>The object's id's and appliable forces.</returns>
        public Dictionary<string, Vector2> GetForces()
        {
            if (collisionIds.Count > 0)
            {
                Dictionary<string, Vector2> forces =
                    new Dictionary<string, Vector2>();

                List<CollisionObject> collisions = BroadPhase.GetCollisionObjects();

                for (int i = 0; i < collisionIds.Count; i++)
                    for (int j = 0; j < collisions.Count; j++)
                        if (collisions[j].ID == collisionIds[i])
                        {
                            Vector2 center = Vector2.Zero;

                            switch (collisions[j].Shape)
                            {
                                case CollisionShapes.Circle:
                                    {
                                        center = ((BoundingCircle)collisions[j].Bounds).Center;
                                    }
                                    break;

                                case CollisionShapes.Rectangle:
                                    {
                                        Rectangle rect = (Rectangle)collisions[j].Bounds;

                                        center = new Vector2(rect.Center.X, rect.Center.Y);
                                    }
                                    break;
                            }

                            forces.Add(collisionIds[i], CalculateForce(center));

                            collisions.Remove(collisions[j]);
                            collisionIds.Remove(collisionIds[i]);
                        }


                collisionIds.Clear();

                return forces;
            }

            return null;
        }

        protected abstract Vector2 CalculateForce(Vector2 position);

        /// <summary>
        /// Removes the PhysicsVolume from use.
        /// </summary>
        public void Remove()
        {
            ForceManager.Remove(this);
        }
    }
}
