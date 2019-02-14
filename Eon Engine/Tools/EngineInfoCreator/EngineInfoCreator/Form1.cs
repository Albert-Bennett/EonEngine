using Eon.Engine;
using Eon.Helpers;
using System;
using System.Windows.Forms;

namespace EngineInfoCreator
{
    public partial class Form1 : Form
    {
        FrameworkCreation frame = new FrameworkCreation();

        string filepath = "";

        public Form1()
        {
            InitializeComponent();

            cboEngComps.SelectedIndex = 0;

            string filter = "Eon Engine System File (*.Engine)|*.Engine";

            //saveFileDialog1.InitialDirectory = @"C:\";
            //saveFileDialog1.Title = "Browse Files";
            //saveFileDialog1.Filter = filter;
        }

        void btnEngCompAdd_Click(object sender, EventArgs e)
        {
            if (cboEngComps.SelectedIndex > 0)
            {
                string comp = cboEngComps.SelectedItem as string;
                string assem = GetAssembly(comp);

                frame.EngineComponents = ArrayHelper.AddItem<string>(comp, frame.EngineComponents);
                frame.AssemblyRefferences = ArrayHelper.AddItem<string>(assem, frame.AssemblyRefferences);

                lstEngComps.Items.Add(comp);
                lstEngAssem.Items.Add(assem);

                cboEngComps.SelectedIndex = 0;
            }
        }

        void btnEngCompManualAdd_Click(object sender, EventArgs e)
        {
            if (txtManualEngCompAdd.Text != string.Empty)
            {
                string comp = txtManualEngCompAdd.Text;
                string assem = GetAssembly(comp);

                frame.EngineComponents = ArrayHelper.AddItem<string>(comp, frame.EngineComponents);
                frame.AssemblyRefferences = ArrayHelper.AddItem<string>(assem, frame.AssemblyRefferences);

                lstEngComps.Items.Add(comp);
                lstEngAssem.Items.Add(assem);
            }
        }

        string GetAssembly(string comp)
        {
            string[] names = comp.Split(new char[]
            {
                '.'
            }, StringSplitOptions.RemoveEmptyEntries);

            string name = "";

            for (int i = 0; i < 2; i++)
            {
                name += names[i];

                if (i != 1)
                    name += ".";
            }

            return name;
        }

        void btnAssemAddManual_Click(object sender, EventArgs e)
        {
            if (txtManualAssemRef.Text != string.Empty)
            {
                string assem = txtManualAssemRef.Text;

                frame.AssemblyRefferences = ArrayHelper.AddItem<string>(assem, frame.AssemblyRefferences);
                lstEngAssem.Items.Add(assem);
            }
        }

        void btnEngRemove_Click(object sender, EventArgs e)
        {
            if (lstEngComps.Items.Count > 0)
            {
                string selected = lstEngComps.SelectedItem as string;

                lstEngComps.Items.Remove(selected);
                frame.EngineComponents = ArrayHelper.RemoveItem<string>(selected, frame.EngineComponents);
            }
        }

        void btnEngAssemRemove_Click(object sender, EventArgs e)
        {
            if (lstEngAssem.Items.Count > 0)
            {
                string selected = lstEngAssem.SelectedItem as string;

                lstEngAssem.Items.Remove(selected);
                frame.AssemblyRefferences = ArrayHelper.RemoveItem<string>(selected, frame.AssemblyRefferences);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (frame.AssemblyRefferences.Length > 0 && frame.EngineComponents.Length > 0)
            {
                if (filepath == "")
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        filepath = folderBrowserDialog1.SelectedPath;

                        filepath += "//Eon.Engine";
                    }

                if (filepath != "")
                {
                    Type[] types = new Type[]
                    {
                         typeof(string[])
                    };

                    XmlHelper.Serialize<FrameworkCreation>(frame, filepath, types);
                }
            }
            else
                MessageBox.Show("No data to be saved");
        }
    }
}
