/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Cameras;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering2D.Lighting
{
    /// <summary>
    /// Used to define a technique for rendering multiple lights.
    /// </summary>
    public sealed class LPP2D
    {
        Rectangle renderBounds;

        RenderTarget2D lightingTarget;
        RenderTarget2D final;

        Effect pointLightEffect;
        Effect dominateLightEffect;
        Effect combinedEffect;

        BlendState blendBlack;
        ScreenQuad ScreenQuad;

        bool created = true;

        /// <summary>
        /// The final composed image from this lighting technique.
        /// </summary>
        public Texture2D Final
        {
            get { return final; }
        }

        internal bool Created
        {
            get { return created; }
        }

        /// <summary>
        /// Creates a new lighting pre pass technique.
        /// </summary>
        public LPP2D()
        {
            try
            {
                pointLightEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/2D/2DPointLight");
            }
            catch
            {
                created = false;
            }

            try
            {
                dominateLightEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/2D/DominateLight");
            }
            catch
            {
                created = false;
            }

            try
            {
                combinedEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/2D/Compose");
            }
            catch
            {
                created = false;
            }

            ScreenQuad = new ScreenQuad();

            blendBlack = new BlendState()
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaSourceBlend = Blend.SourceAlpha,
                AlphaDestinationBlend = Blend.One,

                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.One,
                ColorDestinationBlend = Blend.One
            };

            renderBounds = new Rectangle(0, 0,
                (int)Common.DefaultScreenResolution.X, (int)Common.DefaultScreenResolution.Y);
        }

        /// <summary>
        /// Renders the lighting in the scene. 
        /// </summary>
        public void Draw()
        {
            if (LightingManager2D.LightCount > 0)
            {
                DrawLights();

                Common.Device.Clear(Color.Black);
                DrawFinalMap();
            }
        }

        void DrawFinalMap()
        {
            combinedEffect.Parameters["ColourMap"].SetValue(TextureBuffer.GetTexture("ColourMap"));
            combinedEffect.Parameters["LightMap"].SetValue(lightingTarget);
            combinedEffect.CurrentTechnique.Passes[0].Apply();

            Common.Device.SetRenderTarget(final);
            Common.Device.Clear(Color.Black);

            Common.Batch.Begin(SpriteSortMode.Immediate,
                BlendState.AlphaBlend, null, null, null, combinedEffect);
            Common.Batch.Draw(TextureBuffer.GetTexture("ColourMap"), renderBounds, Color.White);
            Common.Batch.End();

            //DrawTesting();

            Common.Device.SetRenderTarget(null);
        }

        void DrawTesting()
        {
            Common.Batch.Begin();

            Rectangle rect = new Rectangle(0, 0, 256, 128);
            Common.Batch.Draw(TextureBuffer.GetTexture("ColourMap"), rect, Color.White);

            rect.X += 256;
            Common.Batch.Draw(TextureBuffer.GetTexture("OpaqueMap"), rect, Color.White);

            rect.X += 256;
            Common.Batch.Draw(lightingTarget, rect, Color.White);

            Common.Batch.End();
        }

        void DrawLights()
        {
            Common.Device.SetRenderTarget(lightingTarget);
            Common.Device.Clear(Color.Transparent);

            Vector2 gdSize = Common.TextureQuality * CameraManager2D.CurrentCamera.InverseZoom;

            DrawPointLights(gdSize);
            DrawDominateLights();

            Common.Device.SetRenderTarget(null);
        }

        void DrawDominateLights()
        {
            List<DominateLight2D> lights = null;

            LightingManager2D.GetLights<DominateLight2D>(out lights);

            if (lights != null)
                for (int i = 0; i < lights.Count; i++)
                {
                    DominateLight2D light = lights[i];

                    if (light.Enabled)
                    {
                        dominateLightEffect.Parameters["Colour"].SetValue(light.Colour);
                        dominateLightEffect.Parameters["Intensity"].SetValue(light.Intensity);
                        dominateLightEffect.Parameters["Direction"].SetValue(light.Direction);
                        dominateLightEffect.Parameters["SpecPow"].SetValue(light.SpecularPower);

                        dominateLightEffect.Parameters["NormalMap"].SetValue(TextureBuffer.GetTexture("OpaqueMap"));

                        dominateLightEffect.CurrentTechnique.Passes[0].Apply();

                        Common.Device.BlendState = blendBlack;

                        ScreenQuad.Render();
                    }
                }
        }

        void DrawPointLights(Vector2 gdSize)
        {
            List<PointLight2D> lights = null;

            LightingManager2D.GetLights<PointLight2D>(out lights);

            if (lights != null)
                for (int i = 0; i < lights.Count; i++)
                {
                    PointLight2D light = lights[i];

                    if (light.Enabled)
                    {
                        pointLightEffect.Parameters["Colour"].SetValue(light.Colour);
                        pointLightEffect.Parameters["GDSize"].SetValue(gdSize);
                        pointLightEffect.Parameters["Intensity"].SetValue(light.Intensity);
                        pointLightEffect.Parameters["Radius"].SetValue(light.Radius);
                        pointLightEffect.Parameters["SpecPow"].SetValue(light.SpecularPower);
                        pointLightEffect.Parameters["NormalMap"].SetValue(TextureBuffer.GetTexture("OpaqueMap"));

                        pointLightEffect.Parameters["Pos"].SetValue(Vector3.Transform(new Vector3(light.Pos.Y, light.Pos.X, 0),
                            Matrix.CreateTranslation(CameraManager2D.CurrentCamera.Pos.Y,
                            CameraManager2D.CurrentCamera.Pos.X, 0)));

                        pointLightEffect.CurrentTechnique.Passes[0].Apply();

                        Common.Device.BlendState = blendBlack;

                        ScreenQuad.Render();
                    }
                }
        }

        /// <summary>
        /// Changes the size of the render targets 
        /// relative to the size of the screen.
        /// </summary>
        public void TextureQualityChanged()
        {
            final = new RenderTarget2D(Common.Device, (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y, false, SurfaceFormat.Color, DepthFormat.None);

            lightingTarget = new RenderTarget2D(Common.Device, (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y, false, SurfaceFormat.Color, DepthFormat.None);
        }
    }
}
