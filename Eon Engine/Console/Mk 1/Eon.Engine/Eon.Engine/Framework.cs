/* Created 04/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Engine.Media;
using Eon.EngineComponents;
using Eon.Testing.ErrorManagement;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Engine
{
    /// <summary>
    /// A framework that is used to glue togeather all other parts of the game engine.
    /// </summary>
    public class Framework : Microsoft.Xna.Framework.Game
    {
        static EngineComponentManager compManager;
        static AudioManager AudioManager;
        static bool audioManagerExists = false;

        protected Common common;

        /// <summary>
        /// A check to see if the AudioManager exists.
        /// </summary>
        public static bool AudioManagerExists
        {
            get { return audioManagerExists; }
        }

        /// <summary>
        /// Creates a new Eon framework.
        /// </summary>
        public Framework()
        {
            common = new Common(this);

            Common.ContentManager.RootDirectory = "Content\\";
            Common.OnScreenResChanged += new ScreenResolutionChangedEvent(ScreenResChanged);
            Common.OnExit += Common_OnExit;

            compManager = new EngineComponentManager();
        }

        void Common_OnExit()
        {
            Exit();
        }

        void ScreenResChanged()
        {
            compManager.SendMessage("ScreenResolutionChanged");
        }

        /// <summary>
        /// Sets the framerate for the game.
        /// </summary>
        /// <param name="frames">The target number of frames.</param>
        protected void SetFrameRate(int frames)
        {
            TimeSpan time = TimeSpan.FromSeconds(1f / frames);

            this.TargetElapsedTime = time;
        }

        protected override void Initialize()
        {
            common.Initialize();

            new GameObjectManager();
            new InputManager();
            new GameStateManager();

            CreateFrameworks();

            //#if Debug
            new ErrorManager();
            //#endif

            compManager.Initialize();

            base.Initialize();

            compManager.PostInitialize();
        }

        void CreateFrameworks()
        {
            try
            {
                FrameworkCreation frame = Common.ContentManager.Load<FrameworkCreation>("Eon");

                foreach (string s in frame.AssemblyRefferences)
                    AssemblyManager.AddAssemblyRef(s);

                foreach (string s in frame.EngineComponents)
                {
                    object obj = AssemblyManager.CreateInstance(s);

                    if (obj == null)
                        new Error("The following component has not been created: " + s, Seriousness.Error);
                }
            }
            catch (Exception ex)
            {
                new Error("Their was no Eon.ini file found.", Seriousness.CriticalError);
                new Error("Unable to create any engine components.", Seriousness.CriticalError);
            }
        }

        /// <summary>
        /// Used to create an AudioManager for the game.
        /// </summary>
        /// <param name="xaptFilepath">The file location for the Xapt file.</param>
        /// <param name="xaptName">The name of the Xapt file.</param>
        /// <param name="waveBankName">The name given to the wave bank in the Xapt file.</param>
        /// <param name="soundBankName">The name given to the sound bank in the Xapt file.</param>
        public static void CreateAudioManager(string xaptFilepath, string xaptName,
            string waveBankName, string soundBankName)
        {
            if (AudioManager != null)
            {
                AudioManager.StopAll();

                compManager.RemoveComponent(AudioManager);
                AudioManager = null;
            }

            AudioManager = new AudioManager(xaptFilepath, xaptName, waveBankName, soundBankName);

            audioManagerExists = true;
        }

        /// <summary>
        /// Updates the framework.
        /// </summary>
        /// <param name="gameTime">The GameTime to use for the update.</param>
        protected override void Update(GameTime gameTime)
        {
            common.SetElaspedTimeDelta(gameTime.ElapsedGameTime);

            compManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// Used to draw objects.
        /// </summary>
        /// <param name="gameTime">The GameTime to use for the draw.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            compManager.Render();
        }

        /// <summary>
        /// Disposes of the Framework.
        /// </summary>
        /// <param name="disposing">Weather or not to dispose.</param>
        protected override void Dispose(bool disposing)
        {
            compManager.Dispose();
            compManager = null;

            base.Dispose(disposing);
        }
    }
}
