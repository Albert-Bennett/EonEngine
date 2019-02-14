/* Created: 01/09/2014
 * Last Updated: 19/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles.Attachments.Base;
using Eon.Particles.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Particles.D2.RenderMethods
{
    /// <summary>
    /// Used to define a method of rendering Particles.
    /// </summary>
    public sealed class SpriteRenderer : IParticleRenderer, I2DParticleRenderer
    {
        Texture2D texture;
        Vector2 origin;

        public SpriteRenderer(string textureFilepath)
        {
            try
            {
                texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                texture = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/DefaultTexture");
            }

            origin = new Vector2(texture.Width, texture.Height) / 2;
        }

        public void Render(List<PropertySet> properties)
        {
            for (int i = 0; i < properties.Count; i++)
                Common.Batch.Draw(texture,
                    new Vector2(properties[i].Position.X, properties[i].Position.Y),
                    null, properties[i].Colour, properties[i].Rotation.Z,
                    origin, properties[i].Scale, SpriteEffects.None, 0);
        }
    }
}
