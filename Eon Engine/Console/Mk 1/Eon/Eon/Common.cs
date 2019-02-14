/* Created 10/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

namespace Eon
{
    /// <summary>
    /// An event to deal with the exiting of the game.
    /// </summary>
    public delegate void ExitingEvent();

    /// <summary>
    /// A delegate used to define what happends 
    /// when the screen resolution has changed.
    /// </summary>
    public delegate void ScreenResolutionChangedEvent();

    /// <summary>
    /// Defines the common elements in Eon i.e GraphicsDevice.
    /// </summary>
    public sealed class Common
    {
        static Game game;
        static TimeSpan elapsedTimeDelta;
        static TimeSpan totalSecs = TimeSpan.Zero;
        static SpriteBatch batch;
        static GraphicsDeviceManager deviceManager;
        //static VideoPlayer videoPlayer;
        static ContentManager content;
        static Vector2 scrnRes = new Vector2(800, 480);
        static Vector2 prevScrnRes = Vector2.One;

        /// <summary>
        /// An event to be thrown when the game is being exited.
        /// </summary>
        public static event ExitingEvent OnExit;

        /// <summary>
        /// An event to be thrown when the screen
        /// resolution for the game has changed.
        /// </summary>
        public static ScreenResolutionChangedEvent OnScreenResChanged;

        /// <summary>
        /// The game.
        /// </summary>
        public static Game Game { get { return game; } }

        /// <summary>
        /// The common GraphicsDevice.
        /// </summary>
        public static GraphicsDevice Device { get { return Game.GraphicsDevice; } }

        /// <summary>
        /// The common GraphicsDeviceManager.
        /// </summary>
        public static GraphicsDeviceManager DeviceManager { get { return deviceManager; } }

        /// <summary>
        /// The screen resolution of the game.
        /// </summary>
        public static Vector2 ScreenResolution { get { return scrnRes; } }

        /// <summary>
        /// The previous resolution setting for the screen.
        /// </summary>
        public static Vector2 PreviousScreenResolution { get { return prevScrnRes; } }

        /// <summary>
        /// The common Viewport.
        /// </summary>
        internal static Viewport Viewport { get { return Device.Viewport; } }

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
        /// Returns the video player for the game.
        /// </summary>
        //public static VideoPlayer VideoPlayer { get { return videoPlayer; } }

        /// <summary>
        /// The total amount of seconds that the game has been running for.
        /// </summary>
        public static TimeSpan TotalPlayTimeSecs { get { return Common.totalSecs; } }

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
            //videoPlayer = new VideoPlayer();
        }

        /// <summary>
        /// Changes the game to full screen.
        /// </summary>
        public static void FullScreen()
        {
            DeviceManager.IsFullScreen = true;
            DeviceManager.ApplyChanges();
        }

        /// <summary>
        /// Changes the screen resolution of the game.
        /// </summary>
        /// <param name="width">New resolution width.</param>
        /// <param name="height">New resolution height.</param>
        public static void ChangeScreenResolution(float width, float height)
        {
            if (!EonMathHelper.EqualEnough(width, scrnRes.X) ||
                !EonMathHelper.EqualEnough(height, scrnRes.Y))
            {
                prevScrnRes = scrnRes;
                scrnRes = new Vector2(width, height);

                if (OnScreenResChanged != null)
                    OnScreenResChanged();
            }
        }

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

        /// <summary>
        /// Used to re-calibrate the position of objects
        /// when the screen resolution has been changed.
        /// </summary>
        /// <param name="screenSpaceVector">Any Vector2 that has been used 
        /// to define a position or scale of an object.</param>
        /// <returns>The newly calibrated vector.</returns>
        public static Vector2 ReCalibrateScreenSpaceVector(Vector2 screenSpaceVector)
        {
            return (screenSpaceVector / prevScrnRes) * scrnRes;
        }

        /// <summary>
        /// Used to re-calibrate teh scale of objects
        /// when the screen resolution has been 
        /// </summary>
        /// <param name="scale">The scale of an object.</param>
        /// <returns>The newly re-calibrated scale.</returns>
        public static float ReCalibrateScale(float scale)
        {
            Vector2 vec = ReCalibrateScreenSpaceVector(
                new Vector2(scale, scale));

            return (vec.X + vec.Y) / 2;
        }

        #endregion
    }
}
