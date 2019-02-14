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
            : base("SelectiveDesaturatePP", 5, localPostProcessing)
        {
            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/SelectiveDesaturate");

            min = minimumTolerance;
            max = maximumTolerance;
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

        /// <summary>
        /// Creates a new SelectiveDesaturateEffect.
        /// </summary>
        /// <param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="minimumTolerance">The minimum colour tolerance for each colour value.</param>
        /// <param name="maximumTolerance">The maximum colour tolerance for each colour value.</param>
        public SelectiveDesaturateEffect(string renderFrameworkID,
            Vector3 minimumTolerance, Vector3 maximumTolerance)
            : base("SelectiveDesaturatePP", 5, renderFrameworkID)
        {
            effect = Common.ContentManager.Load<Effect>(
                "Eon/Shaders/PostProcessing/SelectiveDesaturate");

            min = minimumTolerance;
            max = maximumTolerance;
        }

        /// <summary>
        /// Creates a new SelectiveDesaturateEffect.
        /// </summary>
        ///<param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="maximumTolerance">The maximum colour tolerance for each colour value.</param>
        public SelectiveDesaturateEffect(string renderFrameworkID, Vector3 maximumTolerance)
            : this(renderFrameworkID, Vector3.Zero, maximumTolerance) { }

        /// <summary>
        /// Creates a new SelectiveDesaturateEffect.
        /// </summary>
        ///<param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        public SelectiveDesaturateEffect(string renderFrameworkID) :
            this(renderFrameworkID, Vector3.Zero, Vector3.Zero) { }

        protected override void Render()
        {
            effect.Parameters["Scene"].SetValue(TextureBuffer.GetTexture("Scene"));
            effect.Parameters["Min"].SetValue(min);
            effect.Parameters["Max"].SetValue(max);

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
