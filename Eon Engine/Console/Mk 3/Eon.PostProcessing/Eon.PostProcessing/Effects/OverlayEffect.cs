/* Created: 20/10/2013
 * Last Updated: 31/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Tools;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// Defines a post process effect that overlays an image.
    /// </summary>
    public sealed class OverlayEffect : PostProcess
    {
        Texture2D texture;
        RenderTarget2D target;

        /// <summary>
        /// The texture to be drawn.
        /// </summary>
        public Texture2D Overlay
        {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// Creates a new OverlayEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="colour">The colour to be overlayed.</param>
        public OverlayEffect(string renderFrameworkID, Color colour) :
            base("OverlayPP", 8, renderFrameworkID)
        {
            texture = new Texture2D(Common.Device, 1, 1);

            texture.SetData<Color>(new Color[] { colour });
        }

        /// <summary>
        /// Creates a new OverlayEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="texture">The texture to be overlayed.</param>
        public OverlayEffect(string renderFrameworkID, Texture2D texture) :
            base("OverlayPP", 8, renderFrameworkID)
        {
            this.texture = texture;
        }

        /// <summary>
        /// Creates a new OverlayEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="textureFilepath">The filepath of the texture to be overlayed.</param>
        public OverlayEffect(string renderFrameworkID, string textureFilepath) :
            base("OverlayPP", 8, renderFrameworkID)
        {
            try
            {
                texture = Common.ContentBuilder.Load<Texture2D>(textureFilepath);
            }
            catch
            {
                new Error("No texture exists at: " + textureFilepath, Seriousness.Error);

                Destroy();
            }
        }

        protected override void Render()
        {
            Texture2D scene = TextureBufferManager.GetTexture(
                TextureBufferID, OutputTextureID);

            if (scene != null)
            {
                Common.Device.SetRenderTarget(target);

                Common.Batch.Begin();

                Common.Batch.Draw(scene, target.Bounds, Color.White);
                Common.Batch.Draw(texture, target.Bounds, Color.White);

                Common.Batch.End();

                Common.Device.SetRenderTarget(null);

                final = target;

                base.Render();
            }
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device, width, height);

            base.TextureQualityChanged(width, height);
        }
    }
}
