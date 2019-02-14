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
    public class RotationAttachment : IAttachment
    {
        List<PropertySet> props = new List<PropertySet>();
        List<Vector2> rotations = new List<Vector2>();

        string id;

        float minRot;
        float maxRot;

        float minRotSpeed;
        float maxRotSpeed;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The unique identifaction name given to the Attachment.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// The properties of the objects editted by this ColourDecay attachment.
        /// </summary>
        public List<PropertySet> Properties
        {
            get { return props; }
        }

        /// <summary>
        /// Creates a new rotation attachment.
        /// </summary>
        /// <param name="id">The unique identifaction name to give to the Attachment.</param>
        /// <param name="minRotation">Minimum rotation.</param>
        /// <param name="maxRotation">Maximum rotation.</param>
        /// <param name="minRotationSpeed">Minimum rotation speed.</param>
        /// <param name="maxRotationSpeed">Maximum rotation speed.</param>
        public RotationAttachment(string id, float minRotation, float maxRotation,
            float minRotationSpeed, float maxRotationSpeed)
        {
            this.id = id;

            minRot = minRotation;
            maxRot = maxRotation;

            minRotSpeed = minRotationSpeed;
            maxRotSpeed = maxRotationSpeed;
        }

        public void _Update()
        {
            for (int i = 0; i < rotations.Count; i++)
            {
                Vector2 rot = rotations[i];
                rot.X += rot.Y;

                rotations[i] = rot;

                props[i].Rotation = rot.X;
            }
        }

        public void Generate()
        {
            Vector2 rot = GenerateRotation();

            props.Add(new PropertySet()
            {
                Rotation = rot.X
            });

            rotations.Add(rot);
        }

        public void Recalculate(int index)
        {
            Vector2 rot = GenerateRotation();

            props[index].Rotation = rot.X;
            rotations[index] = rot;
        }

        Vector2 GenerateRotation()
        {
            return new Vector2(
                RandomHelper.GetRandomFloat(minRot, maxRot),
                RandomHelper.GetRandomFloat(minRotSpeed, maxRotSpeed));
        }

        public void Remove(int index)
        {
            props.Remove(props[index]);
            rotations.Remove(rotations[index]);
        }

        public void Dispose()
        {
            props.Clear();
            props = null;

            rotations.Clear();
            rotations = null;
        }

        public void ScreenResolutionChanged() { }
    }
}
