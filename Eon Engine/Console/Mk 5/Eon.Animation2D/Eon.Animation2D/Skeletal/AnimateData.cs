/* Created 13/05/2015
 * Last Updated: 10/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Animation2D.Skeletal
{
    /// <summary>
    /// Used to define an AnimatedLimb.
    /// </summary>
    public class AnimateData
    {
        public int Columns;
        public int Rows;
        public int TotalFrames;

        public float FrameRate;
        public bool RandomStart = false;
    }
}
