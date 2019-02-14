/* Created 11/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game;
using Eon.Particles.D2;
using Microsoft.Xna.Framework;

namespace Eon.Game2D.Objects
{
    /// <summary>
    /// Defines the position where the particles will  be emitted from.
    /// </summary>
    public class EmitterSpot : GameObject, ILevelAsset
    {
        Vector2 position;
        ParticleSystem2D particles;

        string particleSystemFilepath;
        bool startOnCreation = false;

        /// <summary>
        /// The file path of the ParticleSystem to use.
        /// </summary>
        public string ParticleSystemFilepath
        {
            get { return particleSystemFilepath; }
        }

        /// <summary>
        /// Where the particles will be emitted from.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }

        /// <summary>
        /// Wheather or not to start emitting particles on creation.
        /// </summary>
        public bool StartOnCreation
        {
            get { return startOnCreation; }
        }

        /// <summary>
        /// Creates a new EmitterSpot.
        /// </summary>
        /// <param name="id">The id to give the EmitterSpot.</param>
        /// <param name="emitionPos">The location to emit the particles from.</param>
        /// <param name="filepath">The filepath of the ParticleSystem to use.</param>
        public EmitterSpot(string id, Vector2 emitionPos, string filepath)
            : base(id)
        {
            this.position = emitionPos;
            this.particleSystemFilepath = filepath;
        }

        protected override void Initialize()
        {
            particles = new ParticleSystem2D(ID + "ParticleSystem", particleSystemFilepath);
            particles.SetPosition(position);
            particles.OnComplete += new OnFinishedEvent(Finished);

            base.Initialize();
        }

        void Finished(string particleSystem)
        {
            particles.Start();
        }

        public void Start()
        {
            particles.Start();
        }

        public void Stop()
        {
            particles.Stop();
        }

        public void LevelTransitionOff(string levelID)
        {
            particles.Stop();
            particles.Destroy();
        }

        public void LevelTransitionOn(string levelID)
        {
            if (startOnCreation)
                particles.Start();
        }
    }
}
