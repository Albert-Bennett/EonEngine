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
    public sealed class PixelateEffect : PostProcess
    {
        int factor;

        RenderTarget2D target;
        RenderTarget2D smallTarget;

        SamplerState pointSampler = new SamplerState()
        {
            AddressU = TextureAddressMode.Clamp,
            AddressV = TextureAddressMode.Clamp,
            Filter = TextureFilter.Point
        };

        SamplerState prevSampler;

        public PixelateEffect(PostProcessingLocal localPostProcessing, int factor)
            : base("PixelatePP", 6, localPostProcessing)
        {
            this.factor = factor;

            smallTarget = new RenderTarget2D(Common.Device,
                (int)Common.TextureQuality.X / factor, (int)Common.TextureQuality.Y / factor);
        }

        public PixelateEffect(string renderFrameworkID, int factor)
            : base("PixelatePP", 0, renderFrameworkID)
        {
            this.factor = factor;

            smallTarget = new RenderTarget2D(Common.Device,
                (int)Common.TextureQuality.X / factor, (int)Common.TextureQuality.Y / factor);
        }

        protected override void Render()
        {
            Common.Device.SetRenderTarget(smallTarget);
            Common.Device.Clear(Color.Black);

            Common.Batch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            Common.Batch.Draw(TextureBuffer.GetTexture("Scene"), new Rectangle(0, 0,
                    smallTarget.Width, smallTarget.Height), Color.White);

            Common.Batch.End();

            Common.Device.SetRenderTarget(target);
            Common.Device.Clear(Color.Black);

            Common.Batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            prevSampler = Common.Device.SamplerStates[0];
            Common.Device.SamplerStates[0] = pointSampler;

            Common.Batch.Draw(smallTarget, new Rectangle(0, 0,
                target.Width, target.Height), Color.White);

            Common.Batch.End();

            Common.Device.SetRenderTarget(null);
            Common.Device.SamplerStates[0] = prevSampler;

            final = target;
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device, width, height);

            if (factor > 0)
                smallTarget = new RenderTarget2D(Common.Device, width / factor, height / factor);

            base.TextureQualityChanged(width, height);
        }
    }
}
