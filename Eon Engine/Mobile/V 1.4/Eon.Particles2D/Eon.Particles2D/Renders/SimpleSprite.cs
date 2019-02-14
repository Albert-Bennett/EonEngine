/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Particles2D.Renders
{
    /// <summary>
    /// Defines a class that is used to 
    /// define how a particle will be rendered.
    /// </summary>
    public sealed class SimpleSprite : IParticleType
    {
        Texture2D texture;
        Vector2 origin;

        /// <summary>
        /// Creates a new SimpleSprite.
        /// </summary>
        /// <param name="textureFilepath">The filepath for the texture of this SimpleSprite.</param>
        public SimpleSprite(string textureFilepath)
        {
            texture = Common.ContentManager.Load<Texture2D>(textureFilepath);

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void PreDraw() { }

        public void Draw(Vector2 position, float scale, float rotation, Color colour)
        {
            Common.Batch.Draw(texture, position, null, colour,
                rotation, origin, scale, SpriteEffects.None, 0);
        }

        public void Dispose(bool finalize)
        {
            if (texture != null)
            {
                if (finalize)
                    texture.Dispose();

                texture = null;
            }
        }
    }
}
