/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Interfaces;
using Eon.Particles2D.Attachments;
using Eon.Particles2D.Cycles;
using Eon.Particles2D.Emitters;
using Eon.Particles2D.Renders;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Particles2D
{
    /// <summary>
    /// Defines an emitter of Particles.
    /// </summary>
    public sealed class ParticleEmitter : ObjectComponent, IDrawItem, IDispose
    {
        #region Variables

        ParticleEmitterInfo info;

        List<Particle> particles = new List<Particle>();
        List<Particle> deadParticles = new List<Particle>();

        List<PropertySet> properties = new List<PropertySet>();
        List<IUpdate> updateAttachments = new List<IUpdate>();
        List<IAttachment> attachments = new List<IAttachment>();

        IEmitterType emitter;
        IParticleType particleType;
        Cycle spawnCycle;

        ParticleEmitterStates currentState = ParticleEmitterStates.Paused;

        Vector2 startingPos = Vector2.Zero;

        string id;

        int drawLayer;
        bool postRender;
        bool spawningComplete = false;

        public OnCompleteEvent OnComplete;

        /// <summary>
        /// The layer to draw the Particles on.
        /// </summary>
        public int DrawLayer
        {
            get { return drawLayer; }
        }

        #endregion
        #region Ctor

        /// <summary>
        /// Creates a new ParticleEmitter.
        /// </summary>
        /// <param name="info">The information file for the ParticleEmitter.</param>
        /// <param name="postRender">Wheather or not Particles should be post rendered.</param>
        /// <param name="drawLayer">The layer to render the Particles on.</param>
        public ParticleEmitter(string id, ParticleEmitterInfo info,
            bool postRender, int drawLayer)
            : base(id)
        {
            this.info = info;
            this.drawLayer = drawLayer;
            this.postRender = postRender;

            this.id = info.ID;

            if (postRender)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);

            PreInit();
        }

        void PreInit()
        {
            if (info.Attachments.Length > 0)
                for (int i = 0; i < info.Attachments.Length; i++)
                {
                    object obj = AssemblyManager.CreateInstance(info.Attachments[i]);

                    if (obj != null)
                    {
                        attachments.Add(obj as IAttachment);
                        updateAttachments.Add(obj as IUpdate);
                    }
                }

            if (info.CycleType != null)
            {
                object obj = AssemblyManager.CreateInstance(info.CycleType);

                if (obj != null)
                {
                    updateAttachments.Add(obj as IUpdate);

                    spawnCycle = obj as Cycle;
                    spawnCycle.OnSpawn += new OnSpawnEvent(GenerateParticles);
                    spawnCycle.OnComplete += new OnSpawnCompleteEvent(SpawningComplete);
                }
            }

            if (info.EmitterType != null)
            {
                object obj = AssemblyManager.CreateInstance(info.EmitterType);

                if (obj != null)
                {
                    if (obj is IUpdate)
                        updateAttachments.Add(obj as IUpdate);

                    emitter = obj as IEmitterType;
                }
            }

            if (info.ParticleRenderType != null)
            {
                object obj = AssemblyManager.CreateInstance(info.ParticleRenderType);

                if (obj != null)
                {
                    if (obj is IUpdate)
                        updateAttachments.Add(obj as IUpdate);

                    particleType = obj as IParticleType;
                }
            }
        }

        #endregion
        #region Generate Methods

        void SpawningComplete()
        {
            spawningComplete = true;
        }

        void CheckComplete()
        {
            if (particles.Count == 0)
                if (OnComplete != null)
                    OnComplete(this);
        }

        internal void Update()
        {
            switch (currentState)
            {
                case ParticleEmitterStates.Playing:
                    {
                        spawnCycle._Update();
                    }
                    break;
            }

            for (int i = 0; i < updateAttachments.Count; i++)
                updateAttachments[i]._Update();

            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();
                SetProperties(i);
            }

            if (spawningComplete)
                CheckComplete();
        }

        void SetProperties(int index)
        {
            PropertySet prop = new PropertySet();

            for (int i = 0; i < attachments.Count; i++)
                if (attachments[i].Properties.Count > index)
                    prop += attachments[i].Properties[index];

            if (properties.Count > index)
                properties[index] = prop;
        }

        void GenerateParticles()
        {
            for (int i = 0; i < spawnCycle.SpawnAmount; i++)
                if (!ReUsedParticle())
                {
                    Particle particle = new Particle((ID + "Particle" + particles.Count) + RandomHelper.GetRandomInt(0, 10000));
                    particle.OnDied += new OnDeadParticleEvent(ParticleDied);

                    particles.Add(particle);
                    Owner.AttachComponent(particle);

                    InitializeParticle(particle);

                    CreateNewProperties();
                }
        }

        void CreateNewProperties()
        {
            properties.Add(new PropertySet());

            for (int i = 0; i < attachments.Count; i++)
                attachments[i].Generate();

            if (particleType is IChangeable)
                ((IChangeable)particleType).Generate();

            SetProperties(particles.Count - 1);
        }

        bool ReUsedParticle()
        {
            bool reused = false;

            if (deadParticles != null)
                if (deadParticles.Count > 0)
                {
                    Particle particle = deadParticles[0];
                    deadParticles.Remove(particle);

                    InitializeParticle(particle);

                    CreateNewProperties();

                    reused = true;
                }

            return reused;
        }

        void InitializeParticle(Particle particle)
        {
            particle.ReUse
                     (startingPos + emitter.CreateEmittionPoint(),
                      RandomHelper.GetRandomVector2(Vector2.Zero, Vector2.One),
                      info.Mass,
                      TimeSpan.FromMilliseconds(RandomHelper.GetRandomFloat(info.MinLifeTime, info.MaxLifeTime)));

            particle.Enable();
        }

        void ParticleDied(Particle particle)
        {
            int index = particles.IndexOf(particle);

            properties.Remove(properties[index]);

            for (int i = 0; i < attachments.Count; i++)
                attachments[i].Remove(index);

            particles.Remove(particle);
            particle.Disable();

            deadParticles.Add(particle);

            if (particleType is IChangeable)
                ((IChangeable)particleType).Remove(index);
        }

        #endregion
        #region State Methods

        public void Start()
        {
            if (currentState != ParticleEmitterStates.Finished)
                currentState = ParticleEmitterStates.Playing;
        }

        public void Pause()
        {
            currentState = ParticleEmitterStates.Paused;
        }

        public void Stop()
        {
            currentState = ParticleEmitterStates.Finished;
        }

        #endregion
        #region Misc

        public void Draw()
        {
            particleType.PreDraw();

            for (int i = 0; i < particles.Count; i++)
                particleType.Draw(particles[i].Position,
                    properties[i].Scale, properties[i].Rotation, properties[i].Colour);
        }

        public void Dispose(bool finalize)
        {
            if (!Destroyed)
            {
                particleType.Dispose(finalize);

                for (int i = 0; i < attachments.Count; i++)
                    attachments[i].Dispose();

                attachments.Clear();

                updateAttachments.Clear();

                for (int i = 0; i < particles.Count; i++)
                    particles[i].Destroy(false);

                particles.Clear();

                for (int i = 0; i < deadParticles.Count; i++)
                    deadParticles[i].Destroy(false);

                deadParticles.Clear();
            }
        }

        /// <summary>
        /// Sets the starting position of the Particles.
        /// </summary>
        /// <param name="position">The new starting position.</param>
        public void ChangePosition(Vector2 position)
        {
            startingPos = position;
        }

        /// <summary>
        /// Changes the way in which the particles will be emitted.
        /// </summary>
        /// <param name="emitter">The new emitter.</param>
        public void ChangeEmitter(IEmitterType emitter)
        {
            this.emitter = emitter;
        }

        /// <summary>
        /// Adds / Updates an Attachment to this ParticleEmitter.
        /// </summary>
        /// <param name="attachment">The new Attachment to be added / updated.</param>
        public void AddAttachment(IAttachment attachment)
        {
            IAttachment attach = default(IAttachment);

            attach = (from a in attachments
                      where a.ID == attachment.ID
                      select a).FirstOrDefault();

            if (attach == null)
                attachments.Add(attachment);
            else
            {
                attachments.Remove(attach);
                attachments.Add(attachment);
            }
        }

        public override void Destroy(bool remove)
        {
            Dispose(true);

            base.Destroy(remove);
        }

        public void ScreenResolutionChanged()
        {
            emitter.ScreenResolutionChanged();

            for (int i = 0; i < attachments.Count; i++)
                attachments[i].ScreenResolutionChanged();
        }

        #endregion
    }
}
