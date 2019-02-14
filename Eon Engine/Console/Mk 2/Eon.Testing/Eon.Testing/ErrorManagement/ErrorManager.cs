/* Created 28/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Testing.ErrorManagement
{
    public sealed class ErrorManager : EngineComponent, IPostGameDraw
    {
        static List<Error> errors = new List<Error>();
        static SpriteFont font;

        static readonly Vector2 startPos = new Vector2(15, 45);

        internal static SpriteFont Font
        {
            get { return font; }
        }

        public int Priority
        {
            get { return 0; }
        }

        public ErrorManager()
            : base("ErrorManager")
        {
            font = Common.ContentManager.Load<SpriteFont>("Eon/Fonts/Arial12");
        }

        internal static void Add(Error error)
        {
            error.Position = CalibratePosition(errors.Count);

            errors.Add(error);
        }

        static Vector2 CalibratePosition(int index)
        {
            Vector2 pos = new Vector2();

            if (index == 0)
                pos = startPos;
            else
            {
                float sizeY = font.MeasureString("A").Y + 0.15f;

                pos = errors[index - 1].Position;
                pos.Y += sizeY;
            }
            return pos;
        }

        public void _Update()
        {
            for (int i = 0; i < errors.Count; i++)
                errors[i].Update();
        }

        internal static void Remove(Error error)
        {
            errors.Remove(error);

            for (int i = 0; i < errors.Count; i++)
                errors[i].Position = CalibratePosition(i);
        }

        public void PostGameDraw()
        {
            for (int i = 0; i < errors.Count; i++)
                errors[i].Draw();
        }
    }
}
