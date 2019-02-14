/* Created: 11/09/2014
 * Last Updated: 11/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths;
using Eon.Maths.Helpers;
using Eon.Particles.Attachments.Base;
using Eon.Particles.Base;
using Eon.Particles.Cycles;
using Eon.Particles.D3.EmittionTypes.Base;
using Eon.System.Interfaces.Base;
using Eon.System.States;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles.D3
{
    /// <summary>
    /// Used to define a 3D emitter of particles.
    /// </summary>
    internal sealed class ParticleEmitter3D : ObjectComponent
    {
        #region Fields

        List<Particle3D> particles =
            new List<Particle3D>();

        IGenerateRenderer renderMethod;
        IEmitter3D emittionType;

        IntervalCycle spawnCycle;
        AttachmentPool attachments;

        FloatRange lifeTime;
        FloatRange mass;

        MediaStates currentState = MediaStates.Paused;

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

        #endregion
        #region Ctor

        public ParticleEmitter3D(string id, ParticleEmitterInfo info)
            : base(id)
        {
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
                    emittionType = obj as IEmitter3D;
            }

            if (info.RenderType != null)
            {
                object obj = AssemblyManager.CreateInstance(info.RenderType);

                if (obj != null)
                    renderMethod = obj as IGenerateRenderer;
            }

            lifeTime = new FloatRange(info.MinLifeTime, info.MaxLifeTime);
            mass = new FloatRange(info.MinMass, info.MaxMass);

            key = RandomHelper.GetRandom(0, 10000000);
        }

        #endregion
        #region Updating

        protected override void Update()
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

            renderMethod.Render(attachments.Properties);
        }

        void GenerateParticles(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Particle3D p = new Particle3D("Particle" + totalGenerated + key,
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
        /// Starts the emittion of Particle3D's.
        /// </summary>
        public void Start()
        {
            currentState = MediaStates.Playing;
        }

        /// <summary>
        /// Pauses the emittion of Particle3D's.
        /// </summary>
        public void Pause()
        {
            currentState = MediaStates.Paused;
        }

        /// <summary>
        /// Stops the emittion of Particle3D's.
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
        #region Misc

        internal void SetStartingPos(Vector3 position)
        {
            emittionType.SetPosition(position);
        }

        protected override void _Destroy()
        {
            Reset();

            base._Destroy();
        }

        #endregion
    }
}
