/* Created: 11/06/2014
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
    /// Used to define a bluring effect on the 
    /// background caused by the movement of the camera.
    /// </summary>
    public sealed class MotionBlurEffect : PostProcess
    {
        Matrix prevViewProj = Matrix.Identity;
        Vector2 maxVelocity;

        Effect effect;
        Vector2 halfPixel;

        RenderTarget2D target;

        EngineModule cameraManager;

        /// <summary>
        /// The maximum velocity that can be used 
        /// when blurring, before artifacts begin to appear.
        /// </summary>
        public Vector2 MaxVelocity
        {
            get { return maxVelocity; }
            set { maxVelocity = value; }
        }

        /// <summary>
        /// Creates a new MotionBlurEffect.
        /// </summary>
        /// <param name="maxVelocity">The maximum velocity that can be blurred to.</param>
        public MotionBlurEffect(PostProcessingLocal activeStore, Vector2 maxVelocity)
            : base("MotionBlurPP", 2,activeStore)
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

            effect = Common.ContentBuilder.Load<Effect>("Eon/Shaders/PostProcessing/MotionBlur");

            this.maxVelocity = maxVelocity;
        }

        /// <summary>
        /// Creates a new MotionBlurEffect.
        /// </summary>
        public MotionBlurEffect(PostProcessingLocal activeStore) : this(activeStore,new Vector2(16)) { }

        protected override void Render()
        {
            if (cameraManager != null)
            {
                Common.Device.SetRenderTarget(target);

                Common.Device.Clear(Color.Transparent);

                ObjectComponent camera = (ObjectComponent)cameraManager.SendMessage("CurrentCamera", null);

                if (camera != null)
                {
                    Matrix view = (Matrix)camera.SendMessage("View", null);
                    Matrix proj = (Matrix)camera.SendMessage("Projection", null);

                    Matrix iViewProj = Matrix.Invert(view * proj);

                    effect.Parameters["IViewProj"].SetValue(iViewProj);

                    effect.Parameters["PrevViewProj"].SetValue(prevViewProj);
                    effect.Parameters["MaxVelocity"].SetValue(maxVelocity);

                    effect.Parameters["HalfPixel"].SetValue(halfPixel);

                    effect.Parameters["Scene"].SetValue(ActiveStore.Buffer.Output);

                    effect.Parameters["DepthMap"].SetValue(ActiveStore.Buffer.GetTexture("DepthMap"));

                    effect.Parameters["DistortionMap"].SetValue(ActiveStore.Buffer.GetTexture("DistortionMap"));

                    effect.CurrentTechnique.Passes[0].Apply();

                    ActiveStore.ScreenQuad.Render();

                    Common.Device.SetRenderTarget(null);

                    final = target;

                    prevViewProj = Matrix.Invert(view * proj);
                }
            }
            else
                Destroy();
        }

        public override void TextureQualityChanged(int width, int height)
        {
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
