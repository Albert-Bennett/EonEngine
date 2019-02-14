/* Created: 10/06/2013
 * Last Updated: 25/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Resolution;
using Eon.System.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon
{
    /// <summary>
    /// Defines the common elements in Eon Engine i.e GraphicsDevice.
    /// </summary>
    public sealed class Common
    {
        #region Fields

        public static readonly int MaxSize = 4048;

        static TimeSpan elapsedTimeDelta;
        static TimeSpan totalSecs = TimeSpan.Zero;

        static SpriteBatch batch;
        static GraphicsDeviceManager deviceManager;
        static ContentBuilder contentBuilder;
        static Vector2 defaultScreenRes;

        static float upScale;
        static float prevUpScale;

        static Vector2 quality;
        static TextureQuality currentQuality;
        static ScreenResolutions currentResolution = ScreenResolutions.SVGA;

        #endregion
        #region Properties

        /// <summary>
        /// An event to be thrown when the game is being exited.
        /// </summary>
        public static event ExitingEvent OnExit;

        /// <summary>
        /// An event to be thrown when the 
        /// texture quality has been changed.
        /// </summary>
        public static TextureQualityChangedEvent OnTextureQualityChanged;

        /// <summary>
        /// An event to be thrown when the user whants to show the mouse.
        /// </summary>
        public static ShowMouseEvent OnShowMouse;

        /// <summary>
        /// The common GraphicsDevice.
        /// </summary>
        public static GraphicsDevice Device { get { return deviceManager.GraphicsDevice; } }

        /// <summary>
        /// The common GraphicsDeviceManager.
        /// </summary>
        public static GraphicsDeviceManager DeviceManager { get { return deviceManager; } }

        /// <summary>
        /// The aspect ratio of the renderable screen.
        /// </summary>
        public static float AspectRatio { get { return deviceManager.GraphicsDevice.Viewport.AspectRatio; } }

        /// <summary>
        /// The quality of the render targets.
        /// </summary>
        public static Vector2 TextureQuality { get { return quality; } }

        /// <summary>
        /// The common ContentBuilder.
        /// </summary>
        public static ContentBuilder ContentBuilder { get { return contentBuilder; } }

        /// <summary>
        /// The common SpriteBatch.
        /// </summary>
        public static SpriteBatch Batch { get { return batch; } }

        /// <summary>
        /// The common ElapsedTimeDelta.
        /// </summary>
        public static TimeSpan ElapsedTimeDelta { get { return Common.elapsedTimeDelta; } }

        /// <summary>
        /// The total amount of time that the game has been running for.
        /// </summary>
        public static TimeSpan TotalGameTime
        {
            get { return totalSecs; }
        }

        /// <summary>
        /// The render area for the game.
        /// </summary>
        internal static Rectangle ScreenResolution
        {
            get { return ResolutionManager.ScreenBounds; }
        }

        /// <summary>
        /// The default render area of the game.
        /// </summary>
        public static Vector2 DefaultScreenResolution
        {
            get { return defaultScreenRes; }
        }

        /// <summary>
        /// Defines the precentage of change in texture qualities.
        /// </summary>
        public static float UpScale
        {
            get { return upScale; }
        }

        /// <summary>
        /// Is the game synchronized with a vertical retrace.
        /// </summary>
        public static bool VerticleRetrace
        {
            get { return deviceManager.SynchronizeWithVerticalRetrace; }
            set
            {
                deviceManager.SynchronizeWithVerticalRetrace = value;

                deviceManager.ApplyChanges();
            }
        }

        /// <summary>
        /// The current TextureQuality of EonEngine.
        /// </summary>
        public static TextureQuality CurrentTextureQuality
        {
            get { return currentQuality; }
        }

        #endregion
        #region Ctor

        /// <summary>
        /// Creates a new Common.
        /// </summary>
        /// <param name="game">Game.</param>
        public Common(Game game)
        {
            deviceManager = new GraphicsDeviceManager(game);

            contentBuilder = new ContentBuilder(game.Content.ServiceProvider);
        }

        public void Initialize()
        {
            batch = new SpriteBatch(deviceManager.GraphicsDevice);
        }

        #endregion
        #region ScreenResolutions

        public void SetDefaults(int screenResolution, 
            int screenSize, byte textureQuality, bool fullScreen)
        {
            ChangeScreenSize(screenSize);

            Vector2 res = ResolutionHelper.GetScreenResolution((ScreenResolutions)screenResolution);

            Common.defaultScreenRes = res;
            currentResolution = (ScreenResolutions)screenResolution;

            ResolutionManager.ChangeScreenResolution(new Point((int)res.X, (int)res.Y));

            if (!deviceManager.IsFullScreen)
                deviceManager.IsFullScreen = fullScreen;

            if (textureQuality < 1 || textureQuality > 5)
                textureQuality = 1;

            currentQuality = (System.Resolution.TextureQuality)textureQuality;

            upScale = (float)textureQuality / 2;
            prevUpScale = upScale;

            Common.quality = new Vector2(deviceManager.PreferredBackBufferWidth,
                deviceManager.PreferredBackBufferHeight) * upScale;
        }

        /// <summary>
        /// Changes the size of the screen.
        /// </summary>
        /// <param name="screenSize">The size of the screen.</param>
        public static void ChangeScreenSize(ScreenResolutions screenSize)
        {
            _ChangeScreenSize(ResolutionHelper.GetScreenResolution(screenSize));
        }

        /// <summary>
        /// Changes the size of the screen.
        /// </summary>
        /// <param name="screenSize">The size of the screen.</param>
        public static void ChangeScreenSize(int screenSize)
        {
            _ChangeScreenSize(ResolutionHelper.GetScreenResolution((ScreenResolutions)screenSize));
        }

        static void _ChangeScreenSize(Vector2 screenSize)
        {
            if (deviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Width < (int)screenSize.X)
            {
                float fac = (float)deviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Width / (float)screenSize.X;

                deviceManager.PreferredBackBufferWidth = deviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
                deviceManager.PreferredBackBufferHeight = (int)(screenSize.Y * fac);
            }
            else if (deviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Height < (int)screenSize.Y)
            {
                float fac = (float)deviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Height / (float)screenSize.Y;

                deviceManager.PreferredBackBufferHeight = deviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
                deviceManager.PreferredBackBufferWidth = (int)(screenSize.X * fac);
            }
            else
            {
                if ((int)screenSize.X == deviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Width &&
                    (int)screenSize.Y == deviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Height)
                    deviceManager.IsFullScreen = true;

                deviceManager.PreferredBackBufferWidth = (int)screenSize.X;
                deviceManager.PreferredBackBufferHeight = (int)screenSize.Y;
            }
            
            deviceManager.ApplyChanges();
        }

        /// <summary>
        /// Changes the size of the render area of the game.
        /// </summary>
        /// <param name="resolution">The new screen resolution.</param>
        public static void ChangeScreenResolution(ScreenResolutions resolution)
        {
            if (currentResolution != resolution)
            {
                Vector2 res = ResolutionHelper.GetScreenResolution(resolution);

                ResolutionManager.ChangeScreenResolution(new Point((int)res.X, (int)res.Y));

                currentResolution = resolution;
            }
        }

        /// <summary>
        /// Changes the quality of the render targets.
        /// </summary>
        /// <param name="quality">The quality of the render targets.</param>
        public static void ChangeTextureQuality(TextureQuality quality)
        {
            if (quality != currentQuality)
            {
                currentQuality = quality;

                Common.quality = new Vector2(deviceManager.PreferredBackBufferWidth,
                    deviceManager.PreferredBackBufferHeight) * (float)quality / 2;

                if (Common.quality.X > MaxSize)
                {
                    int dif = (int)Common.quality.X - MaxSize;

                    float ratio = Common.quality.X / Common.quality.Y;

                    Common.quality.X -= dif;
                    Common.quality.Y -= dif * ratio;
                }
                else if (Common.quality.Y > MaxSize)
                {
                    int dif = (int)Common.quality.Y - MaxSize;

                    float ratio = Common.quality.Y / Common.quality.X;

                    Common.quality.X -= dif * ratio;
                    Common.quality.Y -= dif;
                }

                if (quality == Eon.System.Resolution.TextureQuality.LowQuality)
                {
                    deviceManager.GraphicsDevice.PresentationParameters.MultiSampleCount = 1;
                    deviceManager.PreferMultiSampling = false;
                }
                else
                {
                    int multiSampling = 2 * (int)quality;

                    deviceManager.PreferMultiSampling = true;

                    deviceManager.GraphicsDevice.PresentationParameters.MultiSampleCount = multiSampling;
                }

                prevUpScale = upScale;
                upScale = (float)quality / 2;

                deviceManager.ApplyChanges();

                if (OnTextureQualityChanged != null)
                    OnTextureQualityChanged();
            }
        }

        #endregion
        #region Misc

        /// <summary>
        /// Sets the elasped time delta.
        /// </summary>
        /// <param name="time">The time to set to.</param>
        public void SetElaspedTimeDelta(TimeSpan time)
        {
            elapsedTimeDelta = time;
            totalSecs += time;
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        public static void ExitGame()
        {
            if (OnExit != null)
            {
                contentBuilder.UnloadAll(true);

                OnExit();
            }
        }

        /// <summary>
        /// Shows the mouse.
        /// </summary>
        /// <param name="show">Show the mouse?</param>
        public static void ShowMouse(bool show)
        {
            if (OnShowMouse != null)
                OnShowMouse(show);
        }

        /// <summary>
        /// Changes the content root of the content manager.
        /// </summary>
        /// <param name="contentRoot">The new content root.</param>
        public void ChangeContentRoot(string contentRoot)
        {
            ContentBuilder.RootDirectory = contentRoot;
        }

        #endregion
        #region Helper Methods

        /// <summary>
        /// Converts a world position into it's screen space equivelent.
        /// </summary>
        /// <returns>A world space co-ordinate into a screen space co-ordinate.</returns>
        public static Vector2 ConvertToScreenSpace(Vector3 position,
            Matrix view, Matrix projection, Matrix world)
        {
            Vector3 screenPos = Device.Viewport.Project(position, projection, view, world);

            return new Vector2(screenPos.X, screenPos.Y);
        }

        /// <summary>
        /// Casts a ray using a position in 2D space.
        /// </summary>
        /// <param name="position">The position ot be cast as a ray.</param>
        /// <param name="view">The camera's view matrix.</param>
        /// <param name="projection">The camera's projection matrix.</param>
        /// <returns>The casted ray.</returns>
        public static Ray CastUnProjectedRay(Vector2 position,
            Matrix view, Matrix projection)
        {
            Vector3 near = new Vector3(position, Device.Viewport.MinDepth);
            Vector3 far = new Vector3(position, Device.Viewport.MaxDepth);

            near = Device.Viewport.Unproject(near, projection,
                 view, Matrix.Identity);

            far = Device.Viewport.Unproject(far, projection,
                view, Matrix.Identity);

            Vector3 direct = far - near;
            direct.Normalize();

            return new Ray(near, direct);
        }

        /// <summary>
        /// Calculates a re-scaled vector.
        /// </summary>
        /// <param name="vector">The Vector2 to be re-scaled.</param>
        /// <returns>The calculated Vector3.</returns>
        public static Vector3 GetReScaled(Vector3 vector)
        {
            return (vector / prevUpScale) * upScale;
        }

        /// <summary>
        /// Calculates a re-scaled vector.
        /// </summary>
        /// <param name="vector">The Vector2 to be re-scaled.</param>
        /// <returns>The calculated Vector2.</returns>
        public static Vector2 GetReScaled(Vector2 vector)
        {
            return (vector / prevUpScale) * upScale;
        }

        /// <summary>
        /// Calculates a re-scaled vector.
        /// </summary>
        /// <param name="value">The float to be re-scaled.</param>
        /// <returns>The calculated float.</returns>
        public static float GetReScaled(float value)
        {
            return (value / prevUpScale) * upScale;
        }

        #endregion
    }
}
