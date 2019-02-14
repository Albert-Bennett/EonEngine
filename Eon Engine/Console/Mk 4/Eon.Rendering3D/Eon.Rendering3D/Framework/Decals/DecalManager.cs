/* Created: 26/03/2015
 * Last Updated: 29/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Framework.Decals
{
    /// <summary>
    /// A manager of Decals.
    /// </summary>
    internal sealed class DecalManager
    {
        static List<IDecal> decals = new List<IDecal>();

        RenderTarget2D colourTarget;
        RenderTarget2D opaqueTarget;

        RenderTarget2D composeColourTarget;
        RenderTarget2D composeOpaqueTarget;

        BlendState transparent;

        Effect decalMaterial;
        Effect composeEffect;
        Model volume;

        ScreenQuad quad;

        Vector2 halfPixel;

        public DecalManager()
        {
            decalMaterial = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/Decals/Decal");
            composeEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Lighting/3D/Decals/Compose");

            volume = Common.ContentBuilder.Load<Model>("Eon/Models/DecalVolume");

            quad = new ScreenQuad();

            transparent = new BlendState
            {
                AlphaBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.One,
                AlphaSourceBlend = Blend.BlendFactor,
                ColorDestinationBlend = Blend.InverseSourceAlpha,
                AlphaDestinationBlend = Blend.InverseSourceAlpha,
            };
        }

        internal static void Add(IDecal decal)
        {
            DecalManager.decals.Add(decal);
        }

        internal void Render()
        {
            if (decals.Count > 0)
            {
                RenderDecals();
                Compose();
            }
        }

        void RenderDecals()
        {
            for (int i = 0; i < decals.Count; i++)
            {
                Common.Device.SetRenderTargets(colourTarget, opaqueTarget);
                Common.Device.BlendState = transparent;
                Common.Device.Clear(Color.Transparent);

                Matrix viewProj = CameraManager.CurrentCamera.View *
                    CameraManager.CurrentCamera.Projection;

                decalMaterial.Parameters["ViewProj"].SetValue(viewProj);
                decalMaterial.Parameters["IViewProj"].SetValue(Matrix.Invert(viewProj));
                decalMaterial.Parameters["HalfPixel"].SetValue(halfPixel);

                decalMaterial.Parameters["DepthMap"].SetValue(
                    Framework.TextureBuffer.GetTexture("DepthMap"));

                Common.Device.SetVertexBuffer(volume.Meshes[0].MeshParts[0].VertexBuffer);
                Common.Device.Indices = volume.Meshes[0].MeshParts[0].IndexBuffer;

                if (CameraManager.CurrentCamera.IsInView(decals[i].Bounds))
                {
                    decalMaterial.Parameters["World"].SetValue(decals[i].World);
                    decalMaterial.Parameters["InvWorld"].SetValue(Matrix.Invert(decals[i].World));
                    decalMaterial.Parameters["Texture"].SetValue(decals[i].Texture);

                    if (decals[i] is SimpleDecal)
                        decalMaterial.CurrentTechnique = decalMaterial.Techniques["SimpleRender"];
                    else if (decals[i] is Decal)
                    {
                        decalMaterial.Parameters["NormalMap"].SetValue(((Decal)decals[i]).NormalMap);
                        decalMaterial.Parameters["SpecularMap"].SetValue(((Decal)decals[i]).SpecularMap);

                        decalMaterial.CurrentTechnique = decalMaterial.Techniques["AdvancedRender"];
                    }

                    decalMaterial.CurrentTechnique.Passes[0].Apply();

                    Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                        volume.Meshes[0].MeshParts[0].NumVertices,
                        volume.Meshes[0].MeshParts[0].StartIndex,
                        volume.Meshes[0].MeshParts[0].PrimitiveCount);
                }

                Common.Device.SetRenderTarget(null);
            }
        }

        void Compose()
        {
            Common.Device.SetRenderTargets(composeColourTarget, composeOpaqueTarget);
            Common.Device.Clear(Color.Transparent);
            Common.Device.BlendState = BlendState.Opaque;

            composeEffect.Parameters["HalfPixel"].SetValue(halfPixel);

            composeEffect.Parameters["Scene"].SetValue(Framework.TextureBuffer.GetTexture("Scene"));
            composeEffect.Parameters["SceneOpaque"].SetValue(Framework.TextureBuffer.GetTexture("OpaqueMap"));

            composeEffect.Parameters["Colour"].SetValue(colourTarget);
            composeEffect.Parameters["Opaque"].SetValue(opaqueTarget);

            composeEffect.CurrentTechnique.Passes[0].Apply();
            quad.Render();

            Common.Device.SetRenderTarget(null);

            Framework.TextureBuffer.SetBuffer("Scene", composeColourTarget);
            Framework.TextureBuffer.SetBuffer("OpaqueMap", composeOpaqueTarget);
        }

        internal static void Destroy(IDecal decal)
        {
            decals.Remove(decal);
        }

        internal void TextureQualityChanged(int width, int height, Vector2 halfPixel)
        {
            composeColourTarget = new RenderTarget2D(Common.Device, width, height,
               false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            composeOpaqueTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.HalfVector4, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            colourTarget = new RenderTarget2D(Common.Device, width, height,
               false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            opaqueTarget = new RenderTarget2D(Common.Device, width, height,
                false, SurfaceFormat.HalfVector4, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            this.halfPixel = halfPixel;
        }
    }
}
