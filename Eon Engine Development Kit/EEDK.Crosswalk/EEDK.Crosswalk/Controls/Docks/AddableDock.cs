/* Created: 20/01/2015
 * Last Updated: 20/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk.Controls;
using Eon.Collections;
using System.Windows.Forms;

namespace EEDK.Crosswalk.Docks
{
    /// <summary>
    /// Defines a selection dock that helps to
    /// manages the adding and subtracting of it from a list.
    /// </summary>
    public sealed class AddableDock : SelectionDock
    {
        Button btnRemove;

        public ClickRelayEvent OnClick;

        public AddableDock(ParameterCollection obj, TableLayoutPanel parent,
            int row, string id, string[] options, string desc, string[] assemblyPaths)
            : base(obj, parent, row, id, options, desc, assemblyPaths) { }

        public override void Create()
        {
            base.Create();

            btnRemove = ControlCreator.CreateButton("Remove", DockStyle.Left);
            btnRemove.Click += btnRemove_Click;
            maxRow++;

            table.Controls.Add(btnRemove, 1, maxRow);

            EndCreate();
        }

        void btnRemove_Click(object sender, System.EventArgs e)
        {
            if (OnClick != null)
                OnClick(this);
        }

        public override TableLayoutPanel Destroy()
        {
            table.Controls.Remove(btnRemove);

            return base.Destroy();
        }
    }
}
