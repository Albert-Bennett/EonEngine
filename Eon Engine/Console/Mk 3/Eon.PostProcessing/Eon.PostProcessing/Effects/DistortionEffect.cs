/* Created: 13/06/2013
 * Last Updated: 13/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// Defines a distortion post process.
    /// </summary>
    public sealed class DistortionEffect : PostProcess
    {
        Effect effect;
        RenderTarget2D target;

        Vector2 halfPixel;

        /// <summary>
        /// Creates a new DistortionEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        public DistortionEffect(string renderFrameworkID)
            : base("ScreenDistortion", 3, renderFrameworkID)
        {
            effect = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/Distortion");
        }

        protected override void Render()
        {
            Texture2D scene = TextureBufferManager.GetTexture(
                TextureBufferID, OutputTextureID);

            if (scene != null)
            {
                effect.Parameters["Scene"].SetValue(scene);
                effect.Parameters["Distortion"].SetValue(TextureBufferManager.GetTexture(
                    TextureBufferID, "DepthMap"));

                effect.Parameters["HalfPixel"].SetValue(halfPixel);

                effect.CurrentTechnique.Passes[0].Apply();
                localPostProcessing.ScreenQuad.Render();

                Common.Device.SetRenderTarget(null);

                final = target;
            }
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device, width, height);

            halfPixel = Vector2.One / Common.TextureQuality;

            base.TextureQualityChanged(width, height);
        }
    }
}
