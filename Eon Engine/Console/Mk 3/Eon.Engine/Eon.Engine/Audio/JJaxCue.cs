/* Created: 05/03/2014
 * Last Updated: 31/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Eon.Engine.Audio
{
    /// <summary>
    /// Used to define a sound to be played;
    /// </summary>
    public class JJaxCue
    {
        string name;
        string category;

        bool loop;
        float volume;

        bool is3DPlaying = false;
        bool isDisposed = false;

        protected SoundEffectInstance song;
        protected AudioEmitter emitter;

        MediaStates currentState;

        /// <summary>
        /// The name of the sound effect that this is attached to.
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// The JJaxCategory that this belongs to.
        /// </summary>
        public string Category { get { return category; } }

        /// <summary>
        /// Is the sound effect looping.
        /// </summary>
        public bool Loop { get { return loop; } }

        /// <summary>
        /// The volume of the sound effect.
        /// </summary>
        public float Volume { get { return volume; } }

        public int Priority { get { return 0; } }

        /// <summary>
        /// The current play state of the sound effect.
        /// </summary>
        public MediaStates CurrentState { get { return currentState; } }

        internal OnStoppedEvent OnStopped;

        /// <summary>
        /// Creates a new J-Jax cue.
        /// </summary>
        /// <param name="info">The info file for the J-Jax Cue.</param>
        internal JJaxCue(CueInfo info)
        {
            if (info != null)
            {
                this.name = info.Name;
                this.volume = info.Volume * AudioManager.GetCategoryVolume(info.CategoryName);
                this.loop = info.Loop;

                song = (Common.ContentBuilder.Load
                    <SoundEffect>(info.Filepath)).CreateInstance();

                SetSoundVolume();
                song.IsLooped = loop;

                category = info.CategoryName;
            }
            else
                throw new ArgumentNullException("The given JSongInfo is null.");

            currentState = MediaStates.Paused;

            AudioManager.Add(this);
        }

        /// <summary>
        /// Creates a new J-Jax cue.
        /// </summary>
        /// <param name="info">The info file for the J-Jax Cue.</param>
        /// <param name="position">The position from where the sound will be played from.</param>
        internal JJaxCue(CueInfo info, Vector3 position)
        {
            if (info != null)
            {
                this.name = info.Name;
                this.volume = info.Volume;
                this.loop = info.Loop;

                is3DPlaying = true;

                song = (Common.ContentBuilder.Load
                    <SoundEffect>(info.Filepath)).CreateInstance();

                emitter = new AudioEmitter();
                emitter.Position = position;

                if (AudioManager.Listener != null)
                    song.Apply3D(AudioManager.Listener, emitter);

                song.Volume = volume;
                song.IsLooped = loop;

                category = info.CategoryName;
            }
            else
                throw new ArgumentNullException("The given JSongInfo is null.");

            currentState = MediaStates.Paused;

            AudioManager.Add(this);
        }

        protected virtual void SetSoundVolume()
        {
            song.Volume = volume;
        }

        /// <summary>
        /// Changes the volume of the J-JaxCue.
        /// </summary>
        /// <param name="volume">The new volume.</param>
        public void ChangeVolume(float volume)
        {
            song.Volume = this.volume * volume;
        }

        /// <summary>
        /// Updates the song.
        /// </summary>
        public virtual void _Update()
        {
            if (song.State == SoundState.Stopped)
            {
                Stop();

                if (OnStopped != null)
                    OnStopped(this);
            }
            else
                if (is3DPlaying && AudioManager.Listener != null)
                    song.Apply3D(AudioManager.Listener, emitter);
        }

        /// <summary>
        /// Plays the JSong.
        /// </summary>
        public void Play()
        {
            try
            {
                currentState = MediaStates.Playing;

                if (song.State != SoundState.Playing)
                    song.Play();
            }
            catch { }
        }

        /// <summary>
        /// Pauses \ resumes the JSong.
        /// </summary>
        public void PauseResume()
        {
            if (currentState == MediaStates.Playing)
            {
                currentState = MediaStates.Paused;
                song.Pause();
            }
            else if (currentState == MediaStates.Paused)
            {
                currentState = MediaStates.Playing;
                song.Resume();
            }
        }

        /// <summary>
        /// Stops the JSong.
        /// </summary>
        public void Stop()
        {
            if (!isDisposed)
                if (!song.IsDisposed)
                {
                    currentState = MediaStates.Stopped;
                    song.Stop();

                    if (song.IsLooped)
                        song.Stop();
                }
        }

        /// <summary>
        /// Disposes of the JSong.
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                if (currentState != MediaStates.Stopped)
                    Stop();

                AudioManager.Remove(this);

                song = null;
                isDisposed = true;
            }
        }
    }
}
