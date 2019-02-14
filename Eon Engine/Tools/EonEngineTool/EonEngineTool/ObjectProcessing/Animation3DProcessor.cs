using Eon.Animation3D.Animating;
using Eon.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EonEngineTool.ObjectProcessing
{
    public partial class Animation3DProcessor : Form
    {
        List<Animation> animations = new List<Animation>();

        public Animation3DProcessor()
        {
            InitializeComponent();

            string filter = ".FBX File (*.FBX" + ")|*.FBX";

            openDia.InitialDirectory = @"C:\";
            openDia.Title = "Browse Files";
            openDia.Filter = filter;

            txtFPS.Text = "24";
        }

        void tabExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void txtModelFilepath_Click(object sender, EventArgs e)
        {
            if (openDia.ShowDialog() == DialogResult.OK)
            {
                txtModelFilepath.Text = openDia.FileName;

                //Thread th = new Thread(new ThreadStart(ReadFBXFile));
                //th.Start();

                ReadFBXFile();
            }
        }

        void ReadFBXFile()
        {
            FileStream str = new FileStream(txtModelFilepath.Text, System.IO.FileMode.Open);
            StreamReader reader = new StreamReader(str);

            string line = reader.ReadLine();

            while (line != null)
            {
                if (line.Contains("Takes:  {"))
                {
                    while (line != null)
                    {
                        if (line.Contains("Take: \""))
                        {
                            Animation ani = new Animation();

                            string name = GetAnimationName(line);
                            ani.Name = name;
                            lstAnimations.Items.Add(name);

                            int endCount = 0;

                            while (endCount < 3)
                            //while (!line.Contains("}"))
                            {
                                //Testing
                                if (line.Contains("}"))
                                    endCount++;

                                string ident = "Model: \"Model::";

                                if (line.Contains(ident))
                                {
                                    endCount = 0;

                                    line = line.Remove(0, ident.Length + 2);

                                    line = GetBoneName(line);

                                    BoneAnimation bone = new BoneAnimation();
                                    bone.BoneName = line;

                                    while (!line.Contains( "}"))
                                    {
                                        string transformationIdent = "Channel: \"Transform\" {";

                                        if (line.Contains(transformationIdent))
                                        {
                                            List<Vector3> positions = GetTransformationChanel("T", reader, out reader);
                                            List<Vector3> rotations = GetTransformationChanel("R", reader, out reader);
                                            List<Vector3> scales = GetTransformationChanel("S", reader, out reader);

                                            int count = positions.Count;

                                            if (rotations.Count > count)
                                                count = rotations.Count;

                                            if (scales.Count > count)
                                                count = scales.Count;

                                            for (int i = 0; i < count; i++)
                                            {
                                                int pi, ri, si = 0;

                                                if (positions.Count - 1 > i)
                                                    pi = i;
                                                else
                                                    pi = positions.Count - 1;

                                                if (rotations.Count - 1 > i)
                                                    ri = i;
                                                else
                                                    ri = rotations.Count - 1;

                                                if (scales.Count - 1 > i)
                                                    si = i;
                                                else
                                                    si = scales.Count - 1;

                                                bone.Add(positions[pi], rotations[ri], scales[si]);
                                            }
                                        }

                                        line = reader.ReadLine();
                                    }

                                    ani.BoneAnimations = ArrayHelper.AddItem<BoneAnimation>(bone, ani.BoneAnimations);
                                }

                                line = reader.ReadLine();
                            }

                            animations.Add(ani);
                        }

                        line = reader.ReadLine();
                    }
                }
                else
                    line = reader.ReadLine();
            }

            if (animations.Count == 0)
                MessageBox.Show("No animation data found.");

            reader.Close();
        }

        List<Vector3> GetTransformationChanel(string channelIdent,
            StreamReader reader, out StreamReader changedReader)
        {
            List<float> x = new List<float>();
            List<float> y = new List<float>();
            List<float> z = new List<float>();

            string line = reader.ReadLine();
            bool end = false;


            while (!end)
            {
                if (line.Contains("Channel: \"" + channelIdent + "\" {"))
                {
                    while (!line.Contains("}"))
                    {
                        line = reader.ReadLine();

                        if (line.Contains("Channel: \"X\" {"))
                        {
                            while (!line.Contains("Color:"))
                            {
                                if (line.Contains(",L"))
                                {
                                    string[] array = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                                    z.Add(float.Parse(array[1]));
                                }

                                line = reader.ReadLine();
                            }
                        }       
                    }

                    line = reader.ReadLine();

                    while (!line.Contains("}"))
                    {
                        if (line.Contains("Channel: \"Y\" {"))
                        {
                            while (!line.Contains("Color:"))
                            {
                                if (line.Contains(",L"))
                                {
                                    string[] array = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                                    x.Add(float.Parse(array[1]));
                                }

                                line = reader.ReadLine();
                            }
                        }

                        line = reader.ReadLine();
                    }

                    line = reader.ReadLine();

                    while (!line.Contains("}"))
                    {
                        if (line.Contains("Channel: \"Z\" {"))
                        {
                            while (!line.Contains("Color:"))
                            {
                                if (line.Contains(",L"))
                                {
                                    string[] array = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                                    y.Add(float.Parse(array[1]));
                                }

                                line = reader.ReadLine();

                            }
                        }

                        line = reader.ReadLine();
                    }

                    end = true;
                }

                if (!end)
                    line = reader.ReadLine();
            }

            List<Vector3> frames = new List<Vector3>();

            int count = x.Count;

            if (y.Count > count)
                count = y.Count;

            if (z.Count > count)
                count = z.Count;

            for (int i = 0; i < count; i++)
            {
                int xi, yi, zi = 0;

                if (x.Count - 1 > i)
                    xi = i;
                else
                    xi = x.Count - 1;

                if (y.Count - 1 > i)
                    yi = i;
                else
                    yi = y.Count - 1;

                if (z.Count - 1 > i)
                    zi = i;
                else
                    zi = z.Count - 1;

                frames.Add(new Vector3(x[xi], y[yi], z[zi]));
            }

            changedReader = reader;

            return frames;
        }

        string GetBoneName(string line)
        {
            char[] array = line.ToCharArray();

            int idx = 0;
            int i = 0;

            while (idx == 0)
            {
                if (array[i] == '"')
                    idx = i;

                i++;
            }

            line = line.Remove(idx, line.Length - idx);
            line = line.TrimEnd(new char[] { ' ' });

            return line;
        }

        string GetAnimationName(string line)
        {
            char[] array = line.ToCharArray();

            int idx = 0;
            int idx2 = 0;
            int i = 0;

            while (idx2 == 0)
            {
                if (array[i] == '"')
                    if (idx == 0)
                        idx = i;
                    else
                        idx2 = i;

                i++;
            }

            line = line.Remove(idx2, line.Length - idx2);
            line = line.Remove(0, idx + 1);
            line = line.TrimEnd(new char[] { ' ' });

            return line;
        }

        void txtPath_Click(object sender, EventArgs e)
        {
            if (folderBrowzer.ShowDialog() == DialogResult.OK)
                txtPath.Text = folderBrowzer.SelectedPath;
        }

        void lstAnimations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAnimations.SelectedIndex != -1)
            {
                btnExport.Enabled = true;

                txtRename.Text = (string)lstAnimations.SelectedItem;
            }
            else
            {
                btnExport.Enabled = false;

                txtRename.Text = "";
            }
        }

        void btnExport_Click(object sender, EventArgs e)
        {
            string animationName = (string)lstAnimations.SelectedItem;

            Export(animationName);

            lstAnimations.Items.Remove(animationName);

            animations.Remove(animations[lstAnimations.SelectedIndex]);

            btnExport.Enabled = false;

            txtRename.Text = "";
            txtModelFilepath.Text = "";
        }

        void btnExportAll_Click(object sender, EventArgs e)
        {
            if (lstAnimations.Items.Count > 0)
                for (int i = 0; i < lstAnimations.Items.Count; i++)
                    Export((string)lstAnimations.Items[i]);

            animations.Clear();
            lstAnimations.Items.Clear();

            txtRename.Text = "";
            txtModelFilepath.Text = "";
        }

        void Export(string animationName)
        {
            if (txtPath.Text != null)
            {
                if (txtFPS.Text != "0")
                {
                    for (int i = 0; i < animations.Count; i++)
                        if (animations[i].Name == animationName)
                        {
                            string relativeFilePath = txtPath.Text + "/" +
                                animationName + ".Anim3D";

                            Animation ani = animations[i];

                            int fps = int.Parse(txtFPS.Text);

                            ani.FrameRate = 1.0f / (float)fps;

                            Type[] types = new Type[]
                            {
                                typeof(Vector3),
                                typeof(Animation),
                                typeof(Transform[]),
                                typeof(string),
                                typeof(float),
                                typeof(BoneAnimation[])
                            };

                            XmlHelper.Serialize<Animation>(animations[i], relativeFilePath, types);

                            MessageBox.Show("File Saved.");
                        }
                }
                else
                    MessageBox.Show("No frame rate given");
            }
            else
                MessageBox.Show("No output path given.");
        }

        void txtFPS_TextChanged(object sender, EventArgs e)
        {
            int fps = 0;

            if (!int.TryParse(txtFPS.Text, out fps))
                txtFPS.Text = "0";
        }

        void txtRename_TextChanged(object sender, EventArgs e)
        {
            if (lstAnimations.SelectedIndex != -1)
            {
                string name = (string)lstAnimations.SelectedItem;

                for (int i = 0; i < animations.Count; i++)
                    if (animations[i].Name == name)
                        animations[i].Name = txtRename.Text;

                lstAnimations.Items[lstAnimations.SelectedIndex] = txtRename.Text;
            }
        }
    }
}
