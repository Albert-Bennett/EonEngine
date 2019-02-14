/* Created: 01/01/2015
 * Last Updated: 01/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk;
using Eon;
using Eon.Engine;
using Eon.Helpers;
using Eon.Rendering2D;
using Eon.System.States;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Threading;

namespace EEDK.Gui
{
    /// <summary>
    /// The main screen of the development kit.
    /// </summary>
    public sealed class MainScreen : MenuScreen
    {
        string[] itemNames = new string[]
        {
            "File",
            "New"
        };

        public MainScreen() : base("MainScreen", GameStates.Game) { }

        protected override void Initialize()
        {
            Rectangle rect = new Rectangle(0, 0, (int)Common.TextureQuality.X, 24);

            AttachComponent(new Sprite(ID + "Spr", 0, "GUI/MenuBack", Color.White, true, rect));

            StartLibraryReader();
            //TestProcess("EEDK.ParticleCreationTool");

            base.Initialize();
        }

        void TestProcess(string processName)
        {
            Crosswalk.Message message = new Crosswalk.Message()
            {
                Messages = new string[]
                {
                    InterOperations.RootFilepath + InterOperations.ProjectName + ".EPROJ",
                    InterOperations.RootFilepath
                }
            };

            SerializationHelper.Serialize<Crosswalk.Message>(message, "Temp", ".temp");

            Thread.Sleep(500);

            ProcessStartInfo start = new ProcessStartInfo(processName + ".exe");

            Process reader = new Process();
            reader.StartInfo = start;
            reader.Start();

            reader.WaitForExit();

            Type[] extraTypes = new Type[]
            {
                typeof(ObjectListing),
                typeof(FrameworkCreation)
            };

            InterOperations.Project =
                SerializationHelper.Deserialize<ProjectFile>(
                InterOperations.RootFilepath +
                InterOperations.ProjectName + ".EPROJ", false, "", extraTypes);
        }

        void StartLibraryReader()
        {
            Crosswalk.Message message = new Crosswalk.Message()
            {
                Messages = new string[]
                {
                    InterOperations.RootFilepath +
                    InterOperations.ContentFilepathExtention + "\\",
                    "Models\\Planets\\Exterior\\Planet_Shader",
                    InterOperations.RootFilepath + InterOperations.ProjectName + ".EPROJ"
                }
            };

            SerializationHelper.Serialize<Crosswalk.Message>(message, "Temp", ".temp");

            Thread.Sleep(500);

            ProcessStartInfo start = new ProcessStartInfo("EEDK.ModelViewer.exe");

            Process reader = new Process();
            reader.StartInfo = start;
            reader.Start();

            reader.WaitForExit();
        }
    }
}
