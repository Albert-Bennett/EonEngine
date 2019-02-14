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
        Texture2D normalMap;
        Texture2D distortionMap;

        Vector2 origin;

        DrawingStage stage;

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
        /// <param name="normalMapFilepath">The filepath for the normal map.</param>
        /// <param name="distortionMapFilepath">The filepath for the distortion map.</param>
        public RandomTexture(int columns, int rows, int totalFrames,
            string textureFilepath, string normalMapFilepath,
            string distortionMapFilepath)
        {
            this.columns = columns;
            this.totalFrames = totalFrames;

            texture = Common.ContentManager.Load<Texture2D>(textureFilepath);
            try
            {
                normalMap = Common.ContentManager.Load<Texture2D>(normalMapFilepath);
            }
            catch
            {
                normalMap = Common.ContentManager.Load<Texture2D>("Eon/Textures/DefaultNormalMap");
            }

            try
            {
                distortionMap = Common.ContentManager.Load<Texture2D>(distortionMapFilepath);
            }
            catch
            {
                distortionMap = Common.ContentManager.Load<Texture2D>("Eon/Textures/DefaultDistortionMap");
            }

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
            int row = RandomHelper.GetRandom(0, rows);
            int column = RandomHelper.GetRandom(0, columns);

            while (row * column > totalFrames)
                column = RandomHelper.GetRandom(0, columns);

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

        public void PreDraw(DrawingStage stage)
        {
            this.stage = stage;
            currIndex = 0;
        }

        public void Draw(Vector2 position, float scale, float rotation, Color colour)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    {
                        Rectangle source = frames[currIndex].SourceRectangle;

                        Common.Batch.Draw(texture, position, source, colour,
                            rotation, origin * scale, scale, SpriteEffects.None, 0);
                    }
                    break;

                case DrawingStage.Normal:
                    {
                        Rectangle source = frames[currIndex].SourceRectangle;

                        if (normalMap != null)
                            Common.Batch.Draw(normalMap, position, source, colour,
                               rotation, origin * scale, scale, SpriteEffects.None, 0);
                    }
                    break;

                case DrawingStage.Distortion:
                    {
                        if (distortionMap != null)
                        {
                            Rectangle source = frames[currIndex].SourceRectangle;

                            Common.Batch.Draw(distortionMap, position, source, colour,
                               rotation, origin * scale, scale, SpriteEffects.None, 0);
                        }
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

    struct FrameState
    {
        public int Row;
        public int Column;

        public Rectangle SourceRectangle;
    }
}
