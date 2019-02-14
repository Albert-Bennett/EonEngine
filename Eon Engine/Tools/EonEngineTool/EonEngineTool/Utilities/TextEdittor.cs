using System;
using System.Windows.Forms;

namespace EonEngineTool.Utilities
{
    public partial class TextEdittor : Form
    {
        public OnSendBackLinesEvent OnSendLines;
        public OnSendBackTextEvent OnSendText;
        public OnCloseEvent OnClose;

        public string[] Keys
        {
            set
            {
                for (int i = 0; i < value.Length; i++)
                    lstKeys.Items.Add(value[i]);
            }
        }

        public TextEdittor()
        {
            InitializeComponent();
        }

        public void SetInfo(string[] text)
        {
            txtBox.Lines = text;
        }

        void tabExit_Click(object sender, EventArgs e)
        {
            if (OnClose != null)
                OnClose();

            this.Close();
        }

        void tabSave_Click(object sender, EventArgs e)
        {
            if (OnSendLines != null)
                OnSendLines(txtBox.Lines);
            else
                if (OnSendText != null)
                    OnSendText(txtBox.Text);
        }
    }
}
