/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines an attachment that rotates a Particle.
    /// </summary>
    public class ScaleDecayAttachment : IAttachment
    {
        List<PropertySet> props = new List<PropertySet>();
        List<Vector2> scales = new List<Vector2>();

        string id;

        float minScale;
        float maxScale;

        float minDecay;
        float maxDecay;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The properties of the objects editted by this ColourDecay attachment.
        /// </summary>
        public List<PropertySet> Properties
        {
            get { return props; }
        }

        /// <summary>
        /// The unique identifaction name given to the Attachment.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a new ScaleDecayAttachment.
        /// </summary>
        /// <param name="id">The unique identifaction name given to the Attachment.</param>
        /// <param name="minScale">Minimum scale.</param>
        /// <param name="maxScale">Maximum scale.</param>
        /// <param name="minScaleDecay">Minimum scale decay.</param>
        /// <param name="maxScaleDecay">Maximum scale decay.</param>
        public ScaleDecayAttachment(string id, float minScale, float maxScale,
            float minScaleDecay, float maxScaleDecay)
        {
            this.id = id;

            this.minScale = minScale;
            this.maxScale = maxScale;

            minDecay = minScaleDecay;
            maxDecay = maxScaleDecay;
        }

        public void _Update()
        {
            for (int i = 0; i < scales.Count; i++)
            {
                Vector2 scle = scales[i];
                scle.X -= scle.Y;

                if (scle.X < 0)
                    scle.X = 0;

                scales[i] = scle;

                props[i].Scale = scle.X;
            }
        }

        public void Generate()
        {
            Vector2 scle = GenerateScale();

            props.Add(new PropertySet()
            {
                Scale = scle.X
            });

            scales.Add(scle);
        }

        public void Recalculate(int index)
        {
            Vector2 scle = GenerateScale();

            props[index].Scale = scle.X;
            scales[index] = scle;
        }

        Vector2 GenerateScale()
        {
            return new Vector2(
                RandomHelper.GetRandomFloat(minScale, maxScale),
                RandomHelper.GetRandomFloat(minDecay, maxDecay));
        }

        public void Remove(int index)
        {
            props.Remove(props[index]);
            scales.Remove(scales[index]);
        }

        public void Dispose()
        {
            props.Clear();
            props = null;

            scales.Clear();
            scales = null;
        }

        public void ScreenResolutionChanged()
        {
            minScale = Common.ReCalibrateScale(minScale);
            maxScale = Common.ReCalibrateScale(maxScale);

            minDecay = Common.ReCalibrateScale(minDecay);
            maxDecay = Common.ReCalibrateScale(maxDecay);

            for (int i = 0; i < scales.Count; i++)
            {
                Vector2 scle = scales[i];
                Common.ReCalibrateScreenSpaceVector(scle);

                scales[i] = scle;
            }
        }
    }
}
