/* Created: 12/01/2015
 * Last Updated: 12/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk;
using Eon.Engine;
using Eon.Engine.Audio;
using Eon.Helpers;
using Eon.Maths.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EEDK.JJaxCreator
{
    public partial class Form1 : Form
    {
        JJax jjaxFile;

        string filepath = "";

        string projectFilepath = "";
        string rootPath = "";

        public Form1()
        {
            InitializeComponent();

            ClearCueInfo();

            string filter = "J-Jax File (*.JJAX)|*.JJAX";

            saveDia.InitialDirectory = @"C:\";
            saveDia.Filter = filter;

            openDia.InitialDirectory = @"C:\";
            openDia.Filter = filter;

            openSndDia.InitialDirectory = @"C:\";
            openSndDia.Filter = "Built Content File (*.xnb)|*.xnb";

            jjaxFile = new JJax();

            jjaxFile.SoundCategories = new List<string>()
            {
                "Background"
            };

            lstCate.Items.Add("Background");

            try
            {
                Crosswalk.Message temp =
                    SerializationHelper.Deserialize<Crosswalk.Message>("Temp", false, ".temp");

                projectFilepath = temp.Messages[0];
                rootPath = temp.Messages[1];

                saveDia.InitialDirectory = rootPath + "Content\\";
                openDia.InitialDirectory = rootPath + "Content\\";
                openSndDia.InitialDirectory = rootPath + "Content\\";
            }
            catch { }
        }

        void ClearCueInfo()
        {
            cboLoops.SelectedIndex = 1;
            cboSingle.SelectedIndex = 1;

            txtCategory.Text = "Background";
            txtName.Text = "";
            txtVolume.Text = "" + 1.0f;
            txtFilepath.Text = "";
        }

        void lstCate_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lstCate.SelectedIndex != -1)
            {
                lstCue.Items.Clear();

                for (int i = 0; i < jjaxFile.Info.Count; i++)
                    if (jjaxFile.Info[i].CategoryName == (string)lstCate.SelectedItem)
                        lstCue.Items.Add(jjaxFile.Info[i].Name);
            }
        }

        void btnRemoveCate_Click(object sender, System.EventArgs e)
        {
            if (lstCate.SelectedIndex != -1 && jjaxFile.SoundCategories.Count > 0)
            {
                List<CueInfo> cues = new List<CueInfo>();

                for (int i = 0; i < jjaxFile.Info.Count; i++)
                    if (jjaxFile.Info[i].Name.Equals((string)lstCate.SelectedItem))
                        cues.Add(jjaxFile.Info[i]);

                for (int i = 0; i < cues.Count; i++)
                    jjaxFile.Info.Remove(cues[i]);

                jjaxFile.SoundCategories.Remove((string)lstCate.SelectedItem);

                lstCue.Items.Clear();
                cues.Clear();

                lstCate.Items.Remove(lstCate.SelectedItem);
            }
        }

        void btnRemoveCue_Click(object sender, System.EventArgs e)
        {
            if (lstCue.SelectedIndex != -1 && jjaxFile.Info.Count > 0)
            {
                int i = 0;
                bool found = false;

                while (i < jjaxFile.Info.Count && !found)
                {
                    if (jjaxFile.Info[i].Name == (string)lstCue.SelectedItem)
                    {
                        jjaxFile.Info.Remove(jjaxFile.Info[i]);
                        found = true;
                    }

                    i++;
                }

                ClearCueInfo();
            }
        }

        void txtVolume_TextChanged(object sender, System.EventArgs e)
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

        void btnClear_Click(object sender, System.EventArgs e)
        {
            ClearCueInfo();
        }

        void lstCue_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lstCue.SelectedIndex != -1)
            {
                CueInfo c = null;

                for (int i = 0; i < jjaxFile.Info.Count; i++)
                    if (jjaxFile.Info[i].Name == (string)lstCue.SelectedItem)
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
                    txtName.Text = c.Name;
                    txtVolume.Text = "" + c.Volume;
                    txtFilepath.Text = c.Filepath;
                }
            }
        }

        void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (txtCategory.Text != "" &&
                txtFilepath.Text != "" &&
                txtName.Text != "")
            {
                CueInfo cue = new CueInfo()
                {
                    CategoryName = txtCategory.Text,
                    Name = txtName.Text,
                    Filepath = txtFilepath.Text,
                    Volume = StringHelper.FloatFromString(txtVolume.Text),
                    Loop = StringHelper.BoolFromString((string)cboLoops.SelectedItem),
                    SingleInstance = StringHelper.BoolFromString((string)cboSingle.SelectedItem)
                };

                if (!jjaxFile.SoundCategories.Contains(txtCategory.Text))
                {
                    jjaxFile.SoundCategories.Add(txtCategory.Text);
                    lstCate.Items.Add(txtCategory.Text);
                }
                else if ((string)lstCate.SelectedItem == txtCategory.Text)
                    lstCue.Items.Add(cue.Name);

                bool exists = false;
                int i = 0;

                while (!exists && i < jjaxFile.Info.Count)
                {
                    if (jjaxFile.Info[i].Name == txtName.Text)
                    {
                        jjaxFile.Info.Remove(jjaxFile.Info[i]);
                        exists = true;
                    }

                    i++;
                }

                jjaxFile.Info.Add(cue);
            }
            else
                MessageBox.Show("All feilds must be filled.");
        }

        void btnLoad_Click(object sender, System.EventArgs e)
        {
            if (openDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (openDia.FileName != "")
                {
                    filepath = openDia.FileName;

                    Type[] extraTypes = new Type[]
                    {
                         typeof(List<string>),
                         typeof(List<CueInfo>)
                    };

                    this.filepath = openDia.FileName;

                    jjaxFile = SerializationHelper.Deserialize<JJax>(
                        filepath, false, "", extraTypes);

                    for (int i = 0; i < this.jjaxFile.SoundCategories.Count; i++)
                        lstCate.Items.Add(jjaxFile.SoundCategories[i]);
                }
        }

        void btnSave_Click(object sender, System.EventArgs e)
        {
            if (filepath == "")
            {
                if (saveDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    if (saveDia.FileName != "")
                    {
                        filepath = saveDia.FileName;

                        Type[] types = new Type[]
                        {
                            typeof(List<string>),
                            typeof(List<CueInfo>)
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

                        SerializationHelper.Serialize<JJax>(
                            jjaxFile, filepath, types);

                        if (projectFilepath != "")
                            UpdateProject();

                        MessageBox.Show("Save Successful.");
                    }
            }
        }

        void UpdateProject()
        {
            Type[] extraTypes = new Type[]
            {
                typeof(ObjectListing),
                typeof(FrameworkCreation),
                typeof(string[])
            };

            ProjectFile proj = SerializationHelper.Deserialize<ProjectFile>(
                projectFilepath, false, "", extraTypes);

            if (filepath.Contains("Content\\"))
            {
                string[] split = filepath.Split(new char[]
                {
                    '/','\\','.'
                }, StringSplitOptions.RemoveEmptyEntries);

                string path = "";
                int idx = 0;

                for (int i = 0; i < split.Length; i++)
                    if (split[i] == "Content")
                        idx = i + 1;

                for (int i = idx; i < split.Length - 1; i++)
                {
                    path += split[i];

                    if (i != split.Length - 2)
                        path += "\\";
                }

                FrameworkCreation frame = proj.Framework;
                frame.AudioMangerFilepath = path;

                proj.Framework = frame;

                SerializationHelper.Serialize<ProjectFile>(proj,
                    projectFilepath, extraTypes);

                SerializationHelper.Serialize<FrameworkCreation>(proj.Framework,
                    rootPath + "Content\\Eon.Engine");
            }
        }

        void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        void txtFilepath_Click(object sender, EventArgs e)
        {
            if (openSndDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (openSndDia.FileName != "")
                {
                    string sndFilename = openSndDia.FileName;

                    if (sndFilename.Contains("Content\\"))
                    {
                        string[] split = sndFilename.Split(new char[]
                        {
                            '/','\\','.'
                        }, StringSplitOptions.RemoveEmptyEntries);

                        string path = "";
                        int idx = 0;

                        for (int i = 0; i < split.Length; i++)
                            if (split[i] == "Content")
                                idx = i + 1;

                        for (int i = idx; i < split.Length - 1; i++)
                        {
                            path += split[i];

                            if (i != split.Length - 2)
                                path += "\\";
                        }

                        txtFilepath.Text = path;
                    }
                    else
                        txtFilepath.Text = sndFilename;
                }
        }
    }
}
