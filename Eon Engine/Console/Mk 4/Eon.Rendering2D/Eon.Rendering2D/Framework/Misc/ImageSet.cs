/* Created: 18/06/2015
 * Last Updated: 18/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering2D.Framework.Misc
{
    /// <summary>
    /// Used to define an ImageSheet.
    /// </summary>
    public class ImageSet
    {
        public bool PostRender;
        public int DrawLayer;

        public string[] Filepaths;

        public int Width;
        public int Height;

        public int Rows;

        public Vector2 StartingPos = Vector2.Zero;
        public Vector2 Spacing = Vector2.Zero;
    }
}
