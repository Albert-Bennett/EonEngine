/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Particles2D
{
    /// <summary>
    /// Defines a particle that will be emitted by a particle emmitter. 
    /// </summary>
    public sealed class Particle : ObjectComponent
    {
        #region Varibles

        ParticleComponent phy;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan lifeTime;

        #endregion
        #region Props

        /// <summary>
        /// The position of the particle.
        /// </summary>
        public Vector2 Position
        {
            get { return phy.Position; }
        }

        public OnDeadParticleEvent OnDied;

        #endregion
        #region Ctor

        public Particle(string id) : base(id) { }

        internal void ReUse(Vector2 position,
            Vector2 acceleration, float mass, TimeSpan lifeTime)
        {
            if (Owner != null)
            {
                if (phy != null)
                    phy.Destroy(false);

                phy = new ParticleComponent(ID + "Physics", position, acceleration, mass);
                Owner.AttachComponent(phy);

                this.lifeTime = lifeTime;
            }
        }

        internal void Update()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime >= lifeTime)
            {
                phy.Destroy(false);
                phy = null;

                currentTime = TimeSpan.Zero;

                if (OnDied != null)
                    OnDied(this);
            }
        }

        public override void Disable()
        {
            if (phy != null)
                phy.Disable();

            base.Disable();
        }

        public override void Enable()
        {
            if (phy != null)
                phy.Enable();

            base.Enable();
        }

        public override void Destroy(bool remove)
        {
            if (phy != null)
                phy.Destroy(remove);

            base.Destroy(remove);
        }

        #endregion
    }
}
