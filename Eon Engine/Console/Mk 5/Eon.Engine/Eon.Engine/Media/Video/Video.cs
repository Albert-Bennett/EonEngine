/* Created 01/04/2015
 * Last Updated: 01/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.System.States;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Eon.Engine.Media
{
    /// <summary>
    /// Extends the functionality of the video available in xna.
    /// </summary>
    public sealed class Video
    {
        Microsoft.Xna.Framework.Media.VideoPlayer vidPlayer;
        Microsoft.Xna.Framework.Media.Video video;

        MediaStates currentState = MediaStates.Stopped;

        bool looping = false;
        bool ended = false;

        float aspectRatio;

        /// <summary>
        /// The Texture2D from the current frame of the Video.
        /// </summary>
        public Texture2D CurrentFrame
        {
            get { return vidPlayer.GetTexture(); }
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
        /// The current state that the Video is in.
        /// </summary>
        public MediaStates CurrentState
        {
            get { return currentState; }
        }

        /// <summary>
        /// The amount of frames per second this Video is playing at.
        /// </summary>
        public float FramesPerSecond { get { return video.FramesPerSecond; } }

        /// <summary>
        /// The duration of this Video in seconds.
        /// </summary>
        public float Duration { get { return (float)video.Duration.TotalSeconds; } }

        /// <summary>
        /// The amount of frames in this Video.
        /// </summary>
        public int Frames { get { return (int)(FramesPerSecond * Duration); } }

        /// <summary>
        /// Whether or not the video has ended.
        /// </summary>
        public bool HasEnded
        {
            get { return ended; }
        }

        /// <summary>
        /// Whether or not this Video is going to loop after it has ended.
        /// </summary>
        public bool IsLooping
        {
            get { return looping; }
            set { looping = value; }
        }

        /// <summary>
        /// The aspect ratio of the Video.
        /// </summary>
        public float AspectRatio { get { return aspectRatio; } }

        /// <summary>
        /// Creates a new Video.
        /// </summary>
        /// <param name="videoFilepath">The video's filepath.</param>
        public Video(string videoFilepath)
        {
            this.video = Common.ContentBuilder.Load<Microsoft.Xna.Framework.Media.Video>(videoFilepath, "Media");
            vidPlayer = new VideoPlayer();

            aspectRatio = video.Width / video.Height;
        }

        public void Update()
        {
            if (vidPlayer.State == MediaState.Stopped && IsLooping && !HasEnded)
                vidPlayer.Play(video);
            else if (vidPlayer.State == MediaState.Stopped && !IsLooping)
            {
                currentState = MediaStates.Stopped;
                ended = true;
            }
        }

        /// <summary>
        /// Sets the volume of this Video. 
        /// </summary>
        /// <param name="amount">The amount to set the volume by.</param>
        public void SetVolume(float amount)
        {
            if (amount > 1 || amount < 0)
                amount = EonMathsHelper.Clamp(amount);

            vidPlayer.Volume = amount;
        }

        /// <summary>
        /// Plays the Video.
        /// </summary>
        public void Play()
        {
            vidPlayer.Play(video);
            ended = false;
            currentState = MediaStates.Playing;
        }

        /// <summary>
        /// Pauses / resumes the Video.
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
        /// Stops the Video from playing.
        /// </summary>
        public void Stop()
        {
            if (currentState != MediaStates.Stopped)
            {
                vidPlayer.Stop();
                ended = true;
            }
        }
    }
}
