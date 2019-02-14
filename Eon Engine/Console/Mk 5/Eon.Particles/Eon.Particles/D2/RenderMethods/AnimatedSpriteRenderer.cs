/* Created: 01/09/2014
 * Last Updated: 31/05/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Particles.Attachments.Base;
using Eon.Particles.Base;
using Eon.Rendering2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.Particles.D2.RenderMethods
{
    /// <summary>
    /// Defines a Particle renderer that renders animated Particles.
    /// </summary>
    public sealed class AnimatedSpriteRenderer : IUpdateableRenderer, I2DParticleRenderer
    {
        TextureAtlas spriteSheet;

        List<AnimatedSpriteCache> cache =
            new List<AnimatedSpriteCache>();

        /// <summary>
        /// Creates a new AnimatedSpriteRenderer.
        /// </summary>
        /// <param name="spriteSheetFilepath">The filepath for the SpriteSheet.</param>
        public AnimatedSpriteRenderer(string spriteSheetFilepath)
        {
            Type[] extraTypes = new Type[]
            {
                typeof(Vector2),
                typeof(int),
                typeof(string)
            };

            spriteSheet = SerializationHelper.Deserialize<TextureAtlas>(
                spriteSheetFilepath, true, ".SPR", extraTypes);

            spriteSheet.Initalize();
        }

        public void GenerateNew()
        {
            cache.Add(new AnimatedSpriteCache()
            {
                Column = 0,
                Row = 0,
                Frame = 0,
                currentTime = TimeSpan.Zero
            });
        }

        public void Update()
        {
            for (int i = 0; i < cache.Count; i++)
            {
                AnimatedSpriteCache c = cache[i];

                if (c.currentTime + Common.ElapsedTimeDelta > spriteSheet.FrameRate)
                {
                    c.currentTime -= spriteSheet.FrameRate;

                    c.Column++;
                    c.Frame++;

                    if (c.Frame >= spriteSheet.TotalFrames)
                    {
                        c.Row = 0;
                        c.Column = 0;
                        c.Frame = 0;
                    }
                    else if (c.Column >= spriteSheet.Columns)
                    {
                        c.Column = 0;
                        c.Row++;
                    }
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

        public void Render(List<ParticlePropertySet> properties)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                Rectangle sourceRect = spriteSheet.SourceRectangle;
                sourceRect.X = cache[i].Column * spriteSheet.FrameWidth;
                sourceRect.Y = cache[i].Row * spriteSheet.FrameHeight;

                Common.Batch.Draw(spriteSheet.Texture,
                    new Vector2(properties[i].Position.X + spriteSheet.Origin.X,
                        properties[i].Position.Y + spriteSheet.Origin.Y),
                    sourceRect, properties[i].Colour, properties[i].Rotation.Z,
                    spriteSheet.Origin, properties[i].Scale, SpriteEffects.None, 0);
            }
        }

        public void Reset()
        {
            cache.Clear();
        }
    }
}
