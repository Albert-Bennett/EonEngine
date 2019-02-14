/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Tools;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    public sealed class DistortionEffect : PostProcess
    {
        Effect effect;
        RenderTarget2D target;

        public DistortionEffect(PostProcessingLocal localPostProcessing)
            : base("ScreenDistortion",3, localPostProcessing)
        {
            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/Distortion");
        }

        public DistortionEffect(string renderFrameworkID)
            : base("ScreenDistortion", 0, renderFrameworkID)
        {
            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/Distortion");
        }

        protected override void Render()
        {
            effect.Parameters["Scene"].SetValue(TextureBuffer.GetTexture("Scene"));
            effect.Parameters["Distortion"].SetValue(TextureBuffer.GetTexture("DistortionMap"));

            DrawWithEffect(localPostProcessing.FinalImage, target, effect);

            Common.Device.SetRenderTarget(null);

            final = target;
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device, width, height);

            base.TextureQualityChanged(width, height);
        }
    }
}
