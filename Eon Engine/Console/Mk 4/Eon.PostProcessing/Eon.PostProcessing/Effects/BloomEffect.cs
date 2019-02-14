/* Created: 13/06/2013
 * Last Updated: 10/07/2015
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

        /// <summary>
        /// The amount of bluring to applied to the scene.
        /// </summary>
        public float BlurAmount
        {
            get { return blurAmount; }
            set { blurAmount = value; }
        }

        /// <summary>
        /// The amount of colour from the scene.
        /// </summary>
        public float SceneSaturation
        {
            get { return sceneSaturation; }
            set { sceneSaturation = value; }
        }

        /// <summary>
        /// The amount of colour from the bloom.
        /// </summary>
        public float BloomSaturation
        {
            get { return bloomSaturation; }
            set { bloomSaturation = value; }
        }

        /// <summary>
        /// The intensity of the scene.
        /// </summary>
        public float SceneIntensity
        {
            get { return sceneIntensity; }
            set { sceneIntensity = value; }
        }

        /// <summary>
        /// The intensity of the bloom.
        /// </summary>
        public float BloomIntensity
        {
            get { return bloomIntensity; }
            set { bloomIntensity = value; }
        }

        /// <summary>
        /// Creates a new BloomEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework which 
        /// the BloomEffect will be rendering with.</param>
        /// <param name="threshold">Value to clam blur to.</param>
        /// <param name="blurAmount">The amount of bluring to applied to the scene.</param>
        /// <param name="sceneSaturation">The amount of colour from the scene.</param>
        /// <param name="bloomSaturation">The amount of colour from the bloom.</param>
        /// <param name="sceneIntensity">The intensity of the scene.</param>
        /// <param name="bloomIntensity">The intensity of the bloom.</param>
        public BloomEffect(PostProcessingLocal activeStore,
            float threshold, float blurAmount, float sceneSaturation,
            float bloomSaturation, float sceneIntensity, float bloomIntensity)
            : base("BloomPP", 0, activeStore)
        {
            this.threshold = threshold;
            this.blurAmount = blurAmount;

            this.sceneSaturation = sceneSaturation;
            this.bloomSaturation = bloomSaturation;

            this.sceneIntensity = sceneIntensity;
            this.bloomIntensity = bloomIntensity;

            bloom = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/ExtractSaturation");

            blur = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/GausianBlur");

            combined = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/ComposeBloom");
        }

        protected override void Render()
        {
            Texture2D scene = ActiveStore.Buffer.Output;

            if (scene != null)
            {
                RenderExtract(scene);
                RenderBlur(scene);
                RenderComposed(scene);
            }
        }

        void RenderExtract(Texture2D scene)
        {
            bloom.Parameters["Scene"].SetValue(scene);
            bloom.Parameters["Threshold"].SetValue(threshold);

            DrawWithEffect(scene, bloomTarget, bloom);
        }

        void RenderBlur(Texture2D scene)
        {
            blur.Parameters["Scene"].SetValue(scene);

            SetBlur(1 / (float)bloomTarget.Width, 0);
            DrawWithEffect(bloomTarget, blurTarget, blur);

            SetBlur(0, 1 / (float)bloomTarget.Height);
            DrawWithEffect(blurTarget, bloomTarget, blur);

            Common.Device.SetRenderTarget(null);
        }

        void RenderComposed(Texture2D scene)
        {
            combined.Parameters["SceneIntensity"].SetValue(sceneIntensity);
            combined.Parameters["BloomIntensity"].SetValue(bloomIntensity);

            combined.Parameters["SceneSaturation"].SetValue(sceneSaturation);
            combined.Parameters["BloomSaturation"].SetValue(bloomSaturation);

            combined.Parameters["Scene"].SetValue(scene);
            combined.Parameters["PreBloom"].SetValue(bloomTarget);

            DrawWithEffect(scene, bloomTarget, combined);

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
            bloomTarget = new RenderTarget2D(Common.Device, width, height, false, SurfaceFormat.Color, DepthFormat.None);
            blurTarget = new RenderTarget2D(Common.Device, width, height, false, SurfaceFormat.Color, DepthFormat.None);

            base.TextureQualityChanged(width, height);
        }
    }
}
