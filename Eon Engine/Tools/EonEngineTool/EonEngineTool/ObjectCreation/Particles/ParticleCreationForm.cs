using Eon.Collections;
using Eon.Engine;
using Eon.Helpers;
using Eon.Particles;
using Eon.Particles.D2;
using EonEngineTool.Lib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EonEngineTool.ObjectCreation.Particles
{
    public partial class ParticleCreationForm : Form
    {
        ParticleSystem2DInfo info;
        List<EmitterInfo> emitters = new List<EmitterInfo>();

        public ParticleCreationForm()
        {
            InitializeComponent();

            string filter = "Paritcle System File (*" +
                Helper.Particle2DExtention + ")|*" + Helper.Particle2DExtention;

            saveDia.InitialDirectory = @"C:\";
            saveDia.Title = "Browse Files";
            saveDia.Filter = filter;

            openDia.InitialDirectory = @"C:\";
            openDia.Filter = filter;
            openDia.Title = "Browse Files";
        }

        public void SetInfo(ParticleSystem2DInfo info)
        {
            this.info = info;

            txtLyr.Text = "" + info.DrawLayer;
            chkPostDraw.Checked = info.PostRender;

            if (info.Emitters != null && info.Emitters.Count > 0)
                for (int i = 0; i < info.Emitters.Count; i++)
                {
                    emitters.Add(new EmitterInfo(table, info.Emitters[i].Value, info.Emitters[i].Key));
                    lstEmitters.Items.Add(info.Emitters[i].Key);
                }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstEmitters.SelectedIndex != -1)
            {
                EmitterInfo emit = emitters[lstEmitters.SelectedIndex];
                emitters.Remove(emit);
                table.Controls.Remove(emit.Destroy());

                info.Emitters.Remove((string)lstEmitters.SelectedItem);

                lstEmitters.Items.RemoveAt(lstEmitters.SelectedIndex);
            }
        }

        private void tabSave_Click(object sender, EventArgs e)
        {
            if (saveDia.ShowDialog() == DialogResult.OK)
            {
                string filepath = saveDia.FileName;

                int lyr = 0;

                if (int.TryParse(txtLyr.Text, out lyr))
                {
                    info.DrawLayer = lyr;

                    info.PostRender = chkPostDraw.Checked;

                    info.Emitters.Clear();

                    for (int i = 0; i < emitters.Count; i++)
                        info.Emitters.Add(emitters[i].EmitterID, emitters[i].Info);

                    Type[] types = new Type[]
                    {
                        typeof(ParameterCollection),
                        typeof(string),
                        typeof(int),
                        typeof(float)
                    };

                    XmlHelper.Serialize<ParticleSystem2DInfo>(info, filepath, types);

                    MessageBox.Show("Save Successful.");
                }
            }
        }

        private void tabExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tabTest_Click(object sender, EventArgs e)
        {
            if (openDia.ShowDialog() == DialogResult.OK)
            {
                string safeName = openDia.SafeFileName;

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

                ProcessHelper.StartProcess("BuildLocalContent.exe", true);
                ProcessHelper.StartProcess("TestGame.exe", false);
            }
        }

        void SendData(string filepath, string assemblyRef)
        {
            SentData data = new SentData()
            {
                Filepath = filepath,
                ObjectType = assemblyRef
            };

            XmlHelper.Serialize<SentData>(data, Helper.BuildingContentFilepath + "Data", ".xml");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ParticleEmitterInfo emitter = new ParticleEmitterInfo();

            string id = "Emitter" + lstEmitters.Items.Count;

            lstEmitters.Items.Add(id);

            //info.Emitters = ArrayHelper.AddItem<ParticleEmitterInfo>(emitter, info.Emitters);
            emitters.Add(new EmitterInfo(table, emitter, id));
        }
    }
}
