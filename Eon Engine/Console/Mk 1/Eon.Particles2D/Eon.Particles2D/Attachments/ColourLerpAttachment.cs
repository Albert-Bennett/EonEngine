/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines n attachment that lerps between two colours.
    /// </summary>
    public class ColourLerpAttachment : IAttachment
    {
        List<PropertySet> props = new List<PropertySet>();
        List<ColourLerpState> lerps = new List<ColourLerpState>();

        string id;

        Color colour;
        Color toLerpTo;
        Color effectColour;

        float lerpMin;
        float lerpMax;

        /// <summary>
        /// The unique identifaction name given to the Attachment.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// The properties of the objects editted by this ColourLerpAttachment.
        /// </summary>
        public List<PropertySet> Properties
        {
            get { return props; }
        }

        /// <summary>
        /// Creates a new ColourLerpAttachment. Lerps between Color.White and Color.Transparent.
        /// </summary>
        /// <param name="id">The unique identifaction name to give to the Attachment.</param>
        /// <param name="minLerp">The minimum amount to lerp by.</param>
        /// <param name="maxLerp">The maximum amount to lerp by.</param>
        public ColourLerpAttachment(string id, float minLerp, float maxLerp) :
            this(id, minLerp, maxLerp, Color.White, Color.Transparent, Color.White) { }

        /// <summary>
        /// Creates a new ColourLerpAttachment.
        /// </summary>
        /// <param name="id">The unique identifaction name to give to the Attachment.</param>
        /// <param name="minLerp">The minimum amount to lerp by.</param>
        /// <param name="maxLerp">The maximum amount to lerp by.</param>
        /// <param name="colour">The starting colour.</param>
        /// <param name="toLerpTo">The colour to lerp to.</param>
        public ColourLerpAttachment(string id, float minLerp, float maxLerp,
            Color colour, Color toLerpTo)
            : this(id, minLerp, maxLerp, colour, toLerpTo, Color.White) { }

        /// <summary>
        /// Creates a new ColourLerpAttachment.
        /// </summary>
        /// <param name="id">The unique identifaction name to give to the Attachment.</param>
        /// <param name="minLerp">The minimum amount to lerp by.</param>
        /// <param name="maxLerp">The maximum amount to lerp by.</param>
        /// <param name="colour">The starting colour.</param>
        /// <param name="toLerpTo">The colour to lerp to.</param>
        /// <param name="effectColour">The colour to render effects in.</param>
        public ColourLerpAttachment(string id, float minLerp, float maxLerp,
            Color colour, Color toLerpTo, Color effectColour)
        {
            this.id = id;
            this.lerpMin = minLerp;
            this.lerpMax = maxLerp;

            this.colour = colour;
            this.toLerpTo = toLerpTo;
            this.effectColour = effectColour;
        }

        public void _Update()
        {
            for (int i = 0; i < lerps.Count; i++)
            {
                ColourLerpState l = lerps[i];
                l.Update();

                lerps[i] = l;

                props[i] = new PropertySet()
                {
                    EffectColour = effectColour,
                    Colour = l.Colour
                };
            }
        }

        public void Generate()
        {
            lerps.Add(new ColourLerpState()
            {
                Colour = colour,
                LerpRate = RandomHelper.GetRandomFloat(lerpMin, lerpMax),
                ToLerpTo = toLerpTo
            });

            props.Add(new PropertySet()
            {
                Colour = colour,
                EffectColour = effectColour
            });
        }

        public void Remove(int index)
        {
            props.Remove(props[index]);
            lerps.Remove(lerps[index]);
        }

        public void ScreenResolutionChanged() { }

        public void Dispose()
        {
            lerps.Clear();
            lerps = null;

            props.Clear();
            props = null;
        }
    }
}
