/* Created 10/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision;
using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Physics2D.Maths;
using Eon.Physics2D.Maths.Shapes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Physics2D.Forces.Volumes
{
    /// <summary>
    /// Defines an area that applies a force on objects in it.
    /// </summary>
    public abstract class PhysicsVolume : ObjectComponent, IVolumetricForce
    {
        CollisionComponent collide;

        List<CollisionComponent> collisions = 
            new List<CollisionComponent>();

        ConvexShape area;

        protected ConvexShape _AreaOfEffect
        {
            get { return area; }
        }

        /// <summary>
        /// Creates a new PhysicsVolume.
        /// </summary>
        /// <param name="id">The ID of the PhysicsVolume.</param>
        /// <param name="area">The area that when something 
        /// is in, will be affected by the PhysicsVolume.</param>
        public PhysicsVolume(string id, ConvexShape area)
            : base(id)
        {
            this.area = area;

            ForceManager.AddForce(this);
        }

        protected override void Initialize()
        {
            if (Owner != null)
            {
                collide = new CollisionComponent(ID + "Collide", area, true);
                collide.OnCollided += new CollisionEvent(Collide);

                Owner.AttachComponent(collide);
            }

            base.Initialize();
        }

        /// <summary>
        /// Don't call!!
        /// This is used to manage already calculated collisions.
        /// </summary>
        /// <param name="info">Information about the collision that occoured.</param>
        protected virtual void Collide(CollisionInfo info)
        {
            if (info.Instigator.ID != collide.ID)
                collisions.Add(info.Instigator);
            else
                collisions.Add(info.Collider);
        }

        /// <summary>
        /// Gets the objects that get to have a force applied to them.
        /// </summary>
        /// <returns>The object's id's and appliable forces.</returns>
        public Dictionary<string,Vector2> GetForces()
        {
            //This will probably change.

            if (collisions.Count > 0)
            {
                Dictionary<string, Vector2> forces =
                    new Dictionary<string, Vector2>();

                for (int i = 0; i < collisions.Count; i++)
                    forces.Add(collisions[i].ID, CalculateForce(collisions[i].Bounds.Center));

                collisions.Clear();

                return forces;
            }

            return null;
        }

        protected abstract Vector2 CalculateForce(Vector2 position);

        protected void SetBounds(ConvexShape bounds)
        {
            collide.SetBounds(bounds);
        }

        /// <summary>
        /// Removes the PhysicsVolume from use.
        /// </summary>
        public void Remove()
        {
            ForceManager.Remove(this);
        }
    }
}
