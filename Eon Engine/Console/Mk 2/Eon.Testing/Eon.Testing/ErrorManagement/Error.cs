/* Created 05/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.Testing.ErrorManagement
{
    /// <summary>
    /// Defines the seriousness of errors.
    /// </summary>
    public enum Seriousness
    {
        Warning,
        Error,
        CriticalError
    }

    /// <summary>
    /// Describes an error message that doesn't crash the game.
    /// </summary>
    public class Error
    {
        static readonly TimeSpan time = TimeSpan.FromSeconds(5);
        static readonly TimeSpan opacityTime = TimeSpan.FromSeconds(10);
        TimeSpan currentTime = TimeSpan.Zero;

        Seriousness seriousness = Seriousness.Warning;

        string text;
        float scale = 1;
        float step = 0;

        Color colour;
        Vector2 pos;

        internal Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public Error(string text, Seriousness seriousness)
        {
            this.text = text;
            this.seriousness = seriousness;

            colour = GetColour();
            step = 1f / (float)(opacityTime.TotalMilliseconds - time.TotalMilliseconds);

            ErrorManager.Add(this);
        }

        Color GetColour()
        {
            switch (seriousness)
            {
                case Seriousness.Warning:
                    return Color.Yellow;

                case Seriousness.Error:
                    return Color.Blue;

                default:
                    return Color.Red;
            }
        }

        internal void Update()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime > time)
            {
                if (currentTime < opacityTime)
                    colour = Color.Lerp(colour, Color.Transparent, step);
                else
                    Remove();
            }

            if (currentTime > time)
                Remove();
        }

        public void Draw()
        {
            Common.Batch.DrawString(ErrorManager.Font, text, pos, colour, 0,
                Vector2.Zero, scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }

        public void Remove()
        {
            ErrorManager.Remove(this);
        }
    }
}
