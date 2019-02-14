/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Rendering2D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering2D.Lighting
{
    /// <summary>
    /// Used to define a technique for rendering multiple lights.
    /// </summary>
    public sealed class LPP
    {
        Rectangle renderBounds;

        Texture2D normalTarget;
        Texture2D colourTarget;

        CapturedRender lightingTarget;
        RenderTarget2D final;

        Effect pointLightEffect;
        Effect dominateLightEffect;
        Effect combinedEffect;

        BlendState blendBlack;
        QuadRenderer quadRenderer;

        /// <summary>
        /// The final composed image from this lighting technique.
        /// </summary>
        public Texture2D Final
        {
            get { return final; }
        }

        /// <summary>
        /// Creates a new lighting pre pass technique.
        /// </summary>
        public LPP()
        {
            pointLightEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/2DPointLight");
            dominateLightEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/DominateLight");
            combinedEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/Compose");

            quadRenderer = new QuadRenderer();

            blendBlack = new BlendState()
            {
                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.One,
                ColorDestinationBlend = Blend.One,

                AlphaBlendFunction = BlendFunction.Add,
                AlphaSourceBlend = Blend.SourceAlpha,
                AlphaDestinationBlend = Blend.One
            };
        }

        /// <summary>
        /// Sets the target for this technique to use.
        /// </summary>
        /// <param name="colourMap">Colour map.</param>
        /// <param name="normalMap">Normal map.</param>
        public void SetTargets(Texture2D colourMap, Texture2D normalMap)
        {
            colourTarget = colourMap;
            normalTarget = normalMap;
        }

        /// <summary>
        /// Renders the lighting in the scene. 
        /// </summary>
        public void Draw()
        {
            if (LightingManager.LightCount > 0)
            {
                DrawLights();

                Common.Device.Clear(Color.Black);
                DrawFinalMap();
            }
        }

        void DrawFinalMap()
        {
            combinedEffect.Parameters["ColourMap"].SetValue(colourTarget);
            combinedEffect.Parameters["LightMap"].SetValue(lightingTarget.Texture);
            combinedEffect.CurrentTechnique.Passes[0].Apply();

            Common.Device.SetRenderTarget(final);
            Common.Device.Clear(Color.Black);

            Common.Batch.Begin(SpriteSortMode.Immediate,
                BlendState.AlphaBlend, null, null, null, combinedEffect);
            Common.Batch.Draw(colourTarget, renderBounds, Color.White);
            Common.Batch.End();

            //DrawTesting();

            Common.Device.SetRenderTarget(null);
        }

        void DrawTesting()
        {
            Common.Batch.Begin();

            Rectangle rect = new Rectangle(0, 0, 256, 128);
            Common.Batch.Draw(colourTarget, rect, Color.White);

            rect.X += 256;
            Common.Batch.Draw(normalTarget, rect, Color.White);

            rect.X += 256;
            Common.Batch.Draw(lightingTarget.Texture, rect, Color.White);

            Common.Batch.End();
        }

        void DrawLights()
        {
            lightingTarget.Begin();
            Common.Device.Clear(Color.Transparent);

            Vector2 gdSize = new Vector2(Common.ScreenResolution.Y,
                Common.ScreenResolution.X) * CameraManager.CurrentCamera.InverseZoom;

            DrawPointLights(gdSize);
            DrawDominateLights();

            lightingTarget.End();
        }

        void DrawDominateLights()
        {
            List<DominateLight> lights = null;

            LightingManager.GetLights<DominateLight>(out lights);

            if (lights != null)
                for (int i = 0; i < lights.Count; i++)
                {
                    DominateLight light = lights[i];

                    if (light.Enabled)
                    {
                        quadRenderer.BindBuffer();

                        dominateLightEffect.Parameters["Colour"].SetValue(light.Colour);
                        dominateLightEffect.Parameters["Intensity"].SetValue(light.Intensity);
                        dominateLightEffect.Parameters["Direction"].SetValue(light.Direction);
                        dominateLightEffect.Parameters["SpecPow"].SetValue(light.SpecularPower);

                        dominateLightEffect.Parameters["NormalMap"].SetValue(normalTarget);

                        dominateLightEffect.CurrentTechnique.Passes[0].Apply();

                        Common.Device.BlendState = blendBlack;

                        quadRenderer.Draw();
                    }
                }
        }

        void DrawPointLights(Vector2 gdSize)
        {
            List<PointLight> lights = null;

            LightingManager.GetLights<PointLight>(out lights);

            if (lights != null)
                for (int i = 0; i < lights.Count; i++)
                {
                    PointLight light = lights[i];

                    if (light.Enabled)
                    {
                        quadRenderer.BindBuffer();

                        pointLightEffect.Parameters["Colour"].SetValue(light.Colour);
                        pointLightEffect.Parameters["GDSize"].SetValue(gdSize);
                        pointLightEffect.Parameters["Intensity"].SetValue(light.Intensity);
                        pointLightEffect.Parameters["Radius"].SetValue(light.Radius);
                        pointLightEffect.Parameters["SpecPow"].SetValue(light.SpecularPower);
                        pointLightEffect.Parameters["NormalMap"].SetValue(normalTarget);

                        pointLightEffect.Parameters["Pos"].SetValue(Vector3.Transform(new Vector3(light.Pos.Y, light.Pos.X, 0),
                            Matrix.CreateTranslation(CameraManager.CurrentCamera.Pos.Y,
                            CameraManager.CurrentCamera.Pos.X, 0)));

                        pointLightEffect.CurrentTechnique.Passes[0].Apply();

                        Common.Device.BlendState = blendBlack;

                        quadRenderer.Draw();
                    }
                }
        }

        /// <summary>
        /// Changes the size of the render targets 
        /// relative to the size of the screen.
        /// </summary>
        public void ScreenResolutionChanged()
        {
            renderBounds = new Rectangle(0, 0,
                (int)Common.ScreenResolution.X, (int)Common.ScreenResolution.Y);

            PresentationParameters pp = Common.Device.PresentationParameters;

            final = new RenderTarget2D(Common.Device, Common.Device.Viewport.Width,
                Common.Device.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.None);

            lightingTarget = new CapturedRender(SurfaceFormat.Rgba64,
                DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);
        }
    }
}
