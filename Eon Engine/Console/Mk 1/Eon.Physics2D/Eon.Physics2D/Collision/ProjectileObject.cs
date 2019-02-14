/* Created 08/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Collision
{
    /// <summary>
    /// Defines a Projectile object. 
    /// </summary>
    public abstract class ProjectileObject
    {
        CollisionObject collide;
        ParticleObject particle;

        /// <summary>
        /// Creates a new ProjectileObject.
        /// </summary>
        /// <param name="id">The ID of the ProjectileObject.</param>
        /// <param name="bounds">The bounds of the ProjectileObject.</param>
        /// <param name="acceleration">The initial acceleration of the ProjectileObject.</param>
        /// <param name="mass">The mass of the ProjectileObject.</param>
        public ProjectileObject(string id, Rectangle bounds,
            Vector2 acceleration, float mass)
        {
            collide = new CollisionObject(id + "Collide", bounds);

            particle = new ParticleObject(new Vector2(bounds.Center.X,
                bounds.Center.Y), acceleration, mass);

            particle.AttachedToID = collide.ID;

            particle.OnUpdate += new OnParticleUpdateEvent(Update);
            collide.OnCollided += new CollisionEvent(Collided);
        }

        void Update()
        {
            Rectangle rect = (Rectangle)collide.Bounds;

            rect.X = (int)particle.Position.X - rect.Width / 2;
            rect.Y = (int)particle.Position.Y - rect.Height / 2;

            collide.SetBounds(rect);

            _Update(rect);
        }

        protected virtual void _Update(Rectangle collisionBounds) { }

        void Collided(CollisionInfo info)
        {
            if (info.Collider.ID != collide.ID)
            {
                particle.Remove();
                collide.Destroy();

                _Collided(info);
            }
        }

        /// <summary>
        /// A method used to define what 
        /// happends when a collision has occoured.
        /// </summary>
        /// <param name="info">The information from the collision.</param>
        protected abstract void _Collided(CollisionInfo info);
    }
}
