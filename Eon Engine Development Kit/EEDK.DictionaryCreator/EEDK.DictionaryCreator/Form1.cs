/* Created: 12/01/2015
 * Last Updated: 12/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk;
using Eon.Engine;
using Eon.Engine.Languages;
using Eon.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EEDK.DictionaryCreator
{
    public partial class Form1 : Form
    {
        Dictionary dict = new Dictionary();
        List<Entry> entries = new List<Entry>();

        string filepath = "";

        string projectFilepath = "";
        string rootPath = "";

        public Form1()
        {
            InitializeComponent();

            saveDia.Filter = "Language Dictionary File (*.Dict)|*.Dict";
            openDia.Filter = "Language Dictionary File (*.Dict)|*.Dict";

            try
            {
                Crosswalk.Message temp =
                    SerializationHelper.Deserialize<Crosswalk.Message>("Temp", false, ".temp");

                projectFilepath = temp.Messages[0];
                rootPath = temp.Messages[1];

                saveDia.InitialDirectory = rootPath + "Content\\";
                openDia.InitialDirectory = rootPath + "Content\\";
            }
            catch { }
        }

        void lstOriginal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOriginal.SelectedIndex != -1)
                lstTranslated.SelectedIndex = lstOriginal.SelectedIndex;
        }

        void lstTranslated_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTranslated.SelectedIndex != -1)
                lstOriginal.SelectedIndex = lstTranslated.SelectedIndex;
        }

        void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstTranslated.SelectedIndex != -1)
            {
                int idx = lstTranslated.SelectedIndex;

                lstTranslated.Items.RemoveAt(idx);
                lstOriginal.Items.RemoveAt(idx);

                entries.RemoveAt(idx);
            }
        }

        void txtLanguage_TextChanged(object sender, EventArgs e)
        {
            if (dict.Langauage != txtLanguage.Text)
                dict.Langauage = txtLanguage.Text;
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtOriginal.Text != null && txtTranslated.Text != null)
            {
                entries.Add(new Entry()
                {
                    Word = txtOriginal.Text,
                    Translation = txtTranslated.Text
                });

                lstOriginal.Items.Add(txtOriginal.Text);
                lstTranslated.Items.Add(txtTranslated.Text);

                txtOriginal.Text = txtTranslated.Text = "";
            }
        }

        void BtnSave_Click(object sender, EventArgs e)
        {
            if (filepath == "")
            {
                saveDia.FileName = txtLanguage.Text + ".Dict";

                if (saveDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    if (saveDia.FileName != "")
                    {
                        filepath = saveDia.FileName;

                        dict.Entries = entries.ToArray();

                        Type[] extraTypes = new Type[]
                        {
                             typeof(Entry[]),
                             typeof(string)
                        };

                        SerializationHelper.Serialize<Dictionary>(dict,
                            filepath, extraTypes);

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
                    '/','\\'
                }, StringSplitOptions.RemoveEmptyEntries);

                string path = "";
                int idx = 0;

                for (int i = 0; i < split.Length; i++)
                    if (split[i] == "Content")
                        idx = i + 1;

                for (int i = idx; i < split.Length; i++)
                {
                    path += split[i];

                    if (i != split.Length - 1)
                        path += "\\";
                }

                FrameworkCreation frame = proj.Framework;

                if (frame.DictionaryFilepaths[0].Equals("NULL"))
                    frame.DictionaryFilepaths = new string[] { path };
                else
                {
                    List<string> paths = new List<string>(frame.DictionaryFilepaths);
                    paths.Add(path);

                    frame.DictionaryFilepaths = paths.ToArray();
                }

                proj.Framework = frame;

                SerializationHelper.Serialize<ProjectFile>(proj,
                    projectFilepath, extraTypes);

                SerializationHelper.Serialize<FrameworkCreation>(proj.Framework,
                    rootPath + "Content\\Eon.Engine");
            }
        }

        void BtnLoad_Click(object sender, EventArgs e)
        {
            if (openDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (openDia.FileName != "")
                {
                    filepath = openDia.FileName;

                    Type[] extraTypes = new Type[]
                    {
                         typeof(Entry[]),
                         typeof(string)
                    };

                    dict = SerializationHelper.Deserialize<Dictionary>(
                        filepath, false, "", extraTypes);

                    lstOriginal.Items.Clear();
                    lstTranslated.Items.Clear();

                    txtLanguage.Text = dict.Langauage;

                    for (int i = 0; i < dict.Entries.Length; i++)
                    {
                        lstOriginal.Items.Add(dict.Entries[i].Word);
                        lstTranslated.Items.Add(dict.Entries[i].Translation);
                    }

                    entries = new List<Entry>(dict.Entries);
                }
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
