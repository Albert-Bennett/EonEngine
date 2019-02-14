﻿/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.EngineComponents;
using Eon.Helpers;
using Eon.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Particles2D
{
    /// <summary>
    /// Defines a particle system. Used to generate particle effects.
    /// </summary>
    public class ParticleSystem : GameObject, IDispose
    {
        ParticleEmitterStates currentState = ParticleEmitterStates.Paused;

        List<ParticleEmitter> emitters;
        ParticleSystemInfo info;

        Vector2 startingPos = Vector2.Zero;

        public GameStates GameState = GameStates.Game;
        public OnFinishedEvent OnFinished;

        /// <summary>
        /// Creates a new ParticleSystem.
        /// </summary>
        /// <param name="id">The unique identification 
        /// name to give to the ParticleSystem.</param>
        /// <param name="particleSystemInfoFilepath">The filepath of the file
        /// containing the information on the particle system.</param>
        public ParticleSystem(string id,
            string particleSystemInfoFilepath)
            : base(id)
        {
            emitters = new List<ParticleEmitter>();

            info = XmlHelper.DeserializeContent<ParticleSystemInfo>(particleSystemInfoFilepath + ".Part2D");

            AddEmitters();
        }

        void AddEmitters()
        {
            for (int i = 0; i < info.Emitters.Length; i++)
            {
                ParticleEmitter emit = new ParticleEmitter(
                    info.Emitters[i].ID, info.Emitters[i], info.PostDraw, info.DrawLayer);

                emit.ChangePosition(startingPos);

                emit.OnComplete += new OnCompleteEvent(Completed);
                emitters.Add(emit);

                AttachComponent(emit);
            }
        }

        void Completed(ParticleEmitter emitter)
        {
            emitter.Stop();

            int idx = 0;
            bool found = false;

            while (idx < emitters.Count && !found)
            {
                if (emitters[idx].CurrentState == ParticleEmitterStates.Playing)
                    found = true;

                idx++;
            }

            if (!found)
            {
                currentState = ParticleEmitterStates.Finished;

                if (OnFinished != null)
                    OnFinished(this);
            }
        }

        protected override void Update()
        {
            if (GameStateManager.CurrentState == GameState)
                if (currentState == ParticleEmitterStates.Playing)
                    for (int i = 0; i < emitters.Count; i++)
                        emitters[i].Update();
        }

        /// <summary>
        /// Begins the particle system demonstration.
        /// </summary>
        public void Start()
        {
            if (currentState != ParticleEmitterStates.Playing &&
                currentState != ParticleEmitterStates.Finished)
            {
                for (int i = 0; i < emitters.Count; i++)
                    emitters[i].Start();

                currentState = ParticleEmitterStates.Playing;
            }
            else if (currentState == ParticleEmitterStates.Finished)
            {
                if (emitters.Count == 0)
                    AddEmitters();

                for (int i = 0; i < emitters.Count; i++)
                    emitters[i].Start();

                currentState = ParticleEmitterStates.Playing;
            }
        }

        /// <summary>
        /// Pauses the ParticleSystem demonstration.
        /// </summary>
        public void Pause()
        {
            for (int i = 0; i < emitters.Count; i++)
                emitters[i].Pause();

            currentState = ParticleEmitterStates.Paused;
        }

        /// <summary>
        /// Ends the ParticleSyatem demonstration.
        /// </summary>
        public void Stop()
        {
            for (int i = 0; i < emitters.Count; i++)
                emitters[i].Stop();
        }

        /// <summary>
        /// Finds a ParticleEmitter of the given id.
        /// </summary>
        /// <param name="emmitterID">The id of the ParticleEmitter to get.</param>
        /// <returns>The result of the search.</returns>
        public ParticleEmitter GetEmitter(string emmitterID)
        {
            ParticleEmitter emit = null;

            emit = (from e in emitters
                    where e.ID == emmitterID
                    select e).FirstOrDefault();

            return emit;
        }

        /// <summary>
        /// Attaches a new ParticleEmitter to the Particle system. 
        /// </summary>
        /// <param name="emitter">The particleEmitter to be added.</param>
        public void SetEmitter(ParticleEmitter emitter)
        {
            ParticleEmitter emit = null;
            emit.ChangePosition(startingPos);

            emit = (from e in emitters
                    where e.ID == emitter.ID
                    select e).FirstOrDefault();

            if (emit != null)
            {
                emit = emitter;
            }
            else
            {
                emitter.OnComplete += new OnCompleteEvent(Completed);
                emitters.Add(emitter);

                switch (currentState)
                {
                    case ParticleEmitterStates.Playing:
                        emitter.Start();
                        break;

                    case ParticleEmitterStates.Paused:
                        emitter.Pause();
                        break;

                    case ParticleEmitterStates.Finished:
                        emitter.Stop();
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the general starting position for the emitters to begin their emission.
        /// </summary>
        /// <param name="startingPos">The general starting position.</param>
        public void SetEmittionStartingPosition(Vector2 startingPos)
        {
            this.startingPos = startingPos;

            if (emitters.Count > 0)
                for (int i = 0; i < emitters.Count; i++)
                    emitters[i].ChangePosition(startingPos);
        }

        public void Dispose(bool finalize)
        {
            for (int i = 0; i < emitters.Count; i++)
                emitters[i].Destroy(true);

            emitters.Clear();

            currentState = ParticleEmitterStates.Finished;
        }

        public override void Destroy()
        {
            Dispose(true);

            base.Destroy();
        }
    }
}
