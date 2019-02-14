/* Created 08/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D.Drawing
{
    public class PostRenderManager : EngineComponent, IPostRenderComponent, IHoldReferences
    {
        static DrawLayers layers = new DrawLayers();
        RenderTarget2D render;

        Texture2D blank;
        Texture2D final;

        public static int MaximumLayer
        {
            get { return layers.MaximumLayerDepth; }
        }

        public int Priority
        {
            get { return 5; }
        }

        public Texture2D BlankTexture
        {
            get { return blank; }
        }

        public Texture2D FinalImage
        {
            get { return final; }
        }

        public PostRenderManager() : base("PostRenderManager") { }

        protected override void Initialize()
        {
            blank = new Texture2D(Common.Device, 1, 1);
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

                final = render;
            }
            else
                final = blank;
        }

        public void TextureQualityChanged()
        {
            render = new RenderTarget2D(Common.Device, (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y, false, SurfaceFormat.Color, DepthFormat.None);
        }
    }
}
