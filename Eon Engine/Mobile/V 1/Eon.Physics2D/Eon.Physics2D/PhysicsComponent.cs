/* Created 08/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
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
    public class PhysicsComponent : ObjectComponent, IUpdate
    {
        ParticleComponent particlePhysics;
        CollisionComponent collision;

        Rectangle bounds;
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

                particlePhysics = new ParticleComponent(ID + "Particle", 
                    new Vector2(bounds.Center.X, bounds.Center.Y), acceleration, mass);
            }

            base.Initialize();
        }

        public void _Update()
        {
            switch (collision.Shape)
            {
                case CollisionShapes.Rectangle:
                    {
                        Rectangle rect = (Rectangle)collision.Bounds;

                        rect.X = (int)particlePhysics.Position.X - rect.Width / 2;
                        rect.Y = (int)particlePhysics.Position.Y - rect.Height / 2;

                        collision.SetBounds(rect);
                    }
                    break;

                case CollisionShapes.Circle:
                    {
                        BoundingCircle circle = (BoundingCircle)collision.Bounds;

                        Vector2 center = new Vector2(particlePhysics.Position.X,
                            particlePhysics.Position.Y);

                        circle.Center = center;

                        collision.SetBounds(circle);
                    }
                    break;
            }
        }

        protected virtual void Collided(CollisionInfo info)
        {
            Vector2 vec = new Vector2();

            switch (collision.Shape)
            {
                case CollisionShapes.Circle:
                    {
                        vec = (collision.Bounds as BoundingCircle).Center;
                    }
                    break;

                case CollisionShapes.Rectangle:
                    {
                        vec.X = ((Rectangle)collision.Bounds).Center.X;
                        vec.Y = ((Rectangle)collision.Bounds).Center.Y;
                    }
                    break;
            }

            particlePhysics.Position = vec;

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
