/* Created: 23/05/2014
 * Last Updated: 31/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// Used to define a fog effect. 
    /// </summary>
    public sealed class FogEffect : PostProcess
    {
        Effect effect;
        RenderTarget2D target;

        float fogStart;
        float fogEnd;
        float fogThickness;

        string currentTechnique;

        Vector3 fogColour;
        Vector2 halfPixel;

        EngineComponent cameraManager;

        /// <summary>
        /// The colour of the FogEffect.
        /// </summary>
        public Vector3 FogColour
        {
            get { return fogColour; }
            set { fogColour = value; }
        }

        /// <summary>
        /// The thickness of the FogEffect.
        /// </summary>
        public float FogThickness
        {
            get { return fogThickness; }
            set { fogThickness = value; }
        }

        /// <summary>
        /// The distance from where the FogEffect will start.
        /// </summary>
        public float FogStart
        {
            get { return fogStart; }
            set { fogStart = value; }
        }

        /// <summary>
        /// The distance from where the FogEffect will end.
        /// </summary>
        public float FogEnd
        {
            get { return fogEnd; }
            set { fogEnd = value; }
        }

        /// <summary>
        /// Creates a new FogEffect.
        /// </summary>
        /// <param name="fogColour">The colour of the fog.</param>
        /// <param name="fogThickness">The thickness of the fog.</param>
        /// <param name="fogStart">The distance from where the fog will start.</param>
        /// <param name="fogEnd">The distance from where the fog will end.</param>
        /// <param name="fogType">The type of fog.</param>
        public FogEffect(Vector3 fogColour, float fogThickness,
            float fogStart, float fogEnd, FogType fogType)
            : base("FogPP", 0, "Render3DFramework")
        {
            try
            {
                cameraManager = (EngineComponent)EngineComponentManager.Find(
                    "Render3DFramework").SendMessage("GetCameraManager", null);
            }
            catch
            {
                cameraManager = null;
            }

            effect = Common.ContentBuilder.Load<Effect>(
                "Eon/Shaders/PostProcessing/Fog");

            currentTechnique = Enum.GetName(typeof(FogType), fogType);

            this.fogColour = fogColour;
            this.fogEnd = fogEnd;
            this.fogStart = fogStart;
            this.fogThickness = fogThickness;
        }

        protected override void Render()
        {
            if (cameraManager != null)
            {
                Common.Device.SetRenderTarget(target);

                ObjectComponent camera = (ObjectComponent)cameraManager.SendMessage("CurrentCamera", null);

                if (camera != null)
                {
                    Matrix view = (Matrix)camera.SendMessage("View", null);
                    Matrix proj = (Matrix)camera.SendMessage("Projection", null);

                    Matrix iView = Matrix.Invert(view * proj);

                    effect.Parameters["IViewProj"].SetValue(iView);
                    effect.Parameters["CamPos"].SetValue((Vector3)camera.SendMessage("Position", null));
                    effect.Parameters["HalfPixel"].SetValue(halfPixel);

                    effect.Parameters["FogStart"].SetValue(fogStart);
                    effect.Parameters["FogEnd"].SetValue(fogEnd);
                    effect.Parameters["FogThickness"].SetValue(fogThickness);
                    effect.Parameters["FogColour"].SetValue(fogColour);

                    effect.Parameters["Scene"].SetValue(TextureBufferManager.GetTexture(
                        TextureBufferID, OutputTextureID));

                    effect.Parameters["DepthMap"].SetValue(TextureBufferManager.GetTexture(
                        TextureBufferID, "DepthMap"));

                    effect.CurrentTechnique.Passes[0].Apply();
                    localPostProcessing.ScreenQuad.Render();

                    Common.Device.SetRenderTarget(null);

                    final = target;

                    base.Render();
                }
            }
            else
                Destroy();
        }

        public override void TextureQualityChanged(int width, int height)
        {
            target = new RenderTarget2D(Common.Device,height, width,
                false, SurfaceFormat.Color, DepthFormat.None);

            halfPixel = Vector2.One / Common.TextureQuality;

            base.TextureQualityChanged(width, height);
        }

        protected override void _Destroy()
        {
            cameraManager = null;

            base._Destroy();
        }
    }
}
