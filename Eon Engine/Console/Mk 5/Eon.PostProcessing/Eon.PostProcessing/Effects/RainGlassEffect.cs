/* Created: 03/09/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// Defines a post process that blurs the scene as if it was raining.
    /// </summary>
    public sealed class RainGlassEffect : PostProcess
    {
        float blurAmount;
        string distortionID = "Distortion";

        Effect blur;
        Effect rainGlass;

        RenderTarget2D blurTarget;
        RenderTarget2D glassTarget;

        /// <summary>
        /// The amount of blur to be applied.
        /// </summary>
        public float BlurAmount
        {
            get { return blurAmount; }
            set { blurAmount = value; }
        }

        /// <summary>
        /// The ID of the distortion target in the TextureBuffer.
        /// </summary>
        public string DistortionID
        {
            get { return distortionID; }
            set { distortionID = value; }
        }

        /// <summary>
        /// Creates a new GausianBlurEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="blurAmount">The amount of bluring to be applied.</param>
        public RainGlassEffect(PostProcessingLocal activeStore, float blurAmount) :
            base("GausianBlurPP", 7, activeStore)
        {
            this.blurAmount = blurAmount;

            blur = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/GausianBlur");

            rainGlass = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/RainGlass");
        }

        protected override void Render()
        {
            Texture2D scene = ActiveStore.Buffer.Output;
            Texture2D distort = ActiveStore.Buffer.GetTexture(distortionID);

            if (scene != null)
            {
                Blur(scene, distort);
                RainGlass(scene, distort);

                final = glassTarget;

                base.Render();
            }
        }

        void Blur(Texture2D scene, Texture2D distortion)
        {
            blur.Parameters["Scene"].SetValue(scene);
            blur.Parameters["Mask"].SetValue(distortion);

            blur.CurrentTechnique = blur.Techniques["BlurMask"];

            SetBlur(1 / (float)blurTarget.Width, 0);
            DrawWithEffect(scene, blurTarget, blur);

            SetBlur(0, 1 / (float)blurTarget.Height);
            DrawWithEffect(scene, blurTarget, blur);

            Common.Device.SetRenderTarget(null);
        }

        void SetBlur(float dx, float dy)
        {
            Vector2[] offsets = PostProcessingMathHelper.FindBlurOffsets(15, dx, dy);
            float[] weights = PostProcessingMathHelper.FindBlurWeights(15, blurAmount);

            blur.Parameters["Weights"].SetValue(weights);
            blur.Parameters["Offsets"].SetValue(offsets);
        }

        void RainGlass(Texture2D scene, Texture2D distortion)
        {
            rainGlass.Parameters["Distortion"].SetValue(distortion);
            rainGlass.Parameters["Blur"].SetValue(blurTarget);
            rainGlass.Parameters["Scene"].SetValue(scene);

            DrawWithEffect(scene, glassTarget, rainGlass);

            Common.Device.SetRenderTarget(null);
        }

        public override void TextureQualityChanged(int width, int height)
        {
            blurTarget = new RenderTarget2D(Common.Device, width, height, false, SurfaceFormat.Color, DepthFormat.None);
            glassTarget = new RenderTarget2D(Common.Device, width, height, false, SurfaceFormat.Color, DepthFormat.None);

            base.TextureQualityChanged(width, height);
        }
    }
}
