/* Created: 23/07/2014
 * Last Updated: 14/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.Resolution
{
    /// <summary>
    /// Used to define the different acceptable screed resolutions. 
    /// </summary>
    public enum ScreenResolutions : int
    {
        //Auto determines screen resolution and aspect ratio.
        Auto = 0,

        //800 x 480 at 4:3
        SVGA = 1,

        //1024 x 600 at 17:10
        WSVGA = 2,

        //1022 x 768 at 4:3
        XGA = 3,

        //1152 x 864 at 4:3	
        XGAPlus = 4,

        //1280 x 720 at 16:9
        WXGA = 5,

        //1280 x 768 at 5:3
        WXGA2 = 6,

        //1280 x 800 at 16:10	
        WXGA3 = 7,

        //1280 x 960 at 4:3
        UVGA = 8,

        //1280 x 1024 at 5:4
        SXGA = 9,

        //1366 x 768 at 16:9
        HD = 10,

        //1400 x 1050 at 4:3
        SXGAPlus = 11,

        //1440 x 900 at 16:10
        WXGAPlus = 12,

        //1600 x 900 at 16:9
        HDPlus = 13,

        //1600 x 1200 at 4:3
        UXGA = 14,

        //1680 x 1050 at 16:10
        WSXGAPlus = 15,

        //1920 x 1080 at 16:9
        FullHD = 16,

        //1920 x 1200 at 16:10
        WUXGA = 17,

        //2048 x 1152 at 16:9
        QWXGA = 18,

        //2560 x 1440 at 16:9
        WQHD = 19,

        //2560 x 1600 at 16:10
        WQXGA = 20,

        //3840 × 2160 at 16:9
        TwoK = 21,

        //7680 × 4320 at 16:9
        FourK = 22,

        //15360 × 8640 at 16:9
        EightK = 23
    }
}
