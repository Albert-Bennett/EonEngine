/* Created: 13/06/2013
 * Last Updated: 05/02/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
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
        Texture2D mask = null;

        Vector3 min;
        Vector3 max;

        EffectTechnique masked;
        EffectTechnique normal;

        /// <summary>
        /// The minimum threshold for colours.
        /// </summary>
        public Vector3 MinimumColourThreshold
        {
            get { return min; }
            set { min = value; }
        }

        /// <summary>
        /// The maximum threshold for colours.
        /// </summary>
        public Vector3 MaximumColourThreshold
        {
            get { return max; }
            set { max = value; }
        }

        /// <summary>
        /// Used to define what is affected by the 
        /// SelectiveDesaturateEffect.
        /// </summary>
        public Texture2D Mask
        {
            get { return mask; }
            set { mask = value; }
        }

        /// <summary>
        /// Creates a new SelectiveDesaturateEffect.
        /// </summary>
        /// <param name="minimumThreshold">The minimum colour threshold for each colour value.</param>
        /// <param name="maximumThreshold">The maximum colour threshold for each colour value.</param>
        public SelectiveDesaturateEffect(PostProcessingLocal activeStore,
            Vector3 minimumThreshold, Vector3 maximumThreshold)
            : base("SelectiveDesaturatePP", 5, activeStore)
        {
            effect = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/SelectiveDesaturate");

            min = minimumThreshold;
            max = maximumThreshold;

            for (int i = 0; i < effect.Techniques.Count; i++)
                if (effect.Techniques[i].Name.Contains("Masked"))
                    masked = effect.Techniques[i];
                else if (effect.Techniques[i].Name.Contains("Render"))
                    normal = effect.Techniques[i];
        }

        /// <summary>
        /// Creates a new SelectiveDesaturateEffect.
        /// </summary>
        ///<param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        /// <param name="maximumThreshold">The maximum colour threshold for each colour value.</param>
        public SelectiveDesaturateEffect(PostProcessingLocal activeStore, Vector3 maximumThreshold)
            : this(activeStore, Vector3.Zero, maximumThreshold) { }

        /// <summary>
        /// Creates a new SelectiveDesaturateEffect.
        /// </summary>
        ///<param name="renderFrameworkID">The ID of the render framework used for
        /// the current render medium(2D or 3D).</param>
        public SelectiveDesaturateEffect(PostProcessingLocal activeStore) :
            this(activeStore, Vector3.Zero, Vector3.Zero) { }

        protected override void Render()
        {
            Texture2D scene = ActiveStore.Buffer.Output;

            effect.Parameters["Scene"].SetValue(scene);
            effect.Parameters["Min"].SetValue(min);
            effect.Parameters["Max"].SetValue(max);

            if (mask != null)
            {
                effect.Parameters["Mask"].SetValue(mask);
                effect.CurrentTechnique = masked;

                DrawWithEffect(scene, target, effect);
            }
            else
            {
                effect.CurrentTechnique = normal;

                DrawWithEffect(scene, target, effect);
            }

            Common.Device.SetRenderTarget(null);

            final = target;
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None);

            base.TextureQualityChanged(width, height);
        }
    }
}
