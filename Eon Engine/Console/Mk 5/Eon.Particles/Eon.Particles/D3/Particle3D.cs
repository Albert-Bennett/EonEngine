/* Created: 11/09/2014
 * Last Updated: 11/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles.Attachments.Base;
using Eon.Physics3D.Particles;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Particles.D3
{
    /// <summary>
    /// Used to define a 3D particle.
    /// </summary>
    public sealed class Particle3D : GameObject
    {
        Particle3DComponent phy;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan lifeTime;

        ParticlePropertySet prop;

        float mass;

        public ParticlePropertySet Properties
        {
            get { return prop; }
        }

        public OnDeadParticleEvent OnDead;

        public Particle3D(string id, Vector3 position, float mass,
            float lifeTime, ParticlePropertySet properties)
            : base(id)
        {
            this.lifeTime = TimeSpan.FromMilliseconds(lifeTime);

            this.mass = mass;

            prop = properties;
            prop.Position = position;
        }

        protected override void Initialize()
        {
            World.Position = prop.Position;

            phy = new Particle3DComponent(ID + "Physics", mass);
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
    }
}
