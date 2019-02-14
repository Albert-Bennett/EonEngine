using System.Drawing;
using System.Windows.Forms;

namespace EonEngineTool.Lib.Controls
{
    /// <summary>
    /// DEfines a modified check box control.
    /// </summary>
    public class ModCheckBox : IModControl
    {
        TableLayoutPanel parent;
        CheckBox chk;

        int paramIdx;

        public int ParamIndex
        {
            get { return paramIdx; }
        }

        public object ParamValue
        {
            get { return chk.Checked; }
        }

        public ModCheckBox(bool value, int paramIndex,
            TableLayoutPanel parent, int row, string name)
        {
            this.paramIdx = paramIndex;
            this.parent = parent;

            AddCheckBox(row, name, value);
        }

        void AddCheckBox(int row, string text, bool value)
        {
            chk = new CheckBox();
            chk.Checked = value;
            chk.Text = text;
            chk.Dock = DockStyle.Left;
            chk.Font = new Font("Furore", 11.25f);
            chk.ForeColor = Color.Red;
            chk.BackColor = Color.Transparent;
            chk.Width = 160;

            parent.Controls.Add(chk, 0, row);
        }
    }
}
