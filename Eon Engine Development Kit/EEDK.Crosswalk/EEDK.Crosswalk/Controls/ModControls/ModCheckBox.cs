/* Created: 18/01/2015
 * Last Updated: 18/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EEDK.Crosswalk.Controls.ModControls
{
    /// <summary>
    /// DEfines a modified check box control.
    /// </summary>
    public class ModCheckBox : IModControl
    {
        TableLayoutPanel parent;
        CheckBox chk;

        int paramIdx;
        string name;

        public int ParamIndex
        {
            get { return paramIdx; }
        }

        public object ParamValue
        {
            get { return chk.Checked; }
        }

        public string Name
        {
            get { return name; }
        }

        public ModCheckBox(string name, bool value, int paramIndex,
            TableLayoutPanel parent, int row, string labelText)
        {
            this.name = name;
            this.paramIdx = paramIndex;
            this.parent = parent;

            AddCheckBox(row, labelText, value);
        }

        void AddCheckBox(int row, string text, bool value)
        {
            chk = new CheckBox();
            chk.Checked = value;
            chk.Text = text;
            chk.Dock = DockStyle.Left;
            chk.Font = new Font("Verdana", 9.75f);
            chk.ForeColor = Color.Red;
            chk.BackColor = Color.Transparent;
            chk.Width = 160;

            parent.Controls.Add(chk, 0, row);
        }

        public TableLayoutPanel Destroy(TableLayoutPanel panel)
        {
            panel.Controls.Remove(chk);

            return panel;
        }
    }
}
