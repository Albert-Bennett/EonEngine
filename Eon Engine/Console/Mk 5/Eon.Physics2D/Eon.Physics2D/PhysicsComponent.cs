/* Created: 08/11/2013
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Collision.Phases.BroadPhase;
using Eon.Physics2D.Maths.Shapes;
using Eon.System.Interfaces.Base;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines an ObjectComponent that is used to 
    /// apply in-game physics for the GameObject it is attached to.
    /// </summary>
    public class PhysicsComponent : ObjectComponent
    {
        Particle particlePhysics;

        ConvexShape bounds;
        bool isStatic;

        public CollisionEvent OnCollided;

        /// <summary>
        /// The collision space for this CollidableRectangle.
        /// </summary>
        public ConvexShape Bounds
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
        public PhysicsComponent(string id, ConvexShape bounds,
            Vector2 acceleration, float mass, bool isStatic)
            : base(id)
        {
            this.bounds = bounds;
            this.isStatic = isStatic;

            particlePhysics = new Particle(bounds.Center,
                acceleration, mass, isStatic);

            BroadPhase.Add(this);
        }

        protected override void Initialize()
        {
            if (Owner != null)
                Owner.World.Position = new Vector3(particlePhysics.Position, 0);

            base.Initialize();
        }

        protected override void Update()
        {
            if (!particlePhysics.IsStatic)
            {
                Owner.World.Position = new Vector3(particlePhysics.Position, 0);

                bounds.Center = new Vector2(Owner.World.Position.X,
                    Owner.World.Position.Y);
            }
        }

        //internal void Collided(CollisionInfo info)
        //{
        //    if (OnCollided != null)
        //        OnCollided(info);
        //}

        /// <summary>
        /// Sets the collision area for this PhysicsComponent.
        /// </summary>
        /// <param name="newBounds">The new collision area.</param>
        public void SetBounds(ConvexShape newBounds)
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
            bounds.Vertices[0] = position;
        }

        /// <summary>
        /// Destroys the PhysicsComponent.
        /// </summary>
        protected override void _Destroy()
        {
            particlePhysics.Destroy();

            BroadPhase.Remove(this);
            particlePhysics.Destroy();

            base._Destroy();
        }
    }
}