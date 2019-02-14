/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Particles.Attachments.Base;
using Eon.Particles.D2.RenderTypes.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.Particles.D2.RenderTypes
{
    /// <summary>
    /// Defines a Particle renderer that renders animated Particles.
    /// </summary>
    public sealed class AnimatedSpriteRenderer : IUpdateableRenderer
    {
        List<AnimatedSpriteCache> cache =
            new List<AnimatedSpriteCache>();

        Texture2D texture;
        Rectangle[] sourceRects;

        Vector2 origin;

        TimeSpan timeBetweenFrames;

        public AnimatedSpriteRenderer(string textureFilepath,
            int rows, int columns, int totalFrames, float timeBetweenFrames)
        {
            this.timeBetweenFrames = TimeSpan.FromMilliseconds(timeBetweenFrames);

            try
            {
                texture = Common.ContentManager.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                texture = Common.ContentManager.Load<Texture2D>("Eon/Textures/DefaultTexture");
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

            origin = new Vector2(sourceRects[0].Width / 2, sourceRects[0].Height / 2);
        }

        public void GenerateNew()
        {
            cache.Add(new AnimatedSpriteCache()
            {
                Index = RandomHelper.GetRandom(0, sourceRects.Length),
                currentTime = TimeSpan.Zero
            });
        }

        public void Update()
        {
            for (int i = 0; i < cache.Count; i++)
            {
                AnimatedSpriteCache c = cache[i];

                if (c.currentTime + Common.ElapsedTimeDelta > timeBetweenFrames)
                {
                    c.currentTime = TimeSpan.Zero;

                    if (c.Index++ >= sourceRects.Length)
                        c.Index = 0;
                    else
                        c.Index++;
                }
                else
                    c.currentTime += Common.ElapsedTimeDelta;

                cache[i] = c;
            }
        }

        public void Remove(int index)
        {
            cache.RemoveAt(index);
        }

        public void Draw(List<PropertySet> properties)
        {
            for (int i = 0; i < properties.Count; i++)
                Common.Batch.Draw(texture,
                    new Vector2(properties[i].Position.X, properties[i].Position.Y),
                    sourceRects[cache[i].Index],
                    properties[i].Colour, properties[i].Rotation.Z,
                    origin, properties[i].Scale, SpriteEffects.None, 0);
        }

        public void Reset()
        {
            cache.Clear();
        }
    }
}
