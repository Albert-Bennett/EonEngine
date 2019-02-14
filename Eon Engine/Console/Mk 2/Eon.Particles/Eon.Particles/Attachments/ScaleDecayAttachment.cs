/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Particles.Attachments.Base;

namespace Eon.Particles.Attachments
{
    /// <summary>
    /// Used to define an Attachment that decays scale over time.
    /// </summary>
    public sealed class ScaleDecayAttachment : IUpdateAttachment
    {
        string id;

        float minDecay;
        float maxDecay;

        float minScale;
        float maxScale;

        public string ID
        {
            get { return id; }
        }

        public AttachmentTypes AttachmentType
        {
            get { return AttachmentTypes.Scale; }
        }

        public ScaleDecayAttachment(string id, float minScale,
            float maxScale, float minDecay, float maxDecay)
        {
            this.id = id;

            this.minScale = minScale;
            this.maxScale = maxScale;

            this.minDecay = minDecay;
            this.maxDecay = maxDecay;
        }

        public object Generate()
        {
            return RandomHelper.GetRandom(minScale, maxScale);
        }

        public object Generate(object property)
        {
            return (float)property - RandomHelper.GetRandom(minDecay, maxDecay);
        }
    }
}
