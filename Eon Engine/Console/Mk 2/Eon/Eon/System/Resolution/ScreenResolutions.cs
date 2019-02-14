/* Created 23/07/2014
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
        Auto = -1,

        //800 x 600 at 4:3
        SVGA = 0,

        //1024 x 600 at 17:10
        WSVGA = 1,

        //1022 x 768 at 4:3
        XGA = 2,

        //1152 x 864 at 4:3	
        XGAPlus = 3,

        //1280 x 720 at 16:9
        WXGA = 4,

        //1280 x 768 at 5:3
        WXGA2 = 5,

        //1280 x 800 at 16:10	
        WXGA3 = 6,

        //1280 x 960 at 4:3
        UVGA = 7,

        //1280 x 1024 at 5:4
        SXGA = 8,

        //1366 x 768 at 16:9
        HD = 9,

        //1400 x 1050 at 4:3
        SXGAPlus = 10,

        //1440 x 900 at 16:10
        WXGAPlus = 11,

        //1600 x 900 at 16:9
        HDPlus = 12,

        //1600 x 1200 at 4:3
        UXGA = 13,

        //1680 x 1050 at 16:10
        WSXGAPlus = 14,

        //1920 x 1080 at 16:9
        FullHD = 15,

        //1920 x 1200 at 16:10
        WUXGA = 16,

        //2048 x 1152 at 16:9
        QWXGA = 17,

        //2560 x 1440 at 16:9
        WQHD = 18,

        //2560 x 1600 at 16:10
        WQXGA = 19,

        //3840 × 2160 at 16:9
        TwoK = 20,

        //7680 × 4320 at 16:9
        FourK = 21,

        //15360 × 8640 at 16:9
        EightK = 22
    }
}
