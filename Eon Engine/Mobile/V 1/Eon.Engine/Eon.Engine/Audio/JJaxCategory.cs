/* Created 07/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Engine.Audio
{
    /// <summary>
    /// Used to define a category of audio.
    /// </summary>
    public sealed class JJaxCategory : IDispose
    {
        List<JJaxCue> cues = new List<JJaxCue>();

        string name;
        float volume = 0.5f;

        /// <summary>
        /// The name of the J-JaxCategory.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The volume of the J-JaxCategory.
        /// </summary>
        public float Volume
        {
            get { return volume; }
        }

        /// <summary>
        /// The amount of sound currently being played.
        /// </summary>
        public int Count
        {
            get { return cues.Count; }
        }

        /// <summary>
        /// Creates a J-JaxCategory.
        /// </summary>
        /// <param name="name">The name of the J-Jaxcategory.</param>
        public JJaxCategory(string name)
        {
            this.name = name;
        }

        internal void Update()
        {
            for (int i = 0; i < cues.Count; i++)
                cues[i]._Update();
        }

        internal void Play(JJaxCue cue)
        {
            cue.OnStopped += new OnStoppedEvent(SongStopped);
            cue.Play();

            cues.Add(cue);
        }

        void SongStopped(JJaxCue song)
        {
            song.Dispose();
            cues.Remove(song);
        }

        /// <summary>
        /// Changes the volume of every J-JaxCue currently playing in this J-JaxCategory.
        /// </summary>
        /// <param name="volume">The new volume.</param>
        public void ChangeVolume(float volume)
        {
            this.volume = MathHelper.Clamp(volume, 0.1f, 1f);

            for (int i = 0; i < cues.Count; i++)
                cues[i].ChangeVolume(volume);
        }

        internal bool IsPlaying(string name)
        {
            JJaxCue cue = (from c in cues
                           where c.Name == name
                           select c).FirstOrDefault();

            if (cue == null)
                return false;

            return true;
        }

        /// <summary>
        /// Pauses/ Resumes all cues currently being played in the J-JaxCategory. 
        /// </summary>
        public void PauseResumeAll()
        {
            for (int i = 0; i < cues.Count; i++)
                cues[i].PauseResume();
        }

        internal void PauseResume(string name)
        {
            JJaxCue cue = (from c in cues
                           where c.Name == name
                           select c).FirstOrDefault();

            if (cue != null)
                cue.PauseResume();
        }

        /// <summary>
        /// Stops all cues currently being played in the J-JaxCategory. 
        /// </summary>
        public void StopAll()
        {
            for (int i = 0; i < cues.Count; i++)
                cues[i].Stop();
        }

        internal void Stop(string name)
        {
            JJaxCue cue = (from c in cues
                           where c.Name == name
                           select c).FirstOrDefault();

            if (cue != null)
                cue.Stop();
        }

        public void Dispose(bool finalize)
        {
            for (int i = 0; i < cues.Count; i++)
                cues[i].Dispose();
        }
    }
}
