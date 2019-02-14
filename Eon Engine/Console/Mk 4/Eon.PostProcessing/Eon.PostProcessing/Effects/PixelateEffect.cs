/* Created: 13/06/2013
 * Last Updated: 07/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// Defines a post process that is used to pixelate scenes.
    /// </summary>
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

        /// <summary>
        /// Creates a new PixelateEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="factor">The amount of pixelation to be applied.</param>
        public PixelateEffect(PostProcessingLocal activeStore, int factor)
            : base("PixelatePP", 6, activeStore)
        {
            this.factor = factor;

            smallTarget = new RenderTarget2D(Common.Device,
                (int)Common.TextureQuality.X / factor, (int)Common.TextureQuality.Y / factor);
        }

        protected override void Render()
        {
            Texture2D scene = ActiveStore.Buffer.Output;

            if (scene != null)
            {
                Common.Device.SetRenderTarget(smallTarget);
                Common.Device.Clear(Color.Black);

                Common.Batch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
                Common.Batch.Draw(scene, new Rectangle(0, 0,
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
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device, width, height, false, SurfaceFormat.Color, DepthFormat.None);

            if (factor > 0)
                smallTarget = new RenderTarget2D(Common.Device, width / factor, height / factor);

            base.TextureQualityChanged(width, height);
        }
    }
}
