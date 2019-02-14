using Eon;
using Eon.Animation2D.Skeletal;
using Eon.Collections;
using Eon.Engine;
using Eon.Engine.Audio;
using Eon.Game.LevelManagement;
using Eon.Game2D.TileEngine.Management;
using Eon.Helpers;
using Eon.Particles;
using Eon.Particles.D2;
using Eon.Rendering3D.Framework.Shaders;
using Eon.UIApi;
using EonEngineTool.Lib;
using EonEngineTool.ObjectCreation.JJAX;
using EonEngineTool.ObjectCreation.Particles;
using EonEngineTool.ObjectCreation.Shader;
using EonEngineTool.ObjectCreation.Tilemap;
using EonEngineTool.ObjectProcessing;
using EonEngineTool.Utilities;
using System;
using System.Windows.Forms;

namespace EonEngineTool
{
    public partial class Form1 : Form
    {
        UserPrefrences pref;

        public Form1()
        {
            InitializeComponent();

            tabSave.Enabled = false;
            //tabContent.Enabled = false;

            openDia.InitialDirectory = Helper.InitialDirectory;
            openDia.DefaultExt = "";
            openDia.RestoreDirectory = true;
            openDia.Multiselect = false;
            openDia.FileName = "";

            try
            {
                pref = XmlHelper.Deserialize<UserPrefrences>("Prefrences", ".Pref", false);
                Helper.BuiltContentDirectory = pref.ContentFilepath;
            }
            catch
            {
                pref = new UserPrefrences();
                XmlHelper.Serialize<UserPrefrences>(pref, "Prefrences", ".Pref");
            }

            AssemblyManager.AddAssemblyRef(Helper.ParticleAssemblyRef);
        }

        #region Utilities

        private void tabExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        void SendData(string filepath, string assemblyRef)
        {
            SentData data = new SentData()
            {
                Filepath = filepath,
                ObjectType = assemblyRef
            };

            XmlHelper.Serialize<SentData>(data, pref.ContentFilepath + "Data", ".xml");
        }

        private void tabEditContent_Click(object sender, EventArgs e)
        {

        }

        private void tabPref_Click(object sender, EventArgs e)
        {
            UserPrefForm f2 = new UserPrefForm();
            f2.OnExit += new OnExitedChildFormEvent(ChildClosed);
            f2.OnSendBack += new OnSendBackObjectEvent(OnSendBack);
            f2.SetInfo(pref);

            f2.Show();

            Enabled = false;
        }

        void OnSendBack(object obj)
        {
            pref = obj as UserPrefrences;

            //Helper.BuildingContentFilepath = "";
            Helper.BuiltContentDirectory = pref.ContentFilepath;
        }

        private void ChildClosed()
        {
            Enabled = true;
        }

        #endregion
        #region New

        void newJJaxFile_Click(object sender, EventArgs e)
        {
            string filepath = CheckBrowzeOK();

            if (filepath != "")
            {
                JJAXForm f4 = new JJAXForm();
                f4.SetInfo(null, filepath, "");

                f4.Show();
            }
        }

        void new2DParticleSystem_Click(object sender, EventArgs e)
        {
            ParticleCreationForm f4 = new ParticleCreationForm();
            f4.SetInfo(new ParticleSystem2DInfo()
                {
                    Emitters = new EonDictionary<string, ParticleEmitterInfo>()
                });

            f4.Show();
        }

        void NewShader_Click(object sender, EventArgs e)
        {
            string filepath = CheckBrowzeOK();

            if (filepath != "")
            {
                ShaderForm f4 = new ShaderForm();
                f4.SetInfo(new ModelDefination(), filepath, "");

                f4.Show();
            }
        }

        private void newProj2D_Click(object sender, EventArgs e)
        {
            string filepath = CheckBrowzeOK();

            if (filepath != "")
            {
                //Level2DForm f4 = new Level2DForm();
                //f4.SetInfo(new LevelInfo(), filepath, "");

                //f4.Show();
            }
        }

        private void tabNewProj3D_Click(object sender, EventArgs e)
        {
            string filepath = CheckBrowzeOK();

            if (filepath != "")
            {
                //Level3DForm f4 = new Level3DForm();
                //f4.SetInfo(new LevelInfo(), filepath, "");

                //f4.Show();
            }
        }

        private void tabNewMenu_Click(object sender, EventArgs e)
        {
            string filepath = CheckBrowzeOK();

            if (filepath != "")
            {
                //MenuSystemForm f4 = new MenuSystemForm();
                //f4.SetInfo(new MenuSystemSetup(), filepath, "");

                //f4.Show();
            }
        }

        string CheckBrowzeOK()
        {
            if (browzeDia.ShowDialog() == DialogResult.OK)
                return browzeDia.SelectedPath + "/";

            return "";
        }

        #endregion
        #region Load

        void EditJJaxTool_Click(object sender, EventArgs e)
        {
            openDia.Filter = "J-Jax Files (*" + Helper.JJaxExtention
                + ")|*" + Helper.JJaxExtention;

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                JJax info = XmlHelper.Deserialize<JJax>(openDia.FileName, "", false);

                string filepath = GetFilepath();
                string name = GetFileName(Helper.ShaderExtention);

                JJAXForm f4 = new JJAXForm();
                f4.SetInfo(info, filepath, name);

                f4.Show();
            }
        }

        private void Load2DParticleSystem_Click(object sender, EventArgs e)
        {
            openDia.Filter = "2D Particle System Files (*" + Helper.Particle2DExtention
            + ")|*" + Helper.Particle2DExtention;

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                Type[] types = new Type[]
                    {
                        typeof(ParameterCollection),
                        typeof(string),
                        typeof(int),
                        typeof(float)
                    };

                ParticleSystem2DInfo info = XmlHelper.Deserialize<ParticleSystem2DInfo>(openDia.FileName, "", false, types);

                ParticleCreationForm f4 = new ParticleCreationForm();
                f4.SetInfo(info);

                f4.Show();
            }
        }

        void EditShader_Click(object sender, EventArgs e)
        {
            openDia.Filter = "Model Shader Files (*.Shader)|*.Shader";

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                ModelDefination info = XmlHelper.Deserialize<ModelDefination>(openDia.FileName, "", false);

                string filepath = GetFilepath();
                string name = GetFileName(Helper.ShaderExtention);

                ShaderForm f4 = new ShaderForm();
                f4.SetInfo(info, filepath, name);

                f4.Show();
            }
        }

        private void loadPro2DjTab_Click(object sender, EventArgs e)
        {
            openDia.Filter = "Level Files (*.Level)|*.Level";

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                LevelInfo info = XmlHelper.Deserialize<LevelInfo>(openDia.FileName, "", false);

                string filepath = GetFilepath();
                string name = GetFileName(Helper.LevelExtention);

                //Level2DForm f4 = new Level2DForm();
                //f4.SetInfo(info, filepath, name);

                //f4.Show();
            }
        }

        private void tabLoadProj3D_Click(object sender, EventArgs e)
        {
            openDia.Filter = "Level Files (*.Level)|*.Level";

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                LevelInfo info = XmlHelper.Deserialize<LevelInfo>(openDia.FileName, "", false);

                string filepath = GetFilepath();
                string name = GetFileName(Helper.LevelExtention);

                //Level3DForm f4 = new Level3DForm();
                //f4.SetInfo(info, filepath, name);

                //f4.Show();
            }
        }

        private void tabLoadMenu_Click(object sender, EventArgs e)
        {
            openDia.Filter = "Menu System Files (*.Menu)|*.Menu";

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                MenuSystemSetup info = XmlHelper.Deserialize<MenuSystemSetup>(openDia.FileName, "", false);

                string filepath = GetFilepath();

                string name = "MenuSystem";

                //MenuSystemForm f4 = new MenuSystemForm();
                //f4.SetInfo(info, filepath, name);

                //f4.Show();
            }
        }

        string GetFilepath()
        {
            string filepath = openDia.FileName;
            char[] char1 = filepath.ToCharArray();
            char[] char2 = openDia.SafeFileName.ToCharArray();
            int lenght = char1.Length - char2.Length;

            return filepath = filepath.Remove(lenght);
        }

        string GetFileName(string extention)
        {
            string name = openDia.SafeFileName;

            name = name.Remove(name.ToCharArray().Length -
                extention.ToCharArray().Length);

            return name;
        }

        #endregion
        #region Processing

        void Process3DAni_Click(object sender, EventArgs e)
        {
            Animation3DProcessor form4 = new Animation3DProcessor();
            form4.Show();
        }

        #endregion
        #region View

        private void tabViewMenu_Click(object sender, EventArgs e)
        {
            TileMapForm f2 = new TileMapForm();

            TileMapDeffination map = new TileMapDeffination();
            map.PostRender = true;

            map.TileLayers = new TileLayerDeffination[]
            {
                new TileLayerDeffination()
                {
                     Columns = 3,
                     Rows = 4,
                     TotalTileImages = 12,
                     TileSheetFilepath = "C:\\Users\\Develpement\\Pictures\\Backgrounds\\the_end_of_the_line__by_r_sraven-d3ch84i.jpg"
                },

                new TileLayerDeffination()
                {
                     Columns = 15,
                     Rows = 13,
                     TotalTileImages = 181,
                     TileSheetFilepath = "I:\\Super Human\\Source Content\\Tile Sheets\\Urban City\\Urban.png"
                }
            };

            f2.SetInfo(map, "");

            f2.Show();
        }

        #endregion
        #region Testing

        private void tabTestLevel_Click(object sender, EventArgs e)
        {
            openDia.Filter = "Level File (*.Level)|*.Level";

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                string safeName = openDia.SafeFileName;

                LevelInfo infoFile = XmlHelper.Deserialize<LevelInfo>(openDia.FileName);
                XmlHelper.Serialize<ParticleSystem2DInfo>(infoFile, Helper.BuildingContentFilepath + safeName, "");

                FrameworkCreation create = new FrameworkCreation();

                create.EngineComponents = new string[]
                    {
                        Helper.Physics2DAssembly,
                        Helper.Render2DManagerAssembly,
                        Helper.AnimaticManagerAssembly,
                        Helper.LevelManagerAssembly
                    };

                create.AssemblyRefferences = new string[]
                    {
                        Helper.Physics2DAssemblyRef,
                        Helper.Render2DAssemblyRef,
                        Helper.AnimaticAssemblyRef,
                        Helper.Animation2DAssemblyRef,
                        Helper.Game2DAssemblyRef,
                        Helper.GameAssemblyRef,
                        Helper.Physics2DAssemblyRef
                    };

                XmlHelper.Serialize<FrameworkCreation>(create, Helper.BuildingContentFilepath
                    + Helper.EngineManagerFilename, "");

                SendData(Helper.BuiltContentDirectory + safeName.Remove(safeName.Length -
                    Helper.LevelExtention.Length), "Eon.Game.LevelInfo");

                BeginTest();
            }
        }

        private void tabTestParticles_Click(object sender, EventArgs e)
        {
            openDia.Filter = "Particle System (*.Particles2D)|*.Particles2D" +
                            "|Particle System (*.Particles3D)|*.Particles3D";

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                string safeName = openDia.SafeFileName;

                if (safeName.Contains(Helper.Particle2DExtention))
                {
                    ParticleSystem2DInfo infoFile = XmlHelper.Deserialize<ParticleSystem2DInfo>(openDia.FileName);
                    XmlHelper.Serialize<ParticleSystem2DInfo>(infoFile, Helper.BuildingContentFilepath + safeName, "");

                    FrameworkCreation create = new FrameworkCreation();

                    create.EngineComponents = new string[]
                    {
                        Helper.Physics2DAssembly,
                        Helper.Render2DManagerAssembly
                    };

                    create.AssemblyRefferences = new string[]
                    {
                        Helper.Physics2DAssemblyRef,
                        Helper.Render2DAssemblyRef
                    };

                    XmlHelper.Serialize<FrameworkCreation>(create, Helper.BuildingContentFilepath
                        + Helper.EngineManagerFilename, "");

                    SendData(Helper.BuiltContentDirectory + safeName.Remove(safeName.Length -
                        Helper.Particle2DExtention.Length), "Eon.Particles2D.ParticleSystem2DInfo");
                }
                else if (safeName.Contains(Helper.Particle3DExtention))
                {
                    //3D stuff.
                }

                BeginTest();
            }
        }

        private void tabTestAni_Click(object sender, EventArgs e)
        {
            openDia.Filter = "2D Skeletal Animation (*.Skel)|*.Skel";

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                string safeName = openDia.SafeFileName;

                SkeletonDeff infoFile = XmlHelper.Deserialize<SkeletonDeff>(openDia.FileName);
                XmlHelper.Serialize<SkeletonDeff>(infoFile, Helper.BuildingContentFilepath + safeName, "");

                FrameworkCreation create = new FrameworkCreation();

                create.EngineComponents = new string[]
                    {
                        Helper.Animation2DAssemblyRef,
                        Helper.Render2DManagerAssembly
                    };

                create.AssemblyRefferences = new string[]
                    {
                        Helper.Render2DAssemblyRef
                    };

                XmlHelper.Serialize<FrameworkCreation>(create, Helper.BuildingContentFilepath + Helper.EngineManagerFilename, "");

                SendData(Helper.BuiltContentDirectory + safeName.Remove(safeName.Length -
                    Helper.Skeleton2DExtention.Length), "Eon.Animation2D.SkeletonDeff");

                BeginTest();
            }
        }

        private void tabTestMenuSystem_Click(object sender, EventArgs e)
        {
            openDia.Filter = "Menu System (*.Menu)|*.Menu";

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                string safeName = openDia.SafeFileName;

                MenuSystemSetup infoFile = XmlHelper.Deserialize<MenuSystemSetup>(openDia.FileName);
                XmlHelper.Serialize<MenuSystemSetup>(infoFile, Helper.BuildingContentFilepath + Helper.MenuManagerFilename, "");

                FrameworkCreation create = new FrameworkCreation();

                create.EngineComponents = new string[]
                    {
                        Helper.MenuManagerAssembly,
                        Helper.Render2DManagerAssembly
                    };

                create.AssemblyRefferences = new string[]
                    {
                        Helper.UIApiAssemblyRef,
                        Helper.Render2DAssemblyRef
                    };

                XmlHelper.Serialize<FrameworkCreation>(create, Helper.BuildingContentFilepath
                    + Helper.EngineManagerFilename, "");

                SendData("", "None");

                BeginTest();
            }
        }

        void BeginTest()
        {
            ProcessHelper.StartProcess("BuildLocalContent.exe", true);
            ProcessHelper.StartProcess("TestGame.exe", false);
        }

        #endregion
    }
}
