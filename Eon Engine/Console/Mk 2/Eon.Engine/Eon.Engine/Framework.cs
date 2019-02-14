/* Created 04/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Languages;
using Eon.Helpers;
using Eon.System.Management;
using Eon.System.Tools;
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
        static AudioManager audioManager;
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

            Common.OnTextureQualityChanged += new TextureQualityChangedEvent(TextureQualityChanged);
            Common.OnExit += Common_OnExit;

            compManager = new EngineComponentManager();

            TargetElapsedTime = TimeSpan.FromTicks(60000);
        }

        void TextureQualityChanged()
        {
            compManager.SendMessage("TextureQualityChanged");
        }

        void Common_OnExit()
        {
            Exit();
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
            new ErrorManager();
            new TextureBuffer();

            CreateFrameworks();

            compManager.Initialize();

            base.Initialize();

            compManager.PostInitialize();
        }

        void CreateFrameworks()
        {
            FrameworkCreation frame = new FrameworkCreation();

            common.SetDefaultScreenResolution(frame.DefaultScreenResolution);

            try
            {
                frame = XmlHelper.DeserializeContent<FrameworkCreation>("Eon.Engine");
            }
            catch
            {
                new Error("Their was no Eon.Engine file found.", Seriousness.CriticalError);
                new Error("Unable to create any engine components.", Seriousness.CriticalError);
            }

            if (frame.AssemblyRefferences.Length > 0)
            {
                for (int i = 0; i < frame.AssemblyRefferences.Length; i++)
                    try
                    {
                        AssemblyManager.AddAssemblyRef(frame.AssemblyRefferences[i]);
                    }
                    catch
                    {
                        new Error("Their was a problem finding " + frame.AssemblyRefferences[i], Seriousness.Error);
                    }
            }
            else
                new Error("Their were no assembly references found.", Seriousness.CriticalError);

            foreach (string s in frame.EngineComponents)
            {
                object obj = AssemblyManager.CreateInstance(s);

                if (obj == null)
                    new Error("The following component has not been created: " + s, Seriousness.Error);
            }

            new DictionaryManager(frame.DefaultLanguage);

            if (frame.DictionaryFilepaths.Length > 0)
                for (int i = 0; i < frame.DictionaryFilepaths.Length; i++)
                {
                    bool loaded = DictionaryManager.LoadDictionary(frame.DictionaryFilepaths[i]);

                    if (loaded == false)
                        new Error("Failed to load: " + frame.DictionaryFilepaths[i], Seriousness.CriticalError);
                }
        }

        /// <summary>
        /// Used to create an AudioManager for the game.
        /// </summary>
        /// <param name="JJaxFilepath">The filepath for the J-Jax audio file.</param>
        public static void CreateAudioManager(string jJaxFilepath)
        {
            if (audioManager != null)
            {
                AudioManager.StopAll();

                audioManager.Destroy();
                audioManager = null;
            }

            audioManager = new AudioManager(jJaxFilepath);

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
