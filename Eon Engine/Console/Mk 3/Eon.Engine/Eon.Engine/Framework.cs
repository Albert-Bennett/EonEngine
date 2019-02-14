/* Created: 04/06/2013
 * Last Updated: 24/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Engine.Languages;
using Eon.Helpers;
using Eon.System.Management;
using Eon.System.Tools;
using Eon.Testing;
using Microsoft.Xna.Framework;
using System;
using System.Threading;

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

        static TextureBufferManager textureBufferManager;

        protected Common common;

        string engineFileName = "Eon";

        /// <summary>
        /// The name of the file that is used to  define Eon Engine.
        /// </summary>
        public string EngineFileName
        {
            get { return engineFileName; }
            set { engineFileName = value; }
        }

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
            Common.OnShowMouse += new ShowMouseEvent(ShowMouse);

            compManager = new EngineComponentManager();

            TargetElapsedTime = TimeSpan.FromTicks(60000);
        }

        void ShowMouse(bool show)
        {
            IsMouseVisible = show;
        }

        void TextureQualityChanged()
        {
            compManager.SendMessage("TextureQualityChanged");
        }

        void Common_OnExit()
        {
            #if WINDOWS_PHONE_APP
                Exit();
            #endif 
        }

        /// <summary>
        /// Sets the framerate for the game.
        /// </summary>
        /// <param name="frames">The target number of frames.</param>
        void SetFrameRate(int frames)
        {
            TimeSpan time = TimeSpan.FromSeconds(1f / frames);

            this.TargetElapsedTime = time;
        }

        protected override void Initialize()
        {
            common.Initialize();

            new GameObjectManager();
            new InputManager();

            textureBufferManager = new TextureBufferManager();

            CreateFrameworks();

            ManualResetEvent[] doneEvents = new ManualResetEvent[]
            {
                 new ManualResetEvent(false),
                 new ManualResetEvent(false)
            };

            compManager.Initialize(doneEvents[0]);
            compManager.PostInitialize(doneEvents[1]);

            base.Initialize();
        }

        void CreateFrameworks()
        {
            new ErrorConsole();

            try
            {
                FrameworkCreation frame =
                    SerializationHelper.Deserialize<FrameworkCreation>(
                    engineFileName + ".Engine", true, "");

                common.SetDefaults(frame.DefaultScreenResolution,
                    frame.DefaultScreenSize, frame.DefaultTextureQuality, 
                    frame.FullScreen);

                SetFrameRate(frame.TargetFramerate);

                if (!frame.AudioMangerFilepath.Equals("NULL"))
                    CreateAudioManager(frame.AudioMangerFilepath);

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

                foreach (ParameterCollection p in frame.EngineComponents)
                {
                    object obj = AssemblyManager.CreateInstance(p);

                    if (obj == null)
                        new Error("The following component has not been created: " +
                            p.ObjectType, Seriousness.Error);
                }

                new DictionaryManager(frame.DefaultLanguage);

                if (frame.DictionaryFilepaths.Length > 0)
                    for (int i = 0; i < frame.DictionaryFilepaths.Length; i++)
                    {
                        if (frame.DictionaryFilepaths[i] == "")
                            new Error("No language dictionary detected at index " + i, Seriousness.Warning);
                        else
                        {
                            bool loaded = DictionaryManager.LoadDictionary(frame.DictionaryFilepaths[i]);

                            if (loaded == false)
                                new Error("Failed to load: " + frame.DictionaryFilepaths[i], Seriousness.CriticalError);
                        }
                    }
            }
            catch
            {
                new Error("Their was no Eon.Engine file found.", Seriousness.CriticalError);
                new Error("Unable to create any engine components.", Seriousness.CriticalError);
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
            compManager.Render();
            textureBufferManager.Draw();
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
