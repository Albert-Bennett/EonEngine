/* Created: 01/09/2014
 * Last Updated: 19/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Particles.Attachments.Base;
using Eon.Particles.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Particles.D2.RenderMethods
{
    /// <summary>
    /// Defines a Renderer that is used to keep track of 
    /// various random textures for Particles.
    /// </summary>
    public sealed class RandomTextureRenderer : IUpdateableRenderer, I2DParticleRenderer
    {
        List<int> cache = new List<int>();
        Rectangle[] sourceRects;

        Texture2D texture;

        Vector2 origin;

        public RandomTextureRenderer(string textureFilepath,
            int rows, int columns, int totalFrames)
        {
            try
            {
                texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                texture = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/DefaultTexture");
            }

            GetSourceRects(totalFrames, rows, columns);
        }

        void GetSourceRects(int totalFrames, int rows, int cols)
        {
            sourceRects = new Rectangle[totalFrames];

            int width = texture.Width / cols;
            int height = texture.Height / rows;

            int currRow = 0;
            int currCol = 0;

            Vector2 vec = Vector2.Zero;
            int count = 0;

            while (count < totalFrames)
            {
                sourceRects[count] = new Rectangle(
                  currCol * width, currRow * height, width, height);

                count++;

                currCol++;

                if (currCol >= cols)
                {
                    currCol = 0;
                    currRow++;
                }
            }

            origin = new Vector2(sourceRects[0].Width / 2,
                sourceRects[0].Height / 2);
        }

        public void GenerateNew()
        {
            cache.Add(RandomHelper.GetRandom(0, sourceRects.Length));
        }

        public void Update() { }

        public void Remove(int index)
        {
            cache.RemoveAt(index);
        }

        public void Render(List<PropertySet> properties)
        {
            for (int i = 0; i < properties.Count; i++)
                Common.Batch.Draw(texture,
                    new Vector2(properties[i].Position.X, properties[i].Position.Y),
                    sourceRects[cache[i]], properties[i].Colour,
                    properties[i].Rotation.Z, origin, properties[i].Scale,
                    SpriteEffects.None, 0);
        }

        public void Reset()
        {
            cache.Clear();
        }
    }
}
