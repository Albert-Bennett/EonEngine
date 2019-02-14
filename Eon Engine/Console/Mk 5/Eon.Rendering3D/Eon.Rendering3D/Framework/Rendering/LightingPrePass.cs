/* Created: 12/05/2014
 * Last Updated: 10/07/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering.Lighting;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Framework.Rendering
{
    /// <summary>
    /// Defines the lighting model that Eon Engine is using.
    /// </summary>
    internal sealed class LightingPrePass
    {
        #region Fields

        ScreenQuad quad;

        RenderTarget2D depthTarget;
        RenderTarget2D opaqueTarget;
        RenderTarget2D colourTarget;
        RenderTarget2D lightTarget;

        RenderTarget2D composed;

        Effect clearGBuffer;
        Effect compose;

        Effect pointLightEffect;
        Effect directionalLightEffect;
        Effect spotLightEffect;

        Model pointLightModel;
        Model spotLightModel;

        BlendState lightingBlend;

        Texture2D blank;

        #endregion
        #region Ctor/ Init

        public LightingPrePass() { }

        public void Initialize()
        {
            quad = new ScreenQuad();

            lightingBlend = new BlendState()
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaSourceBlend = Blend.One,
                AlphaDestinationBlend = Blend.One,

                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.One,
                ColorDestinationBlend = Blend.One
            };

            clearGBuffer = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/ClearGBuffer");
            compose = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/Compose");

            pointLightEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/PointLight");
            directionalLightEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/DirectionalLight");
            spotLightEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/SpotLight");

            pointLightModel = Common.ContentBuilder.Load<Model>("Eon/Models/PointLightModel");
            spotLightModel = Common.ContentBuilder.Load<Model>("Eon/Models/SpotLightModel");

            blank = Common.ContentBuilder.Load<Texture2D>("Eon/Textures/Blank");
        }
        #endregion
        #region Rendering
        #region Clear/ Render

        public void Render()
        {
            if (ModelManager.ModelCount(RenderTypes.LPP) > 0)
            {
                Common.Device.BlendState = BlendState.Opaque;
                Common.Device.DepthStencilState = DepthStencilState.Default;
                Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;

                ClearGBuffer();
                RenderOpaque();
            }
        }

        void ClearGBuffer()
        {
            Common.Device.DepthStencilState = DepthStencilState.DepthRead;

            Common.Device.SetRenderTargets(opaqueTarget, depthTarget, colourTarget);

            clearGBuffer.CurrentTechnique.Passes[0].Apply();

            Common.Device.DepthStencilState = DepthStencilState.Default;
        }

        void RenderOpaque()
        {
            ModelManager.Render(RenderTypes.LPP, "Opaque");

            Common.Device.SetRenderTargets(null);

            Framework.TextureBuffer.SetBuffer("Opaque", opaqueTarget);
            Framework.TextureBuffer.SetBuffer("DepthMap", depthTarget);
            Framework.TextureBuffer.SetBuffer("Colour", colourTarget);
        }

        #endregion
        #region Light Rendering

        public void RenderLighting()
        {
            RenderLights();
            Compose();
        }

        void RenderLights()
        {
            Matrix viewProj = CameraManager.CurrentCamera.View * 
                CameraManager.CurrentCamera.Projection;

            Matrix invViewProj = Matrix.Invert(viewProj);
            Matrix invView = Matrix.Invert(CameraManager.CurrentCamera.View);

            Common.Device.BlendState = lightingBlend;
            Common.Device.SetRenderTarget(lightTarget);
            Common.Device.Clear(Color.Transparent);

            RenderDirectionalLights(invViewProj);

            Common.Device.RasterizerState = RasterizerState.CullClockwise;

            RenderPointLights(invViewProj, viewProj);
            RenderMeshLights(invViewProj, viewProj);
            RenderSpotLights(invViewProj, viewProj, invView);

            Common.Device.BlendState = BlendState.Opaque;
            Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;
            Common.Device.DepthStencilState = DepthStencilState.Default;

            Common.Device.SetVertexBuffer(null);
            Common.Device.Indices = null;

            Common.Device.SetRenderTargets(null);
        }

        void RenderDirectionalLights(Matrix invViewProj)
        {
            List<Eon.Rendering3D.Framework.Rendering.Lighting.DirectionalLight> lights =
                LightManager.GetLights<Eon.Rendering3D.Framework.Rendering.Lighting.DirectionalLight>();

            List<SunLight> suns = LightManager.GetLights<SunLight>();

            for (int i = 0; i < suns.Count; i++)
                lights.Add(suns[i]);

            if (lights.Count > 0)
            {
                directionalLightEffect.Parameters["IViewProj"].SetValue(invViewProj);

                directionalLightEffect.Parameters["Opaque"].SetValue(Framework.TextureBuffer.GetTexture("Opaque"));
                directionalLightEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));

                directionalLightEffect.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.Position);

                for (int i = 0; i < lights.Count; i++)
                {
                    directionalLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                    directionalLightEffect.Parameters["Direction"].SetValue(lights[i].Direction);
                    directionalLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                    if (lights[i] is SunLight)
                        directionalLightEffect.CurrentTechnique = directionalLightEffect.Techniques["SunLighting"];
                    else
                        directionalLightEffect.CurrentTechnique = directionalLightEffect.Techniques["SpotLighting"];

                    directionalLightEffect.CurrentTechnique.Passes[0].Apply();

                    quad.Render();
                }
            }
        }

        void RenderPointLights(Matrix invViewProj, Matrix viewProj)
        {
            List<PointLight> lights = LightManager.GetLights<PointLight>();

            if (lights.Count > 0)
            {
                pointLightEffect.Parameters["IViewProj"].SetValue(invViewProj);
                pointLightEffect.Parameters["ViewProj"].SetValue(viewProj);

                pointLightEffect.Parameters["Opaque"].SetValue(Framework.TextureBuffer.GetTexture("Opaque"));
                pointLightEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));

                Common.Device.SetVertexBuffer(pointLightModel.Meshes[0].MeshParts[0].VertexBuffer);
                Common.Device.Indices = pointLightModel.Meshes[0].MeshParts[0].IndexBuffer;

                for (int i = 0; i < lights.Count; i++)
                {
                    pointLightEffect.Parameters["World"].SetValue(lights[i].World.Matrix);
                    pointLightEffect.Parameters["Pos"].SetValue(lights[i].World.Position);
                    pointLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                    pointLightEffect.Parameters["Radius"].SetValue(lights[i].World.Size.X);
                    pointLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                    pointLightEffect.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                        pointLightModel.Meshes[0].MeshParts[0].NumVertices,
                        pointLightModel.Meshes[0].MeshParts[0].StartIndex,
                        pointLightModel.Meshes[0].MeshParts[0].PrimitiveCount);
                }
            }
        }

        void RenderMeshLights(Matrix invViewProj, Matrix viewProj)
        {
            List<MeshLight> lights = LightManager.GetLights<MeshLight>();

            if (lights.Count > 0)
            {
                pointLightEffect.Parameters["IViewProj"].SetValue(invViewProj);
                pointLightEffect.Parameters["ViewProj"].SetValue(viewProj);

                pointLightEffect.Parameters["Opaque"].SetValue(Framework.TextureBuffer.GetTexture("Opaque"));
                pointLightEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));

                for (int i = 0; i < lights.Count; i++)
                    for (int j = 0; j < lights[i].MeshVolume.Meshes.Count; j++)
                        for (int k = 0; k < lights[i].MeshVolume.Meshes[j].MeshParts.Count; k++)
                        {
                            Common.Device.SetVertexBuffer(lights[i].MeshVolume.Meshes[j].MeshParts[k].VertexBuffer);
                            Common.Device.Indices = lights[i].MeshVolume.Meshes[j].MeshParts[k].IndexBuffer;

                            pointLightEffect.Parameters["World"].SetValue(lights[i].World.Matrix);
                            pointLightEffect.Parameters["Pos"].SetValue(lights[i].World.Position);
                            pointLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                            pointLightEffect.Parameters["Radius"].SetValue(lights[i].World.Size.X);
                            pointLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                            pointLightEffect.CurrentTechnique.Passes[0].Apply();

                            Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                                lights[i].MeshVolume.Meshes[j].MeshParts[k].NumVertices,
                                lights[i].MeshVolume.Meshes[j].MeshParts[k].StartIndex,
                                lights[i].MeshVolume.Meshes[j].MeshParts[k].PrimitiveCount);
                        }
            }
        }

        void RenderSpotLights(Matrix invViewProj, Matrix viewProj, Matrix invView)
        {
            List<SpotLight> lights = LightManager.GetLights<SpotLight>();

            if (lights.Count > 0)
            {
                spotLightEffect.Parameters["IViewProj"].SetValue(invViewProj);
                spotLightEffect.Parameters["IView"].SetValue(invView);
                spotLightEffect.Parameters["ViewProj"].SetValue(viewProj);

                spotLightEffect.Parameters["Opaque"].SetValue(Framework.TextureBuffer.GetTexture("Opaque"));
                spotLightEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));

                Common.Device.SetVertexBuffer(spotLightModel.Meshes[0].MeshParts[0].VertexBuffer);
                Common.Device.Indices = spotLightModel.Meshes[0].MeshParts[0].IndexBuffer;

                for (int i = 0; i < lights.Count; i++)
                {
                    spotLightEffect.Parameters["World"].SetValue(lights[i].World.Matrix);
                    spotLightEffect.Parameters["Pos"].SetValue(lights[i].World.Position);

                    spotLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                    spotLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                    spotLightEffect.Parameters["Direction"].SetValue(lights[i].Direction);
                    spotLightEffect.Parameters["FallOff"].SetValue(lights[i].FallOff);
                    spotLightEffect.Parameters["OuterConeAngle"].SetValue(lights[i].OuterConeAngle);
                    spotLightEffect.Parameters["InnerConeAngle"].SetValue(lights[i].InnerConeAngle);

                    spotLightEffect.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                        spotLightModel.Meshes[0].MeshParts[0].NumVertices,
                        spotLightModel.Meshes[0].MeshParts[0].StartIndex,
                        spotLightModel.Meshes[0].MeshParts[0].PrimitiveCount);

                    quad.Render();
                }
            }
        }

        #endregion
        #region Composition

        void Compose()
        {
            Common.Device.BlendState = BlendState.Opaque;

            Common.Device.SetRenderTargets(composed);
            Common.Device.Clear(Color.Transparent);

            compose.Parameters["ColourMap"].SetValue(Framework.TextureBuffer.GetTexture("Colour"));
            compose.Parameters["LightMap"].SetValue(lightTarget);

            Texture2D shadowMap = Framework.TextureBuffer.GetTexture("Shadows");

            if (shadowMap != null)
                compose.CurrentTechnique = compose.Techniques["CombineShadows"];
            else
                compose.CurrentTechnique = compose.Techniques["Combine"];

            compose.Parameters["ShadowMap"].SetValue(shadowMap);

            compose.CurrentTechnique.Passes[0].Apply();
            quad.Render();

            Common.Device.SetRenderTargets(null);

            Framework.TextureBuffer.SetBuffer("Scene", composed);
        }

        #endregion
        #endregion
        #region Misc

        public void TextureQualityChanged(int width, int height, Vector2 halfPixel)
        {
            pointLightEffect.Parameters["HalfPixel"].SetValue(halfPixel);
            spotLightEffect.Parameters["HalfPixel"].SetValue(halfPixel);
            directionalLightEffect.Parameters["HalfPixel"].SetValue(halfPixel);

            compose.Parameters["HalfPixel"].SetValue(halfPixel);

            depthTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            opaqueTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);

            colourTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            lightTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            composed = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.HalfVector4, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
        }

        #endregion
    }
}
