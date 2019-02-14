/* Created: 21/08/2014
 * Last Updated: 02/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.System.Resolution
{
    /// <summary>
    /// Used to help manage multiple sreen resolutions.
    /// </summary>
    public static class ResolutionManager
    {
        static Point screenRes;
        static Viewport view;

        /// <summary>
        /// The actual screen resolution.
        /// </summary>
        public static Point AcutalScreenResolution
        {
            get { return screenRes; }
        }

        internal static Rectangle ScreenBounds
        {
            get { return view.Bounds; }
        }

        public static void ChangeScreenResolution(Point screenRes)
        {
            float aspect = AspectRatio();
            int height = (int)((float)screenRes.X / (aspect + 0.5f));

            if (height > Common.DeviceManager.PreferredBackBufferHeight)
            {
                screenRes.Y = Common.DeviceManager.PreferredBackBufferHeight;
                screenRes.X = (int)((float)height * (aspect + 0.5f));
            }

            ResolutionManager.screenRes = screenRes;

            view = new Viewport();

            view.X = (Common.DeviceManager.PreferredBackBufferWidth
                - screenRes.X) / 2;

            view.Y = (Common.DeviceManager.PreferredBackBufferHeight -
                screenRes.Y) / 2;

            view.Width = screenRes.X;
            view.Height = screenRes.Y;
            view.MinDepth = 0;
            view.MaxDepth = 1;

            Common.DeviceManager.GraphicsDevice.Viewport = view;
        }

        public static float AspectRatio()
        {
            return (float)screenRes.X / (float)screenRes.Y;
        }
    }
}
