/* Created: 14/01/2015
 * Last Updated: 18/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace EEDK.Crosswalk.Controls
{
    /// <summary>
    /// A helper class for creating controls.
    /// </summary>
    public static class ControlCreator
    {
        public static Label CreateLabel(string text, DockStyle style)
        {
            Label label = new Label();
            label.Dock = style;
            label.Font = new Font("Verdana", 9.75f);

            label.Text = text;

            label.TextAlign = ContentAlignment.MiddleLeft;
            label.ForeColor = System.Drawing.Color.Red;
            label.BackColor = System.Drawing.Color.Transparent;
            label.Width = 160;

            return label;
        }

        public static Button CreateButton(string text, DockStyle dockStyle)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Verdana", 9.75f);

            btn.Dock = dockStyle;

            return btn;
        }

        public static ComboBox CreateComboBox(string[] types, string selected, bool areProperties)
        {
            ComboBox cbo = new ComboBox();

            for (int i = 0; i < types.Length; i++)
            {
                string name = "";

                if (areProperties)
                    name = PropertyHelper.GetName(types[i]);
                else
                    name = types[i];

                cbo.Items.Add(name);

                if (name.Contains(selected) || types[i].Contains(selected))
                {
                    cbo.SelectedIndex = i;
                    cbo.SelectedItem = selected;
                }
            }

            cbo.Dock = DockStyle.Left;
            cbo.Font = new Font("Verdana", 9.75f);
            cbo.Width = 150;

            return cbo;
        }

        public static TextBox CreateTextBox(string text)
        {
            TextBox textBox = new TextBox();
            textBox.Dock = DockStyle.Left;
            textBox.Font = new Font("Verdana", 9.75f);
            textBox.Text = text;

            return textBox;
        }

        public static TextBox CreateTextBox(string text, string name)
        {
            TextBox textBox = new TextBox();
            textBox.Name = name;
            textBox.Dock = DockStyle.Left;
            textBox.Font = new Font("Verdana", 9.75f);
            textBox.Text = text;

            return textBox;
        }
    }
}
