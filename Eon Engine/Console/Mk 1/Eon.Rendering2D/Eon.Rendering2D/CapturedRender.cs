/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Eon.Rendering2D
{
    /// <summary>
    /// A class that extends RenderTarget2D.
    /// </summary>
    public class CapturedRender
    {
        RenderTarget2D renderTarget;

        /// <summary>
        /// The RenderTarget2D that this generates.
        /// </summary>
        public RenderTarget2D Target { get { return renderTarget; } }

        /// <summary>
        /// Finds the RenderTarget.
        /// </summary>
        public Texture2D Texture { get { return renderTarget; } }

        /// <summary>
        /// The width of the CapturedRender.
        /// </summary>
        public int Width { get { return Target.Width; } }

        /// <summary>
        /// The height of the CapturedRender.
        /// </summary>
        public int Height { get { return Target.Height; } }

        /// <summary>
        /// Creates a new CapturedRender.
        /// </summary>
        /// <param name="format">The surface format for the RenderTarget.</param>
        public CapturedRender(SurfaceFormat format)
        {
            renderTarget = new RenderTarget2D(Common.Device,
                (int)Common.ScreenResolution.X,
                (int)Common.ScreenResolution.Y, false, format,
                DepthFormat.Depth24);
        }

        /// <summary>
        /// Creates a new CapturedRender.
        /// </summary>
        /// <param name="format">The surface format for the RenderTarget.</param>
        /// <param name="depth">The DepthFormat to be use.</param>
        public CapturedRender(SurfaceFormat format, DepthFormat depth)
        {
            renderTarget = new RenderTarget2D(Common.Device, (int)Common.ScreenResolution.X,
                (int)Common.ScreenResolution.Y, false, format, depth);
        }

        /// <summary>
        /// Creates a new CapturedRender.
        /// </summary>
        public CapturedRender()
        {
            renderTarget = new RenderTarget2D(Common.Device,
               (int)Common.ScreenResolution.X,
                (int)Common.ScreenResolution.Y,
                false, SurfaceFormat.Color, DepthFormat.Depth24);
        }

        public CapturedRender(SurfaceFormat format, DepthFormat depthFormat,
            int multiSampleCount, RenderTargetUsage useage)
        {
            renderTarget = new RenderTarget2D(Common.Device,
                (int)Common.ScreenResolution.X,
                (int)Common.ScreenResolution.Y,
                false, format, depthFormat, multiSampleCount, useage);
        }

        /// <summary>
        /// Sets the GraphicsDevice's RenderTarget to the one contained in this.
        /// </summary>
        public void Begin()
        {
            Common.Device.SetRenderTarget(renderTarget);
        }

        /// <summary>
        /// Sets the GraphicDevice's RenderTarget to null. 
        /// </summary>
        public void End()
        {
            Common.Device.SetRenderTarget(null);
        }

        /// <summary>
        /// Disposes of the captured render.
        /// </summary>
        public void Dispose()
        {
            renderTarget.Dispose();
            renderTarget = null;
        }

        /// <summary>
        /// Saves the render target contained as a .png file 
        /// in the output folder for the game.
        /// </summary>
        /// <param name="name">The name to give the saved image.</param>
        public void Save(string name)
        {
            Stream stream = new FileStream(name + ".png", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            renderTarget.SaveAsPng(stream, (int)Common.ScreenResolution.X, (int)Common.ScreenResolution.Y);
        }
    }
}
