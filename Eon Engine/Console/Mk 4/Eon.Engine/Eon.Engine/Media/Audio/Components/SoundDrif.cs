/* Created 06/04/2015
 * Last Updated: 06/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon;
using Eon.Engine;

namespace Eon.Engine.Media.Audio.Components
{
    /// <summary>
    /// Used to define a set of sound clips that play on loop.
    /// </summary>
    public sealed class SoundDrif : AudioComponent
    {
        string[] sounds;
        int currentIdx = 0;

        bool change = false;
        int idxToChangeTo = 0;

        /// <summary>
        /// Creates a new SoundDrif.
        /// </summary>
        /// <param name="name">The name of the SoundDrif.</param>
        /// <param name="sounds">The name of the sounds to be contained.</param>
        /// <param name="currentIdx">The current index of the sound to be played.</param>
        public SoundDrif(string name, string[] sounds, int currentIdx)
            : base(name)
        {
            this.sounds = sounds;
            this.currentIdx = currentIdx;
        }

        /// <summary>
        /// Changes the current sound to a new one.
        /// </summary>
        /// <param name="soundName">The name of the sound to be played next.</param>
        public void ChangeCurrent(string soundName)
        {
            bool found = false;
            int i = 0;

            while (i < sounds.Length && !found)
            {
                if (sounds[i].Equals(soundName))
                {
                    idxToChangeTo = i;
                    change = true;
                }
            }
        }

        protected override void Update()
        {
            if (!AudioManager.IsPlaying(sounds[currentIdx]))
            {
                if (change)
                {
                    change = false;
                    currentIdx = idxToChangeTo;
                }

                AudioManager.Play(sounds[currentIdx]);
            }
        }
    }
}
