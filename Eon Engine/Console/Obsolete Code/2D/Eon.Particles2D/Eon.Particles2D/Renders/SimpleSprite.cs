/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Drawing;
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
        Texture2D normalMap;
        Texture2D distortionMap;
        Vector2 origin;

        DrawingStage stage;

        /// <summary>
        /// Creates a new SimpleSprite.
        /// </summary>
        /// <param name="textureFilepath">The filepath for the texture of this SimpleSprite.</param>
        /// <param name="normalMapFilepath">The filepath for the normal map of this SimpleSprite.</param>
        /// <param name="distortionMapFilepath">The filepath for the distortion map of this SimpleSprite.</param>
        public SimpleSprite(string textureFilepath,
            string normalMapFilepath, string distortionMapFilepath)
        {
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

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void PreDraw(DrawingStage stage)
        {
            this.stage = stage;
        }

        public void Draw(Vector2 position, float scale, float rotation, Color colour)
        {
            switch (stage)
            {
                case DrawingStage.Colour:
                    Common.Batch.Draw(texture, position, null, colour,
                        rotation, origin * scale, scale, SpriteEffects.None, 0);
                    break;

                case DrawingStage.Normal:
                    Common.Batch.Draw(normalMap, position, null, colour,
                       rotation, origin * scale, scale, SpriteEffects.None, 0);
                    break;

                case DrawingStage.Distortion:
                    Common.Batch.Draw(distortionMap, position, null, colour,
                       rotation, origin * scale, scale, SpriteEffects.None, 0);
                    break;
            }
        }

        public void Dispose(bool finalize)
        {
            if (texture != null)
            {
                if(finalize)
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
        }
    }
}
