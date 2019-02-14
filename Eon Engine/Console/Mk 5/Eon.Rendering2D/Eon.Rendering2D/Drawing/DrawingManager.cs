/* Created: 01/06/2013
 * Last Updated: 05/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Lighting;
using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D.Drawing
{
    /// <summary>
    /// Defines an EngineModule that is used to draw objects. 
    /// </summary>
    public sealed class DrawingManager : EngineModule, IRenderComponent
    {
        static DrawLayers layers = new DrawLayers();

        RenderTarget2D render;
        RenderTarget2D opaqueRender;

        Texture2D black;

        LPP2D lighting = new LPP2D();

        /// <summary>
        /// The maximum draw layer.
        /// </summary>
        public static int MaximumLayer
        {
            get { return layers.MaximumLayerDepth; }
        }

        public int Order
        {
            get { return 2; }
        }

        /// <summary>
        /// Creates a new DrawingManager.
        /// </summary>
        public DrawingManager() : base("DrawingManager") { }

        protected override void Initialize()
        {
            black = new Texture2D(Common.Device, 1, 1);

            TextureQualityChanged();

            base.Initialize();
        }

        /// <summary>
        /// Adds an item to be drawn by this.
        /// </summary>
        /// <param name="item">The item to be drawn.</param>
        public static void Add(IDrawItem item)
        {
            layers.AddDrawItem(item);
        }

        /// <summary>
        /// Removes an item from this.
        /// </summary>
        /// <param name="item">The item to be removed.</param>
        public static void Remove(IDrawItem item)
        {
            layers.Remove(item);
        }

        /// <summary>
        /// Draws all of the IDrawItems that are to be drawn.
        /// </summary>
        public void Render()
        {
            if (layers.Layers > 0)
                if (lighting.Created && (LightingManager2D.LightCount > 0 && CameraManager2D.CurrentCamera != null))
                {
                    Draw(DrawingStage.Colour);
                    Draw(DrawingStage.Normal);

                    Framework.Framework.TextureBuffer.SetBuffer("OpaqueMap", opaqueRender);
                    Framework.Framework.TextureBuffer.SetBuffer("ColourMap", render);

                    lighting.Draw();
                }
                else
                {
                    Common.Device.Clear(Color.Transparent);

                    Draw(DrawingStage.Colour);

                    Framework.Framework.TextureBuffer.SetBuffer("Final", render);
                }
            else
                Framework.Framework.TextureBuffer.SetBuffer("Final", black);
        }

        void Draw(DrawingStage stage)
        {
            Common.Device.BlendState = BlendState.AlphaBlend;

            switch (stage)
            {
                case DrawingStage.Colour:
                    Common.Device.SetRenderTarget(render);
                    break;

                default:
                    Common.Device.SetRenderTarget(opaqueRender);
                    break;
            }

            Common.Device.Clear(Color.Transparent);

            if (CameraManager2D.CurrentCamera != null)
                Common.Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                    null, null, null, CameraManager2D.CurrentCamera.ViewMatrix);
            else
                Common.Batch.Begin();

            layers.Draw(stage);

            Common.Batch.End();

            Common.Device.SetRenderTarget(null);
        }

        public void TextureQualityChanged()
        {
            int width = (int)Common.TextureQuality.X;
            int height = (int)Common.TextureQuality.Y;

            render = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            opaqueRender = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
        }
    }
}
