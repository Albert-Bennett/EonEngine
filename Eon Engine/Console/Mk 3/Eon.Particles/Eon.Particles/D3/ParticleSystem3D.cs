/* Created: 11/09/2014
 * Last Updated: 19/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Testing;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles.D3
{
    /// <summary>
    /// Used to define a 3D Particle System.
    /// </summary>
    public sealed class ParticleSystem3D : GameObject
    {
        List<ParticleEmitter3D> emitters =
            new List<ParticleEmitter3D>();

        int active;

        public OnFinishedEvent OnComplete;

        /// <summary>
        /// Creates a new ParticleSystem3D.
        /// </summary>
        /// <param name="id">The id of the ParticleSystem2D.</param>
        /// <param name="info">The filepath of the file that  
        /// contains all of the ParticleSystem2D's data.</param>
        public ParticleSystem3D(string id, string filepath)
            : base(id)
        {
            try
            {
                ParticleSystem3DInfo info = SerializationHelper.Deserialize<ParticleSystem3DInfo>(filepath + ".Part3D", true, "");

                foreach (string s in info.AssemblyRefrences)
                    AssemblyManager.AddAssemblyRef(s);

                for (int i = 0; i < info.Emitters.Count; i++)
                {
                    ParticleEmitter3D emit = new ParticleEmitter3D(
                        info.Emitters[i].Key, info.Emitters[i].Value);

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

        /// <summary>
        /// Starts the ParticleSystem2D's simulation. 
        /// </summary>
        public void Start()
        {
            active = emitters.Count;

            for (int i = 0; i < emitters.Count; i++)
                emitters[i].Start();
        }

        /// <summary>
        /// Pauses the ParticleSystem demonstration.
        /// </summary>
        public void Pause()
        {
            for (int i = 0; i < emitters.Count; i++)
                emitters[i].Pause();
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
        /// Sets the starting position of every ParticleEmitter2D 
        /// in this ParticleSystem2D.
        /// </summary>
        /// <param name="position">The new starting position.</param>
        public void SetPosition(Vector3 position)
        {
            for (int i = 0; i < emitters.Count; i++)
                emitters[i].SetStartingPos(position);
        }
    }
}
