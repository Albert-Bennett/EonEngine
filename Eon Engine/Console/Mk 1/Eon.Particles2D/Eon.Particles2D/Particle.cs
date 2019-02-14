/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Particles2D
{
    /// <summary>
    /// Defines a particle that will be emitted by a particle emmitter. 
    /// </summary>
    public sealed class Particle
    {
        #region Varibles

        ParticleObject phy;

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
        #region Methods

        internal void Initialize(Vector2 position,
            Vector2 acceleration, float mass, TimeSpan lifeTime)
        {
            phy = new ParticleObject(position, acceleration, mass);

            this.lifeTime = lifeTime;
        }

        internal void Update()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime >= lifeTime)
            {
                phy.Remove();
                currentTime = TimeSpan.Zero;

                if (OnDied != null)
                    OnDied(this);
            }
        }

        #endregion
    }
}
