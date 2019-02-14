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
    /// Defines an Attachment that is used to rotate Particles.
    /// </summary>
    public sealed class RotationalAttachment : IUpdateAttachment
    {
        string id;

        Vector3 minRotation;
        Vector3 maxRotation;

        Vector3 minRotationSpeed;
        Vector3 maxRotationSpeed;

        public string ID
        {
            get { return id; }
        }

        public AttachmentTypes AttachmentType
        {
            get { return AttachmentTypes.Rotation; }
        }

        public RotationalAttachment(string id,
            float minRotX, float minRotY, float minRotZ,
            float maxRotX, float maxRotY, float maxRotZ,
            float minSpeedX, float minSpeedY, float minSpeedZ,
            float maxSpeedX, float maxSpeedY, float maxSpeedZ)
        {
            this.id = id;

            minRotation = new Vector3(minRotX, minRotY, minRotZ);
            maxRotation = new Vector3(maxRotX, maxRotY, maxRotZ);

            minRotationSpeed = new Vector3(minSpeedX, minSpeedY, minSpeedZ);
            maxRotationSpeed = new Vector3(maxSpeedX, maxSpeedY, maxSpeedZ);
        }

        public object Generate()
        {
            return RandomHelper.GetRandom(minRotation, maxRotation) +
                RandomHelper.GetRandom(minRotationSpeed, maxRotationSpeed);
        }

        public object Generate(object property)
        {
            return (Vector3)property + RandomHelper.GetRandom(minRotationSpeed, maxRotationSpeed);
        }
    }
}
