/* Created: 05/10/2014
 * Last Updated: 12/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Framework.Rendering.Lighting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Rendering.Shadowing
{
    /// <summary>
    /// Used to define the shadows created by a single light source.
    /// </summary>
    internal sealed class ShadowEntry
    {
        ICastShadows owner;

        RenderTarget2D shadowMap;
        RenderTarget2D occlusion;

        BoundingFrustum frustum;
        Vector3[] farCorners = new Vector3[4];

        static Vector2 shadowSize;

        bool isUsed;

        public RenderTarget2D ShadowMap { get { return shadowMap; } }
        public RenderTarget2D Occlusion { get { return occlusion; } }

        public Matrix[] SplitViewProjection { get { return owner.SplitViewProjection; } }

        public Vector3[] FarCorners { get { return farCorners; } }

        public static Vector2 ShadowSize { get { return shadowSize; } }

        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }

        public ShadowEntry(ICastShadows owner)
        {
            this.owner = owner;
            isUsed = true;

            ScreenResolutionChanged();

            ShadowRenderer.Add(this);
        }

        internal void CalculateFrustum(float[] splitDepths)
        {
            owner.CalcFrustum(splitDepths);
            frustum = new BoundingFrustum(owner.ViewProjection);

            Vector3[] corners = frustum.GetCorners();

            for (int i = 0; i < 4; i++)
                farCorners[i] = corners[i + 4];
        }

        public bool Shadows(BoundingSphere sphere)
        {
            return frustum.Contains(sphere) != ContainmentType.Disjoint;
        }

        public void ScreenResolutionChanged()
        {
            int width = (int)Common.TextureQuality.X / 4;
            int height = (int)Common.TextureQuality.Y / 4;

            shadowMap = new RenderTarget2D(Common.Device, width, height, false,
                SurfaceFormat.Single, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            occlusion = new RenderTarget2D(Common.Device, width, height, false,
                SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            shadowSize = new Vector2(width, height);
        }

        public void Dispose()
        {
            if (shadowMap != null)
            {
                shadowMap.Dispose();
                shadowMap = null;
            }

            if (occlusion != null)
            {
                occlusion.Dispose();
                occlusion = null;
            }

            ShadowRenderer.Remove(this);
        }
    }
}
