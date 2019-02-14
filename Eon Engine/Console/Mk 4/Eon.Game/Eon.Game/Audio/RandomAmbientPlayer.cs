/* Created 27/09/2013
 * Last Updated: 20/05/2015 
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Maths.Helpers;
using Eon.System.Interfaces;
using System;

namespace Eon.Game.Audio
{
    /// <summary>
    /// Defines an audio player which plays a random sound.
    /// </summary>
    public sealed class RandomAmbientPlayer : ILevelAsset, IUpdate
    {
        string[] sounds;

        int idx = 0;
        bool stopped = false;

        string id;

        float minDelay;
        float maxDelay;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan delayTime = TimeSpan.Zero;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The various sounds that this 
        /// RandomAmbientPlayer will play.
        /// </summary>
        public string[] Sounds
        {
            get { return sounds; }
        }

        /// <summary>
        /// The id of the RandomAmbientPlayer.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a new RandomAmbientPlayer.
        /// </summary>
        /// <param name="id">The unique identifaction name of this RandomAmbientPlayer.</param>
        /// <param name="sounds">The sounds to be played.</param>
        /// <param name="delayTime">The amount of time to delay between plays.</param>
        public RandomAmbientPlayer(string id, string[] sounds, float minDelay, float maxDelay)
        {
            this.id = id;
            this.sounds = sounds;

            this.minDelay = minDelay;
            this.maxDelay = maxDelay;

            if (maxDelay > 0)
                delayTime = TimeSpan.FromMilliseconds(
                    RandomHelper.GetRandom(minDelay, maxDelay));
            else
                delayTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Creates a new RandomAmbientPlayer.
        /// </summary>
        /// <param name="id">The unique identifaction name of this RandomAmbientPlayer.</param>
        /// <param name="sounds">The sounds to be played.</param>
        public RandomAmbientPlayer(string id, string[] sounds) : this(id, sounds, 0, 0) { }

        /// <summary>
        /// Used to update this RandomAmbientPlayer.
        /// </summary>
        public void _Update()
        {
            if (!stopped)
                if (!AudioManager.IsPlaying(sounds[idx]))
                    if (maxDelay > 0)
                    {
                        currentTime += Common.ElapsedTimeDelta;

                        if (currentTime >= delayTime)
                        {
                            PlayRandom();

                            currentTime = TimeSpan.Zero;

                            delayTime = TimeSpan.FromMilliseconds(
                                RandomHelper.GetRandom(minDelay, maxDelay));
                        }
                    }
                    else
                        PlayRandom();
        }

        public void _PostUpdate() { }

        void PlayRandom()
        {
            int temp = idx;

            while (idx == temp)
                idx = RandomHelper.GetRandom(0, sounds.Length - 1);

            AudioManager.Play(sounds[idx]);
        }

        /// <summary>
        /// Begins playing this RandomAmbientPlayer.
        /// </summary>
        public void Play()
        {
            stopped = false;

            PlayRandom();
        }

        /// <summary>
        /// Stops this RandomAmbientPlayer from playing.
        /// </summary>
        public void Stop()
        {
            stopped = true;

            if (AudioManager.IsPlaying(sounds[idx]))
                AudioManager.Stop(sounds[idx]);
        }

        public void LevelTransitionOn(string levelID)
        {
            Play();
        }

        public void LevelTransitionOff(string levelID)
        {
            Stop();
        }
    }
}
