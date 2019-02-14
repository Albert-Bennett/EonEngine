/* Created 13/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// Defines a post process effect that desalurates 
    /// the scene except for colour contained within a 
    /// given range of colours.
    /// </summary>
    public sealed class SelectiveDesaturateEffect : PostProcess
    {
        Effect effect;
        RenderTarget2D target;

        Vector3 min;
        Vector3 max;

        /// <summary>
        /// Creates a new SelectiveDesaturateEffect.
        /// </summary>
        /// <param name="localPostProcessing">The PostProcessingLocal used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="minimumTolerance">The minimum colour tolerance for each colour value.</param>
        /// <param name="maximumTolerance">The maximum colour tolerance for each colour value.</param>
        public SelectiveDesaturateEffect(PostProcessingLocal localPostProcessing,
            Vector3 minimumTolerance, Vector3 maximumTolerance)
            : base(3, localPostProcessing)
        {
            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/SelectiveDesaturate");

            min = minimumTolerance;
            max = maximumTolerance;

            ScreenResolutionChanged();
        }

        /// <summary>
        /// Creates a new SelectiveDesaturateEffect.
        /// </summary>
        /// <param name="localPostProcessing">The PostProcessingLocal used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="maximumTolerance">The maximum colour tolerance for each colour value.</param>
        public SelectiveDesaturateEffect(PostProcessingLocal localPostProcessing, Vector3 maximumTolerance)
            : this(localPostProcessing, Vector3.Zero, maximumTolerance) { }

        /// <summary>
        /// Creates a new SelectiveDesaturateEffect.
        /// </summary>
        /// <param name="localPostProcessing">The PostProcessingLocal used for
        /// the current render medium(2D or 3D).</param>
        public SelectiveDesaturateEffect(PostProcessingLocal localPostProcessing) :
            this(localPostProcessing, Vector3.Zero, Vector3.Zero) { }

        protected override void Render()
        {
            effect.Parameters["Scene"].SetValue(localPostProcessing.FinalImage);
            effect.Parameters["Min"].SetValue(min);
            effect.Parameters["Max"].SetValue(max);

            DrawWithEffect(localPostProcessing.FinalImage, target, effect);

            Common.Device.SetRenderTarget(null);

            final = target;
        }

        /// <summary>
        /// Used to cahnge the size of various render 
        /// targets relative to the size of the screen.
        /// </summary>
        public override void ScreenResolutionChanged()
        {
            target = new RenderTarget2D(Common.Device,
                (int)Common.ScreenResolution.X, (int)Common.ScreenResolution.Y);

            base.ScreenResolutionChanged();
        }
    }
}
