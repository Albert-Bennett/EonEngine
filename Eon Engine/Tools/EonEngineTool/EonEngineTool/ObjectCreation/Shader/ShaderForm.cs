using Eon.Collections;
using Eon.Helpers;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Shaders;
using EonEngineTool.Lib;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace EonEngineTool.ObjectCreation.Shader
{
    public partial class ShaderForm : Form
    {
        List<ShaderInfo> controls = new List<ShaderInfo>();

        ModelDefination ModelShaders
        {
            get
            {
                ModelDefination shaders = new ModelDefination();
                shaders.Shaders = new EonDictionary<string, Eon.Rendering3D.Framework.Shaders.Shader>();

                for (int i = 0; i < controls.Count; i++)
                    shaders.Shaders.Add(
                        controls[i].MeshPartName,
                        controls[i].Info);

                shaders.CollisionModelFilepath = textBox1.Text;

                Vector3 pos = new Vector3()
                {
                    X = float.Parse(txtPosX.Text),
                    Y = float.Parse(txtPosY.Text),
                    Z = float.Parse(txtPosZ2.Text)
                };

                shaders.PositionalOffset = pos;

                Vector3 rot = new Vector3()
                {
                    X = float.Parse(txtRotX.Text),
                    Y = float.Parse(txtRotY.Text),
                    Z = float.Parse(txtRotZ.Text)
                };

                shaders.RotationalOffset = rot;

                shaders.Scale = float.Parse(txtScale.Text);

                return shaders;
            }
        }

        string filepath;

        public ShaderForm()
        {
            InitializeComponent();

            string filter = "Model Shader File (*" +
                Helper.ShaderExtention + ")|*" + Helper.ShaderExtention;

            saveDia.InitialDirectory = @"C:\";
            saveDia.Title = "Browse Files";
            saveDia.Filter = filter;

            openDia.InitialDirectory = @"C:\";
            openDia.Filter = filter;
            openDia.Title = "Browse Files";
        }

        public void SetInfo(ModelDefination shader, string filepath, string name)
        {
            this.filepath = filepath;

            txtName.Text = name;

            if (shader.Shaders.Count > 0)
                for (int i = 0; i < shader.Shaders.Count; i++)
                {
                    controls.Add(new ShaderInfo(table, shader.Shaders[i].Key,
                        shader.Shaders[i].Value));

                    lstMeshParts.Items.Add(shader.Shaders.Keys[i]);
                }

            textBox1.Text = shader.CollisionModelFilepath;

            txtPosX.Text = "" + shader.PositionalOffset.X;
            txtPosY.Text = "" + shader.PositionalOffset.Y;
            txtPosZ2.Text = "" + shader.PositionalOffset.Z;

            txtRotX.Text = "" + shader.RotationalOffset.X;
            txtRotY.Text = "" + shader.RotationalOffset.Y;
            txtRotZ.Text = "" + shader.RotationalOffset.Z;

            txtScale.Text = "" + shader.Scale;
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMeshPartName.Text != string.Empty)
            {
                bool exists = false;
                int idx = 0;

                while (!exists && idx < controls.Count)
                {
                    if (controls[idx].MeshPartName == txtMeshPartName.Text)
                        exists = true;

                    idx++;
                }

                if (!exists)
                {
                    Eon.Rendering3D.Framework.Shaders.Shader shader =
                        new Eon.Rendering3D.Framework.Shaders.Shader()
                        {
                            Parameters = new ShaderParameter[0]
                        };

                    controls.Add(new ShaderInfo(table, txtMeshPartName.Text, shader));

                    //modelShaders.Shaders.Add(txtMeshPartName.Text, shader);

                    lstMeshParts.Items.Add(txtMeshPartName.Text);

                    txtMeshPartName.Text = "";
                }
                else
                    txtMeshPartName.Text = "";
            }
        }

        void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstMeshParts.SelectedIndex != -1 && lstMeshParts.Items.Count > 0)
            {
                //modelShaders.Shaders.Remove(modelShaders.Shaders.Keys[lstMeshParts.SelectedIndex]);

                ShaderInfo shader = controls[lstMeshParts.SelectedIndex];
                controls.Remove(shader);

                table.Controls.Remove(shader.Destroy());

                lstMeshParts.Items.RemoveAt(lstMeshParts.SelectedIndex);
            }
        }

        void tabSave_Click(object sender, System.EventArgs e)
        {
            if (txtName.Text != "")
            {
                if (filepath == "")
                    if (saveDia.ShowDialog() == DialogResult.OK)
                        filepath = saveDia.FileName;

                string relativeFilePath = filepath +
                    txtName.Text + Helper.ShaderExtention;

                if (filepath != "")
                {
                    Type[] types = new Type[]
                    {
                        typeof(string),
                        typeof(ParameterTypes),
                        typeof(EonDictionary<string, Eon.Rendering3D.Framework.Shaders.Shader>),
                        typeof(ShaderParameter[]),
                        typeof(Vector3),
                        typeof(float),
                        typeof(RenderTypes)
                    };

                    XmlHelper.Serialize<ModelDefination>(ModelShaders, relativeFilePath, types);
                }
            }
        }

        void tabExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        void textFloatChanged(object sender, EventArgs e)
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
    }
}
