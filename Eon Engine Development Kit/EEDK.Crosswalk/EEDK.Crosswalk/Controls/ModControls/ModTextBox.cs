/* Created: 16/01/2015
 * Last Updated: 18/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EEDK.Crosswalk.Controls.ModControls
{
    /// <summary>
    /// Used to define a modified TextBox.
    /// </summary>
    /// <typeparam name="T">The type of object that the textbox
    /// will convert the text contained to.</typeparam>
    public class ModTextBox<T> : IModControl
    {
        TableLayoutPanel parent;
        TextBox txt;

        string prevText = "";
        string name;

        int paramIndex = 0;
        T obj = default(T);
        int row;

        bool contentUsage = false;

        OpenFileDialog openDia;
        bool disableEdit = false;

        /// <summary>
        /// The index of the parameter 
        /// that this hold a value for.
        /// </summary>
        public int ParamIndex
        {
            get { return paramIndex; }
        }

        /// <summary>
        /// The name of the ModTextBox.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The value of the object to be held.
        /// </summary>
        public object ParamValue
        {
            get
            {
                if (disableEdit)
                    return (T)Convert.ChangeType(txt.Text, typeof(T));
                else
                    return obj;
            }
        }

        internal TextBox TextBox
        {
            get { return txt; }
        }

        /// <summary>
        /// Created a new modified text box.
        /// </summary>
        /// <param name="obj">The obj value of the ModTextBox.</param>
        /// <param name="paramIndex">The index of the value to store.</param>
        public ModTextBox(T obj, int paramIndex,
            TableLayoutPanel parent, int row)
        {
            this.obj = obj;
            this.parent = parent;
            this.row = row;

            CreateTextBox();
        }

        public ModTextBox(T obj, string name, int paramIndex,
            TableLayoutPanel parent, int row,
            string[] extentions, bool contentUsage)
            : this(obj, paramIndex, parent, row)
        {
            this.contentUsage = contentUsage;
            this.name = name;

            if (extentions != null)
            {
                openDia = new OpenFileDialog();
                openDia.InitialDirectory = @"C:\";
                openDia.Title = "Browse Files";

                string ext = "";

                for (int i = 0; i < extentions.Length; i++)
                {
                    string start = extentions[i] +
                        " Files (*" + extentions[i] + ")|*" + extentions[i];

                    if (i != extentions.Length - 1)
                        start += "|";

                    ext += start;
                }

                openDia.Filter = ext;
                openDia.RestoreDirectory = false;
                openDia.Multiselect = false;

                disableEdit = true;

                txt.Click += txt_Click;
            }
        }

        void txt_Click(object sender, EventArgs e)
        {
            if (openDia.ShowDialog() == DialogResult.OK)
                txt.Text = openDia.FileName;
        }

        void CreateTextBox()
        {
            txt = new TextBox();
            txt.Dock = DockStyle.Left;
            txt.Font = new Font("Verdana", 9.75f);
            txt.Text = (string)Convert.ChangeType(obj, typeof(string));
            txt.TextChanged += txt_TextChanged;

            parent.Controls.Add(txt, 1, row);
        }

        void txt_TextChanged(object sender, EventArgs e)
        {
            if (!disableEdit)
            {
                if (typeof(T) == typeof(float))
                {
                    float f = 0.0f;

                    if (float.TryParse(txt.Text, out f))
                    {
                        obj = (T)Convert.ChangeType(f, typeof(T));
                        prevText = txt.Text;
                    }
                    else
                        txt.Text = prevText;
                }
                else if (typeof(T) == typeof(int))
                {
                    int i = 0;

                    if (int.TryParse(txt.Text, out i))
                    {
                        obj = (T)Convert.ChangeType(i, typeof(T));
                        prevText = txt.Text;
                    }
                    else
                        txt.Text = prevText;
                }
                else
                    obj = (T)Convert.ChangeType(txt.Text, typeof(T));
            }

            if (contentUsage && typeof(T) == typeof(string))
            {
                string text = txt.Text;

                string sourceContent = "Content\\Source\\";
                string builtContent = "Content\\Built\\";
                string content = "Content\\";

                if (text.Contains(sourceContent))
                {
                    text = FilepathHelper.GetFilePath(text, sourceContent);

                    txt.Text = text;
                }
                else if (text.Contains(builtContent))
                {
                    text = FilepathHelper.GetFilePath(text, builtContent);

                    txt.Text = text;
                }
                else if (text.Contains(content))
                {
                    text = FilepathHelper.GetFilePath(text, content);

                    txt.Text = text;
                }
            }
        }

        public TableLayoutPanel Destroy(TableLayoutPanel panel)
        {
            panel.Controls.Remove(txt);

            return panel;
        }
    }
}
