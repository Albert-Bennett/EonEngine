/* Created: 08/09/2014
 * Last Updated: 05/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Eon.System.Tools;
using Eon.Testing.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Testing
{
    /// <summary>
    /// Used to define a encasment tool that is used to 
    /// manage Errors and various other testing Utilities.
    /// </summary>
    public sealed class ErrorConsole : EngineModule, IPostGameDraw
    {
        #region Varibles

        static List<Error> errors = new List<Error>();

        static readonly Vector2 startPos = new Vector2(15, 105);
        static TextureBuffer buffer;

        List<Utility> utilities = new List<Utility>();
        Texture2D bg;
        Texture2D blank;
        Rectangle bounds;

        Rectangle sourceRect;
        Rectangle errorBounds;

        RenderTarget2D errorTarget;
        RenderTarget2D finalTarget;

        Color consoleColour = new Color(0, 0, 0, 200);

        static SpriteFont font;

        bool hidden = true;

        #endregion
        #region Fields

        internal static SpriteFont Font
        {
            get { return font; }
        }

        public static TextureBuffer Buffer
        {
            get { return buffer; }
        }

        public int Priority
        {
            get { return 1; }
        }

        #endregion
        #region  Ctor

        public ErrorConsole()
            : base("ErrorConsole")
        {
            buffer = new TextureBuffer("ErrorBuffer", 3, "Final");

            font = Common.ContentBuilder.Load<SpriteFont>("Eon/Fonts/Arial12");
            bg = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/DefaultTexture");
        }

        protected override void Initialize()
        {
            bounds = new Rectangle(0, 0, (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y / 2);

            sourceRect = new Rectangle((int)startPos.X, (int)startPos.Y,
                bounds.Width - 15, (bounds.Height - 15) - (int)startPos.Y);

            errorBounds = sourceRect;

            errorTarget = new RenderTarget2D(Common.Device,
                (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y);

            finalTarget = new RenderTarget2D(Common.Device,
                (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y);

            Attach(new FPSCounter());
            Attach(new TestParameters());

            blank = new Texture2D(Common.Device, 1, 1);

            base.Initialize();
        }

        public void _Update()
        {
            for (int i = 0; i < errors.Count; i++)
                errors[i].Update();
        }

        public void _PostUpdate() { }

        #endregion
        #region Render

        public void PostGameDraw()
        {
            if (!hidden)
                Draw();
            else
                buffer.SetBuffer("Final", blank);
        }

        void Draw()
        {
            DrawErrors();

            DrawFinal();

            buffer.SetBuffer("Final", finalTarget);
        }

        void DrawErrors()
        {
            Common.Device.SetRenderTargets(null);
            Common.Device.SetRenderTarget(errorTarget);

            Common.Batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            Common.Device.Clear(Color.Transparent);

            for (int i = 0; i < errors.Count; i++)
                errors[i].Draw();

            Common.Batch.End();
            Common.Device.SetRenderTarget(null);
        }

        void DrawFinal()
        {
            Common.Device.SetRenderTarget(finalTarget);
            Common.Batch.Begin();
            Common.Device.Clear(Color.Transparent);

            Common.Batch.Draw(bg, bounds, consoleColour);

            Common.Batch.Draw(errorTarget, errorBounds, sourceRect, Color.White);

            for (int i = 0; i < utilities.Count; i++)
                utilities[i].Draw();

            Common.Batch.End();
            Common.Device.SetRenderTarget(null);
        }

        #endregion
        #region Helpers

        internal void Attach(Utility utility)
        {
            utility.Owner = this;
            utility.Initialize();

            utilities.Add(utility);
        }

        public static void Attach(Error error)
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

        public static void Remove(Error error)
        {
            for (int i = 0; i < errors.Count; i++)
                errors[i].Position = CalibratePosition(i);
        }

        public void ToggleHidden()
        {
            hidden = !hidden;
        }

        #endregion
    }
}
