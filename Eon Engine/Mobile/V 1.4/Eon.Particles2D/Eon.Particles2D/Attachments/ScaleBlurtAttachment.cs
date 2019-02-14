/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using System.Collections.Generic;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines a Particle effect that scales 
    /// particles between minimum and maximum scales. 
    /// </summary>
    public class ScaleBlurtAttachment : IAttachment
    {
        List<PropertySet> props = new List<PropertySet>();
        List<BlurtState> blurts = new List<BlurtState>();

        string id;

        float minMinScale;
        float maxMinScale;

        float minMaxScale;
        float maxMaxScale;

        float minDecay;
        float maxDecay;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The properties of the objects editted by this Blurt attachment.
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
        /// Creates a new Blurt attachment.
        /// </summary>
        /// <param name="id">The unique identifaction name to give to the Attachment.</param>
        /// <param name="minMinScale">Minimum scale.</param>
        /// <param name="maxMinScale">Maximum minimum scale.</param>
        /// <param name="minMaxScale">Minimum maximum scale.</param>
        /// <param name="maxMaxScale">Maximum scale.</param>
        /// <param name="minDecay">Minmum scale decay.</param>
        /// <param name="maxDecay">Maximum scale decay.</param>
        public ScaleBlurtAttachment(string id, float minMinScale, float maxMinScale,
            float minMaxScale, float maxMaxScale, float minDecay, float maxDecay)
        {
            this.id = id;

            this.minMinScale = minMinScale;
            this.maxMinScale = maxMinScale;

            this.minMaxScale = minMaxScale;
            this.maxMaxScale = maxMaxScale;

            this.minDecay = minDecay;
            this.maxDecay = maxDecay;
        }

        public void _Update()
        {
            for (int i = 0; i < blurts.Count; i++)
            {
                BlurtState blurt = blurts[i];
                blurt.Update();

                PropertySet prop = props[i];
                props[i].Scale = blurts[i].Value;

                blurts[i] = blurt;
            }
        }

        public void Generate()
        {
            BlurtState blurt = GenerateBlurt();

            props.Add(new PropertySet()
            {
                Scale = blurt.Value
            });

            blurts.Add(blurt);
        }

        public void Recalculate(int index)
        {
            BlurtState blurt = GenerateBlurt();

            props[index].Scale = blurt.Value;
            blurts[index] = blurt;
        }

        BlurtState GenerateBlurt()
        {
            return new BlurtState()
            {
                DeValue = false,
                MaxValue = RandomHelper.GetRandomFloat(minMaxScale, maxMaxScale),
                MinValue = RandomHelper.GetRandomFloat(minMinScale, maxMinScale),
                Decay = RandomHelper.GetRandomFloat(minDecay, maxDecay),
                Value = RandomHelper.GetRandomFloat(minMinScale, maxMaxScale)
            };
        }

        public void Remove(int index)
        {
            blurts.Remove(blurts[index]);
            props.Remove(props[index]);
        }

        public void Dispose()
        {
            blurts.Clear();
            blurts = null;

            props.Clear();
            props = null;
        }


        public void ScreenResolutionChanged()
        {
            for (int i = 0; i < blurts.Count; i++)
                blurts[i].ScreeResChanged();
        }
    }
}
