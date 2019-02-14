/* Created: 05/09/2014
 * Last Updated: 09/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics3D.Particles.Forces;
using Eon.System.Interfaces.Base;
using Microsoft.Xna.Framework;

namespace Eon.Physics3D.Particles
{
    /// <summary>
    /// Used to define particle physics for a 
    /// 3D object.
    /// </summary>
    public sealed class Particle3DComponent : ObjectComponent, IForceAccumulator
    {
        float mass;

        Vector3 velocity;
        Vector3 acceleration;

        Vector3 forces;

        /// <summary>
        /// The mass of the Particle3DComponent.
        /// </summary>
        public float Mass
        {
            get { return mass; }
        }

        /// <summary>
        /// The position of the Particle3DComponent.
        /// </summary>
        public Vector3 Position
        {
            get { return Owner.World.Position; }
        }

        /// <summary>
        /// The velocity of the Particle3DComponent.
        /// </summary>
        public Vector3 Velocity
        {
            get { return velocity; }
        }

        internal Vector3 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        /// <summary>
        /// The applyable forces.
        /// </summary>
        public Vector3 Forces
        {
            get { return forces; }
        }

        /// <summary>
        /// Creates a new Particle3DComponent.
        /// </summary>
        /// <param name="id">The id of the Particle3DComponent.</param>
        /// <param name="mass">The mass of the Particle3DComponent.</param>
        /// <param name="acceleration">The starting acceleration of the Particle3DComponent.</param>
        public Particle3DComponent(string id,
            float mass, Vector3 acceleration)
            : base(id)
        {
            this.acceleration = acceleration;
            this.mass = mass;

            ForceManager.Add(this);
        }

        /// <summary>
        /// Creates a new Particle3DComponent.
        /// </summary>
        /// <param name="id">The id of the Particle3DComponent.</param>
        /// <param name="mass">The mass of the Particle3DComponent.</param>
        public Particle3DComponent(string id, float mass)
            : this(id, mass, Vector3.Zero) { }

        protected override void Update()
        {
            float dt = (float)Common.ElapsedTimeDelta.TotalSeconds;

            Vector3 pos = Owner.World.Position;

            acceleration += (Forces * mass) * dt;
            velocity += acceleration * dt;
            pos += velocity;

            Reset();

            Owner.World.Position = pos;
        }

        /// <summary>
        /// Used to accumulate outside forces.
        /// </summary>
        /// <param name="force">The magnitude of the force.</param>
        public void AccumulateForces(Vector3 force)
        {
            forces += force;
        }

        /// <summary>
        /// Resets the accumulated forces.
        /// </summary>
        public void Reset()
        {
            forces = Vector3.Zero;
        }

        protected override void _Destroy()
        {
            ForceManager.Remove(this);

            base._Destroy();
        }
    }
}
