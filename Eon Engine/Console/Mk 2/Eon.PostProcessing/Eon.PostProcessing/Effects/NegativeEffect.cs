/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Tools;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    public sealed class NegitiveEffect : PostProcess
    {
        Effect effect;
        RenderTarget2D target;

        public NegitiveEffect(PostProcessingLocal localPostProcessing)
            : base("NegitivePP", 5, localPostProcessing)
        {
            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/Negitive");
        }

        public NegitiveEffect(string renderFrameworkID)
            : base("NegitivePP", 4, renderFrameworkID)
        {
            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/Negitive");
        }

        protected override void Render()
        {
            effect.Parameters["Scene"].SetValue(TextureBuffer.GetTexture("Scene"));

            DrawWithEffect(TextureBuffer.GetTexture("Scene"), target, effect);

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
