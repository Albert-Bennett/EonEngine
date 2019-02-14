using Eon.Rendering3D.Framework.Shaders;
using EonEngineTool.Lib.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EonEngineTool.ObjectCreation.Shader
{
    public sealed class ShaderParameterDock
    {
        ShaderParameter param;

        TableLayoutPanel table;

        List<TextBox> textBoxes = new List<TextBox>();
        ComboBox comboBox;
        Button remove;

        public OnRemoveShaderParameter OnClick;

        Label endLabel;

        int maxRow = 0;
        int initialRow;
        int index;
        string id;

        public ShaderParameter Info
        {
            get
            {
                param = new ShaderParameter()
                {
                    ParameterName = textBoxes[0].Text,
                    ValueString = textBoxes[1].Text,
                    ValueType = (ParameterTypes)Enum.Parse(typeof(ParameterTypes),
                        (string)comboBox.SelectedItem)
                };

                return param;
            }
        }

        public int MaxRow
        {
            get { return maxRow; }
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public string ID
        {
            get { return id; }
        }

        public ShaderParameterDock(ShaderParameter param,
            TableLayoutPanel table, int maxRow, string id)
        {
            this.param = param;
            this.maxRow = initialRow = maxRow;
            this.id = id;
            this.table = table;
        }

        public void Create()
        {
            table.Controls.Add(ControlCreator.CreateLabel("Parameter Name",
                 DockStyle.Left), 0, maxRow);

            TextBox txt1 = ControlCreator.CreateTextBox(param.ParameterName);

            textBoxes.Add(txt1);
            table.Controls.Add(txt1, 1, maxRow);

            maxRow++;

            table.Controls.Add(ControlCreator.CreateLabel("Value String",
                DockStyle.Left), 0, maxRow);

            TextBox txt2 = ControlCreator.CreateTextBox(param.ValueString);

            textBoxes.Add(txt2);
            table.Controls.Add(txt2, 1, maxRow);

            maxRow++;

            table.Controls.Add(ControlCreator.CreateLabel("Value Type",
                DockStyle.Left), 0, maxRow);

            string[] types = Enum.GetNames(typeof(ParameterTypes));

            comboBox = ControlCreator.CreateComboBox(types,
                Enum.GetName(typeof(ParameterTypes), param.ValueType), false);

            table.Controls.Add(comboBox, 1, maxRow);

            maxRow++;

            remove = ControlCreator.CreateButton("Remove", DockStyle.Left);
            remove.Click += remove_Click;

            table.Controls.Add(remove, 0, maxRow);

            maxRow++;

            EndCreate();
        }

        void remove_Click(object sender, EventArgs e)
        {
            if (OnClick != null)
                OnClick(this);
        }

        public void EndCreate()
        {
            if (endLabel != null)
                table.Controls.Remove(endLabel);

            maxRow += 2;

            endLabel = ControlCreator.CreateLabel("", DockStyle.None);
            table.Controls.Add(endLabel, 0, maxRow);
            maxRow++;
        }

        public TableLayoutPanel Destroy()
        {
            for (int i = initialRow; i <= maxRow; i++)
            {
                Control c = table.GetControlFromPosition(0, i);

                if (c != null)
                    table.Controls.Remove(c);

                c = table.GetControlFromPosition(1, i);

                if (c != null)
                    table.Controls.Remove(c);
            }

            if (endLabel != null)
                table.Controls.Remove(endLabel);

            textBoxes.Clear();

            return table;
        }
    }
}
