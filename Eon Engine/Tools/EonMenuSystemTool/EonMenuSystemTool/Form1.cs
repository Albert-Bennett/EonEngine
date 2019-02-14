using Eon.Collections;
using Eon.Helpers;
using Eon.UIApi;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EonMenuSystemTool
{
    public partial class Form1 : Form
    {
        MenuSystemSetup file = new MenuSystemSetup();

        List<string> assemblies = new List<string>();
        List<ParameterCollection> parameters = new List<ParameterCollection>();
        List<string> screenNames = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        void btnAddScreen_Click(object sender, EventArgs e)
        {
            if (txtScreenName.Text != null && txtObjectType.Text != null)
            {
                lslScreens.Items.Add(txtScreenName.Text);

                ParameterCollection para = new ParameterCollection();
                para.ObjectType = txtObjectType.Text;

                parameters.Add(para);
                screenNames.Add(txtScreenName.Text);

                txtScreenName.Text = "";
                txtObjectType.Text = "";
            }
        }

        void btnAddAssembly_Click(object sender, EventArgs e)
        {
            if (txtAssem.Text != null)
            {
                assemblies.Add(txtAssem.Text);
                lslAssemblies.Items.Add(txtAssem.Text);

                txtAssem.Text = "";
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string filepath = "";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                filepath = folderBrowserDialog1.SelectedPath;

                filepath += "//MenuSystem.Menu";
            }

            Type[] types = new Type[]
                    {
                         typeof(string[]),
                         typeof(ParameterCollection[])
                    };

            file.Assemblies = assemblies.ToArray();
            file.ScreenNames = screenNames.ToArray();
            file.Screens = parameters.ToArray();

            XmlHelper.Serialize<MenuSystemSetup>(file, filepath, types);
        }
    }
}
