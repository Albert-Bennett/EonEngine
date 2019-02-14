/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Particles2D.Renders
{
    /// <summary>
    /// Defines a particle which can have one of several textures draw for it.
    /// </summary>
    public class RandomTexture : IParticleType, IChangeable
    {
        List<FrameState> frames = new List<FrameState>();
        Rectangle sourceRect;

        Texture2D texture;

        Vector2 origin;

        int totalFrames;
        int rows;
        int columns;
        int currIndex;

        int width;
        int height;

        /// <summary>
        /// Creates a new RandomTexture particle type.
        /// </summary>
        /// <param name="columns">The number of columns in the image.</param>
        /// <param name="rows">The number of rows in the image.</param>
        /// <param name="totalFrames">The total number of frames that the image contains.</param>
        /// <param name="textureFilepath">The filepath of the texture.</param>
        public RandomTexture(int columns, int rows, int totalFrames,
            string textureFilepath)
        {
            this.columns = columns;
            this.totalFrames = totalFrames;

            texture = Common.ContentManager.Load<Texture2D>(textureFilepath);

            this.rows = rows;

            width = texture.Width / columns;
            height = texture.Height / rows;

            sourceRect = new Rectangle(0, 0, width, height);
            origin = new Vector2(width / 2, height / 2);
        }

        /// <summary>
        /// Generates a new frame location.
        /// </summary>
        public void Generate()
        {
            frames.Add(GenerateLocation());
        }

        FrameState GenerateLocation()
        {
            int row = RandomHelper.GetRandomInt(0, rows);
            int column = RandomHelper.GetRandomInt(0, columns);

            while (row * column > totalFrames)
                column = RandomHelper.GetRandomInt(0, columns);

            return new FrameState()
            {
                Row = row,
                Column = column,
                SourceRectangle = new Rectangle(row * width, column * height, width, height)
            };
        }

        public void Remove(int index)
        {
            frames.Remove(frames[index]);
        }

        public void PreDraw()
        {
            currIndex = 0;
        }

        public void Draw(Vector2 position, float scale, float rotation, Color colour)
        {
            Rectangle source = frames[currIndex].SourceRectangle;

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

    struct FrameState
    {
        public int Row;
        public int Column;

        public Rectangle SourceRectangle;
    }
}
