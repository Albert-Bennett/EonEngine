/* Created: 29/03/2015
 * Last Updated: 30/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering3D.Framework.Billboards
{
    /// <summary>
    /// A manager class for Billboard objects.
    /// </summary>
    internal sealed class BillboardManager
    {
        static List<IBillboard> billboards = new List<IBillboard>();
        List<IBillboard> currentRenders = new List<IBillboard>();

        int billboardCount = 0;
        int aniBillboardCount = 0;
        int laBillboardCount = 0;

        static float alphaBias = 1.0f;
        Vector2 halfPixel;

        short[] indices = new short[6];
        VertexPositionTexture[] vertices = new VertexPositionTexture[4];

        IndexBuffer ib;
        VertexBuffer vb;

        BoundingSphere bounds = new BoundingSphere();

        Effect billboardEffect;
        Effect aniBillboardEffect;
        Effect laBillboardEffect;

        bool preRendered = false;

        //The amount of bias allowed for alpha.
        public static float AlphaBias
        {
            get { return alphaBias; }
            set { alphaBias = value; }
        }

        internal int Count
        {
            get { return billboards.Count; }
        }

        /// <summary>
        /// Creates a new BillboardManager.
        /// </summary>
        public BillboardManager()
        {
            billboardEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Materials/Billboard");
            aniBillboardEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Materials/AnimatedBillboard");
            laBillboardEffect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/Materials/LABillboard");

            Assemble();
        }

        void Assemble()
        {
            vertices[0] = new VertexPositionTexture(Vector3.Zero, new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(Vector3.Zero, new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(Vector3.Zero, new Vector2(1, 1));
            vertices[3] = new VertexPositionTexture(Vector3.Zero, new Vector2(0, 1));

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            vb = new VertexBuffer(Common.Device,
                typeof(VertexPositionTexture), vertices.Length,
                BufferUsage.WriteOnly);

            vb.SetData<VertexPositionTexture>(vertices);

            ib = new IndexBuffer(Common.Device, IndexElementSize.SixteenBits,
                indices.Length, BufferUsage.WriteOnly);

            ib.SetData<short>(indices);
        }

        public static void Add(IBillboard billboard)
        {
            billboards.Add(billboard);
        }

        public void Render(float minDepth, float maxDepth)
        {
            if (!preRendered)
                PreRender();

            if (currentRenders.Count > 0)
                RenderBillboards(minDepth, maxDepth);
        }

        void PreRender()
        {
            currentRenders.Clear();

            for (int i = 0; i < billboards.Count; i++)
                if(Vector3.Dot(billboards[i].Position, CameraManager.CurrentCamera.Direction) > 0)
                    if (CameraManager.CurrentCamera.GetLODLevel(billboards[i].Position) == LODLevels.Zero)
                    {
                        bounds.Center = billboards[i].Position;
                        bounds.Radius = billboards[i].Scale;

                        if (CameraManager.CurrentCamera.IsInView(bounds))
                        {
                            currentRenders.Add(billboards[i]);

                            if (billboards[i] is AnimatedBillboard)
                                aniBillboardCount++;
                            else if (billboards[i] is LockedAxisBillboard)
                                laBillboardCount++;
                            else if (billboards[i] is Billboard)
                                billboardCount++;
                        }
                    }

            if (billboardCount > 0)
            {
                billboardEffect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
                billboardEffect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

                billboardEffect.Parameters["AlphaBias"].SetValue(AlphaBias);

                billboardEffect.Parameters["Up"].SetValue(new Vector3(0, 1, 0));
                billboardEffect.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.Position);

                billboardEffect.Parameters["HalfPixel"].SetValue(halfPixel);
                billboardEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));
            }

            if (aniBillboardCount > 0)
            {
                aniBillboardEffect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
                aniBillboardEffect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

                aniBillboardEffect.Parameters["AlphaBias"].SetValue(AlphaBias);

                aniBillboardEffect.Parameters["Up"].SetValue(new Vector3(0, 1, 0));
                aniBillboardEffect.Parameters["CamPos"].SetValue(CameraManager.CurrentCamera.Position);

                aniBillboardEffect.Parameters["HalfPixel"].SetValue(halfPixel);
                aniBillboardEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));
            }

            if (laBillboardCount > 0)
            {
                laBillboardEffect.Parameters["View"].SetValue(CameraManager.CurrentCamera.View);
                laBillboardEffect.Parameters["Proj"].SetValue(CameraManager.CurrentCamera.Projection);

                laBillboardEffect.Parameters["AlphaBias"].SetValue(AlphaBias);

                laBillboardEffect.Parameters["HalfPixel"].SetValue(halfPixel);
                laBillboardEffect.Parameters["DepthMap"].SetValue(Framework.TextureBuffer.GetTexture("DepthMap"));
            }

            preRendered = true;
        }

        void RenderBillboards(float minDepth, float maxDepth)
        {
            RasterizerState rs = Common.Device.RasterizerState;
            Common.Device.RasterizerState = new RasterizerState()
            {
                CullMode = CullMode.None
            };

            Common.Device.SetVertexBuffer(vb);
            Common.Device.Indices = ib;

            if (billboardCount > 0)
            {
                billboardEffect.Parameters["MinDepth"].SetValue(minDepth);
                billboardEffect.Parameters["MaxDepth"].SetValue(maxDepth);
            }

            if (laBillboardCount > 0)
            {
                laBillboardEffect.Parameters["MinDepth"].SetValue(minDepth);
                laBillboardEffect.Parameters["MaxDepth"].SetValue(maxDepth);
            }

            if (aniBillboardCount > 0)
            {
                aniBillboardEffect.Parameters["MinDepth"].SetValue(minDepth);
                aniBillboardEffect.Parameters["MaxDepth"].SetValue(maxDepth);
            }

            for (int i = 0; i < currentRenders.Count; i++)
            {
                if (currentRenders[i] is LockedAxisBillboard)
                {
                    laBillboardEffect.Parameters["Scale"].SetValue(currentRenders[i].Scale);
                    laBillboardEffect.Parameters["Pos"].SetValue(currentRenders[i].Position);
                    laBillboardEffect.Parameters["World"].SetValue(currentRenders[i].World);

                    laBillboardEffect.Parameters["Axis"].SetValue(
                        ((LockedAxisBillboard)currentRenders[i]).Axis);

                    laBillboardEffect.Parameters["Up"].SetValue(
                         ((LockedAxisBillboard)currentRenders[i]).Up);

                    laBillboardEffect.Parameters["Texture"].SetValue(currentRenders[i].Texture);

                    laBillboardEffect.CurrentTechnique.Passes[0].Apply();
                }
                else if (currentRenders[i] is AnimatedBillboard)
                {
                    aniBillboardEffect.Parameters["Scale"].SetValue(currentRenders[i].Scale);
                    aniBillboardEffect.Parameters["Pos"].SetValue(currentRenders[i].Position);
                    aniBillboardEffect.Parameters["World"].SetValue(currentRenders[i].World);

                    aniBillboardEffect.Parameters["Rot"].SetValue(
                        ((AnimatedBillboard)currentRenders[i]).Rotation);

                    aniBillboardEffect.Parameters["Texture"].SetValue(currentRenders[i].Texture);

                    aniBillboardEffect.Parameters["Width"].SetValue(
                        ((AnimatedBillboard)currentRenders[i]).Width);

                    aniBillboardEffect.Parameters["Height"].SetValue(
                        ((AnimatedBillboard)currentRenders[i]).Height);

                    aniBillboardEffect.Parameters["Row"].SetValue(
                        ((AnimatedBillboard)currentRenders[i]).CurrentRow);

                    aniBillboardEffect.Parameters["Col"].SetValue(
                        ((AnimatedBillboard)currentRenders[i]).CurrentColumn);

                    aniBillboardEffect.Parameters["Cols"].SetValue(
                        ((AnimatedBillboard)currentRenders[i]).Columns);

                    aniBillboardEffect.Parameters["Rows"].SetValue(
                        ((AnimatedBillboard)currentRenders[i]).Rows);

                    aniBillboardEffect.CurrentTechnique.Passes[0].Apply();

                    ((AnimatedBillboard)currentRenders[i]).Update();
                }
                else if (currentRenders[i] is Billboard)
                {
                    billboardEffect.Parameters["Scale"].SetValue(currentRenders[i].Scale);
                    billboardEffect.Parameters["Pos"].SetValue(currentRenders[i].Position);
                    billboardEffect.Parameters["World"].SetValue(currentRenders[i].World);

                    billboardEffect.Parameters["Rot"].SetValue(
                        ((Billboard)currentRenders[i]).Rotation);

                    billboardEffect.Parameters["Texture"].SetValue(currentRenders[i].Texture);

                    billboardEffect.CurrentTechnique.Passes[0].Apply();
                }

                Common.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                    0, 0, vertices.Length, 0, 2);
            }

            Common.Device.SetVertexBuffer(null);
            Common.Device.Indices = null;

            Common.Device.RasterizerState = rs;
        }

        public void EndRender()
        {
            billboardCount = 0;
            aniBillboardCount = 0;
            laBillboardCount = 0;

            preRendered = false;
        }

        public static void Destroy(IBillboard billboard)
        {
            billboards.Remove(billboard);
        }

        public void TextureQualityChanged(Vector2 halfPixel)
        {
            this.halfPixel = halfPixel;
        }
    }
}


