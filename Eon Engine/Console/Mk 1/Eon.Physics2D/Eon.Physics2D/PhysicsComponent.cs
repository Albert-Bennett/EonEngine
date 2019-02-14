/* Created 08/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Interfaces;
using Eon.Physics2D.Collision;
using Eon.Physics2D.Math;
using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines an ObjectComponent that is used to 
    /// apply in-game physics for the GameObject it is attached to.
    /// </summary>
    public class PhysicsComponent : ObjectComponent, IPriorityComponent
    {
        ParticleObject particlePhysics;
        CollisionObject collisionObject;

        public CollisionEvent OnCollision;

        /// <summary>
        /// The priority of the update of this PhysicsComponent.
        /// </summary>
        public int Priority
        {
            get { return 2; }
        }

        /// <summary>
        /// The position of the PhysicsComponent.
        /// </summary>
        public Vector2 Position
        {
            get { return particlePhysics.Position; }
        }

        /// <summary>
        /// Creates a new PhysicsComponent.
        /// </summary>
        /// <param name="id">The ID of the PhysicsComponent.</param>
        /// <param name="bounds">The bounding area of the PhysicsComponent.</param>
        /// <param name="acceleration">The initial acceleration of the PhysicsComponent.</param>
        /// <param name="mass">The mass of the PhysicsComponent.</param>
        public PhysicsComponent(string id, Rectangle bounds,
            Vector2 acceleration, float mass)
            : base(id)
        {
            collisionObject = new CollisionObject(id + "Collide", bounds);
            collisionObject.SetAttachedTo(ID);

            particlePhysics = new ParticleObject(new Vector2(bounds.Center.X,
                bounds.Center.Y), acceleration, mass);

            particlePhysics.AttachedToID = collisionObject.ID;

            particlePhysics.OnUpdate += new OnParticleUpdateEvent(ParticleUpdate);
            collisionObject.OnCollided += new CollisionEvent(Collided);
        }

        public void _Update() { }

        protected virtual void Collided(CollisionInfo info)
        {
            if (OnCollision != null)
                OnCollision(info);
        }

        void ParticleUpdate()
        {
            switch (collisionObject.Shape)
            {
                case CollisionShapes.Rectangle:
                    {
                        Rectangle rect = (Rectangle)collisionObject.Bounds;

                        rect.X = (int)particlePhysics.Position.X - rect.Width / 2;
                        rect.Y = (int)particlePhysics.Position.Y - rect.Height / 2;

                        collisionObject.SetBounds(rect);
                    }
                    break;

                case CollisionShapes.Circle:
                    {
                        BoundingCircle circle = (BoundingCircle)collisionObject.Bounds;

                        Vector2 center = new Vector2(particlePhysics.Position.X,
                            particlePhysics.Position.Y);

                        circle.Center = center;

                        collisionObject.SetBounds(circle);
                    }
                    break;
            }
        }

        /// <summary>
        /// Destroys the PhysicsComponent.
        /// </summary>
        public override void Destroy()
        {
            particlePhysics.Remove();
            particlePhysics = null;

            collisionObject.Destroy();
            collisionObject = null;

            base.Destroy();
        }
    }
}
