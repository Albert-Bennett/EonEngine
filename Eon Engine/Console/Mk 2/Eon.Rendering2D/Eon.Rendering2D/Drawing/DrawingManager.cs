/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Lighting;
using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D.Drawing
{
    /// <summary>
    /// Defines an EngineComponent that is used to draw objects. 
    /// </summary>
    public sealed class DrawingManager : EngineComponent, IRenderComponent
    {
        static DrawLayers layers = new DrawLayers();

        RenderTarget2D render;
        RenderTarget2D opaqueRender;
        RenderTarget2D distortionRender;

        Texture2D final;
        Texture2D black;

        LPP2D lighting = new LPP2D();

        /// <summary>
        /// The maximum draw layer.
        /// </summary>
        public static int MaximumLayer
        {
            get { return layers.MaximumLayerDepth; }
        }

        /// <summary>
        /// The render priority of this.
        /// </summary>
        public int Priority
        {
            get { return 3; }
        }

        /// <summary>
        /// Wheather or not this is the final stage when rendering.
        /// </summary>
        public bool RenderFinal
        {
            get
            {
                EngineComponent comp = EngineComponentManager.Find("D2RenderFramework");

                if (comp != null)
                    return !(bool)comp.SendMessage("PostProcessingExists");

                return true;
            }
        }

        /// <summary>
        /// The final composed image.
        /// </summary>
        public Texture2D FinalImage
        {
            get { return final; }
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
                    Draw(DrawingStage.Distortion);

                    TextureBuffer.SetBuffer("DistortionMap", distortionRender);
                    TextureBuffer.SetBuffer("OpaqueMap", opaqueRender);
                    TextureBuffer.SetBuffer("ColourMap", render);

                    lighting.Draw();

                    final = lighting.Final;
                }
                else
                {
                    Draw(DrawingStage.Colour);

                    final = render;
                }
            else
                final = black;

            EngineComponent comp = EngineComponentManager.Find("D2RenderFramework");

            if (comp != null)
                comp.SendMessage("SetPostProcessing", new object[] { final, 
                    opaqueRender, distortionRender });
        }

        void Draw(DrawingStage stage)
        {
            Common.Device.BlendState = BlendState.AlphaBlend;

            switch (stage)
            {
                case DrawingStage.Colour:
                    Common.Device.SetRenderTarget(render);
                    break;

                case DrawingStage.Distortion:
                    Common.Device.SetRenderTarget(distortionRender);
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
            render = new RenderTarget2D(Common.Device, (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y, false, SurfaceFormat.Color, DepthFormat.None);

            opaqueRender = new RenderTarget2D(Common.Device, (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y, false, SurfaceFormat.Color, DepthFormat.None);

            distortionRender = new RenderTarget2D(Common.Device, (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y, false, SurfaceFormat.Color, DepthFormat.None);
        }
    }
}
