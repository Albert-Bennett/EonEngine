/* Created: 16/12/2014
 * Last Updated: 02/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Rendering
{
    internal class Peel
    {
        RenderTarget2D colourTarget;
        RenderTarget2D depthTarget;
        RenderTarget2D opaqueTarget;

        /// <summary>
        /// The colour target of the Peel.
        /// </summary>
        public RenderTarget2D ColourTarget
        {
            get { return colourTarget; }
        }

        /// <summary>
        /// The depth target of the Peel.
        /// </summary>
        public RenderTarget2D DepthTarget
        {
            get { return depthTarget; }
        }

        /// <summary>
        /// The opaque target of the Peel.
        /// </summary>
        public RenderTarget2D OpaqueTarget
        {
            get { return opaqueTarget; }
        }

        public Peel(int width, int height)
        {
            Reload(width, height);
        }

        public void Reload(int width, int height)
        {
            depthTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Single, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);

            opaqueTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.HalfVector4, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            colourTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
        }
    }
}
