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
    /// Used to define amn Attachment that decays a colour over time.
    /// </summary>
    public sealed class ColourDecayAttachment : IUpdateAttachment
    {
        string id;

        Color colour;

        int minDecay;
        int maxDecay;

        public string ID
        {
            get { return id; }
        }

        public AttachmentTypes AttachmentType
        {
            get { return AttachmentTypes.Colour; }
        }

        public ColourDecayAttachment(string id, int r, int g,
            int b, int a, int minDecay, int maxDecay)
        {
            this.id = id;

            this.minDecay = minDecay;
            this.maxDecay = maxDecay;

            colour = new Color(r, g, b, a);
        }

        public object Generate()
        {
            return colour;
        }

        public object Generate(object property)
        {
            Color c = (Color)property;

            if (c != Color.Transparent)
            {
                byte decay = (byte)RandomHelper.GetRandom(minDecay, maxDecay);

                if (c.R - decay >= 255)
                    c.R = 0;
                else
                    c.R -= decay;

                if (c.G - decay >= 255)
                    c.G = 0;
                else
                    c.G -= decay;

                if (c.B - decay >= 255)
                    c.B = 0;
                else
                    c.B -= decay;

                if (c.A - decay >= 255)
                    c.A = 0;
                else
                    c.A -= decay;
            }
            else
                c = Color.Transparent;

            return c;
        }
    }
}
