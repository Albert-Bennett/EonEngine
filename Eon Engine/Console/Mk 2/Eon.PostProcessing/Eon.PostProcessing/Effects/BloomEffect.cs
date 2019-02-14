/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    public sealed class BloomEffect : PostProcess
    {
        float threshold;
        float blurAmount;

        float sceneSaturation;
        float bloomSaturation;

        float sceneIntensity;
        float bloomIntensity;

        Effect bloom;
        Effect blur;
        Effect combined;

        RenderTarget2D bloomTarget;
        RenderTarget2D blurTarget;

        public BloomEffect(PostProcessingLocal localPostProcessing,
            float threshold, float blurAmount, float sceneSaturation,
            float bloomSaturation, float sceneIntensity, float bloomIntensity)
            : base("BloomPP", 4, localPostProcessing)
        {
            this.threshold = threshold;
            this.blurAmount = blurAmount;

            this.sceneSaturation = sceneSaturation;
            this.bloomSaturation = bloomSaturation;

            this.sceneIntensity = sceneIntensity;
            this.bloomIntensity = bloomIntensity;

            bloom = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/ExtractSaturation");

            blur = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/GausianBlur");

            combined = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/ComposeBloom");
        }

        public BloomEffect(string renderFrameworkID,
            float threshold, float blurAmount, float sceneSaturation,
            float bloomSaturation, float sceneIntensity, float bloomIntensity)
            : base("BloomPP", 0, renderFrameworkID)
        {
            this.threshold = threshold;
            this.blurAmount = blurAmount;

            this.sceneSaturation = sceneSaturation;
            this.bloomSaturation = bloomSaturation;

            this.sceneIntensity = sceneIntensity;
            this.bloomIntensity = bloomIntensity;

            bloom = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/ExtractSaturation");

            blur = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/GausianBlur");

            combined = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/ComposeBloom");
        }

        protected override void Render()
        {
            RenderExtract();
            RenderBlur();
            RenderComposed();
        }

        void RenderExtract()
        {
            bloom.Parameters["Scene"].SetValue(TextureBuffer.GetTexture("Scene"));
            bloom.Parameters["Threshold"].SetValue(threshold);

            DrawWithEffect(localPostProcessing.FinalImage, bloomTarget, bloom);
        }

        void RenderBlur()
        {
            blur.Parameters["Scene"].SetValue(TextureBuffer.GetTexture("Scene"));

            SetBlur(1 / (float)bloomTarget.Width, 0);
            DrawWithEffect(bloomTarget, blurTarget, blur);

            SetBlur(0, 1 / (float)bloomTarget.Height);
            DrawWithEffect(blurTarget, bloomTarget, blur);

            Common.Device.SetRenderTarget(null);
        }

        void RenderComposed()
        {
            combined.Parameters["SceneIntensity"].SetValue(sceneIntensity);
            combined.Parameters["BloomIntensity"].SetValue(bloomIntensity);

            combined.Parameters["SceneSaturation"].SetValue(sceneSaturation);
            combined.Parameters["BloomSaturation"].SetValue(bloomSaturation);

            combined.Parameters["Scene"].SetValue(localPostProcessing.FinalImage);
            combined.Parameters["PreBloom"].SetValue(bloomTarget);

            DrawWithEffect(localPostProcessing.FinalImage, bloomTarget, combined);

            final = bloomTarget;
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
            bloomTarget = new RenderTarget2D(Common.Device, width, height);
            blurTarget = new RenderTarget2D(Common.Device, width, height);

            base.TextureQualityChanged(width, height);
        }
    }
}
