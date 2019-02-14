/* Created 12/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Framework.Rendering.Lighting;
using Eon.Rendering3D.Framework.Rendering.Shadowing;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Framework.Rendering
{
    internal sealed class LightingPrePass
    {
        ScreenQuad quad;
        ShadowRenderer shadowing;

        RenderTarget2D opaqueTarget;
        RenderTarget2D depthTarget;
        RenderTarget2D lightTarget;
        RenderTarget2D colourTarget;
        RenderTarget2D distortionTarget;

        Effect clearGBuffer;

        Effect pointLightEffect;
        Effect directionalLightEffect;
        Effect spotLightEffect;

        Model pointLightModel;
        Model spotLightModel;

        BlendState lightingBlend;

        public LightingPrePass()
        {
            shadowing = new ShadowRenderer();
        }

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

            clearGBuffer = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/ClearGBuffer");

            pointLightEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/3D/PointLight");
            directionalLightEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/3D/DirectionalLight");
            spotLightEffect = Common.ContentManager.Load<Effect>("Eon/Shaders/Lighting/3D/SpotLight");

            pointLightModel = Common.ContentManager.Load<Model>("Eon/Models/PointLightModel");
            spotLightModel = Common.ContentManager.Load<Model>("Eon/Models/SpotLightModel");
        }

        public void Render()
        {
            Common.Device.BlendState = BlendState.Opaque;
            Common.Device.DepthStencilState = DepthStencilState.Default;
            Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;

            ClearGBuffer();

            RenderOpaque();

            shadowing.Render();

            RenderLights();
            RenderColour();
        }

        void ClearGBuffer()
        {
            Common.Device.DepthStencilState = DepthStencilState.DepthRead;

            Common.Device.SetRenderTargets(opaqueTarget,
                depthTarget, distortionTarget);

            clearGBuffer.CurrentTechnique.Passes[0].Apply();

            Common.Device.DepthStencilState = DepthStencilState.Default;
        }

        void RenderOpaque()
        {
            ModelManager.Render(RenderTypes.LightingPrePass, "Opaque");

            Common.Device.SetRenderTargets(null);

            TextureBuffer.SetBuffer("DepthMap", depthTarget);
            TextureBuffer.SetBuffer("OpaqueMap", opaqueTarget);
            TextureBuffer.SetBuffer("DistortionMap", distortionTarget);
        }

        void RenderLights()
        {
            Common.Device.BlendState = lightingBlend;

            Common.Device.SetRenderTarget(lightTarget);

            Common.Device.Clear(Color.Transparent);

            Matrix invView = Matrix.Invert(CameraManager.CurrentCamera.View);
            Matrix viewProj = CameraManager.CurrentCamera.View * CameraManager.CurrentCamera.Projection;
            Matrix invViewProj = Matrix.Invert(viewProj);

            RenderDirectionalLights(invView, invViewProj);

            Common.Device.RasterizerState = RasterizerState.CullClockwise;

            RenderPointLights(invView, invViewProj, viewProj);
            RenderMeshLights(invView, invViewProj, viewProj);
            RenderSpotLights(invView, invViewProj, viewProj);

            Common.Device.BlendState = BlendState.Opaque;
            Common.Device.RasterizerState = RasterizerState.CullCounterClockwise;
            Common.Device.DepthStencilState = DepthStencilState.Default;

            Common.Device.SetRenderTargets(null);

            TextureBuffer.SetBuffer("LightMap", lightTarget);
        }

        void RenderDirectionalLights(Matrix invView, Matrix invViewProj)
        {
            List<Eon.Rendering3D.Framework.Rendering.Lighting.DirectionalLight> lights =
                LightManager.GetLights<Eon.Rendering3D.Framework.Rendering.Lighting.DirectionalLight>();

            if (lights.Count > 0)
            {
                directionalLightEffect.Parameters["IView"].SetValue(invView);
                directionalLightEffect.Parameters["IViewProj"].SetValue(invViewProj);

                directionalLightEffect.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.Position);
                directionalLightEffect.Parameters["GDSize"].SetValue(Common.TextureQuality);

                directionalLightEffect.Parameters["Opaque"].SetValue(opaqueTarget);
                directionalLightEffect.Parameters["DepthMap"].SetValue(depthTarget);

                for (int i = 0; i < lights.Count; i++)
                {
                    directionalLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                    directionalLightEffect.Parameters["Direction"].SetValue(lights[i].Direction);
                    directionalLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                    directionalLightEffect.CurrentTechnique.Passes[0].Apply();

                    quad.Render();
                }
            }
        }

        void RenderPointLights(Matrix invView, Matrix invViewProj, Matrix viewProj)
        {
            List<PointLight> lights = LightManager.GetLights<PointLight>();

            if (lights.Count > 0)
            {
                pointLightEffect.Parameters["IView"].SetValue(invView);
                pointLightEffect.Parameters["IViewProj"].SetValue(invViewProj);
                pointLightEffect.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.Position);
                pointLightEffect.Parameters["GDSize"].SetValue(Common.TextureQuality);

                pointLightEffect.Parameters["ViewProj"].SetValue(viewProj);

                Common.Device.SetVertexBuffer(pointLightModel.Meshes[0].MeshParts[0].VertexBuffer);
                Common.Device.Indices = pointLightModel.Meshes[0].MeshParts[0].IndexBuffer;

                pointLightEffect.Parameters["Opaque"].SetValue(opaqueTarget);
                pointLightEffect.Parameters["DepthMap"].SetValue(depthTarget);

                for (int i = 0; i < lights.Count; i++)
                {
                    pointLightEffect.Parameters["World"].SetValue(lights[i].World);
                    pointLightEffect.Parameters["Pos"].SetValue(lights[i].World.Translation);
                    pointLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                    pointLightEffect.Parameters["Radius"].SetValue(lights[i].Radius);
                    pointLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                    pointLightEffect.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                        pointLightModel.Meshes[0].MeshParts[0].NumVertices,
                        pointLightModel.Meshes[0].MeshParts[0].StartIndex,
                        pointLightModel.Meshes[0].MeshParts[0].PrimitiveCount);
                }
            }
        }

        void RenderMeshLights(Matrix invView, Matrix invViewProj, Matrix viewProj)
        {
            List<MeshLight> lights = LightManager.GetLights<MeshLight>();

            if (lights.Count > 0)
            {
                pointLightEffect.Parameters["IView"].SetValue(invView);
                pointLightEffect.Parameters["IViewProj"].SetValue(invViewProj);
                pointLightEffect.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.Position);
                pointLightEffect.Parameters["GDSize"].SetValue(Common.TextureQuality);

                pointLightEffect.Parameters["ViewProj"].SetValue(viewProj);

                pointLightEffect.Parameters["Opaque"].SetValue(opaqueTarget);
                pointLightEffect.Parameters["DepthMap"].SetValue(depthTarget);

                for (int i = 0; i < lights.Count; i++)
                    for (int j = 0; j < lights[i].MeshVolume.Meshes.Count; j++)
                        for (int k = 0; k < lights[i].MeshVolume.Meshes[j].MeshParts.Count; k++)
                        {
                            Common.Device.SetVertexBuffer(lights[i].MeshVolume.Meshes[j].MeshParts[k].VertexBuffer);
                            Common.Device.Indices = lights[i].MeshVolume.Meshes[j].MeshParts[k].IndexBuffer;

                            pointLightEffect.Parameters["World"].SetValue(lights[i].World);
                            pointLightEffect.Parameters["Pos"].SetValue(lights[i].World.Translation);
                            pointLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                            pointLightEffect.Parameters["Radius"].SetValue(lights[i].Size);
                            pointLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                            pointLightEffect.CurrentTechnique.Passes[0].Apply();

                            Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                                lights[i].MeshVolume.Meshes[j].MeshParts[k].NumVertices,
                                lights[i].MeshVolume.Meshes[j].MeshParts[k].StartIndex,
                                lights[i].MeshVolume.Meshes[j].MeshParts[k].PrimitiveCount);
                        }
            }
        }

        void RenderSpotLights(Matrix invView, Matrix invViewProj, Matrix viewProj)
        {
            List<SpotLight> lights = LightManager.GetLights<SpotLight>();

            if (lights.Count > 0)
            {
                spotLightEffect.Parameters["IView"].SetValue(invView);
                spotLightEffect.Parameters["IViewProj"].SetValue(invViewProj);
                spotLightEffect.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.Position);
                spotLightEffect.Parameters["GDSize"].SetValue(Common.TextureQuality);

                spotLightEffect.Parameters["ViewProj"].SetValue(viewProj);

                spotLightEffect.Parameters["Opaque"].SetValue(opaqueTarget);
                spotLightEffect.Parameters["DepthMap"].SetValue(depthTarget);

                Common.Device.SetVertexBuffer(spotLightModel.Meshes[0].MeshParts[0].VertexBuffer);
                Common.Device.Indices = spotLightModel.Meshes[0].MeshParts[0].IndexBuffer;

                for (int i = 0; i < lights.Count; i++)
                {
                    spotLightEffect.Parameters["World"].SetValue(lights[i].World);
                    spotLightEffect.Parameters["Pos"].SetValue(lights[i].Position);

                    spotLightEffect.Parameters["Colour"].SetValue(lights[i].Colour);
                    spotLightEffect.Parameters["Intensity"].SetValue(lights[i].Intensity);

                    spotLightEffect.Parameters["Direction"].SetValue(lights[i].Direction);
                    spotLightEffect.Parameters["FallOff"].SetValue(lights[i].FallOff);
                    spotLightEffect.Parameters["OuterConeAngle"].SetValue(lights[i].OuterConeAngle);
                    spotLightEffect.Parameters["InnerConeAngle"].SetValue(lights[i].InnerConeAngle);

                    //Shadowing
                    //{
                    //    spotLightEffect.Parameters["ShadowMap"].SetValue(TextureBuffer.GetTexture("Shadows"));
                    //    spotLightEffect.Parameters["DepthBias"].SetValue(ShadowRenderer.DepthBias);

                    //    spotLightEffect.Parameters["LightViewProj"].SetValue(
                    //      Matrix.Invert(  lights[i].View * lights[i].Proj));
                    //}

                    spotLightEffect.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                        spotLightModel.Meshes[0].MeshParts[0].NumVertices,
                        spotLightModel.Meshes[0].MeshParts[0].StartIndex,
                        spotLightModel.Meshes[0].MeshParts[0].PrimitiveCount);
                }
            }
        }

        void RenderColour()
        {
            Common.Device.SetRenderTargets(colourTarget);
            Common.Device.Clear(Color.Black);

            ModelManager.Render(RenderTypes.LightingPrePass, "Render");

            Common.Device.SetRenderTargets(null);

            TextureBuffer.SetBuffer("ColourMap", colourTarget);
        }

        public void TextureQualityChanged(int width, int height)
        {
            opaqueTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.HalfVector4, DepthFormat.Depth24);

            depthTarget = new RenderTarget2D(Common.Device, width, height,
                 false, SurfaceFormat.HalfVector4, DepthFormat.None);

            lightTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.None);

            distortionTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.HalfVector4, DepthFormat.None);

            colourTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.Color, DepthFormat.Depth24);

            shadowing.TextureQualityChanged();
        }
    }
}
