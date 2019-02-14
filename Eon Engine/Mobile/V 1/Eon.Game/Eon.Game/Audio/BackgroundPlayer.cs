/* Created 27/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Engine.Audio;

namespace Eon.Game.Audio
{
    /// <summary>
    /// Defines an object that is used to play audio.
    /// </summary>
    public sealed class BackgroundPlayer : GameObject, ILevelAsset
    {
        string soundName;
        bool paused = false;

        /// <summary>
        /// The name of the sound to be played. 
        /// </summary>
        public string SoundName { get { return soundName; } }

        /// <summary>
        /// Creates a new BackgroundPlayer.
        /// </summary>
        /// <param name="soundName">The name of the sound to be played.</param>
        public BackgroundPlayer(string id, string soundName)
            : base(id)
        {
            this.soundName = soundName;
        }

        /// <summary>
        /// Pauses or resumes this BackgroundPlayer.
        /// </summary>
        public void PauseResume()
        {
            if (Framework.AudioManagerExists)
            {
                if (!paused)
                {
                    AudioManager.PauseResume(soundName);
                    paused = true;
                }

                if (paused)
                {
                    AudioManager.PauseResume(soundName);
                    paused = false;
                }
            }
        }

        /// <summary>
        /// Stops playing this BackgroundPlayer.
        /// </summary>
        public void Stop()
        {
            if (Framework.AudioManagerExists)
                AudioManager.Stop(soundName);
        }

        public void LevelTransitionOn(string levelID)
        {
            if (Framework.AudioManagerExists)
                if (!AudioManager.IsPlaying(soundName))
                    AudioManager.Play(soundName);
        }

        public void LevelTransitionOff(string levelID)
        {
            if (Framework.AudioManagerExists)
                if (AudioManager.IsPlaying(soundName))
                    AudioManager.Stop(soundName);
        }
    }
}
