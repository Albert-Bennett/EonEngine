/* Created: 14/01/2015
 * Last Updated: 26/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk;
using Eon;
using Eon.Collections;
using Eon.Engine;
using Eon.Helpers;
using Eon.Rendering3D.Framework.Shaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EEDK.ModelShaderTool
{
    public partial class Form1 : Form
    {
        List<MaterialInfo> controls = new List<MaterialInfo>();
        List<string> meshNames = new List<string>();

        EonDictionary<string, string> materials = 
            new EonDictionary<string, string>();

        Crosswalk.Message temp;

        string filepath = "";

        List<string> coppiedDlls = new List<string>();

        ModelInfo ModelShaders
        {
            get
            {
                ModelInfo shaders = new ModelInfo();

                EonDictionary<string, Shader> shade =
                    new EonDictionary<string, Shader>();

                for (int i = 0; i < controls.Count; i++)
                    shade.Add(controls[i].MeshPartName,
                        controls[i].OutputShader);

                LODModelInfo mdl = new LODModelInfo()
                {
                    CollisionModelFilepath = txtCollisionMdlPath.Text,
                    ModelFilepath = txtMdlPath.Text,
                    Shaders = shade
                };

                shaders.Models = new EonDictionary<int, LODModelInfo>();
                shaders.Models.Add(0, mdl);

                shaders.PositionX = float.Parse(txtPosX.Text);
                shaders.PositionY = float.Parse(txtPosY.Text);
                shaders.PositionZ = float.Parse(txtPosZ.Text);

                shaders.RotationX = float.Parse(txtRotX.Text);
                shaders.RotationY = float.Parse(txtRotY.Text);
                shaders.RotationZ = float.Parse(txtRotZ.Text);

                shaders.Scale = float.Parse(txtScale.Text);

                shaders.IsStatic = bool.Parse((string)cboStatic.SelectedItem);

                return shaders;
            }
        }

        public Form1()
        {
            InitializeComponent();

            cboStatic.SelectedIndex = 1;

            string filter = ".FBX File (*.FBX)|*.FBX";

            openDia.InitialDirectory = @"C:\";
            openDia.Filter = filter;

            saveDia.InitialDirectory = @"C:\";
            saveDia.Filter = "Model Shader File (*.Shader)|*.Shader";

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

                materials = project.CreatableObjects.Shaders;

                for (int i = 0; i < materials.Count; i++)
                    if (!materials[i].Value.Contains("Eon."))
                    {
                        if (!coppiedDlls.Contains(materials[i].Value))
                        {
                            coppiedDlls.Add(materials[i].Value);

                            string path = temp.Messages[1] + "Libraries\\" + materials[i].Value;

                            File.Copy(path, materials[i].Value);
                        }
                    }
            }
            catch { }

            SetDefaults();
        }

        void SetDefaults()
        {
            txtPosX.Text = "0.0";
            txtPosY.Text = "0.0";
            txtPosZ.Text = "0.0";

            txtRotX.Text = "0.0";
            txtRotY.Text = "0.0";
            txtRotZ.Text = "0.0";

            txtScale.Text = "1.0";
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < coppiedDlls.Count; i++)
                File.Delete(coppiedDlls[i]);

            Close();
        }

        void txtFloatChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text.EndsWith("."))
                    (sender as TextBox).Text += "0";

                float f = 0;

                if (!float.TryParse((sender as TextBox).Text, out f))
                {
                    string s = (sender as TextBox).Text;

                    s = s.Remove(s.Length - 1, 1);

                    (sender as TextBox).Text = s;
                }
            }
            catch (StackOverflowException)
            {
                (sender as TextBox).Text = "0.0";
            }
        }

        void PathClicked(object sender, EventArgs e)
        {
            if (openDia.ShowDialog() == DialogResult.OK)
            {
                string path = openDia.FileName;

                if (path.Contains("Content\\"))
                {
                    string[] split = path.Split(new char[]
                    {
                        '.','\\','/'
                    }, StringSplitOptions.RemoveEmptyEntries);

                    int idx = 0;
                    bool found = false;

                    while (!found && idx < split.Length)
                    {
                        if (split[idx].Contains("Content"))
                            found = true;

                        idx++;
                    }

                    if (found)
                    {
                        path = "";

                        for (int i = idx; i < split.Length - 1; i++)
                        {
                            path += split[i];

                            if (i < split.Length - 2)
                                path += "\\";
                        }
                    }
                }

                if ((sender as TextBox).Name == "txtMdlPath")
                    ReadModel(openDia.FileName);

                (sender as TextBox).Text = path;
            }
        }

        void ReadModel(string path)
        {
            FileStream str = new FileStream(path, System.IO.FileMode.Open);
            StreamReader reader = new StreamReader(str);

            string line = reader.ReadLine();

            while (line != null)
            {
                if (line.Contains("Model:") &&
                    line.Contains("Model::") &&
                    line.Contains("Mesh"))
                {
                    string[] split = line.Split(new char[]
                    {
                        ':','"'
                    }, StringSplitOptions.RemoveEmptyEntries);

                    if (!meshNames.Contains(split[3]))
                        meshNames.Add(split[3]);

                    line = reader.ReadLine();
                }
                else
                    line = reader.ReadLine();
            }
        }

        void btnAddLOD_Click(object sender, EventArgs e)
        {
            if (meshNames.Count > 0)
                for (int i = 0; i < meshNames.Count; i++)
                    controls.Add(new MaterialInfo(table,
                        meshNames[i], materials, temp.Messages[1]));

            lstLOD.Items.Add(0);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (lstLOD.Items.Count > 0)
            {
                if (filepath == "")
                    if (saveDia.ShowDialog() == DialogResult.OK)
                        if (saveDia.FileName != "")
                        {
                            filepath = saveDia.FileName;

                            List<Type> extraTypes = new List<Type>();
                            extraTypes.Add(typeof(EonDictionary<string, Shader>));
                            extraTypes.Add(typeof(EonDictionary<int, LODModelInfo>));

                            for (int i = 0; i < materials.Count; i++)
                                extraTypes.Add(AssemblyManager.GetType(
                                    materials.Values[i], materials.Keys[i]));

                            SerializationHelper.Serialize<ModelInfo>(ModelShaders,
                                filepath, extraTypes.ToArray());
                        }
            }
        }

        void btnLoad_Click(object sender, EventArgs e)
        {
            filepath = "";

            //Clear all controls in the table.
            //Set controls in table to loaded file.
        }
    }
}
