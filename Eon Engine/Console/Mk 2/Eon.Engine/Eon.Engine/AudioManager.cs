/* Created 05/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Audio;
using Eon.Helpers;
using Eon.System.Interfaces;
using Eon.System.Management;
using Eon.Testing.ErrorManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eon.Engine
{
    /// <summary>
    /// Defines a class that is used to manage the cues of music.
    /// </summary>
    public class AudioManager : EngineComponent, IDispose, IUpdate
    {
        static List<CueInfo> cueInfos = new List<CueInfo>();
        static List<JJaxCategory> categories = new List<JJaxCategory>();

        internal static AudioListener listener = new AudioListener();

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new AudioManager.
        /// </summary>
        /// <param name="jJaxFilepath">The filepath for the J-Jax file.</param>
        public AudioManager(string jJaxFilepath)
            : base("AudioManager")
        {
            JJax file = null;

            try
            {
                file = XmlHelper.DeserializeContent<JJax>(jJaxFilepath + ".JJAX");
            }
            catch (FileNotFoundException)
            {
                new Error("No JJFile found at: " + jJaxFilepath, Seriousness.CriticalError);
            }
            catch
            {
                new Error("Problem with JJax file: " + jJaxFilepath, Seriousness.CriticalError);
            }

            if (file != null)
            {
                if (file.Info.Count > 0)
                    for (int i = 0; i < file.Info.Count; i++)
                        cueInfos.Add(file.Info[i]);
                else
                    new Error("No sounds found in: " + jJaxFilepath, Seriousness.Error);

                if (file.SoundCategories.Count > 0)
                    for (int i = 0; i < file.SoundCategories.Count; i++)
                        categories.Add(new JJaxCategory(file.SoundCategories[i]));
                else
                    new Error("No sound categories found in: " + jJaxFilepath, Seriousness.Warning);
            }
        }

        /// <summary>
        /// Sets teh position of the listener for audio.
        /// </summary>
        /// <param name="listenerPosition">The position of the listener.</param>
        public static void SetListnerPosition(Vector3 listenerPosition)
        {
            listener.Position = listenerPosition;
        }

        /// <summary>
        /// Used to update the AudioManager.
        /// </summary>
        public void _Update()
        {
            if (this.Enabled)
                for (int i = 0; i < categories.Count; i++)
                    categories[i].Update();
        }

        /// <summary>
        /// Changes the volume of an JJaxCategory
        /// </summary>
        /// <param name="categoryName">The name of the JJaxCategory 
        /// to have the volume changed.</param>
        /// <param name="volume">The new volume.</param>
        public static void ChangeVolume(string categoryName, float volume)
        {
            for (int i = 0; i < categories.Count; i++)
                if (categories[i].Name == categoryName)
                    categories[i].ChangeVolume(volume);
        }

        /// <summary>
        /// Used to play a Sound.
        /// </summary>
        /// <param name="songName">The name of the Jsong.</param>
        public static void Play(string songName)
        {
            PlaySound3D(songName, Vector3.Zero);
        }

        /// <summary>
        /// Plays a sound that sounds like it's being played in 3D space. 
        /// </summary>
        /// <param name="soundName">The name of the sound.</param>
        /// <param name="position">The position of the sound.</param>
        public static void PlaySound3D(string soundName, Vector3 position)
        {
            if (soundName != null)
                if (GetTotalBeingPlayed() <= 60)
                {
                    CueInfo info = (from c in cueInfos
                                    where c.Name == soundName
                                    select c).FirstOrDefault();

                    if (info != null)
                    {
                        if (!info.SingleInstance || (info.SingleInstance && !IsPlaying(soundName)))
                        {
                            JJaxCue song;

                            if (position != Vector3.Zero)
                                song = new JJaxCue(info, position);
                            else
                                song = new JJaxCue(info);

                            JJaxCategory cate = GetCategory(soundName);
                            cate.Play(song);
                        }
                    }
                    else
                        throw new ArgumentNullException("Sound dosn't exist " + soundName);
                }
        }

        static int GetTotalBeingPlayed()
        {
            int total = 0;

            for (int i = 0; i < categories.Count; i++)
                total += categories[i].Count;

            return total;
        }

        /// <summary>
        /// Finds out is a particular song is being played.
        /// </summary>
        /// <param name="name">The name of the song to be checked.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsPlaying(string name)
        {
            JJaxCategory cat = GetCategory(name);

            if (cat != null)
                return cat.IsPlaying(name);

            return false;
        }

        /// <summary>
        /// Pauses \ resumes a JSong.
        /// </summary>
        /// <param name="name">The name of the JSong.</param>
        public static void PauseResume(string name)
        {
            JJaxCategory cat = GetCategory(name);

            if (cat != null)
                cat.PauseResume(name);
        }

        /// <summary>
        /// Pauses \ resumes every song currently being played.
        /// </summary>
        public static void PauseResumeAll()
        {
            for (int i = 0; i < categories.Count; i++)
                categories[i].PauseResumeAll();
        }

        /// <summary>
        /// Stops a J-JaxCue.
        /// </summary>
        /// <param name="name">The name of the JSong to be stopped.</param>
        public static void Stop(string name)
        {
            JJaxCategory cat = GetCategory(name);

            if (cat != null)
                cat.Stop(name);
        }

        static JJaxCategory GetCategory(string name)
        {
            CueInfo info = (from c in cueInfos
                            where c.Name == name
                            select c).FirstOrDefault();

            if (info != null)
            {
                JJaxCategory cat = (from c in categories
                                    where c.Name == info.CategoryName
                                    select c).FirstOrDefault();

                return cat;
            }

            return null;
        }

        /// <summary>
        /// Stops all of the music.
        /// </summary>
        public static void StopAll()
        {
            for (int i = 0; i < categories.Count; i++)
                categories[i].StopAll();
        }

        public void ToggleEnabled()
        {
            Enabled = !Enabled;
        }

        /// <summary>
        /// Disposes of all of the music.
        /// </summary>
        public void Dispose(bool finalize)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                categories[i].StopAll();
                categories[i].Dispose(finalize);
            }

            categories.Clear();
        }
    }
}