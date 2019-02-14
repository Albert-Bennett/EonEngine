/* Created 08/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.EngineComponents.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D.Drawing
{
    public class PostRenderManager : EngineComponent, IPostRenderComponent
    {
        static DrawLayers layers = new DrawLayers();
        CapturedRender render;

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
            blank = Common.ContentManager.Load<Texture2D>("Eon/Textures/DefaultNormalMap");
            ScreenResolutionChanged();

            base.Initialize();
        }

        public static void Add(IDrawItem item)
        {
            layers.AddDrawItem(item);
        }

        public static void Remove(IDrawItem item)
        {
            layers.Remove(item);
        }

        public void PostRender()
        {
            if (layers.Layers > 0)
            {
                render.Begin();

                Common.Device.Clear(Color.Transparent);

                Common.Batch.Begin();
                layers.Draw(DrawingStage.Colour);
                Common.Batch.End();
                render.End();

                final = render.Texture;
            }
            else
                final = blank;
        }

        public void ScreenResolutionChanged()
        {
            render = new CapturedRender();
        }
    }
}
