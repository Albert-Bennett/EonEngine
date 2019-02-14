/* Created 17/08/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.System.Resolution
{
    /// <summary>
    /// A helper class for calculating scree resolutions.
    /// </summary>
    public static class ResolutionHelper
    {
        /// <summary>
        /// Gets a vector2 defining a given ScreenResolution.
        /// </summary>
        /// <param name="resolution">The ScreenResolution to get the value of.</param>
        public static Vector2 GetScreenResolution(ScreenResolutions resolution)
        {
            int width = 0;
            int height = 0;

            switch (resolution)
            {
                case ScreenResolutions.Auto:
                    {
                        width = Common.Device.Adapter.CurrentDisplayMode.Width;
                        height = Common.Device.Adapter.CurrentDisplayMode.Height;
                    }
                    break;

                case ScreenResolutions.EightK:
                    {
                        width = 15560;
                        height = 8640;
                    }
                    break;

                case ScreenResolutions.FourK:
                    {
                        width = 7680;
                        height = 4320;
                    }
                    break;

                case ScreenResolutions.FullHD:
                    {
                        width = 1920;
                        height = 1080;
                    }
                    break;

                case ScreenResolutions.HD:
                    {
                        width = 1400;
                        height = 1050;
                    }
                    break;

                case ScreenResolutions.HDPlus:
                    {
                        width = 1600;
                        height = 900;
                    }
                    break;

                case ScreenResolutions.QWXGA:
                    {
                        width = 2048;
                        height = 1152;
                    }
                    break;

                case ScreenResolutions.SVGA:
                    {
                        width = 800;
                        height = 480;
                    }
                    break;

                case ScreenResolutions.SXGA:
                    {
                        width = 1280;
                        height = 1024;
                    }
                    break;

                case ScreenResolutions.SXGAPlus:
                    {
                        width = 1400;
                        height = 1050;
                    }
                    break;

                case ScreenResolutions.TwoK:
                    {
                        width = 3840;
                        height = 2160;
                    }
                    break;

                case ScreenResolutions.UVGA:
                    {
                        width = 1290;
                        height = 960;
                    }
                    break;

                case ScreenResolutions.UXGA:
                    {
                        width = 1600;
                        height = 1200;
                    }
                    break;

                case ScreenResolutions.WQHD:
                    {
                        width = 2560;
                        height = 1440;
                    }
                    break;

                case ScreenResolutions.WQXGA:
                    {
                        width = 2560;
                        height = 1600;
                    }
                    break;

                case ScreenResolutions.WSVGA:
                    {
                        width = 1024;
                        height = 600;
                    }
                    break;

                case ScreenResolutions.WSXGAPlus:
                    {
                        width = 1680;
                        height = 1050;
                    }
                    break;

                case ScreenResolutions.WUXGA:
                    {
                        width = 1920;
                        height = 1200;
                    }
                    break;

                case ScreenResolutions.WXGA:
                    {
                        width = 1280;
                        height = 720;
                    }
                    break;

                case ScreenResolutions.WXGA2:
                    {
                        width = 1280;
                        height = 768;
                    }
                    break;

                case ScreenResolutions.WXGA3:
                    {
                        width = 1280;
                        height = 800;
                    }
                    break;

                case ScreenResolutions.WXGAPlus:
                    {
                        width = 1440;
                        height = 900;
                    }
                    break;

                case ScreenResolutions.XGA:
                    {
                        width = 1022;
                        height = 768;
                    }
                    break;

                case ScreenResolutions.XGAPlus:
                    {
                        width = 1152;
                        height = 864;
                    }
                    break;
            }

            return new Vector2(width, height);
        }
    }
}
