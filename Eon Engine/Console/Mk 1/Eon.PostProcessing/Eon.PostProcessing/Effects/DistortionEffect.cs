using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    public sealed class DistortionEffect : PostProcess
    {
        Effect effect;
        RenderTarget2D target;

        public DistortionEffect(PostProcessingLocal localPostProcessing)
            : base(0, localPostProcessing)
        {
            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/Distortion");

            ScreenResolutionChanged();
        }

        protected override void Render()
        {
            effect.Parameters["Scene"].SetValue(localPostProcessing.FinalImage);
            effect.Parameters["Distortion"].SetValue(localPostProcessing.Distortion);

            DrawWithEffect(localPostProcessing.FinalImage, target, effect);

            Common.Device.SetRenderTarget(null);

            final = target;
        }

        public override void ScreenResolutionChanged()
        {
            target = new RenderTarget2D(Common.Device,
                (int)Common.ScreenResolution.X, (int)Common.ScreenResolution.Y);

            base.ScreenResolutionChanged();
        }
    }
}
