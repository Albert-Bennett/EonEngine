using Eon.Engine.Audio;
using Eon.Helpers;
using Eon.Maths.Helpers;
using EonEngineTool.Lib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EonEngineTool.ObjectCreation.JJAX
{
    public partial class JJAXForm : Form
    {
        string filepath;

        JJax jjaxFile;

        public JJAXForm()
        {
            InitializeComponent();

            ClearCueInfo();

            string filter = "J-Jax File (*" +
                 Helper.JJaxExtention + ")|*" + Helper.JJaxExtention;

            saveDia.InitialDirectory = @"C:\";
            saveDia.Title = "Browse Files";
            saveDia.Filter = filter;
        }

        public void SetInfo(JJax file, string filepath, string name)
        {
            this.jjaxFile = file;

            if (jjaxFile == null)
            {
                jjaxFile = new JJax();

                jjaxFile.SoundCategories = new List<string>()
                {
                    "Background"
                };

                jjaxFile.Info = new List<CueInfo>();
            }

            this.filepath = filepath;

            txtName.Text = name;

            for (int i = 0; i < this.jjaxFile.SoundCategories.Count; i++)
                lstCategories.Items.Add(jjaxFile.SoundCategories[i]);
        }

        void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCategories.SelectedIndex != -1)
            {
                lstCues.Items.Clear();

                for (int i = 0; i < jjaxFile.Info.Count; i++)
                    if (jjaxFile.Info[i].CategoryName == (string)lstCategories.SelectedItem)
                        lstCues.Items.Add(jjaxFile.Info[i].Name);
            }
        }

        void btnRemoveCat_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedIndex != -1 && jjaxFile.SoundCategories.Count > 0)
            {
                List<CueInfo> cues = new List<CueInfo>();

                for (int i = 0; i < jjaxFile.Info.Count; i++)
                    if (jjaxFile.Info[i].Name.Equals((string)lstCategories.SelectedItem))
                        cues.Add(jjaxFile.Info[i]);

                for (int i = 0; i < cues.Count; i++)
                    jjaxFile.Info.Remove(cues[i]);

                jjaxFile.SoundCategories.Remove((string)lstCategories.SelectedItem);

                lstCues.Items.Clear();
                cues.Clear();

                lstCategories.Items.Remove(lstCategories.SelectedItem);
            }
        }

        void btnRemoveCue_Click(object sender, EventArgs e)
        {
            if (lstCues.SelectedIndex != -1 && jjaxFile.Info.Count > 0)
            {
                int i = 0;
                bool found=false;

                while (i < jjaxFile.Info.Count && !found)
                {
                    if (jjaxFile.Info[i].Name == (string)lstCues.SelectedItem)
                    {
                        jjaxFile.Info.Remove(jjaxFile.Info[i]);
                        found = true;
                    }

                    i++;
                }

                ClearCueInfo();
            }
        }

        void txtVolume_TextChanged(object sender, EventArgs e)
        {
            float f = 0.0f;

            if (float.TryParse(txtVolume.Text, out f))
            {
                f = EonMathsHelper.Clamp(f, 0.1f, 1.0f);

                txtVolume.Text = "" + f;
            }
            else
                txtVolume.Text = "" + 0.1f;
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            ClearCueInfo();
        }

        void lstCues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCues.SelectedIndex != -1)
            {
                CueInfo c = null;

                for (int i = 0; i < jjaxFile.Info.Count; i++)
                    if (jjaxFile.Info[i].Name == (string)lstCues.SelectedItem)
                        c = jjaxFile.Info[i];

                if (c != null)
                {
                    if (c.Loop)
                        cboLoops.SelectedIndex = 0;
                    else
                        cboLoops.SelectedIndex = 1;

                    if (c.SingleInstance)
                        cboSingle.SelectedIndex = 0;
                    else
                        cboSingle.SelectedIndex = 1;

                    txtCategory.Text = c.CategoryName;
                    txtCueName.Text = c.Name;
                    txtVolume.Text = "" + c.Volume;
                    txtFilepath.Text = c.Filepath;
                }
            }
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCategory.Text != "" &&
                txtFilepath.Text != "" &&
                txtCueName.Text != "")
            {
                CueInfo cue = new CueInfo()
                {
                    CategoryName = txtCategory.Text,
                    Name = txtCueName.Text,
                    Filepath = txtFilepath.Text,
                    Volume = GetFloat(txtVolume.Text),
                    Loop = GetBool((string)cboLoops.SelectedItem),
                    SingleInstance = GetBool((string)cboSingle.SelectedItem)
                };

                if (!jjaxFile.SoundCategories.Contains(txtCategory.Text))
                {
                    jjaxFile.SoundCategories.Add(txtCategory.Text);
                    lstCategories.Items.Add(txtCategory.Text);
                }
                else if ((string)lstCategories.SelectedItem == txtCategory.Text)
                    lstCues.Items.Add(cue.Name);

                bool exists = false;
                int i = 0;

                while (!exists && i < jjaxFile.Info.Count)
                {
                    if (jjaxFile.Info[i].Name == txtCueName.Text)
                    {
                        jjaxFile.Info.Remove(jjaxFile.Info[i]);
                        exists = true;
                    }

                    i++;
                }

                jjaxFile.Info.Add(cue);
            }
            else
                MessageBox.Show("All fields must be filled.");
        }

        bool GetBool(string text)
        {
            bool b = false;

            if (bool.TryParse(text, out b))
                return b;

            return false;
        }

        float GetFloat(string text)
        {
            float f = 0.0f;

            if (float.TryParse(text, out f))
                return f;

            return 0.1f;
        }

        void ClearCueInfo()
        {
            cboLoops.SelectedIndex = 1;
            cboSingle.SelectedIndex = 1;

            txtCategory.Text = "Background";
            txtCueName.Text = "";
            txtVolume.Text = "" + 1.0f;
            txtFilepath.Text = "";
        }

        void tabSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                if (filepath == "")
                    if (folderBrowzer.ShowDialog() == DialogResult.OK)
                        filepath = folderBrowzer.SelectedPath;

                string relativeFilePath = filepath +
                    txtName.Text + Helper.JJaxExtention;

                if (filepath != "")
                {
                    Type[] types = new Type[]
                    {
                        typeof(List<string>),
                        typeof(List<CueInfo>),
                        typeof(string),
                        typeof(float),
                        typeof(bool)
                    };

                    jjaxFile.SoundCategories.Sort();

                    List<string> names = new List<string>();

                    for (int i = 0; i < jjaxFile.Info.Count; i++)
                        names.Add(jjaxFile.Info[i].Name);

                    names.Sort();

                    List<CueInfo> cues = new List<CueInfo>();

                    for (int i = 0; i < names.Count; i++)
                        for (int j = 0; j < jjaxFile.Info.Count; j++)
                            if (names[i] == jjaxFile.Info[j].Name)
                                cues.Add(jjaxFile.Info[j]);

                    jjaxFile.Info = cues;

                    XmlHelper.Serialize<JJax>(jjaxFile, relativeFilePath, types);

                    MessageBox.Show("File Saved.");
                }
            }
            else
                MessageBox.Show("No name given.");
        }

        void tabExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
