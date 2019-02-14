/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Resolution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        static Game game;
        static TimeSpan elapsedTimeDelta;
        static TimeSpan totalSecs = TimeSpan.Zero;
        static SpriteBatch batch;
        static GraphicsDeviceManager deviceManager;
        static ContentManager content;
        static Vector2 defaultScreenRes;

        static Vector2 quality;
        static Vector2 prevQuailty;
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
        /// The game.
        /// </summary>
        public static Game Game { get { return game; } }

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
        /// The common ContentManager.
        /// </summary>
        public static ContentManager ContentManager { get { return content; } }

        /// <summary>
        /// The common SpriteBatch.
        /// </summary>
        public static SpriteBatch Batch { get { return batch; } }

        /// <summary>
        /// The common ElapsedTimeDelta.
        /// </summary>
        public static TimeSpan ElapsedTimeDelta { get { return Common.elapsedTimeDelta; } }

        /// <summary>
        /// The render area for the game.
        /// </summary>
        internal static Rectangle ScreenResolution
        {
            get { return ResolutionManager.ScreenBounds; }
        }

        /// <summary>
        /// The default screen resolution of the game.
        /// </summary>
        public static Vector2 DefaultScreenResolution
        {
            get { return defaultScreenRes; }
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

            Common.game = game;
            Common.content = game.Content;
            Common.content.RootDirectory = "Content\\";
        }

        public void Initialize()
        {
            batch = new SpriteBatch(game.GraphicsDevice);

            quality = new Vector2(deviceManager.PreferredBackBufferWidth,
                deviceManager.PreferredBackBufferHeight);
        }

        #endregion
        #region ScreenResolutions

        public void SetDefaultScreenResolution(int screenResolution)
        {
            if (!deviceManager.IsFullScreen)
                FullScreen();

            Vector2 res = ResolutionHelper.GetScreenResolution((ScreenResolutions)screenResolution);

            Common.defaultScreenRes = res;
            currentResolution = (ScreenResolutions)screenResolution;

            ResolutionManager.ChangeScreenResolution(new Point((int)res.X, (int)res.Y));
        }

        static void FullScreen()
        {
            Vector2 full = ResolutionHelper.GetScreenResolution(ScreenResolutions.Auto);

            deviceManager.PreferredBackBufferWidth = (int)full.X;
            deviceManager.PreferredBackBufferHeight = (int)full.Y;

            deviceManager.IsFullScreen = true;

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
                prevQuailty = Common.quality;

                currentQuality = quality;

                Common.quality = new Vector2(deviceManager.PreferredBackBufferWidth,
                    deviceManager.PreferredBackBufferHeight) * (int)quality;

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
                OnExit();
        }

        #endregion
        #region Helper Methods

        /// <summary>
        /// Converts a 2D co-ordinate into 3D space.
        /// </summary>
        /// <param name="position">The co-ordinate to be converted.</param>
        /// <param name="near">Wheather or not the co-ordinate should be 
        /// calculated as being near / far from the viewport</param>
        /// <param name="viewMatrix">The camera's view matrix.</param>
        /// <param name="projection">The camera's projection matrix.</param>
        /// <returns>The converted co-ordinate.</returns>
        public static Vector3 ConvertTo3DSpace(Vector2 position, bool near,
            Matrix viewMatrix, Matrix projection)
        {
            Vector3 pos;

            if (near)
                pos = new Vector3(position, 0);
            else
                pos = new Vector3(position, 1);

            return Device.Viewport.Unproject(pos, projection,
                viewMatrix, Matrix.Identity);
        }

        #endregion
    }
}
