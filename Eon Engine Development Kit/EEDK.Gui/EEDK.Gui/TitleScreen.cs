/* Created: 01/01/2015
 * Last Updated: 24/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk;
using EEDK.Gui.Controls;
using Eon;
using Eon.Collections;
using Eon.Engine;
using Eon.Helpers;
using Eon.Rendering2D;
using Eon.System.States;
using Eon.UIApi.Cursors;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace EEDK.Gui
{
    /// <summary>
    /// The title screen of the development kit.
    /// </summary>
    public sealed class TitleScreen : MenuScreen
    {
        string[] itemNames = new string[]
        {
            "Load Level",
            "New Level",
            "Exit"
        };

        OpenFileDialog openDia;
        FolderBrowserDialog folderDia;
        RecentFile recent;

        public TitleScreen() : base("TitleScreen", GameStates.MainMenu) { }

        protected override void Initialize()
        {
            Rectangle rect = new Rectangle(0, 0,
                (int)Common.TextureQuality.X,
                (int)Common.TextureQuality.Y);

            AttachComponent(new Sprite(ID + "Spr", 0, "GUI/Logo", Color.White, true, rect));

            Vector2 intitalPos = new Vector2(48, 180);
            float down = 64;

            new Eon.UIApi.Cursors.Cursor("Cursor", 
                "Cursors/ClickCursor", 12);

            for (int i = 0; i < itemNames.Length; i++)
            {
                TextButton btn = new TextButton(itemNames[i], itemNames[i], "Eon/Fonts/Arial12", this, intitalPos);
                btn.OnClicked += new OnClickedEvent(BtnClicked);

                intitalPos.Y += down;
            }

            try
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(List<string>)
                };

                recent = SerializationHelper.Deserialize<RecentFile>(
                    "Recent.temp", false, "", extraTypes);

                intitalPos = new Vector2(256, 180);
                down = 22;

                for (int i = 0; i < recent.Recent.Count; i++)
                {
                    TextButton btn = new TextButton("Recent_" + i, recent.Recent[i], "Eon/Fonts/Arial12", this, intitalPos);
                    btn.OnClicked += new OnClickedEvent(BtnClicked);

                    intitalPos.Y += down;
                }
            }
            catch
            {
                recent = new RecentFile();
            }

            folderDia = new FolderBrowserDialog();
            folderDia.ShowNewFolderButton = true;

            openDia = new OpenFileDialog();
            openDia.InitialDirectory = @"C:\";
            openDia.Filter = EEDK.Crosswalk.InterOperations.ProjectFileFilter;
            openDia.Title = "Browze files";

            base.Initialize();
        }

        void BtnClicked(string controlID)
        {
            if (controlID.Contains("Recent_"))
            {
                string filepath = ((TextButton)CurrentControl).Text;
                bool found = false;

                if (SerializationHelper.FileExists(filepath))
                {
                    recent.Add(filepath);

                    string text = filepath;

                    string[] split = text.Split(new char[]
                    {
                        '\\', '/'
                    });

                    int len = split[split.Length - 1].Length;
                    len++;

                    text = text.Remove(text.Length - len, len);

                    text += InterOperations.LibraryFilepathExtention;

                    Crosswalk.Message message = new Crosswalk.Message()
                    {
                        Messages = new string[] { text }
                    };

                    SerializationHelper.Serialize<EEDK.Crosswalk.Message>(message, "Temp.temp", "");

                    LoadProject(filepath);

                    InterOperations.ProjectName = SerializationHelper.GetFolderName(
                        filepath.Remove(filepath.Length -
                        InterOperations.ProjectFileExtention.Length,
                        InterOperations.ProjectFileExtention.Length));

                    len = ".EPROJ".Length + InterOperations.ProjectName.Length;
                    InterOperations.RootFilepath = filepath.Remove(filepath.Length - len, len);

                    InterOperations.Project.ProjectRootFilepath = InterOperations.RootFilepath;
                    InterOperations.Project.ProjectName = InterOperations.ProjectName;

                    found = true;
                }
                else
                {
                    recent.Remove(filepath);
                    CurrentControl.Destroy();
                }

                SaveRecent();

                if (found)
                    StartLibraryReader();
            }
            else
            {
                switch (controlID)
                {
                    case "Load Level":
                        {
                            Common.ShowMouse(true);

                            if (openDia.ShowDialog() == DialogResult.OK)
                            {
                                string filepath = openDia.FileName;

                                recent.Add(filepath);

                                Crosswalk.Message message = new Crosswalk.Message()
                                {
                                    Messages = new string[]{ (filepath.Remove(filepath.Length -
                                    (openDia.SafeFileName.Length + 1), openDia.SafeFileName.Length + 1)) +
                                    InterOperations.LibraryFilepathExtention}
                                };

                                SerializationHelper.Serialize<Crosswalk.Message>(message, "Temp.temp", "");

                                LoadProject(filepath);

                                InterOperations.ProjectName = SerializationHelper.GetFolderName(
                                    filepath.Remove(filepath.Length -
                                    InterOperations.ProjectFileExtention.Length,
                                    InterOperations.ProjectFileExtention.Length));

                                int len = ".EPROJ".Length + InterOperations.ProjectName.Length;
                                InterOperations.RootFilepath = filepath.Remove(filepath.Length - len, len);

                                InterOperations.Project.ProjectRootFilepath = InterOperations.RootFilepath;
                                InterOperations.Project.ProjectName = InterOperations.ProjectName;

                                StartLibraryReader();
                            }

                            Common.ShowMouse(false);

                            SaveRecent();
                        }
                        break;

                    case "New Level":
                        {
                            Common.ShowMouse(true);

                            if (folderDia.ShowDialog() == DialogResult.OK)
                            {
                                string root = folderDia.SelectedPath;

                                InterOperations.RootFilepath = root + "\\";
                                InterOperations.Project = new Crosswalk.ProjectFile();
                                InterOperations.ProjectName = SerializationHelper.GetFolderName(root);
                                recent.Add(root + "\\" + InterOperations.ProjectName + InterOperations.ProjectFileExtention);

                                InterOperations.Project.ProjectRootFilepath = InterOperations.RootFilepath;
                                InterOperations.Project.ProjectName = InterOperations.ProjectName;
                                InterOperations.Project.Framework = new FrameworkCreation()
                                {
                                    AssemblyRefferences = new string[]
                                    {
                                        "Eon",
                                        "Eon.Rendering2D",
                                        "Eon.Rendering3D"
                                    },
                                    DefaultLanguage = "English",
                                    DefaultScreenResolution = 0,
                                    DefaultScreenSize = 0,
                                    DefaultTextureQuality = 2,
                                    FullScreen = true,
                                    TargetFramerate = 166,
                                    EngineComponents = new ParameterCollection[]
                                    {
                                        new ParameterCollection
                                        {
                                            ObjectType = "Eon.Rendering3D.Framework.Framework"
                                        },
                                        new ParameterCollection
                                        {
                                            ObjectType = "Eon.Rendering2D.Framework.Framework"
                                        }
                                    }
                                };

                                string lib = root + InterOperations.LibraryFilepathExtention;
                                SerializationHelper.CopyFiles(Environment.CurrentDirectory, lib, ".dll", "Eon.", true);

                                string content = root + EEDK.Crosswalk.InterOperations.ContentFilepathExtention + "\\Eon";
                                string eonRoot = Environment.CurrentDirectory + "\\Content\\Eon";
                                SerializationHelper.CopyFiles(eonRoot, content, ".xnb", true);

                                EEDK.Crosswalk.Message message = new Crosswalk.Message()
                                {
                                    Messages = new string[] { lib }
                                };

                                SerializationHelper.Serialize<EEDK.Crosswalk.Message>(message, "Temp.temp", "");

                                StartLibraryReader();
                            }

                            Common.ShowMouse(false);

                            SaveRecent();
                        }
                        break;

                    default:
                        Common.ExitGame();
                        break;
                }
            }
        }

        static void LoadProject(string filepath)
        {
            Type[] extraTypes = new Type[]
            {
                typeof(ObjectListing),
                typeof(FrameworkCreation)
            };

            InterOperations.Project =
                SerializationHelper.Deserialize<ProjectFile>(
                filepath, false, "", extraTypes);
        }

        void StartLibraryReader()
        {
            ProcessStartInfo start = new ProcessStartInfo("EEDK.LibraryReader.exe");

            Process reader = new Process();
            reader.StartInfo = start;
            reader.Start();

            reader.WaitForExit();

            Crosswalk.InterOperations.Project.CreatableObjects = 
                SerializationHelper.Deserialize<ObjectListing>("Listing.lst");

            SerializationHelper.DeleteFile("Listing.lst");

            SaveProject();

            SwitchScreen("MainScreen");
        }

        void SaveProject()
        {
            try
            {
                Type[] extraTypes = new Type[]
                {
                    typeof(ObjectListing),
                    typeof(FrameworkCreation),
                    typeof(string)
                };

                SerializationHelper.Serialize<ProjectFile>(Crosswalk.InterOperations.Project,
                    InterOperations.RootFilepath + InterOperations.ProjectName + ".EPROJ", extraTypes);

                SerializationHelper.Serialize<FrameworkCreation>(InterOperations.Project.Framework,
                    InterOperations.RootFilepath + "Content//Eon.Engine");
            }
            catch { }
        }

        void SaveRecent()
        {
            Type[] extraTypes = new Type[]
                {
                    typeof(List<string>)
                };

            SerializationHelper.Serialize<RecentFile>(recent, "Recent.temp", extraTypes);
        }
    }
}
