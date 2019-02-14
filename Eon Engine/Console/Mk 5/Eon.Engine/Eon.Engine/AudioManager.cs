/* Created: 05/03/2014
 * Last Updated: 15/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Media.Audio;
using Eon.Engine.Media.Audio.Components;
using Eon.Helpers;
using Eon.System.Interfaces.Base;
using Eon.System.Management;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eon.Engine
{
    /// <summary>
    /// Defines a class that is used to manage the cues of music.
    /// </summary>
    public sealed class AudioManager : EngineModule, IUpdate
    {
        static List<CueInfo> cueInfos = new List<CueInfo>();
        static List<JJaxCategory> categories = new List<JJaxCategory>();
        static List<AudioComponent> components = new List<AudioComponent>();

        static AudioListener listener;

        JJax file;

        /// <summary>
        /// The listner of the sounds.
        /// </summary>
        public static AudioListener Listener
        {
            get { return listener; }
        }

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
            ReCreate(jJaxFilepath);
        }

        /// <summary>
        /// Loads a differnt set of J-JaxCues.
        /// </summary>
        /// <param name="jJaxFilepath">The filepath of the J-Jax file.</param>
        public void ReCreate(string jJaxFilepath)
        {
            if (categories != null)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    categories[i].StopAll();
                    categories[i].Dispose();
                }

                categories.Clear();
            }

            if (components != null)
            {
                foreach (AudioComponent c in components)
                    c.Destroy();

                components.Clear();
            }

            try
            {
                if (file != null)
                    Common.ContentBuilder.Unload("Media");
                else
                    Common.ContentBuilder.AddContentTyper("Media", true);

                file = SerializationHelper.Deserialize<JJax>(jJaxFilepath, true, ".JJAX");
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
        /// Gets the volume of a specific category.
        /// </summary>
        /// <param name="categoryName">The name of the category to get the volume of.</param>
        /// <returns>The result of the search.</returns>
        public static float GetCategoryVolume(string categoryName)
        {
            bool found = false;
            int idx = 0;

            while (idx < categories.Count && !found)
            {
                if (categories[idx].Name == categoryName)
                {
                    found = true;
                    return categories[idx].Volume;
                }

                idx++;
            }

            if (!found)
                new Error("No category named: " + categoryName, Seriousness.Warning);

            return 0.0f;
        }

        internal static void Add(JJaxCue cue)
        {
            bool found = false;
            int idx = 0;

            while (!found && idx < categories.Count)
            {
                if (categories[idx].Name == cue.Category)
                {
                    found = true;
                    categories[idx].Add(cue);
                }

                idx++;
            }
        }

        internal static void Remove(JJaxCue cue)
        {
            bool found = false;
            int idx = 0;

            while (!found && idx < categories.Count)
            {
                if (categories[idx].Name == cue.Category)
                {
                    found = true;
                    categories[idx].Remove(cue);
                }

                idx++;
            }
        }

        internal static void Add(AudioComponent component)
        {
            AudioComponent comp = null;

            comp = (from c in components
                    where c.Name == component.Name
                    select c).FirstOrDefault();

            if (comp == null)
            {
                components.Add(component);
            }
        }

        /// <summary>
        /// A check to see if the listner exists.
        /// </summary>
        /// <returns>The result of the check.</returns>
        public static bool ListnerExists()
        {
            return listener != null;
        }

        /// <summary>
        /// Sets teh position of the listener for audio.
        /// </summary>
        /// <param name="listenerPosition">The position of the listener.</param>
        public static void SetListnerPosition(Vector3 listenerPosition)
        {
            if (listener == null)
                listener = new AudioListener();

            listener.Position = listenerPosition;
        }

        /// <summary>
        /// Destroys the listner.
        /// </summary>
        public static void DestroyListner()
        {
            listener = null;
        }

        /// <summary>
        /// Used to update the AudioManager.
        /// </summary>
        public void _Update()
        {
            if (this.Enabled)
            {
                for (int i = 0; i < categories.Count; i++)
                    categories[i].Update();

                for (int i = 0; i < components.Count; i++)
                    components[i]._Update();
            }
        }

        public void _PostUpdate() { }

        /// <summary>
        /// Changes the volume of an JJaxCategory
        /// </summary>
        /// <param name="categoryName">The name of the JJaxCategory 
        /// to have the volume changed.</param>
        /// <param name="volume">The new volume.</param>
        public static void ChangeVolume(string categoryName, float volume)
        {
            JJaxCategory cat = (from c in categories
                                where c.Name == categoryName
                                select c).FirstOrDefault();

            if (cat != null)
                cat.ChangeVolume(volume);
            else
                new Error("The category: " + categoryName + " dosen't Exist.", Seriousness.Warning);
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
                    new Error("Sound dosn't exist " + soundName, Seriousness.Error);
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
        /// <param name="categoryName">The name of the J-Jax Category that the sound exists in.</param>
        /// <param name="soundName">The name of the JSong to be stopped.</param>
        public static void StopSound(string categoryName, string soundName)
        {
            JJaxCategory cat = (from c in categories
                                where c.Name == categoryName
                                select c).FirstOrDefault();

            if (cat != null)
                cat.Stop(soundName);
        }

        /// <summary>
        /// Stops a J-JaxCue.
        /// </summary>
        /// <param name="soundName">The name of the JSong to be stopped.</param>
        public static void Stop(string soundName)
        {
            int idx = 0;
            bool found = false;

            while (!found && idx < categories.Count)
            {
                if (categories[idx].IsPlaying(soundName))
                {
                    categories[idx].Stop(soundName);

                    found = true;
                }

                idx++;
            }
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
        /// Finds a CueInfo for a particular sound.
        /// </summary>
        /// <param name="soundName">The name of the sound.</param>
        /// <returns>The result of the check.</returns>
        public static CueInfo GetCue(string soundName)
        {
            for (int i = 0; i < cueInfos.Count; i++)
                if (cueInfos[i].Name == soundName)
                    return cueInfos[i];

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

        protected override void Destroy()
        {
            for (int i = 0; i < categories.Count; i++)
            {
                categories[i].StopAll();
                categories[i].Dispose();
            }

            categories.Clear();

            base.Destroy();
        }


        internal static void Destroy(AudioComponent component)
        {
            if (components.Contains(component))
                components.Remove(component);
        }

        /// <summary>
        /// Destroys an AudioComponent. 
        /// </summary>
        /// <param name="componentName">The name of the AudioComponent.</param>
        public static void Destroy(string componentName)
        {
            AudioComponent comp = null;

            comp = (from c in components
                    where c.Name == componentName
                    select c).FirstOrDefault();

            if (comp != null)
                Destroy(comp);
        }
    }
}