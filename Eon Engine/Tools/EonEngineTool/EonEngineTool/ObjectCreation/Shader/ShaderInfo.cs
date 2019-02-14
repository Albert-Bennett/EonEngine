using Eon.Helpers;
using Eon.Rendering3D;
using Eon.Rendering3D.Framework.Shaders;
using EonEngineTool.Lib.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EonEngineTool.ObjectCreation.Shader
{
    /// <summary>
    /// Used to display a series of controls that defines a Shader.
    /// </summary>
    public sealed class ShaderInfo
    {
        #region Properties

        TableLayoutPanel panel;

        Label endSign;
        Button btnAdd;
        ComboBox cboTypes;

        Eon.Rendering3D.Framework.Shaders.Shader info;

        List<TextBox> textBoxes = new List<TextBox>();
        List<ShaderParameterDock> docks = new List<ShaderParameterDock>();

        int maxRow = 0;
        int startingRow;
        int maxOrder;

        string meshPartName;

        public string MeshPartName
        {
            get { return meshPartName; }
        }

        public Eon.Rendering3D.Framework.Shaders.Shader Info
        {
            get
            {
                List<ShaderParameter> parameters =
                    new List<ShaderParameter>();

                for (int i = 0; i < docks.Count; i++)
                    parameters.Add(docks[i].Info);

                info.ShaderFilepath = textBoxes[0].Text;
                info.DefaultTechnique = textBoxes[1].Text;
                info.RenderType = (RenderTypes)cboTypes.SelectedIndex;

                info.Parameters = parameters.ToArray();

                return info;
            }
        }

        #endregion
        #region Ctor

        public ShaderInfo(Control parent, string meshPartName,
            Eon.Rendering3D.Framework.Shaders.Shader info)
        {
            this.info = info;
            this.meshPartName = meshPartName;

            panel = new TableLayoutPanel();
            panel.Parent = parent;

            panel.BackColor = Color.Transparent;
            panel.AutoSize = true;
            panel.Dock = DockStyle.Top | DockStyle.Left;

            CreateEndLabel("MeshName: ", 0);
            CreateEndLabel(meshPartName, 1);

            CreateEndLabel("Shader Filepath", 2);
            AddTextBox(2, "effectFilepath", info.ShaderFilepath);

            CreateEndLabel("Default Technique", 3);
            AddTextBox(3, "defaultTech", info.DefaultTechnique);

            CreateEndLabel("Render Type", 4);
            AddComboBox(4);

            maxRow += 2;
            CreateEndLabel("", maxRow);
            CreateEndLabel("", maxRow);

            startingRow = maxRow;

            for (int i = 0; i < info.Parameters.Length; i++)
                CreateShaderParameters(info.Parameters[i]);

            ReAddEnding();
        }

        void ReAddEnding()
        {
            panel.Controls.Remove(btnAdd);

            maxRow++;

            int row = maxRow;
            row--;

            btnAdd = CreateButton("Add", row, -1);
        }

        #endregion
        #region Control Displsy

        void CreateShaderParameters(ShaderParameter param)
        {
            maxRow++;

            ShaderParameterDock dock = new ShaderParameterDock(
                param, panel, maxRow, "Param" + docks.Count);

            dock.Index = maxOrder;
            dock.OnClick += new OnRemoveShaderParameter(btnRemoveClick);

            dock.Create();
            docks.Add(dock);

            maxRow = dock.MaxRow;
            maxOrder++;
        }

        void btnRemoveClick(ShaderParameterDock sender)
        {
            if (sender.Index == 0)
            {
                maxRow = startingRow;
                maxOrder = 0;
            }
            else
            {
                int prevIdx = sender.Index;
                prevIdx--;

                ShaderParameterDock prev = (from d in docks
                                            where d.Index == prevIdx
                                            select d).First();

                maxRow = prev.MaxRow;
                maxOrder = prev.Index;
            }

            info = Info;

            if (info.Parameters.Length > 0)
            {
                int idx = sender.Index;

                info.Parameters = ArrayHelper.RemoveAt<ShaderParameter>(idx, info.Parameters);
            }
            else
                info.Parameters = new ShaderParameter[0];

            if (docks.Count > 0)
            {
                for (int i = sender.Index; i < docks.Count; i++)
                    docks[i].Destroy();

                int count = 0;
                count = docks.Count - sender.Index;

                docks.RemoveRange(sender.Index, count);
                sender.Destroy();

                int idx = maxOrder++;
                idx -= 2;

                for (int i = idx; i < info.Parameters.Length; i++)
                    CreateShaderParameters(info.Parameters[i]);
            }

            ReAddEnding();
        }

        #endregion
        #region Control Creation

        Button CreateButton(string textID, int row, int index)
        {
            Button btn = ControlCreator.CreateButton("Add", DockStyle.Right);
            btn.Click += btn_Click;

            panel.Controls.Add(btn, 0, row);
            maxRow = row;

            return btn;
        }

        void AddComboBox(int row)
        {
            string[] types = new string[]
            {
                "LightingPrePass",
                "ForwardRender",
                "Transparency"
            };

            cboTypes = ControlCreator.CreateComboBox(types, types[0], false);

            panel.Controls.Add(cboTypes, 1, row);

            maxRow = row;
        }

        void btn_Click(object sender, System.EventArgs e)
        {
            ShaderParameter param = new ShaderParameter();

            CreateShaderParameters(param);

            ReAddEnding();
        }

        void AddTextBox(int row, string name, string text)
        {
            TextBox textBox = ControlCreator.CreateTextBox(text);

            panel.Controls.Add(textBox, 1, row);
            textBoxes.Add(textBox);

            maxRow = row;
        }

        Label CreateEndLabel(string text, int row)
        {
            Label label = ControlCreator.CreateLabel(text, DockStyle.Left);

            panel.Controls.Add(label, 0, row);
            maxRow = row;
            endSign = label;

            return label;
        }

        #endregion
        #region Destruction

        public TableLayoutPanel Destroy()
        {
            panel.Controls.Clear();
            return panel;
        }

        #endregion
    }
}
