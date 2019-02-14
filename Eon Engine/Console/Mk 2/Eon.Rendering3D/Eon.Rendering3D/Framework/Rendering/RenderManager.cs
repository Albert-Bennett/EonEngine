/* Created 19/06/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Rendering
{
    internal sealed class RenderManager : EngineComponent, IRenderComponent
    {
        LightingPrePass lpp;
        ForwardRendering fr;

        Framework renderFramework;

        Texture2D blank;
        ScreenQuad quad;

        RenderTarget2D composed;
        RenderTarget2D depth;

        Effect compose;

        public bool RenderFinal
        {
            get
            {
                if (!renderFramework.PostProcessingExists())
                {
                    EngineComponent comp = EngineComponentManager.Find("D2RenderFramework");

                    if (comp != null)
                        return false;

                    return true;
                }
                else
                    return false;
            }
        }

        public int Priority
        {
            get { return 0; }
        }

        public Texture2D FinalImage
        {
            get
            {
                //return TextureBuffer.GetTexture("DepthMap");

                if (ModelManager.ModelCount > 0)
                    return composed;
                else
                    return blank;
            }
        }

        public RenderManager()
            : base("RenderManager3D")
        {
            renderFramework = (Framework)EngineComponentManager.Find("Render3DFramework");

            lpp = new LightingPrePass();
            fr = new ForwardRendering();
        }

        protected override void Initialize()
        {
            quad = new ScreenQuad();

            blank = new Texture2D(Common.Device, 8, 8);

            compose = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/3D/Compose");

            lpp.Initialize();

            TextureQualityChanged();

            base.Initialize();
        }

        public void Render()
        {
            if (ModelManager.Count > 0)
            {
                lpp.Render();
                fr.Render();

                Compose();
            }
        }

        void Compose()
        {
            Common.Device.BlendState = BlendState.Opaque;

            Common.Device.SetRenderTargets(composed, depth);
            Common.Device.Clear(Color.TransparentBlack);

            compose.Parameters["DepthBias"].SetValue(0.75f);
            compose.Parameters["ColourMap"].SetValue(TextureBuffer.GetTexture("ColourMap"));
            compose.Parameters["LightMap"].SetValue(TextureBuffer.GetTexture("LightMap"));
            compose.Parameters["DepthMap"].SetValue(TextureBuffer.GetTexture("DepthMap"));

            compose.Parameters["FRDepthMap"].SetValue(TextureBuffer.GetTexture("FRDepthMap"));
            compose.Parameters["FRColourMap"].SetValue(TextureBuffer.GetTexture("FRColourMap"));

            compose.CurrentTechnique.Passes[0].Apply();
            quad.Render();

            RenderDebug();

            Common.Device.SetRenderTargets(null);

            TextureBuffer.SetBuffer("Scene", composed);
            TextureBuffer.SetBuffer("DepthMap", depth);

            TextureBuffer.Remove("FRDepthMap");
            TextureBuffer.Remove("FRColourMap");
        }

        void RenderDebug()
        {
            Common.Batch.Begin();

            int width = (int)Common.DefaultScreenResolution.X / 6;
            int height = (int)Common.DefaultScreenResolution.Y / 6;

            Rectangle rect = new Rectangle(0, 0, width, height);

            Common.Batch.Draw(TextureBuffer.GetTexture("DepthMap"), rect, Color.White);

            rect.X += width;

            Common.Batch.Draw(TextureBuffer.GetTexture("LightMap"), rect, Color.White);

            rect.X += width;

            Common.Batch.Draw(TextureBuffer.GetTexture("ColourMap"), rect, Color.White);

            rect.Y += height;
            rect.X = 0;

            Common.Batch.Draw(TextureBuffer.GetTexture("FRDepthMap"), rect, Color.White);

            rect.X += width;

            Common.Batch.Draw(TextureBuffer.GetTexture("FRColourMap"), rect, Color.White);

            Common.Batch.End();
        }

        public void TextureQualityChanged()
        {
            int width = (int)Common.TextureQuality.X;
            int height = (int)Common.TextureQuality.Y;

            composed = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None);

            depth = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None);

            lpp.TextureQualityChanged(width, height);
            fr.TextureQualityChanged(width, height);
        }
    }
}
