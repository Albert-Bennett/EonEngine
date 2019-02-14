/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Eon.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Engine.Media
{
    /// <summary>
    /// Extends the functionality of the video available in xna.
    /// </summary>
    public class Cinematic : ObjectComponent, IUpdate
    {
        Microsoft.Xna.Framework.Media.VideoPlayer vidPlayer;
        Microsoft.Xna.Framework.Media.Video video;

        Texture2D currentFrame;
        MediaStates currentState = MediaStates.Stopped;

        /// <summary>
        /// The Texture2D from the current frame of the Cinematic.
        /// </summary>
        public Texture2D CurrentFrame
        {
            get { return currentFrame; }
        }

        /// <summary>
        /// The width of each video frame.
        /// </summary>
        public int Width
        {
            get { return video.Width; }
        }

        /// <summary>
        /// The height of each video frame.
        /// </summary>
        public int Height
        {
            get { return video.Height; }
        }

        /// <summary>
        /// The current state that the Cinematic is in.
        /// </summary>
        public MediaStates CurrentState
        {
            get { return currentState; }
        }

        /// <summary>
        /// The amount of frames per second this Cinematic is playing at.
        /// </summary>
        public float FramesPerSecond { get { return video.FramesPerSecond; } }

        /// <summary>
        /// The duration of this Cinematic in seconds.
        /// </summary>
        public float Duration { get { return (float)video.Duration.TotalSeconds; } }

        /// <summary>
        /// The amount of frames in this Cinematic.
        /// </summary>
        public int Frames { get { return (int)(FramesPerSecond * Duration); } }

        /// <summary>
        /// Wheather or not the video has ended.
        /// </summary>
        public bool HasEnded { get; private set; }

        /// <summary>
        /// Weather or not this Cinematic is going to loop after it has ended.
        /// </summary>
        public bool IsLooping { get; private set; }

        /// <summary>
        /// The aspect ratio of the Cinematic.
        /// </summary>
        public float AspectRatio
        {
            get
            {
                if (currentFrame != null)
                    return currentFrame.Width / currentFrame.Height;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Creates a new Cinematic.
        /// </summary>
        /// <param name="videoFilepath">The video's filepath.</param>
        public Cinematic(string id, string videoFilepath)
            : base(id)
        {
            this.video = Common.ContentManager.Load<Microsoft.Xna.Framework.Media.Video>(videoFilepath);
            vidPlayer = Common.VideoPlayer;
        }

        public void _Update()
        {
            if (vidPlayer.State == Microsoft.Xna.Framework.Media.MediaState.Stopped)
                HasEnded = true;

            if (currentState == MediaStates.Playing)
                currentFrame = vidPlayer.GetTexture();
            else if (IsLooping && currentState == MediaStates.Stopped)
                vidPlayer.Play(this.video);
        }

        /// <summary>
        /// Sets the volume of this Cinematic. 
        /// </summary>
        /// <param name="amount">The amount to set the volume by.</param>
        public void SetVolume(float amount)
        {
            if (amount > 1 || amount < 0)
                amount = EonMathHelper.Clamp(amount);

            vidPlayer.Volume = amount;
        }

        /// <summary>
        /// Pauses / resumes the Cinematic.
        /// </summary>
        public void PauseResume()
        {
            switch (currentState)
            {
                case MediaStates.Playing:
                    vidPlayer.Pause();
                    currentState = MediaStates.Paused;
                    break;
                case MediaStates.Paused:
                    vidPlayer.Resume();
                    currentState = MediaStates.Playing;
                    break;
            }
        }

        /// <summary>
        /// Stops the Cinematic from playing.
        /// </summary>
        public void Stop()
        {
            if (currentState != MediaStates.Stopped)
            {
                vidPlayer.Stop();
                IsLooping = false;
                HasEnded = true;
            }
        }

        /// <summary>
        /// Plays the video.
        /// </summary>
        public void Play()
        {
            vidPlayer.Play(video);
            HasEnded = false;
            currentState = MediaStates.Playing;
        }

        /// <summary>
        /// Loops the video.
        /// </summary>
        public void Loop()
        {
            if (!IsLooping)
                IsLooping = true;
        }
    }
}
