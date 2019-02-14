/* Created: 19/01/2015
 * Last Updated: 22/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk;
using Eon;
using Eon.Collections;
using Eon.Engine;
using Eon.Helpers;
using Eon.Particles;
using Eon.Particles.D3;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EEDK.ParticleCreationTool
{
    public partial class Form1 : Form
    {
        ParticleSystem3DInfo info;
        List<EmitterDock> emitters = new List<EmitterDock>();

        Crosswalk.Message temp;

        string filepath = "";

        public Form1()
        {
            InitializeComponent();

            saveDia.InitialDirectory = @"C:\";
            saveDia.Title = "Save File";
            saveDia.Filter = "3D Paritcle System File (*.Part3D)|*.Part3D";

            openDia.InitialDirectory = @"C:\";
            openDia.Title = "Browze Files";
            openDia.Filter = "3D Paritcle System File (*.Part3D)|*.Part3D";

            AssemblyManager.AddAssemblyRef("Eon.Particles");

            info = new ParticleSystem3DInfo();
            info.Emitters = new EonDictionary<string, ParticleEmitterInfo>();

            try
            {
                temp = SerializationHelper.Deserialize<Crosswalk.Message>("Temp", false, ".temp");

                Type[] extraTypes = new Type[]
                {
                    typeof(ObjectListing),
                    typeof(FrameworkCreation)
                };

                ProjectFile project =
                    SerializationHelper.Deserialize<ProjectFile>(
                    temp.Messages[0], false, "", extraTypes);

                FileSource.Attachments = project.CreatableObjects.ParticleAttachments;
                FileSource.Cycles = project.CreatableObjects.ParticleCycles;
                FileSource.EmittionTypes = project.CreatableObjects.ParticleEmitters3D;
                FileSource.RenderTypes = project.CreatableObjects.ParticleRenderMethods3D;

                FileSource.LoadAssemblies(temp.Messages[1]);
            }
            catch { }
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            FileSource.DeleteCoppiedDLLs();

            this.Close();
        }

        void btnLoad_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < emitters.Count; i++)
                emitters[i].Destroy();

            table.Controls.Clear();

            if (openDia.ShowDialog() == DialogResult.OK)
                if (openDia.FileName != "")
                {
                    filepath = openDia.FileName;

                    Type[] extraTypes = new Type[]
                    {
                        typeof(List<ParticleEmitterInfo>),
                        typeof(ParameterCollection[]),
                        typeof(ParameterCollection),
                        typeof(float)
                    };

                    info = SerializationHelper.Deserialize<ParticleSystem3DInfo>(
                        filepath, false, ".Part3D", extraTypes);

                    for (int i = 0; i < info.Emitters.Count; i++)
                    {
                        emitters.Add(new EmitterDock(table, info.Emitters[i].Value, info.Emitters[i].Key));
                        lstEmitters.Items.Add(info.Emitters[i].Key);
                    }
                }
        }

        void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstEmitters.SelectedIndex != -1)
            {
                EmitterDock emit = emitters[lstEmitters.SelectedIndex];
                emitters.Remove(emit);
                table.Controls.Remove(emit.Destroy());

                info.Emitters.Remove((string)lstEmitters.SelectedItem);

                lstEmitters.Items.RemoveAt(lstEmitters.SelectedIndex);
            }
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            ParticleEmitterInfo emitter = new ParticleEmitterInfo();

            string id = "Emitter" + lstEmitters.Items.Count;

            lstEmitters.Items.Add(id);

            emitters.Add(new EmitterDock(table, emitter, id));
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (filepath == "")
                if (saveDia.ShowDialog() == DialogResult.OK)
                    if (saveDia.FileName != "")
                        filepath = saveDia.FileName;

            info.Emitters.Clear();

            for (int i = 0; i < emitters.Count; i++)
                info.Emitters.Add(emitters[i].EmitterID, emitters[i].Info);

            if (FileSource.CoppiedDlls.Count > 0)
            {
                List<string> references = FileSource.CoppiedDlls;
                references.Add("Eon.Particles");

                info.AssemblyRefrences = references.ToArray();
            }

            Type[] types = new Type[]
                    {
                        typeof(ParameterCollection),
                        typeof(string),
                        typeof(int),
                        typeof(float)
                    };

            SerializationHelper.Serialize<ParticleSystem3DInfo>(info, filepath, types);

            MessageBox.Show("Save Successful.");
        }
    }
}
