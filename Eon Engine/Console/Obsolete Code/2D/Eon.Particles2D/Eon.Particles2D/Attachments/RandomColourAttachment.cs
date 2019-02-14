/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
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
        List<RandomColourState> randColours = new List<RandomColourState>();

        string id;

        float minChangeTime;
        float maxChangeTime;

        public int Priority
        {
            get { return 0; }
        }

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
        public RandomColourAttachment(string id,
            float minChangeTime, float maxChangeTime)
        {
            this.id = id;

            this.minChangeTime = minChangeTime;
            this.maxChangeTime = maxChangeTime;
        }

        public void _Update()
        {
            for (int i = 0; i < randColours.Count; i++)
            {
                RandomColourState r = randColours[i];

                r.ChangeRate = TimeSpan.FromMilliseconds(
                    RandomHelper.GetRandom(minChangeTime, maxChangeTime));

                props[i] = new PropertySet()
                {
                    EffectColour = Color.White,
                    Colour = randColours[i].Current
                };

                randColours[i] = r;
            }
        }

        public void Generate()
        {
            props.Add(new PropertySet()
            {
                EffectColour = Color.White,
                Colour = Color.White
            });

            randColours.Add(new RandomColourState()
            {
                ChangeRate = TimeSpan.FromMilliseconds(
                       RandomHelper.GetRandom(minChangeTime, maxChangeTime)),

                CurrentTime = TimeSpan.Zero,
                Current = Color.White,

                Random = new Color(RandomHelper.GetRandom(1, 255),
                    RandomHelper.GetRandom(1, 255),
                    RandomHelper.GetRandom(1, 255),
                    RandomHelper.GetRandom(1, 255))
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
        }
    }

    struct RandomColourState
    {
        public TimeSpan CurrentTime;
        public TimeSpan ChangeRate;
        public Color Current;
        public Color Random;

        public void Update()
        {
            CurrentTime += Common.ElapsedTimeDelta;

            float rate = 1 / (float)ChangeRate.Ticks;

            if (CurrentTime >= ChangeRate)
            {
                Random = new Color(
                    RandomHelper.GetRandom(1, 255),
                    RandomHelper.GetRandom(1, 255),
                    RandomHelper.GetRandom(1, 255),
                    RandomHelper.GetRandom(1, 255));

                CurrentTime = TimeSpan.Zero;
            }
            else
                Current = Color.Lerp(Current, Random, rate);
        }
    }
}
