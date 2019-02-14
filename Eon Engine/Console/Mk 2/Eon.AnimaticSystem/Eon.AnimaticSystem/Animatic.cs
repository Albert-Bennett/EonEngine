/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.System.Interfaces;
using Eon.System.Management;
using System.Collections.Generic;

namespace Eon.AnimaticSystem
{
    /// <summary>
    /// Defines an object that can execute a 
    /// series of Actions's simultaneously.
    /// </summary>
    public sealed class Animatic : IUpdate
    {
        List<AnimaticStream> streams =
            new List<AnimaticStream>();

        internal FinishedAnimaticEvent OnFinished;

        bool playing = false;
        bool init = false;

        GameStates activeState;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The GameState when this will play.
        /// </summary>
        internal GameStates ActiveInState
        {
            get { return activeState; }
        }

        /// <summary>
        /// Creates a new Animatic. 
        /// </summary>
        /// <param name="filepath">The filepath of the Animatic.</param>
        public Animatic(string filepath)
        {
            AnimaticInfo info = XmlHelper.Deserialize<AnimaticInfo>(filepath, ".Animatic", true);

            activeState = info.ActiveInState;

            for (int i = 0; i < info.Streams.Length; i++)
            {
                AnimaticStream stream = new AnimaticStream(info.Streams[i], i);
                stream.OnEnd += new EndOfStreamEvent(OnEnd);

                streams.Add(stream);
            }

            AnimaticManager am = (AnimaticManager)EngineComponentManager.Find("AnimaticManager");

            if (am != null)
                am.Add(this);
        }

        void OnEnd(int streamNumber)
        {
            streams.Remove(streams[streamNumber]);

            if (streams.Count == 0 && OnFinished != null)
                OnFinished();
        }

        internal AnimaticStream GetStream(int streamNumber)
        {
            return streams[streamNumber];
        }

        public void Play()
        {
            if (!init)
            {
                for (int i = 0; i < streams.Count; i++)
                    streams[i].Init();

                init = true;
            }

            playing = true;
        }

        public void Pause()
        {
            playing = false;
        }

        public void _Update()
        {
            if (playing)
                for (int i = 0; i < streams.Count; i++)
                    streams[i]._Update();
        }

        public void Dispose()
        {
            for (int i = 0; i < streams.Count; i++)
                streams[i].Dispose();

            streams.Clear();
        }
    }
}
