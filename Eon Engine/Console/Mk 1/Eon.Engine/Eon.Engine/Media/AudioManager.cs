/* Created 04/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Eon.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace Eon.Engine.Media
{
    /// <summary>
    /// Defines a class that is used to manage the playing of music.
    /// </summary>
    public class AudioManager : EngineComponent, IDispose, IUpdate
    {
        static List<Cue> cueSounds = new List<Cue>();
        static List<Cue> cuesToDelete = new List<Cue>();

        static AudioEngine audioEngine;
        static WaveBank waveBank;
        static SoundBank soundBank;

        public string AudioFilepath { get; private set; }

        /// <summary>
        /// Creates a new AudioManager.
        /// </summary>
        /// <param name="xaptFilepath">The file location for the Xapt file.</param>
        /// <param name="xaptName">The name of the Xapt file.</param>
        /// <param name="waveBankName">The name given to the wave bank in the Xapt file.</param>
        /// <param name="soundBankName">The name given to the sound bank in the Xapt file.</param>
        public AudioManager(string xaptFilepath, string xaptName,
            string waveBankName, string soundBankName)
            : base("AudioManager")
        {
            string path = "Content/" + xaptFilepath + "/";

            AudioFilepath = xaptFilepath;
            audioEngine = new AudioEngine(path + xaptName + ".xgs");
            waveBank = new WaveBank(audioEngine, path + waveBankName + ".xwb");
            soundBank = new SoundBank(audioEngine, path + soundBankName + ".xsb");
        }

        /// <summary>
        /// Changes the volume of an existing audio category.
        /// </summary>
        /// <param name="value">The volume to change to.</param>
        /// <param name="audioCategory">The audio category to be eddited.</param>
        public static void ChangeVolume(float value, string audioCategory)
        {
            value = EonMathHelper.Clamp(value, 0, 100);

            if (audioEngine.GetCategory(audioCategory).Name != null)
                audioEngine.GetCategory(audioCategory).SetVolume(value);
            else
                throw new ArgumentNullException("Invalid audio category: " + audioCategory);
        }

        /// <summary>
        /// Used to update the AudioManager.
        /// </summary>
        public void _Update()
        {
            if (this.Enabled)
            {
                if (audioEngine != null)
                    audioEngine.Update();

                cuesToDelete.Clear();

                foreach (Cue c in cueSounds)
                    if (c.IsStopped)
                        cuesToDelete.Add(c);

                foreach (Cue c in cuesToDelete)
                {
                    cueSounds.Remove(c);
                    c.Dispose();
                }
            }
        }

        /// <summary>
        /// Disposes of all of the music.
        /// </summary>
        public void Dispose(bool finalize)
        {
            if (PlayingAnything())
                StopAll();

            if (audioEngine != null)
            {
                audioEngine.Dispose();
                audioEngine = null;
            }

            if (soundBank != null)
            {
                soundBank.Dispose();
                soundBank = null;
            }

            if (waveBank != null)
            {
                waveBank.Dispose();
                waveBank = null;
            }
        }

        /// <summary>
        /// Plays a sound.
        /// </summary>
        /// <param name="soundName">The name of the sound.</param>
        public static void PlaySound(string soundName)
        {
            PlaySnd(soundName);
        }
        static void PlaySnd(string name)
        {
            cueSounds.Add(soundBank.GetCue(name));
            cueSounds[cueSounds.Count - 1].Play();
        }

        /// <summary>
        /// A check to see if a spacific sound is playing.
        /// </summary>
        /// <param name="soundName">The name of the sound to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsPlaying(String soundName)
        {
            Cue c = soundBank.GetCue(soundName);

            foreach (Cue cu in cueSounds)
                if (c.ToString() == cu.ToString())
                    if (cu.IsPlaying)
                        return true;

            return false;
        }

        /// <summary>
        /// Pauses all of the audio from an audio category.
        /// </summary>
        /// <param name="audioCategoryName">The name of the audio category to be stopped.</param>
        public static void PauseAudioCategory(string audioCategoryName)
        {
            AudioCategory category = audioEngine.GetCategory(audioCategoryName);

            if (category.Name != null)
                category.Pause();
        }

        /// <summary>
        /// Resumes all of the audio from an audio category.
        /// </summary>
        /// <param name="audioCategoryName">The name of the audio category to be resumed.</param>
        public static void ResumeCategory(string audioCategoryName)
        {
            AudioCategory category = audioEngine.GetCategory(audioCategoryName);

            if (category.Name != null)
                category.Resume();
        }

        /// <summary>
        /// Stops playing anything from an audio category.
        /// </summary>
        /// <param name="audioCategoryName">The name of the audio category to stop playing from.</param>
        public static void StopAudioCategory(string audioCategoryName)
        {
            AudioCategory category = audioEngine.GetCategory(audioCategoryName);

            if (category.Name != null)
                category.Stop();
        }

        /// <summary>
        /// Plays a 3D sound.
        /// </summary>
        /// <param name="soundName">The name of the sound to play.</param>
        /// <param name="listnerPos">The position of the listner.</param>
        /// <param name="origin">The origin of the sound.</param>
        public static void Play3DSound(string soundName, Vector3 listnerPos, Vector3 origin)
        {
            float minDist = 1e10f;

            float dist = (origin - listnerPos).LengthSquared();

            if (dist < minDist)
                minDist = dist;

            Cue c = soundBank.GetCue(soundName);
            cueSounds.Add(c);

            c.SetVariable("Distance", (float)System.Math.Sqrt(minDist));

            c.Play();
        }

        /// <summary>
        /// Pauses all sounds that are currently playing.
        /// </summary>
        public static void PauseAll()
        {
            foreach (Cue c in cueSounds)
                if (c.IsPlaying)
                    c.Pause();
        }

        /// <summary>
        /// Pauses a playing sound.
        /// </summary>
        /// <param name="soundName">The name of the playing sound.</param>
        public static void Pause(string soundName)
        {
            for (int i = 0; i < cueSounds.Count; i++)
                if (cueSounds[i].Name == soundName)
                    cueSounds[i].Pause();
        }

        /// <summary>
        /// Resumes all paused sounds.
        /// </summary>
        public static void ResumeAll()
        {
            foreach (Cue c in cueSounds)
                if (c.IsPaused)
                    c.Resume();
        }

        /// <summary>
        /// Resumes a paused sound.
        /// </summary>
        /// <param name="soundName">The name of the paused sound.</param>
        public static void Resume(string soundName)
        {
            for (int i = 0; i < cueSounds.Count; i++)
                if (cueSounds[i].Name == soundName)
                    if (cueSounds[i].IsPaused)
                        cueSounds[i].Resume();
        }

        /// <summary>
        /// Stops a sound.
        /// </summary>
        /// <param name="soundName">The name of the sound to stop.</param>
        public static void Stop(string soundName)
        {
            for (int i = 0; i < cueSounds.Count; i++)
                if (cueSounds[i].Name == soundName)
                {
                    cueSounds[i].Stop(AudioStopOptions.Immediate);
                    cuesToDelete.Add(cueSounds[i]);
                }
        }

        /// <summary>
        /// Stops all of the sounds that are playing.
        /// </summary>
        public static void StopAll()
        {
            foreach (Cue c in cueSounds)
                if (c.IsPlaying)
                    c.Stop(AudioStopOptions.Immediate);
        }

        static bool PlayingAnything()
        {
            foreach (Cue c in cueSounds)
                if (c.IsPlaying)
                    return true;
                else
                    return false;

            return true;
        }

        public void ToggleEnabled()
        {
            Enabled = !Enabled;
        }
    }
}