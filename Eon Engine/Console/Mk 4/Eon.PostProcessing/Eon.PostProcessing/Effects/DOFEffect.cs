/* Created: 10/06/2014
 * Last Updated: 02/10/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Management;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.PostProcessing.Effects
{
    /// <summary>
    /// Used to define a depth of field effect.
    /// </summary>
    public sealed class DOFEffect : PostProcess
    {
        float blurStart;
        float blurEnd;
        float blurAmount;

        Effect effect;
        Effect blur;

        Vector2 halfPixel;

        RenderTarget2D target;
        RenderTarget2D blurred;

        EngineModule cameraManager;

        /// <summary>
        /// The distance for when the blurring of 
        /// the scene will begin at.
        /// </summary>
        public float BlurStart
        {
            get { return blurStart; }
            set { blurStart = value; }
        }

        /// <summary>
        /// The distance for when the blurring of 
        /// the scene will be complete.
        /// </summary>
        public float BlurEnd
        {
            get { return blurEnd; }
            set { blurEnd = value; }
        }

        /// <summary>
        /// The amount of blur to be applied to the background.
        /// </summary>
        public float BlurAmount
        {
            get { return blurAmount; }
            set { blurAmount = value; }
        }

        /// <summary>
        /// Creates a new depth of field effect.
        /// </summary>
        /// <param name="blurStart">When the blur is to begin.</param>
        /// <param name="blurEnd">The distance at which the bluring has been completed.</param>
        /// <param name="blurAmount">The amount to blur the scene by.</param>
        public DOFEffect(PostProcessingLocal activeStore,float blurStart, float blurEnd, float blurAmount)
            : base("DOFPP", 1, activeStore)
        {
            try
            {
                cameraManager = (EngineModule)EngineModuleManager.Find(
                    "Render3DFramework").SendMessage("GetCameraManager", null);
            }
            catch
            {
                cameraManager = null;
            }

            this.blurStart = blurStart;
            this.blurEnd = blurEnd;
            this.blurAmount = blurAmount;

            effect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/PostProcessing/DOF");
            blur = Common.ContentBuilder.Load<Effect>("Eon/Shaders/PostProcessing/GausianBlur");
        }

        protected override void Render()
        {
            if (cameraManager != null)
            {
                RenderBlur();
                RenderDOF();
            }
            else
                Destroy();
        }

        void RenderBlur()
        {
            Texture2D scene = ActiveStore.Buffer.Output;

            if (scene != null)
            {
                blur.Parameters["Scene"].SetValue(scene);

                SetBlur(1 / (float)blurred.Width, 0);
                DrawWithEffect(scene, blurred, blur);

                SetBlur(0, 1 / (float)blurred.Height);
                DrawWithEffect(scene, blurred, blur);
            }
        }

        void SetBlur(float dx, float dy)
        {
            Vector2[] offsets = PostProcessingMathHelper.FindBlurOffsets(15, dx, dy);
            float[] weights = PostProcessingMathHelper.FindBlurWeights(15, blurAmount);

            blur.Parameters["Weights"].SetValue(weights);
            blur.Parameters["Offsets"].SetValue(offsets);
        }

        void RenderDOF()
        {
            Common.Device.SetRenderTarget(target);

            Common.Device.Clear(Color.Black);

            ObjectComponent camera = (ObjectComponent)cameraManager.SendMessage("CurrentCamera", null);

            if (camera != null)
            {
                Matrix view = (Matrix)camera.SendMessage("View", null);
                Matrix proj = (Matrix)camera.SendMessage("Projection", null);

                Matrix iView = Matrix.Invert(view * proj);

                effect.Parameters["IViewProj"].SetValue(iView);
                effect.Parameters["CamPos"].SetValue((Vector3)camera.SendMessage("Position", null));
                effect.Parameters["HalfPixel"].SetValue(halfPixel);

                effect.Parameters["BlurStart"].SetValue(blurStart);
                effect.Parameters["BlurEnd"].SetValue(blurEnd);

                effect.Parameters["Scene"].SetValue(ActiveStore.Buffer.Output);

                effect.Parameters["BlurredScene"].SetValue(blurred);

                effect.Parameters["DepthMap"].SetValue(ActiveStore.Buffer.GetTexture("DepthMap"));

                effect.CurrentTechnique.Passes[0].Apply();

                ActiveStore.ScreenQuad.Render();

                Common.Device.SetRenderTarget(null);

                final = target;
            }
        }

        public override void TextureQualityChanged(int width, int height)
        {
            blurred = new RenderTarget2D(Common.Device, width, height, false, SurfaceFormat.Color, DepthFormat.None);
            target = new RenderTarget2D(Common.Device, width, height, false, SurfaceFormat.Color, DepthFormat.None);

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
