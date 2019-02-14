/* Created 05/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
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
        static readonly TimeSpan time = TimeSpan.FromSeconds(20);
        static readonly TimeSpan opacityTime = TimeSpan.FromSeconds(15);
        TimeSpan currentTime = TimeSpan.Zero;

        Seriousness seriousness = Seriousness.Warning;

        string text;
        float scale = 1;

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

            ErrorManager.Add(this);
        }

        Color GetColour()
        {
            switch (seriousness)
            {
                case Seriousness.Warning:
                    return Color.Yellow;

                case Seriousness.Error:
                    return Color.Red;

                default:
                    return Color.Blue;
            }
        }

        internal void Update()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime > opacityTime)
            {
                float alpha = 255;

                TimeSpan temp = currentTime;
                temp -= opacityTime;

                int secs = temp.Seconds;

                if (secs > 0)
                    alpha -= 25.5f * secs;

                colour.A = (byte)alpha;
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

        public void ScreenResolutionChanged()
        {
            pos = Common.ReCalibrateScreenSpaceVector(pos);
            scale = Common.ReCalibrateScale(scale);
        }
    }
}
