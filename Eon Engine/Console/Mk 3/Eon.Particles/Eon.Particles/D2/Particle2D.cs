/* Created: 03/09/2014
 * Last Updated: 11/09/2014
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

        ParticleComponent phy;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan lifeTime;

        PropertySet prop;

        float mass;

        #endregion
        #region Props

        public PropertySet Properties
        {
            get { return prop; }
        }

        public OnDeadParticleEvent OnDead;

        #endregion
        #region Ctor

        public Particle2D(string id, Vector2 position, float mass,
            float lifeTime, PropertySet properties)
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

            phy = new ParticleComponent(ID + "Physics",
                Vector2.Zero, mass);

            AttachComponent(phy);

            base.Initialize();
        }

        protected override void Update()
        {
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