/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Interfaces;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.Particles2D.Types
{
    /// <summary>
    /// Defines an animated particle.
    /// </summary>
    public class SimpleAnimation : IParticleType, IUpdate, IChangeable
    {
        List<AnimatedFrame> frames = new List<AnimatedFrame>();
        Rectangle sourceRect;

        Texture2D texture;
        Texture2D normalMap;
        Texture2D distortionMap;

        DrawingStage stage;

        int totalFrames;
        int columns;
        int rows;
        int currIndex;

        int width;
        int height;

        TimeSpan frameRate;

        /// <summary>
        /// Creates a new SimpleAnimation particle type.
        /// </summary>
        /// <param name="columns">The number of columns in the image.</param>
        /// <param name="rows">The number of rows in this image.</param>
        /// <param name="totalFrames">The total number of frames that the image contains.</param>
        /// <param name="frameRate">The amount of time it takes to change frames.</param>
        /// <param name="textureFilepath">The filepath of the texture.</param>
        public SimpleAnimation(int columns, int rows, int totalFrames, float frameRate,
            string textureFilepath)
            : this(columns, rows, totalFrames, frameRate, textureFilepath, "None", "None") { }

        /// <summary>
        /// Creates a new SimpleAnimation particle type.
        /// </summary>
        /// <param name="columns">The number of columns in the image.</param>
        /// <param name="rows">The number of rows in this image.</param>
        /// <param name="totalFrames">The total number of frames that the image contains.</param>
        /// <param name="frameRate">The amount of time it takes to change frames.</param>
        /// <param name="textureFilepath">The filepath of the texture.</param>
        /// <param name="normalMapFilepath">The filepath for the normal map.</param>
        public SimpleAnimation(int columns, int rows, int totalFrames, float frameRate,
            string textureFilepath, string normalMapFilepath)
            : this(columns, rows, totalFrames, frameRate, textureFilepath, normalMapFilepath, "None") { }

        /// <summary>
        /// Creates a new SimpleAnimation particle type.
        /// </summary>
        /// <param name="columns">The number of columns in the image.</param>
        /// <param name="rows">The number of rows in this image.</param>
        /// <param name="totalFrames">The total number of frames that the image contains.</param>
        /// <param name="frameRate">The amount of time it takes to change frames.</param>
        /// <param name="textureFilepath">The filepath of the texture.</param>
        /// <param name="normalMapFilepath">The filepath for the normal map.</param>
        /// <param name="distortionMapFilepath">The filepath for the distortion map.</param>
        public SimpleAnimation(int columns, int rows, int totalFrames, float frameRate,
            string textureFilepath, string normalMapFilepath,
            string distortionMapFilepath)
        {
            this.columns = columns;
            this.totalFrames = totalFrames;

            this.frameRate = TimeSpan.FromMilliseconds(frameRate);

            texture = Common.ContentManager.Load<Texture2D>(textureFilepath);

            if (normalMapFilepath != "None")
                normalMap = Common.ContentManager.Load<Texture2D>(normalMapFilepath);

            if (distortionMapFilepath != "None")
                distortionMap = Common.ContentManager.Load<Texture2D>(distortionMapFilepath);

            this.rows = rows;

            width = texture.Width / columns;
            height = texture.Height / rows;

            sourceRect = new Rectangle(0, 0, width, height);
        }

        /// <summary>
        /// Generates a new frame.
        /// </summary>
        public void Generate()
        {
            frames.Add(GenerateFrame());
        }

        public void Remove(int index)
        {
            frames.Remove(frames[index]);
        }

        AnimatedFrame GenerateFrame()
        {
            return new AnimatedFrame(columns, rows,
                totalFrames, frameRate, width, height);
        }

        public void _Update()
        {
            for (int i = 0; i < frames.Count; i++)
            {
                AnimatedFrame f = frames[i];
                f.Update();

                frames[i] = f;
            }
        }

        public void PreDraw(DrawingStage stage)
        {
            currIndex = 0;
            this.stage = stage;
        }

        public void Draw(Vector2 position, float scale, float rotation, Color colour)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    {
                        Rectangle source = frames[currIndex].SourceRect;
                        Vector2 origin = frames[currIndex].Origin;

                        Common.Batch.Draw(texture, position, source, colour,
                            rotation, origin, scale, SpriteEffects.None, 0);
                    }
                    break;

                case DrawingStage.Normal:
                    if (normalMap != null)
                    {
                        Rectangle source = frames[currIndex].SourceRect;
                        Vector2 origin = frames[currIndex].Origin;

                        Common.Batch.Draw(normalMap, position, source, colour,
                           rotation, origin, scale, SpriteEffects.None, 0);
                    }
                    break;

                case DrawingStage.Distortion:
                    if (distortionMap != null)
                    {
                        Rectangle source = frames[currIndex].SourceRect;
                        Vector2 origin = frames[currIndex].Origin;

                        Common.Batch.Draw(distortionMap, position, source, colour,
                           rotation, origin, scale, SpriteEffects.None, 0);
                    }
                    break;
            }

            currIndex++;
        }

        public void Dispose(bool finalize)
        {
            if (texture != null)
            {
                if (finalize)
                    texture.Dispose();

                texture = null;
            }

            if (normalMap != null)
            {
                if (finalize)
                    normalMap.Dispose();

                normalMap = null;
            }

            if (distortionMap != null)
            {
                if (finalize)
                    distortionMap.Dispose();

                distortionMap = null;
            }

            frames.Clear();
            frames = null;
        }
    }
}
