/* Created 30/05/2015
 * Last Updated: 30/05/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Animation2D.SpriteSheet
{
    /// <summary>
    /// Defines a SpriteSheet (.SPR).
    /// [Serializable]
    /// </summary>
    public class SpriteSheet
    {
        Texture2D texture;
        Texture2D normalMap;
        Texture2D distortion;

        Rectangle sourceRect;
        TimeSpan frameRate;

        int moveWidth;
        int moveHeight;

        bool initialized = false;

        public string TextureFilepath = "Eon/Textures/Blank";
        public string NormalMapFilepath = "Eon/Textures/Blank";
        public string DistortionMapFilepath = "Eon/Textures/Blank";

        public Vector2 Origin = Vector2.Zero;

        public int Rows;
        public int Columns;
        public int TotalFrames;

        public int TimeBetweenFrames = 10;

        /// <summary>
        /// The time between frames.
        /// </summary>
        public TimeSpan FrameRate
        {
            get { return frameRate; }
        }

        /// <summary>
        /// Texture.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Normal Map.
        /// </summary>
        public Texture2D NormalMap
        {
            get { return normalMap; }
        }

        /// <summary>
        /// Distortion Map.
        /// </summary>
        public Texture2D DistortionMap
        {
            get { return distortion; }
        }

        /// <summary>
        /// The initial source rectangle.
        /// </summary>
        public Rectangle SourceRectangle
        {
            get { return sourceRect; }
        }

        /// <summary>
        /// Width of a frame in the SpriteSheet.
        /// </summary>
        public int FrameWidth
        {
            get { return moveWidth; }
        }

        /// <summary>
        /// Height of a frame in the SpriteSheet.
        /// </summary>
        public int FrameHeight
        {
            get { return moveHeight; }
        }

        /// <summary>
        /// Initializes the SpriteSheet.
        /// </summary>
        public void Initalize()
        {
            if (!initialized)
            {
                try
                {
                    texture = Common.ContentBuilder.Load<Texture2D>(TextureFilepath);
                }
                catch
                {
                    new Error("The SpriteSheet is invalid because: " +
                        TextureFilepath + ", dosen't exist.", Seriousness.CriticalError);
                }

                try
                {
                    normalMap = Common.ContentBuilder.Load<Texture2D>(NormalMapFilepath);
                }
                catch
                {
                    new Error("The SpriteSheet is invalid because: " +
                        NormalMapFilepath + ", dosen't exist.", Seriousness.CriticalError);
                }

                try
                {
                    distortion = Common.ContentBuilder.Load<Texture2D>(DistortionMapFilepath);
                }
                catch
                {
                    new Error("The SpriteSheet is invalid because: " +
                        DistortionMapFilepath + ", dosen't exist.", Seriousness.CriticalError);
                }

                moveHeight = texture.Height / Rows;
                moveWidth = texture.Width / Columns;

                sourceRect = new Rectangle()
                {
                    X = 0,
                    Y = 0,
                    Width = moveWidth,
                    Height = moveHeight
                };

                frameRate = TimeSpan.FromMilliseconds(TimeBetweenFrames);

                initialized = true;
            }
        }
    }
}
