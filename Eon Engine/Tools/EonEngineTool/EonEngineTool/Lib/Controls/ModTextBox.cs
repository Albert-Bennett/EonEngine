using System;
using System.Drawing;
using System.Windows.Forms;

namespace EonEngineTool.Lib.Controls
{
    public class ModTextBox<T> : IModControl
    {
        TableLayoutPanel parent;
        TextBox txt;
        string prevText = "";

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

        /// <summary>
        /// Created a new modified text box.
        /// </summary>
        /// <param name="obj">The obj value of the ModTextBox.</param>
        /// <param name="paramIndex">The index of the value to store.</param>
        public ModTextBox(T obj, int paramIndex,
            TableLayoutPanel parent, int row)
        {
            this.paramIndex = paramIndex;
            this.obj = obj;
            this.parent = parent;
            this.row = row;

            CreateTextBox();
        }

        public ModTextBox(T obj, int paramIndex,
            TableLayoutPanel parent, int row, string[] extentions, bool contentUsage)
            : this(obj, paramIndex, parent, row)
        {
            this.contentUsage = contentUsage;
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

        void txt_Click(object sender, EventArgs e)
        {
            if (openDia.ShowDialog() == DialogResult.OK)
                txt.Text = openDia.FileName;
        }

        void CreateTextBox()
        {
            txt = new TextBox();
            txt.Dock = DockStyle.Left;
            txt.Font = new Font("Furore", 9.75f);
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

                if (text.Contains(sourceContent))
                {
                    text = Helper.GetFilePath(text, sourceContent);

                    txt.Text = text;
                }
                else if (text.Contains(builtContent))
                {
                    text = Helper.GetFilePath(text, builtContent);

                    txt.Text = text;
                }
            }
        }
    }
}
