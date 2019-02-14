/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles.Attachments.Base;
using Microsoft.Xna.Framework;

namespace Eon.Particles.Attachments
{
    /// <summary>
    /// Used to define a ColourAttachment.
    /// </summary>
    public sealed class ColourAttachment : IAttachment
    {
        Color colour;

        string id;

        public string ID
        {
            get { return id; }
        }

        public AttachmentTypes AttachmentType
        {
            get { return AttachmentTypes.Colour; }
        }

        public ColourAttachment(string id, int r, int g, int b, int a)
        {
            this.id = id;

            colour = new Color(r, g, b, a);
        }

        public object Generate()
        {
            return colour;
        }
    }
}
