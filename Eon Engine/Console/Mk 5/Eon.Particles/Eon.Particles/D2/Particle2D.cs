/* Created: 03/09/2014
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles.Attachments.Base;
using Eon.Physics2D;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Particles.D2
{
    /// <summary>
    /// Used to define a Particle.
    /// </summary>
    public sealed class Particle2D : GameObject
    {
        #region Varibles

        Particle phy;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan lifeTime;

        ParticlePropertySet prop;

        float mass;

        #endregion
        #region Props

        public ParticlePropertySet Properties
        {
            get { return prop; }
        }

        public OnDeadParticleEvent OnDead;

        #endregion
        #region Ctor

        public Particle2D(string id, Vector2 position, float mass,
            float lifeTime, ParticlePropertySet properties)
            : base(id)
        {
            this.lifeTime = TimeSpan.FromMilliseconds(lifeTime);

            this.mass = mass;

            prop = properties;
            prop.Position = new Vector3(position, 0);
        }

        protected override void Initialize()
        {
            World.Position = prop.Position;
            Vector2 pos = new Vector2(World.Position.X, World.Position.Y);

            phy = new Particle(pos, Vector2.Zero, mass);

            base.Initialize();
        }

        protected override void Update()
        {
            World.Position += new Vector3(phy.Position, 0);
            prop.Position = World.Position;

            currentTime += Common.ElapsedTimeDelta;

            if (currentTime >= lifeTime)
            {
                currentTime = TimeSpan.Zero;

                if (OnDead != null)
                    OnDead(this.ID);
            }

            base.Update();
        }

        #endregion
    }
}