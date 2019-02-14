/* Created 03/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.System.States;
using Eon.Testing.ErrorManagement;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles.D2
{
    /// <summary>
    /// Used to define a 2D Particle System.
    /// </summary>
    public class ParticleSystem2D : GameObject
    {
        #region Fields

        MediaStates currentState = MediaStates.Paused;

        List<ParticleEmitter2D> emitters =
            new List<ParticleEmitter2D>();

        int active;

        #endregion
        #region Properties

        public OnFinishedEvent OnComplete;

        #endregion
        #region Ctor

        /// <summary>
        /// Creates a new ParticleSystem2D.
        /// </summary>
        /// <param name="id">The id of the ParticleSystem2D.</param>
        /// <param name="info">The filepath of the file that  
        /// contains all of the ParticleSystem2D's data.</param>
        public ParticleSystem2D(string id, string filepath)
            : base(id)
        {
            try
            {
                ParticleSystem2DInfo info = XmlHelper.DeserializeContent<ParticleSystem2DInfo>(filepath + ".Part2D");

                AssemblyManager.AddAssemblyRef(info.AssemblyRef);

                for (int i = 0; i < info.Emitters.Count; i++)
                {
                    ParticleEmitter2D emit = new ParticleEmitter2D(
                        info.Emitters[i].Key, info.Emitters[i].Value, info.DrawLayer, info.PostRender);

                    emit.OnComplete += new OnSpawningCompleteEvent(OnEmitterFinished);

                    emitters.Add(emit);

                    AttachComponent(emit);
                }
            }
            catch
            {
                new Error("No Particle System data found. " +
                    "Unable to create ParticleSystem2D: " + id,
                    Seriousness.CriticalError);

                this.Destroy();
            }
        }

        void OnEmitterFinished()
        {
            active--;

            if (active == 0)
                if (OnComplete != null)
                    OnComplete(this.ID);
        }

        #endregion
        #region States

        /// <summary>
        /// Starts the ParticleSystem2D's simulation. 
        /// </summary>
        public void Start()
        {
            active = emitters.Count;

            for (int i = 0; i < emitters.Count; i++)
                emitters[i].Start();

            currentState = MediaStates.Playing;
        }

        /// <summary>
        /// Pauses the ParticleSystem demonstration.
        /// </summary>
        public void Pause()
        {
            for (int i = 0; i < emitters.Count; i++)
                emitters[i].Pause();

            currentState = MediaStates.Paused;
        }

        /// <summary>
        /// Ends the ParticleSyatem demonstration.
        /// </summary>
        public void Stop()
        {
            for (int i = 0; i < emitters.Count; i++)
                emitters[i].Stop();

            currentState = MediaStates.Stopped;
        }

        #endregion
        #region Misc

        /// <summary>
        /// Sets the starting position of every ParticleEmitter2D 
        /// in this ParticleSystem2D.
        /// </summary>
        /// <param name="position">The new starting position</param>
        public void SetPosition(Vector2 position)
        {
            for (int i = 0; i < emitters.Count; i++)
                emitters[i].SetStartingPos(position);
        }

        #endregion
    }
}
