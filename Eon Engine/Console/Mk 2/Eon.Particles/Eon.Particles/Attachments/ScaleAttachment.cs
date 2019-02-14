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
    /// Used to define an Attachment that signifies scale.
    /// </summary>
    public sealed class ScaleAttachment : IAttachment
    {
        string id;

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

        public ScaleAttachment(string id, float minScale, float maxScale)
        {
            this.id = id;

            this.minScale = minScale;
            this.maxScale = maxScale;
        }

        public object Generate()
        {
            return RandomHelper.GetRandom(minScale, maxScale);
        }
    }
}
