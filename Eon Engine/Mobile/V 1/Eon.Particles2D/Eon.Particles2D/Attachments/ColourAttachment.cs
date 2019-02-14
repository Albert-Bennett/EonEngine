/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines an attachment that applies a colour to particles.
    /// </summary>
    public class ColourAttachment : IAttachment
    {
        List<PropertySet> props = new List<PropertySet>();

        string id;

        Color colour;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The properties of the objects editted by this ColourAttachment.
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
        /// Creates a new ColourAttachment.
        /// </summary>
        /// <param name="id">The unique identifaction name to give to the Attachment.</param>
        /// <param name="colour">The colour to render the Particles in.</param>
        /// <param name="effectColour">The colour to render the effects in.</param>
        public ColourAttachment(string id, Color colour)
        {
            this.id = id;
            this.colour = colour;
        }

        public void _Update() { }

        public void Generate()
        {
            props.Add(new PropertySet()
            {
                Colour = colour
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

        public void ScreenResolutionChanged() { }
    }
}
