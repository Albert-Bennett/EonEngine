using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    public sealed class NegitiveEffect : PostProcess
    {
        Effect effect;
        RenderTarget2D target;

        public NegitiveEffect(PostProcessingLocal localPostProcessing)
            : base(4, localPostProcessing)
        {
            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/Negitive");

            ScreenResolutionChanged();
        }

        protected override void Render()
        {
            effect.Parameters["Scene"].SetValue(localPostProcessing.FinalImage);

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
