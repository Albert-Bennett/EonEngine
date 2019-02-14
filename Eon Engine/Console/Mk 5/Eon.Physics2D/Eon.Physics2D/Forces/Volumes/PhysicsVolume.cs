/* Created: 10/11/2013
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision;
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
        PhysicsComponent collide;

        List<PhysicsComponent> collisions =
            new List<PhysicsComponent>();

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
                collide = new PhysicsComponent(ID + "Collide", area, Vector2.Zero, -1, true);
                //collide.OnCollided += new CollisionEvent(Collide);

                Owner.AttachComponent(collide);
            }

            base.Initialize();
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
