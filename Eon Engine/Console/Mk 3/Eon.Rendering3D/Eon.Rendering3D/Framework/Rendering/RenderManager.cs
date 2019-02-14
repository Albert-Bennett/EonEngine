/* Created: 19/06/2014
 * Last Updated: 07/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Framework.Rendering.Shadowing;
using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Rendering
{
    /// <summary>
    /// Defines a manager class for all active render modules.
    /// </summary>
    internal sealed class RenderManager : EngineComponent, IRenderComponent
    {
        ShadowRenderer shadows;
        LightingPrePass lpp;
        DepthPeeling peeling;

        Texture2D blank;
        RenderTarget2D debug;

        bool renderDebug = false;

        public int Order
        {
            get { return 0; }
        }

        public RenderManager()
            : base("RenderManager3D")
        {
            lpp = new LightingPrePass();
            shadows = new ShadowRenderer();
            peeling = new DepthPeeling();
        }

        protected override void Initialize()
        {
            blank = new Texture2D(Common.Device, 8, 8);

            lpp.Initialize();

            TextureQualityChanged();

            base.Initialize();
        }

        public void Render()
        {
            if (ModelManager.Count > 0)
            {
                if (ModelManager.ModelCount(RenderTypes.LPP) > 0)
                {
                    lpp.RenderOpaque();
                    peeling.Render();
                    shadows.Render();

                    lpp.RenderLighting();

                    if (renderDebug)
                        _RenderDebug();
                }
            }
            else
            {
                Framework.TextureBuffer.SetBuffer("DepthMap", blank);
                Framework.TextureBuffer.SetBuffer("Scene", blank);
            }
        }

        public void RenderDebug()
        {
            renderDebug = !renderDebug;
        }

        void _RenderDebug()
        {
            Common.Device.SetRenderTarget(debug);

            Common.Batch.Begin();

            Rectangle rect = new Rectangle(0, 0,
                (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y);

            Common.Batch.Draw(Framework.TextureBuffer.GetTexture("Scene"), rect, Color.White);

            int width = (int)Common.TextureQuality.X / 5;
            int height = (int)Common.TextureQuality.Y / 5;

            rect = new Rectangle(0, 0, width, height);

            try
            {
                Common.Batch.Draw(Framework.TextureBuffer.GetTexture("DepthMap"), rect, Color.White);

                rect.X += width;
                Common.Batch.Draw(Framework.TextureBuffer.GetTexture("ShadowDepths"), rect, Color.White);

                rect.X += width;
                Common.Batch.Draw(Framework.TextureBuffer.GetTexture("Shadows"), rect, Color.White);

                rect.X = 0;
                rect.Y += height;
                Common.Batch.Draw(Framework.TextureBuffer.GetTexture("OpaqueMap"), rect, Color.White);
            }
            catch
            {
                Common.Batch.DrawString(Common.ContentBuilder.Load<SpriteFont>("Eon/Fonts/Arial12"),
                    "No Data", new Vector2(rect.X + (width / 2), rect.Y + 10), Color.White);
            }

            Common.Batch.End();

            Common.Device.SetRenderTarget(null);

            Framework.TextureBuffer.SetBuffer("Scene", debug);
        }

        public void TextureQualityChanged()
        {
            int width = (int)Common.TextureQuality.X;
            int height = (int)Common.TextureQuality.Y;

            debug = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            Vector2 halfPixel = Vector2.One / Common.TextureQuality;

            lpp.TextureQualityChanged(width, height, halfPixel);
            peeling.TextureQualityChanged(width, height, halfPixel);
            shadows.TextureQualityChanged();
        }

        #region Testing

        public void ChangeBias(float amount)
        {
            shadows.Bias = amount;
        }

        #endregion
    }
}
