/* Created: 08/08/2013
 * Last Updated: 02/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D.Drawing
{
    public class PostRenderManager : EngineComponent, IPostRenderComponent, IHoldReferences
    {
        static DrawLayers layers = new DrawLayers();

        RenderTarget2D render;

        Texture2D blank;
        TextureBuffer textureBuffer;

        public static int MaximumLayer
        {
            get { return layers.MaximumLayerDepth; }
        }

        public int Order
        {
            get { return 0; }
        }

        public PostRenderManager() : base("PostRenderManager") { }

        protected override void Initialize()
        {
            blank = new Texture2D(Common.Device, 1, 1);

            textureBuffer = new TextureBuffer("PostDraw2D", 2, "Final");

            TextureQualityChanged();

            base.Initialize();
        }

        public static void Add(IDrawItem item)
        {
            layers.AddDrawItem(item);

            if (item is ObjectComponent)
                ((ObjectComponent)item).AddReference(EngineComponentManager.Find("PostRenderManager") as IHoldReferences);
            else if (item is GameObject)
                ((GameObject)item).AddReference(EngineComponentManager.Find("PostRenderManager") as IHoldReferences);
        }

        public void Remove(object obj)
        {
            layers.Remove(obj as IDrawItem);
        }

        public static void Remove(IDrawItem item)
        {
            layers.Remove(item);
        }

        public void PostRender()
        {
            if (layers.Layers > 0)
            {
                Common.Device.SetRenderTarget(render);

                Common.Device.Clear(Color.Transparent);

                Common.Batch.Begin();
                layers.Draw(DrawingStage.Colour);
                Common.Batch.End();

                Common.Device.SetRenderTarget(null);

                textureBuffer.SetBuffer("Final", render);
            }
            else
                textureBuffer.SetBuffer("Final", blank);
        }

        public void TextureQualityChanged()
        {
            int width = (int)Common.TextureQuality.X;
            int height = (int)Common.TextureQuality.Y;

            render = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
        }
    }
}
