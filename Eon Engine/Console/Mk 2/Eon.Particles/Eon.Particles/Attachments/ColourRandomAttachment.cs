/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Particles.Attachments.Base;
using Microsoft.Xna.Framework;

namespace Eon.Particles.Attachments
{
    /// <summary>
    /// Used to define an Attachment that gebnerates random colours. 
    /// </summary>
    public sealed class ColourRandomAttachment : IAttachment
    {
        string id;

        public string ID
        {
            get { return id; }
        }

        public AttachmentTypes AttachmentType
        {
            get { return AttachmentTypes.Colour; }
        }

        public ColourRandomAttachment(string id)
        {
            this.id = id;
        }

        public object Generate()
        {
            return new Color(
                RandomHelper.GetRandom(0, 255),
                RandomHelper.GetRandom(0, 255),
                RandomHelper.GetRandom(0, 255),
                RandomHelper.GetRandom(0, 255));
        }
    }
}
