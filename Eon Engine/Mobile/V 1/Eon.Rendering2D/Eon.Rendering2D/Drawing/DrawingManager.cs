/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.EngineComponents.Interfaces;
using Eon.Rendering2D.Cameras;
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

        Texture2D final;
        Texture2D black;

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
                {
                    Draw();

                    final = render.Texture;
                }
            else
                final = black;
        }

        void Draw()
        {
            Common.Device.BlendState = BlendState.AlphaBlend;

            render.Begin();

            Common.Device.Clear(Color.Transparent);

            if (CameraManager.CurrentCamera != null)
                Common.Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                    null, null, null, CameraManager.CurrentCamera.ViewMatrix);
            else
                Common.Batch.Begin();

            layers.Draw();

            Common.Batch.End();

            render.End();

        }

        /// <summary>
        /// Used to resize various aspects that 
        /// are used in the rendering of objects.
        /// </summary>
        public void ScreenResolutionChanged()
        {
            render = new CapturedRender();
        }
    }
}
