/* Created: 16/01/2015
 * Last Updated: 22/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk.Controls;
using EEDK.Crosswalk.Controls.ModControls;
using Eon;
using Eon.Collections;
using Eon.Rendering3D.Framework.Shaders;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace EEDK.ModelShaderTool
{
    /// <summary>
    /// Used to display controls that 
    /// define a model mesh in a model.
    /// </summary>
    public sealed class MaterialInfo
    {
        #region  Fields/ Properties

        TableLayoutPanel panel;

        Label endSign;
        ComboBox cboMaterials;
        Type matType;

        object shader;

        List<Label> labels = new List<Label>();
        List<IModControl> textBoxes = new List<IModControl>();

        EonDictionary<string, string> materials;

        int maxRow = 0;
        int startingRow;

        string meshPartName;
        string rootPath;

        public string MeshPartName
        {
            get { return meshPartName; }
        }

        /// <summary>
        /// The Shader for the mesh.
        /// </summary>
        public Shader OutputShader
        {
            get
            {
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    FieldInfo field = shader.GetType().GetField(textBoxes[i].Name);
                    Type type = field.FieldType;

                    field.SetValue(shader, textBoxes[i].ParamValue);
                }

                return (Shader)shader;
            }
        }

        #endregion
        #region Ctor

        public MaterialInfo(Control parent, string meshPartName,
            EonDictionary<string,string> materials, string rootPath)
        {
            this.meshPartName = meshPartName;
            this.materials = materials;
            this.rootPath = rootPath;

            panel = new TableLayoutPanel();
            panel.Parent = parent;

            panel.BackColor = System.Drawing.Color.Transparent;
            panel.AutoSize = true;
            panel.Dock = DockStyle.Top | DockStyle.Left;

            CreateEndLabel("MeshName: ", 0);
            CreateEndLabel(meshPartName, 1);

            CreateEndLabel("Render Type", 2);
            AddComboBox(2, materials.Keys);

            maxRow ++;
            CreateEndLabel("", maxRow);
            CreateEndLabel("", maxRow);

            startingRow = maxRow;
        }

        #endregion
        #region Control Creation

        Label CreateEndLabel(string text, int row)
        {
            Label label = ControlCreator.CreateLabel(text, DockStyle.Left);

            panel.Controls.Add(label, 0, row);
            maxRow = row;
            endSign = label;

            labels.Add(label);

            return label;
        }

        void AddComboBox(int row, List<string> items)
        {
            string[] names = GetNames(items);

            cboMaterials = ControlCreator.CreateComboBox(names, names[0], false);

            panel.Controls.Add(cboMaterials, 1, row);

            maxRow = row;

            cboMaterials.SelectedIndexChanged += cboMaterials_SelectedIndexChanged;
        }

        string[] GetNames(List<string> items)
        {
            string[] array = new string[items.Count];

            for (int i = 0; i < items.Count; i++)
            {
                string[] split = items[i].Split(new char[]
                {
                    '.'
                }, StringSplitOptions.RemoveEmptyEntries);

                array[i] = split[split.Length - 1];
            }

            return array;
        }

        void CreateTextBox(int row, Type T, string name)
        {
            if (T.Equals(typeof(System.Int32)))
            {
                ModTextBox<int> txt = new ModTextBox<int>(
                    0, name, 0, panel, row, null, false);

                textBoxes.Add(txt);
            }
            else if (T.Equals(typeof(String)))
            {
                bool findPath = false;
                string[] ext = null;

                if (name.Contains("Filepath"))
                {
                    findPath = true;

                    ext = new string[]
                    {
                        ".Png",".Jpg",".Tga",".Xnb"
                    };
                }

                ModTextBox<string> txt = new ModTextBox<string>(
                    "", name, 0, panel, row, ext, findPath);

                textBoxes.Add(txt);
            }
            else if (T.Equals(typeof(Single)))
            {
                ModTextBox<float> txt = new ModTextBox<float>(
                    0.0f, name, 0, panel, row, null, false);

                textBoxes.Add(txt);
            }
            else if (T.Equals(typeof(bool)))
            {
                ModTextBox<bool> txt = new ModTextBox<bool>(
                    false, name, 0, panel, row, null, false);

                textBoxes.Add(txt);
            }
            else if (T.Equals(typeof(Vector3)))
            {
                GroupedTextBox<float> txt = new GroupedTextBox<float>(
                     "", name, new float[] { 0, 0, 0 },
                     new string[] { "X", "Y", "Z" }, 0, panel, row);

                textBoxes.Add(txt);
            }
            else if (T.Equals(typeof(Vector2)))
            {
                GroupedTextBox<float> txt = new GroupedTextBox<float>(
                 "", name, new float[] { 0, 0 },
                 new string[] { "X", "Y" }, 0, panel, row);

                textBoxes.Add(txt);
            }
            else if (T.Equals(typeof(Vector4)))
            {
                GroupedTextBox<float> txt = new GroupedTextBox<float>(
                 "", name, new float[] { 0, 0, 0, 0 },
                 new string[] { "X", "Y", "Z", "W" }, 0, panel, row);

                textBoxes.Add(txt);
            }
            else if (T.Equals(typeof(Color)))
            {
                GroupedTextBox<byte> txt = new GroupedTextBox<byte>(
                 "", name, new byte[] { 0, 0, 0, 0 },
                 new string[] { "R", "G", "B", "A" }, 0, panel, row);

                textBoxes.Add(txt);
            }
        }

        void cboMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaterials.SelectedIndex != -1)
            {
                DestroyToStart();

                shader = AssemblyManager.CreateInstance(
                    materials.Values[cboMaterials.SelectedIndex],
                    materials.Keys[cboMaterials.SelectedIndex]);

                matType = shader.GetType();

                FieldInfo[] props = matType.GetFields();

                for (int i = 0; i < props.Length; i++)
                {
                    CreateEndLabel(props[i].Name, maxRow);
                    CreateTextBox(maxRow, props[i].FieldType, props[i].Name);

                    maxRow++;
                }

                CreateEndLabel("", maxRow);
            }
        }

        #endregion
        #region Destruction

        public void DestroyToStart()
        {
            for (int i = startingRow; i < labels.Count; i++)
                panel.Controls.Remove(labels[i]);

            for (int i = 0; i < textBoxes.Count; i++)
                panel = textBoxes[i].Destroy(panel);

            textBoxes.Clear();

            maxRow = startingRow;
        }

        public TableLayoutPanel Destroy()
        {
            panel.Controls.Clear();
            return panel;
        }

        #endregion
    }
}
