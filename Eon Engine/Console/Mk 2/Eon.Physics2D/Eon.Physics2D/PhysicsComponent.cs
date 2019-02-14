/* Created 08/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision;
using Eon.Physics2D.Maths.Shapes;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines an ObjectComponent that is used to 
    /// apply in-game physics for the GameObject it is attached to.
    /// </summary>
    public class PhysicsComponent : ObjectComponent, IUpdate
    {
        ParticleComponent particlePhysics;
        CollisionComponent collision;

        ConvexShape bounds;
        Vector2 acceleration;
        float mass;

        public CollisionEvent OnCollision;

        /// <summary>
        /// The priority of the update of this PhysicsComponent.
        /// </summary>
        public int Priority
        {
            get { return 1; }
        }

        /// <summary>
        /// Creates a new PhysicsComponent.
        /// </summary>
        /// <param name="id">The ID of the PhysicsComponent.</param>
        /// <param name="bounds">The bounding area of the PhysicsComponent.</param>
        /// <param name="acceleration">The initial acceleration of the PhysicsComponent.</param>
        /// <param name="mass">The mass of the PhysicsComponent.</param>
        public PhysicsComponent(string id, ConvexShape bounds,
            Vector2 acceleration, float mass)
            : base(id)
        {
            this.bounds = bounds;
            this.mass = mass;
            this.acceleration = acceleration;
        }

        protected override void Initialize()
        {
            if (Owner != null)
            {
                collision = new CollisionComponent(ID + "Collide", bounds);
                collision.OnCollided += new CollisionEvent(Collided);
                Owner.AttachComponent(collision);

                particlePhysics = new ParticleComponent(
                    ID + "Particle", acceleration, mass);

                Owner.AttachComponent(particlePhysics);
            }

            base.Initialize();
        }

        public void _Update()
        {
            collision.Bounds.Center = new Vector2(
                Owner.World.Translation.X, Owner.World.Translation.Y);
        }

        protected virtual void Collided(CollisionInfo info)
        {
            //Used for the responce to collision
            //????

            if (OnCollision != null)
                OnCollision(info);
        }

        /// <summary>
        /// Destroys the PhysicsComponent.
        /// </summary>
        public override void Destroy(bool remove)
        {
            particlePhysics.Destroy(false);
            collision.Destroy(remove);

            base.Destroy(remove);
        }
    }
}
