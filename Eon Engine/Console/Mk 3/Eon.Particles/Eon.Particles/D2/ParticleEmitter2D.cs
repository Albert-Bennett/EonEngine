/* Created: 03/09/2014
 * Last Updated: 10/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths;
using Eon.Maths.Helpers;
using Eon.Particles.Attachments.Base;
using Eon.Particles.Base;
using Eon.Particles.Cycles;
using Eon.Particles.D2.EmittionTypes.Base;
using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces;
using Eon.System.States;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles.D2
{
    /// <summary>
    /// Defines a 2D emitter of Particles.
    /// </summary>
    public sealed class ParticleEmitter2D : ObjectComponent, IUpdate, IDrawItem
    {
        #region Fields

        List<Particle2D> particles =
            new List<Particle2D>();

        IParticleRenderer renderMethod;
        IEmitter2D emittionType;

        IntervalCycle spawnCycle;
        AttachmentPool attachments;

        FloatRange lifeTime;
        FloatRange mass;

        MediaStates currentState = MediaStates.Paused;

        int drawLayer;

        bool endParticles = false;
        int totalGenerated = 0;

        int key = 0;

        #endregion
        #region Properties

        public OnSpawningCompleteEvent OnComplete;

        public MediaStates CurrentState
        {
            get { return currentState; }
        }

        public int DrawLayer
        {
            get { return drawLayer; }
        }

        public int Priority
        {
            get { return 0; }
        }

        #endregion
        #region Ctor

        public ParticleEmitter2D(string id, ParticleEmitterInfo info,
            int drawLayer, bool postDraw)
            : base(id)
        {
            this.drawLayer = drawLayer;

            if (postDraw)
                PostRenderManager.Add(this);
            else
                DrawingManager.Add(this);

            attachments = new AttachmentPool(info.Attachments);

            if (info.CycleType != null)
            {
                object obj = AssemblyManager.CreateInstance(info.CycleType);

                if (obj != null)
                {
                    spawnCycle = obj as IntervalCycle;
                    spawnCycle.OnSpawn += new OnSpawnEvent(GenerateParticles);
                    spawnCycle.OnCompleted += new OnSpawningCompleteEvent(OnCompletedSpawning);
                }
            }

            if (info.EmittionType != null)
            {
                object obj = AssemblyManager.CreateInstance(info.EmittionType);

                if (obj != null)
                    emittionType = obj as IEmitter2D;
            }

            if (info.RenderType != null)
            {
                object obj = AssemblyManager.CreateInstance(info.RenderType);

                if (obj != null)
                    renderMethod = obj as IParticleRenderer;
            }

            lifeTime = new FloatRange(info.MinLifeTime, info.MaxLifeTime);
            mass = new FloatRange(info.MinMass, info.MaxMass);

            key = RandomHelper.GetRandom(0, 10000000);
        }

        #endregion
        #region Updating

        public void _Update()
        {
            if (currentState == MediaStates.Playing)
            {
                attachments.Update();

                if (renderMethod is IUpdateableRenderer)
                    ((IUpdateableRenderer)renderMethod).Update();

                if (!endParticles)
                    spawnCycle.Update();
                else
                    if (particles.Count == 0)
                        Stop();
            }
        }

        void GenerateParticles(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Particle2D p = new Particle2D("Particle" + totalGenerated+key,
                    emittionType.GeneratePosition(), mass.GetRandomWithin(),
                    lifeTime.GetRandomWithin(), attachments.GeneratePropertySet());

                p.OnDead += new OnDeadParticleEvent(ParticleDead);

                if (renderMethod is IGenerateRenderer)
                    ((IGenerateRenderer)renderMethod).GenerateNew();

                particles.Add(p);

                totalGenerated++;
            }
        }

        void ParticleDead(string particleID)
        {
            int idx = 0;
            bool found = false;

            while (!found && idx < particles.Count)
            {
                if (particles[idx].ID == particleID)
                    found = true;
                else
                    idx++;
            }

            particles[idx].Destroy();
            particles.RemoveAt(idx);

            if (renderMethod is IGenerateRenderer)
                ((IGenerateRenderer)renderMethod).Remove(idx);

            attachments.Remove(idx);
        }

        void OnCompletedSpawning()
        {
            endParticles = true;
        }

        #endregion
        #region State Changing

        /// <summary>
        /// Starts the emittion of Particle2D's.
        /// </summary>
        public void Start()
        {
            currentState = MediaStates.Playing;
        }

        /// <summary>
        /// Pauses the emittion of Particle2D's.
        /// </summary>
        public void Pause()
        {
            currentState = MediaStates.Paused;
        }

        /// <summary>
        /// Stops the emittion of Particle2D's.
        /// </summary>
        public void Stop()
        {
            currentState = MediaStates.Stopped;

            Reset();

            if (OnComplete != null)
                OnComplete();
        }

        void Reset()
        {
            attachments.Reset();
            spawnCycle.Reset();

            if (renderMethod is IGenerateRenderer)
                ((IGenerateRenderer)renderMethod).Reset();

            for (int i = 0; i < particles.Count; i++)
                particles[i].Destroy();

            particles.Clear();

            endParticles = false;
            totalGenerated = 0;
        }

        #endregion
        #region Rendering

        public void Draw(DrawingStage stage)
        {
            if (stage == DrawingStage.Colour)
                renderMethod.Render(attachments.Properties);
        }

        #endregion
        #region Misc

        internal void SetStartingPos(Vector2 position)
        {
            emittionType.SetPosition(position);
        }

        public override void Destroy(bool remove)
        {
            Reset();

            base.Destroy(remove);
        }

        #endregion
    }
}
