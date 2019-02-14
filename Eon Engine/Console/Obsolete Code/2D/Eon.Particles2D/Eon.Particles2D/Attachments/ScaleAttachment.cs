/* Created 29/08/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines an attachment that is used to 
    /// randomally scale Particles.
    /// </summary>
    public sealed class ScaleAttachment : IAttachment
    {
        List<PropertySet> props = new List<PropertySet>();

        string id;

        float minScale;
        float maxScale;

        /// <summary>
        /// Property updates.
        /// </summary>
        public List<PropertySet> Properties
        {
            get { return props; }
        }

        /// <summary>
        /// Scale attachment ID.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Update priority.
        /// </summary>
        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new ScaleAttachent.
        /// </summary>
        /// <param name="id">The ID of the ScaleAttachment.</param>
        /// <param name="minScale">Minimum scale.</param>
        /// <param name="maxScale">Maximum scale.</param>
        public ScaleAttachment(string id, float minScale, float maxScale)
        {
            this.id = id;

            this.minScale = minScale;
            this.maxScale = maxScale;
        }

        public void _Update() { }

        public void Generate()
        {
            props.Add(new PropertySet()
            {
                Scale = RandomHelper.GetRandom(minScale, maxScale),
                Colour = Color.Transparent,
                EffectColour = Color.Transparent
            });
        }

        public void Remove(int index)
        {
            props.Remove(props[index]);
        }

        public void Dispose()
        {
            props.Clear();
            props = null;
        }
    }
}
