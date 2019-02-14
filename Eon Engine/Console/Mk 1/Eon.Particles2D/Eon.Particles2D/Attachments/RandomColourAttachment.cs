/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines an attachment that chooses 
    /// a random colour to render particles in.
    /// </summary>
    public class RandomColourAttachment : IAttachment
    {
        List<PropertySet> props = new List<PropertySet>();
        List<Color> colours;
        List<RandomColourState> randColours = new List<RandomColourState>();

        string id;

        Color effectColour;

        float minChangeTime;
        float maxChangeTime;

        /// <summary>
        /// The properties of the objects editted by this RandomColourAttachment.
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
        /// Creates a new RandomColourAttachment.
        /// </summary>
        /// <param name="id">The unique identifaction name to give to the Attachment.</param>
        /// <param name="colours">The colour that can be randomally 
        /// choosen to render Particles.</param>
        /// <param name="minChangeTime">The minimum amount of 
        /// time between colour changes.</param>
        /// <param name="maxChangeTime">The maximum amount of 
        /// time between colour changes.</param>
        /// <param name="effectColour">The colour to render effects in.</param>
        public RandomColourAttachment(string id, List<Color> colours,
            float minChangeTime, float maxChangeTime, Color effectColour)
        {
            this.id = id;

            this.colours = colours;
            this.minChangeTime = minChangeTime;
            this.maxChangeTime = maxChangeTime;
            this.effectColour = effectColour;
        }

        public void _Update()
        {
            for (int i = 0; i < randColours.Count; i++)
            {
                RandomColourState r = randColours[i];
                bool change = r.UpdateChange();

                if (change)
                {
                    r.ChangeRate = TimeSpan.FromMilliseconds(
                        RandomHelper.GetRandomFloat(minChangeTime, maxChangeTime));

                    props[i] = new PropertySet()
                    {
                        EffectColour = effectColour,
                        Colour = colours[RandomHelper.GetRandomInt(0, colours.Count - 1)]
                    };
                }

                randColours[i] = r;
            }
        }

        public void Generate()
        {
            props.Add(new PropertySet()
            {
                EffectColour = effectColour,
                Colour = colours[RandomHelper.GetRandomInt(0, colours.Count - 1)]
            });

            randColours.Add(new RandomColourState()
            {
                ChangeRate = TimeSpan.FromMilliseconds(
                       RandomHelper.GetRandomFloat(minChangeTime, maxChangeTime)),
                currentTime = TimeSpan.Zero
            });
        }

        public void Remove(int index)
        {
            props.Remove(props[index]);
            randColours.Remove(randColours[index]);
        }

        public void ScreenResolutionChanged() { }

        public void Dispose()
        {
            props.Clear();
            props = null;

            colours.Clear();
            colours = null;
        }
    }

    struct RandomColourState
    {
        public TimeSpan currentTime;
        public TimeSpan ChangeRate;

       public bool UpdateChange()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime >= ChangeRate)
            {
                currentTime = TimeSpan.Zero;
                return true;
            }

            return false;
        }
    }
}
