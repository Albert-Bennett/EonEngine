/* Created: 13/06/2013
 * Last Updated: 31/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Tools;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// Defines a post process that creates a negitive of the current scene. 
    /// </summary>
    public sealed class NegitiveEffect : PostProcess
    {
        Effect effect;
        RenderTarget2D target;

        /// <summary>
        /// Creates a new NegitiveEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        public NegitiveEffect(PostProcessingLocal activeStore)
            : base("NegitivePP", 5, activeStore)
        {
            effect = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/Negitive");
        }

        protected override void Render()
        {
            Texture2D scene = ActiveStore.Buffer.Output;

            if (scene != null)
            {
                effect.Parameters["Scene"].SetValue(scene);

                DrawWithEffect(scene, target, effect);

                Common.Device.SetRenderTarget(null);

                final = target;
            }
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device, width, height, false, SurfaceFormat.Color, DepthFormat.None);

            base.TextureQualityChanged(width, height);
        }
    }
}
