/* Created 05/03/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Audio;
using Eon.EngineComponents;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Engine
{
    /// <summary>
    /// A framework that is used to glue togeather all other parts of the game engine.
    /// </summary>
    public class Framework : Game
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
                    AssemblyManager.CreateInstance(s);
            }
            catch { }
        }

        /// <summary>
        /// Used to create an AudioManager for the game.
        /// </summary>
        /// <param name="jJaxFilePath">The file path for the J-JaxFile.</param>
        public static void CreateAudioManager(string jJaxFilePath)
        {
            if (AudioManager != null)
            {
                AudioManager.StopAll();

                compManager.RemoveComponent(AudioManager);
                AudioManager = null;
            }

            AudioManager = new AudioManager(jJaxFilePath);

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

            compManager.PostUpdate();
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
