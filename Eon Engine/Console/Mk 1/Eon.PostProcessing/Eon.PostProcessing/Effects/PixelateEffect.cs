using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    public sealed class PixelateEffect : PostProcess
    {
        int factor;

        RenderTarget2D target;
        RenderTarget2D smallTarget;

        SamplerState pointSampler;
        SamplerState prevSampler;

        public PixelateEffect(PostProcessingLocal localPostProcessing, int factor)
            : base(0, localPostProcessing)
        {
            this.factor = factor;

            pointSampler = new SamplerState()
            {
                AddressU = TextureAddressMode.Clamp,
                AddressV = TextureAddressMode.Clamp,
                Filter = TextureFilter.Point
            };

            ScreenResolutionChanged();
        }

        protected override void Render()
        {
            Common.Device.SetRenderTarget(smallTarget);
            Common.Device.Clear(Color.Black);

            Common.Batch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            Common.Batch.Draw(localPostProcessing.FinalImage, new Rectangle(0, 0,
                    smallTarget.Width, smallTarget.Height), Color.White);

            Common.Batch.End();

            Common.Device.SetRenderTarget(target);
            Common.Device.Clear(Color.Black);

            Common.Batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            prevSampler = Common.Device.SamplerStates[0];
            Common.Device.SamplerStates[0] = pointSampler;

            Common.Batch.Draw(smallTarget,new Rectangle(0, 0, 
                target.Width, target.Height), Color.White);

            Common.Batch.End();

            Common.Device.SetRenderTarget(null);
            Common.Device.SamplerStates[0] = prevSampler;

            final = target;
        }

        public override void ScreenResolutionChanged()
        {
            target = new RenderTarget2D(Common.Device,
                (int)Common.ScreenResolution.X, 
                (int)Common.ScreenResolution.Y);

            smallTarget = new RenderTarget2D(Common.Device,
                (int)Common.ScreenResolution.X / factor, 
                (int)Common.ScreenResolution.Y / factor);

            base.ScreenResolutionChanged();
        }
    }
}
