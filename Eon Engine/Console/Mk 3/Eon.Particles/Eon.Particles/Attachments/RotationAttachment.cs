/* Created: 01/09/2014
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
    /// Used to define the rotation of a Particle.
    /// </summary>
    public sealed class RotationAttachment : IAttachment
    {
        string id;

        Vector3 minRotation;
        Vector3 maxRotation;

        public string ID
        {
            get { return id; }
        }

        public AttachmentTypes AttachmentType
        {
            get { return AttachmentTypes.Rotation; }
        }

        public RotationAttachment(string id, float minRotX, float minRotY,
            float minRotZ, float maxRotX, float maxRotY, float maxRotZ)
        {
            this.id = id;
            this.minRotation = new Vector3(minRotX, minRotY, minRotZ);
            this.maxRotation = new Vector3(maxRotX, maxRotY, maxRotZ);
        }

        public object Generate()
        {
            return RandomHelper.GetRandom(minRotation, maxRotation);
        }
    }
}
