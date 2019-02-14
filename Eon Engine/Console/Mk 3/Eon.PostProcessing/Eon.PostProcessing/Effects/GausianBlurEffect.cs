/* Created: 19/10/2013
 * Last Updated: 31/12/2014
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
    /// Defines a post process that blurs the screen.
    /// </summary>
    public sealed class GausianBlurEffect : PostProcess
    {
        float blurAmount;

        Effect blur;
        RenderTarget2D blurTarget;

        /// <summary>
        /// The amount of blur to be applied.
        /// </summary>
        public float BlurAmount
        {
            get { return blurAmount; }
            set { blurAmount = value; }
        }

        /// <summary>
        /// Creates a new GausianBlurEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="blurAmount">The amount of bluring to be applied.</param>
        public GausianBlurEffect(string renderFrameworkID, float blurAmount) :
            base("GausianBlurPP", 7, renderFrameworkID)
        {
            this.blurAmount = blurAmount;

            blur = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/GausianBlur");
        }

        protected override void Render()
        {
            Texture2D scene = TextureBufferManager.GetTexture(
                TextureBufferID, OutputTextureID);

            if (scene != null)
            {
                blur.Parameters["Scene"].SetValue(scene);

                SetBlur(1 / (float)blurTarget.Width, 0);
                DrawWithEffect(scene, blurTarget, blur);

                SetBlur(0, 1 / (float)blurTarget.Height);
                DrawWithEffect(scene, blurTarget, blur);

                Common.Device.SetRenderTarget(null);

                final = blurTarget;

                base.Render();
            }
        }

        void SetBlur(float dx, float dy)
        {
            Vector2[] offsets = PostProcessingMathHelper.FindBlurOffsets(15, dx, dy);
            float[] weights = PostProcessingMathHelper.FindBlurWeights(15, blurAmount);

            blur.Parameters["Weights"].SetValue(weights);
            blur.Parameters["Offsets"].SetValue(offsets);
        }

        public override void TextureQualityChanged(int width, int height)
        {
            blurTarget = new RenderTarget2D(Common.Device, width, height);

            base.TextureQualityChanged(width, height);
        }
    }
}
