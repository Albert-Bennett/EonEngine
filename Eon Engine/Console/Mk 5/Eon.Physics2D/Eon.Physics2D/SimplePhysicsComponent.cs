/* Created: 24/09/2015
 * Last Updated: 26/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Physics2D.Maths;
using Eon.System.Interfaces.Base;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines an ObjectComponent that is used to 
    /// apply in-game physics for the GameObject it is attached to.
    /// </summary>
    public class SimplePhysicsComponent : ObjectComponent
    {
        Particle particlePhysics;

        IBounding2D bounds;
        bool isStatic;

        List<string> collides = new List<string>();

        public CollisionEvent OnCollided;

        /// <summary>
        /// The collision space for this CollidableRectangle.
        /// </summary>
        public IBounding2D Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// Wheather or not the CollisionComponent is moveable.
        /// </summary>
        public bool Static
        {
            get { return isStatic; }
        }

        /// <summary>
        /// The ID's of evey object that had collided with this.
        /// </summary>
        internal List<string> Collides
        {
            get { return collides; }
        }

        /// <summary>
        /// Creates a new PhysicsComponent.
        /// </summary>
        /// <param name="id">The ID of the PhysicsComponent.</param>
        /// <param name="bounds">The bounding area of the PhysicsComponent.</param>
        /// <param name="acceleration">The initial acceleration of the PhysicsComponent.</param>
        /// <param name="mass">The mass of the PhysicsComponent.</param>
        public SimplePhysicsComponent(string id, IBounding2D bounds,
            Vector2 acceleration, float mass)
            : base(id)
        {
            this.bounds = bounds;

            particlePhysics = new Particle(bounds.Center, acceleration, mass);
            Priority = 1;

            BroadPhase.Add(this);
        }

        /// <summary>
        /// Creates a new PhysicsComponent.
        /// </summary>
        /// <param name="id">The ID of the PhysicsComponent.</param>
        /// <param name="bounds">The bounding area of the PhysicsComponent.</param>
        /// <param name="acceleration">The initial acceleration of the PhysicsComponent.</param>
        /// <param name="mass">The mass of the PhysicsComponent.</param>
        /// <param name="isStatic">Wheater or not the PhysicsComponent is moveable.</param>
        public SimplePhysicsComponent(string id, IBounding2D bounds,
            Vector2 acceleration, float mass, bool isStatic)
            : base(id)
        {
            this.bounds = bounds;
            this.isStatic = isStatic;

            particlePhysics = new Particle(bounds.Center,
                acceleration, mass, isStatic);

            BroadPhase.Add(this);
        }

        protected override void Update()
        {
            Vector2 pos = new Vector2(Owner.World.Position.X,
                Owner.World.Position.Y) + (bounds.Size / 2);

            particlePhysics.Position = pos;
            bounds.Center = pos;
        }

        protected override void PostUpdate()
        {
            if (!particlePhysics.IsStatic && Owner != null)
            {
                Owner.World.Position = new Vector3(particlePhysics.Position
                    - (bounds.Size / 2), 0);
            }

            collides.Clear();

            base.PostUpdate();
        }

        internal void Collided(MTV mtv, GameObject instigator)
        {
            if (!isStatic)
            {
                Vector2 movement = mtv.Direction * mtv.Distance;
                movement *= 1.0f / particlePhysics.Mass;

                bounds.Center += movement;
                particlePhysics.Position += movement;
                Owner.World.Position += new Vector3(movement, 0);
            }

            collides.Add(instigator.ID);

            if (OnCollided != null)
                OnCollided(mtv, instigator);
        }

        /// <summary>
        /// Sets the collision area for this PhysicsComponent.
        /// </summary>
        /// <param name="newBounds">The new collision area.</param>
        public void SetBounds(IBounding2D newBounds)
        {
            bounds = newBounds;
        }

        /// <summary>
        /// Sets the center position of the Bounds.
        /// </summary>
        /// <param name="position">The position to set the
        /// center of the bounds to.</param>
        public void SetPosition(Vector2 position)
        {
            bounds.Center = position;
        }

        /// <summary>
        /// Destroys the PhysicsComponent.
        /// </summary>
        protected override void _Destroy()
        {
            particlePhysics.Destroy();

            BroadPhase.Remove(this);

            base._Destroy();
        }
    }
}