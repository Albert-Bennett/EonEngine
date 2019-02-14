/* Created: 19/06/2014
 * Last Updated: 10/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Framework.Decals;
using Eon.Rendering3D.Framework.Rendering.Shadowing;
using Eon.System.Management;
using Eon.System.Management.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering3D.Framework.Rendering
{
    /// <summary>
    /// Defines a manager class for all active render modules.
    /// </summary>
    internal sealed class RenderManager : EngineModule, IRenderComponent
    {
        static List<RenderPass> preLighting = new List<RenderPass>();
        static List<RenderPass> postLighting = new List<RenderPass>();

        ShadowRenderer shadows;
        LightingPrePass lpp;
        DepthPeeling peeling;
        DecalManager decals;

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
            decals = new DecalManager();
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
                ModelManager.PreRender();

                lpp.Render();
                decals.Render();
                peeling.Render();

                shadows.Render();

                for (int i = 0; i < preLighting.Count; i++)
                    preLighting[i]._Render();

                lpp.RenderLighting();

                for (int i = 0; i < postLighting.Count; i++)
                    postLighting[i]._Render();

                if (renderDebug)
                    _RenderDebug();
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

            Common.Batch.Draw(Framework.TextureBuffer.GetTexture("Opaque"), rect, Color.White);

            int width = (int)Common.TextureQuality.X / 5;
            int height = (int)Common.TextureQuality.Y / 5;

            rect = new Rectangle(0, 0, width, height);

            try
            {
                Common.Batch.Draw(Framework.TextureBuffer.GetTexture("Opaque"), rect, Color.White);

                rect.X += width;
                Common.Batch.Draw(Framework.TextureBuffer.GetTexture("ShadowDepths"), rect, Color.White);

                rect.X += width;
                Common.Batch.Draw(Framework.TextureBuffer.GetTexture("Shadows"), rect, Color.White);

                rect.X = 0;
                rect.Y += height;
                Common.Batch.Draw(Framework.TextureBuffer.GetTexture("Colour"), rect, Color.White);

                rect.X += width;
                Common.Batch.Draw(Framework.TextureBuffer.GetTexture("LightMap"), rect, Color.White);
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

        /// <summary>
        /// Adds a render pass to this.
        /// </summary>
        /// <param name="pass">The RenderPass to be added.</param>
        internal static void AddPass(RenderPass pass)
        {
            switch (pass.Phase)
            {
                case RenderPhases.PreLighting:
                    {
                        if (!preLighting.Contains(pass))
                        {
                            preLighting.Add(pass);
                            preLighting = preLighting.OrderBy(p => p.Order).ToList();
                        }
                    }
                    break;

                case RenderPhases.PostLighting:
                    {
                        if (!postLighting.Contains(pass))
                        {
                            postLighting.Add(pass);
                            postLighting = postLighting.OrderBy(p => p.Order).ToList();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Removes a render pass to this.
        /// </summary>
        /// <param name="pass">The RenderPass to be removed.</param>
        internal static void RemovePass(RenderPass pass)
        {
            switch (pass.Phase)
            {
                case RenderPhases.PreLighting:
                    {
                        if (!preLighting.Contains(pass))
                            preLighting.Remove(pass);
                    }
                    break;

                case RenderPhases.PostLighting:
                    {
                        if (!postLighting.Contains(pass))
                            postLighting.Remove(pass);
                    }
                    break;
            }
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
            decals.TextureQualityChanged(width, height, halfPixel);
        }

        #region Testing

        public void ChangeBias(float amount)
        {
            shadows.Bias = amount;
        }

        #endregion
    }
}
