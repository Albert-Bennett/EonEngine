/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.EngineComponents;
using Eon.EngineComponents.Interfaces;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Lighting;
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

        CapturedRender render;
        CapturedRender opaqueRender;
        CapturedRender distortionRender;

        Texture2D final;
        Texture2D black;

        LPP lighting = new LPP();

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
            ScreenResolutionChanged();

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
                if (LightingManager.LightCount > 0 && CameraManager.CurrentCamera != null)
                {
                    Draw(DrawingStage.Colour);
                    Draw(DrawingStage.Normal);
                    Draw(DrawingStage.Distortion);
                    lighting.SetTargets(render.Texture, opaqueRender.Texture);
                    lighting.Draw();

                    final = lighting.Final;
                }
                else
                {
                    Draw(DrawingStage.Colour);

                    final = render.Texture;
                }
            else
                final = black;

            EngineComponent comp = EngineComponentManager.Find("D2RenderFramework");

            if (comp != null)
                comp.SendMessage("SetPostProcessing", new object[] { final, 
                    opaqueRender.Texture, distortionRender.Texture });
        }

        void Draw(DrawingStage stage)
        {
            Common.Device.BlendState = BlendState.AlphaBlend;

            switch (stage)
            {
                case DrawingStage.Colour:
                    render.Begin();
                    break;

                case DrawingStage.Distortion:
                    distortionRender.Begin();
                    break;

                default:
                    opaqueRender.Begin();
                    break;
            }

            Common.Device.Clear(Color.Transparent);

            if (CameraManager.CurrentCamera != null)
                Common.Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                    null, null, null, CameraManager.CurrentCamera.ViewMatrix);
            else
                Common.Batch.Begin();

            layers.Draw(stage);

            Common.Batch.End();

            switch (stage)
            {
                case DrawingStage.Colour:
                    render.End();
                    break;

                case DrawingStage.Distortion:
                    distortionRender.End();
                    break;

                default:
                    opaqueRender.End();
                    break;
            }
        }

        /// <summary>
        /// Used to resize various aspects that 
        /// are used in the rendering of objects.
        /// </summary>
        public void ScreenResolutionChanged()
        {
            render = new CapturedRender();
            opaqueRender = new CapturedRender();
            distortionRender = new CapturedRender();

            lighting.ScreenResolutionChanged();
        }
    }
}
