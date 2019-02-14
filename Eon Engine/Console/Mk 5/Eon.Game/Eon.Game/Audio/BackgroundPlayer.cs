/* Created 27/09/2013
 * Last Updated: 01/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Game.Assets;

namespace Eon.Game.Audio
{
    /// <summary>
    /// Defines an object that is used to play audio.
    /// </summary>
    public sealed class BackgroundPlayer : GameObject, ILevelAsset
    {
        string soundName;
        string audioCategory;

        /// <summary>
        /// The name of the sound to be played. 
        /// </summary>
        public string SoundName { get { return soundName; } }

        /// <summary>
        /// Creates a new BackgroundPlayer.
        /// </summary>
        /// <param name="id">The uniqu identifaction name of this BackgroundPlayer.</param>
        /// <param name="soundName">The name of the sound to be played.</param>
        /// <param name="audioCategory">The audio category of the sound.</param>
        public BackgroundPlayer(string id, string soundName, string audioCategory)
            : base(id)
        {
            this.soundName = soundName;
            this.audioCategory = audioCategory;

            Presidence = System.States.GameStates.None;
        }

        bool paused = false;

        /// <summary>
        /// Pauses or resumes this BackgroundPlayer.
        /// </summary>
        public void PauseResume()
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

        /// <summary>
        /// Stops playing this BackgroundPlayer.
        /// </summary>
        public void Stop()
        {
            AudioManager.Stop(soundName);
        }

        /// <summary>
        /// Changes the volume of the audio category
        /// that the sound belongs to.
        /// </summary>
        /// <param name="volume">The new volume.</param>
        public void ChangeVolume(float volume)
        {
            AudioManager.ChangeVolume(audioCategory, volume);
        }

        public void LevelTransitionOn(string levelID)
        {
            if (!AudioManager.IsPlaying(soundName))
            {
                AudioManager.StopAll();
                AudioManager.Play(soundName);
            }
        }

        public void LevelTransitionOff(string levelID)
        {
            if (AudioManager.IsPlaying(soundName))
            {
                AudioManager.StopAll();
                AudioManager.Stop(soundName);
            }
        }
    }
}
