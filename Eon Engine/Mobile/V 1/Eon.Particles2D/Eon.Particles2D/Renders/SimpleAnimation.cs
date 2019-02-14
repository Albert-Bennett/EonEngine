/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.Particles2D.Renders
{
    /// <summary>
    /// Defines an animated particle.
    /// </summary>
    public class SimpleAnimation : IParticleType, IUpdate, IChangeable
    {
        List<AnimatedFrame> frames = new List<AnimatedFrame>();
        Rectangle sourceRect;

        Texture2D texture;

        int totalFrames;
        int columns;
        int rows;
        int currIndex;

        int width;
        int height;

        float frameRate;

        public int Priority
        {
            get { return 0; }
        }

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
        {
            this.columns = columns;
            this.totalFrames = totalFrames;

            this.frameRate = frameRate;

            texture = Common.ContentManager.Load<Texture2D>(textureFilepath);

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

        public void PreDraw()
        {
            currIndex = 0;
        }

        public void Draw(Vector2 position, float scale, float rotation, Color colour)
        {
            Rectangle source = frames[currIndex].SourceRect;
            Vector2 origin = frames[currIndex].Origin;

            Common.Batch.Draw(texture, position, source, colour,
                rotation, origin, scale, SpriteEffects.None, 0);

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

            frames.Clear();
            frames = null;
        }
    }
}
