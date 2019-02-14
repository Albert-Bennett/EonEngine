/* Created: 04/06/2013
 * Last Updated: 15/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Engine.Input;
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
    public class Framework : Game
    {
        static EngineModuleManager compManager;
        static AudioManager audioManager;
        static InputManager input;

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
        /// The game's InputManager.
        /// </summary>
        public static InputManager InputManager
        {
            get { return input; }
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

            compManager = new EngineModuleManager();

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
        /// Saves a snap shot of the current frame.
        /// </summary>
        public void SaveSnapShot()
        {
            string filepath = DateTime.Now.ToShortDateString() + " ("
                      + DateTime.Now.ToShortTimeString() + "-" + DateTime.Now.Second + ")";

            filepath = filepath.Replace('/', '-');
            filepath = filepath.Replace(':', '-');

            textureBufferManager.SaveSnapShot(filepath);
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
            input = new InputManager();

            textureBufferManager = new TextureBufferManager();

#if DEBUG
            new ErrorConsole();
#endif

            CreateFrameworks();

            compManager.Initialize();

            base.Initialize();
        }

        void CreateFrameworks()
        {
            try
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(EonDictionary<string, Command>),
                    typeof(Message[]),
                    typeof(Keys),
                    typeof(TouchType),
                    typeof(GamePadButtons),
                    typeof(MouseButtons),
                    typeof(TriggerIndex)
                };

                FrameworkCreation frame =
                    SerializationHelper.Deserialize<FrameworkCreation>(
                    engineFileName + ".Engine", true, "", extraTypes);

                common.SetDefaults(frame.DefaultScreenResolution,
                    frame.DefaultScreenSize, frame.DefaultTextureQuality,
                    frame.FullScreen);

                SetFrameRate(frame.TargetFramerate);

                if (!frame.AudioMangerFilepath.Equals("NULL"))
                    CreateAudioManager(frame.AudioMangerFilepath);

                InputHandler.SetInputCommands(frame.InputCommands);

                new DictionaryManager();

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

                DictionaryManager.ChangeLanguage(frame.DefaultLanguage);

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

                audioManager.ReCreate(jJaxFilepath);
            }
            else
                audioManager = new AudioManager(jJaxFilepath);
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
        /// Sends a Message to each EngineModule.
        /// </summary>
        /// <param name="message">The Message to be executed.</param>
        public static void SendMessage(Message message)
        {
            compManager.SendMessage(message);
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
        /// <param name="disposing">Whether or not to dispose.</param>
        protected override void Dispose(bool disposing)
        {
            compManager.Dispose();
            compManager = null;

            base.Dispose(disposing);
        }
    }
}
