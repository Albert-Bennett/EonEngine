using Eon.Collections;
using EonEngineTool.Lib.Controls;
using System.Windows.Forms;

namespace EonEngineTool.Lib.ContainerDocks
{
    /// <summary>
    /// Defines a property dock that includes a 
    /// combo box that when changed changes the
    /// parameters of the object.
    /// </summary>
    public class SelectionDock : PropertyDock
    {
        ComboBox cbo;
        Label descLabel;

        string[] options;
        string selected;
        string desc;

        string objectOptionsFolderPath;

        int index;

        public OnComboBoxSelectedChangedEvent OnSelectionChanged;

        /// <summary>
        /// The index of this SelectionDock in a collection.
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public SelectionDock(ParameterCollection obj, TableLayoutPanel parent,
            int row, string id, string[] options, string desc, string objectOptionsFolderPath)
            : base(obj, parent, row, id)
        {
            this.options = options;
            this.desc = desc;
            this.objectOptionsFolderPath = objectOptionsFolderPath;

            this.selected = PropertyHelper.GetTypeName(obj.ObjectType);
        }

        public override void Create()
        {
            descLabel = ControlCreator.CreateLabel(desc, DockStyle.Left);
            table.Controls.Add(descLabel, 0, maxRow);

            cbo = ControlCreator.CreateComboBox(options, selected, true);
            cbo.SelectedIndexChanged += cbo_SelectedIndexChanged;
            table.Controls.Add(cbo, 1, maxRow);

            maxRow++;

            base.Create();
        }

        void cbo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Destroy();

            maxRow = initialRow;
            maxRow++;

            string path = objectOptionsFolderPath;
            path += PropertyHelper.GetClassName((string)cbo.SelectedItem);

            obj = new ParameterCollection();
            obj.ObjectType = path;

            Create();

            if (OnSelectionChanged != null)
                OnSelectionChanged(this);
        }

        public override TableLayoutPanel Destroy()
        {
            table.Controls.Remove(cbo);
            table.Controls.Remove(descLabel);

            return base.Destroy();
        }
    }
}
