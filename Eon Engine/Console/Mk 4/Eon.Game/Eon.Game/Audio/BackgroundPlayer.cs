/* Created 27/09/2013
 * Last Updated: 20/05/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;

namespace Eon.Game.Audio
{
    /// <summary>
    /// Defines an object that is used to play audio.
    /// </summary>
    public sealed class BackgroundPlayer : ILevelAsset
    {
        string id;
        string soundName;
        string audioCategory;

        /// <summary>
        /// The name of the sound to be played. 
        /// </summary>
        public string SoundName { get { return soundName; } }

        /// <summary>
        /// The ID of this BackgroundPlayer.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a new BackgroundPlayer.
        /// </summary>
        /// <param name="id">The uniqu identifaction name of this BackgroundPlayer.</param>
        /// <param name="soundName">The name of the sound to be played.</param>
        /// <param name="audioCategory">The audio category of the sound.</param>
        public BackgroundPlayer(string id, string soundName, string audioCategory)
        {
            this.id = id;
            this.soundName = soundName;
            this.audioCategory = audioCategory;
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
                AudioManager.Play(soundName);
        }

        public void LevelTransitionOff(string levelID)
        {
            if (AudioManager.IsPlaying(soundName))
                AudioManager.Stop(soundName);
        }
    }
}
