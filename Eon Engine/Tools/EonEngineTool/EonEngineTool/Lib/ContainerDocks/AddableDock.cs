using Eon.Collections;
using EonEngineTool.Lib.Controls;
using System.Windows.Forms;

namespace EonEngineTool.Lib.ContainerDocks
{
    /// <summary>
    /// Defines a selection dock that helps to
    /// manages the adding and subtracting of it from a list.
    /// </summary>
    public sealed class AddableDock : SelectionDock
    {
        Button btnRemove;

        public OnClickRelayEvent OnClick;

        public AddableDock(ParameterCollection obj, TableLayoutPanel parent,
            int row, string id, string[] options, string desc, string objectOptionsFolderPath)
            : base(obj, parent, row, id, options, desc, objectOptionsFolderPath) { }

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
