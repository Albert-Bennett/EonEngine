/* Created 05/03/2014
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
    public sealed class JJaxCue
    {
        string name;
        bool loop;
        float volume;

        bool is3DPlaying = false;

        SoundEffectInstance song;
        AudioEmitter emitter;

        MediaStates currentState;

        public string Name { get { return name; } }
        public bool Loop { get { return loop; } }
        public float Volume { get { return volume; } }

        public int Priority { get { return 0; } }

        public MediaStates CurrentState { get { return currentState; } }

        internal OnStoppedEvent OnStopped;

        /// <summary>
        /// Creates a new J-Jax cue.
        /// </summary>
        /// <param name="info">The info file for the J-Jax Cue.</param>
        public JJaxCue(CueInfo info)
        {
            if (info != null)
            {
                this.name = info.Name;
                this.volume = info.Volume;
                this.loop = info.Loop;

                song = (Common.ContentManager.Load
                    <SoundEffect>(info.Filepath)).CreateInstance();

                song.Volume = volume;
                song.IsLooped = loop;
            }
            else
                throw new ArgumentNullException("The given JSongInfo is null.");

            currentState = MediaStates.Paused;
        }

        /// <summary>
        /// Changes the volume of the J-JaxCue.
        /// </summary>
        /// <param name="volume">The new volume.</param>
        public void ChangeVolume(float volume)
        {
            this.volume = MathHelper.Clamp(volume, 0.1f, 1f);

            song.Volume = volume;
        }

        /// <summary>
        /// Creates a new J-Jax cue.
        /// </summary>
        /// <param name="info">The info file for the J-Jax Cue.</param>
        /// <param name="position">The position from where the sound will be played from.</param>
        public JJaxCue(CueInfo info, Vector3 position)
        {
            if (info != null)
            {
                this.name = info.Name;
                this.volume = info.Volume;
                this.loop = info.Loop;

                is3DPlaying = true;

                song = (Common.ContentManager.Load
                    <SoundEffect>(info.Filepath)).CreateInstance();

                AudioListener listner = AudioManager.listener;

                emitter = new AudioEmitter();
                emitter.Position = position;

                song.Apply3D(listner, emitter);

                song.Volume = volume;
                song.IsLooped = loop;
            }
            else
                throw new ArgumentNullException("The given JSongInfo is null.");

            currentState = MediaStates.Paused;
        }

        /// <summary>
        /// Updates the song.
        /// </summary>
        public void _Update()
        {
            if (song.State == SoundState.Stopped)
            {
                Stop();

                if (OnStopped != null)
                    OnStopped(this);
            }
            else
                if (is3DPlaying)
                    song.Apply3D(AudioManager.listener, emitter);
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
            if (!song.IsDisposed)
            {
                if (currentState != MediaStates.Stopped)
                    Stop();

                song = null;
            }
        }
    }
}
