using Eon.Helpers;
using EonEngineTool.Lib;
using System;
using System.Windows.Forms;

namespace EonEngineTool.Utilities
{
    public partial class UserPrefForm : Form
    {
        UserPrefrences curr;

        public OnExitedChildFormEvent OnExit;
        public OnSendBackObjectEvent OnSendBack;

        public UserPrefForm()
        {
            InitializeComponent();
            browser.ShowNewFolderButton = true;

            CenterToParent();
        }

        public void SetInfo(UserPrefrences current)
        {
            curr = current;

            txtContent.Text = curr.ContentFilepath;
            chkAuto.Checked = curr.AutoSave;
        }

        private void tabExit_Click(object sender, EventArgs e)
        {
            XmlHelper.Serialize<UserPrefrences>(curr, "Prefrences", ".Pref");

            this.Close();
        }

        private void txtContent_Click(object sender, EventArgs e)
        {
            if (browser.ShowDialog() == DialogResult.OK)
            {
                curr.ContentFilepath = browser.SelectedPath + "/";

                txtContent.Text = curr.ContentFilepath;
            }
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            curr.AutoSave = chkAuto.Checked;
        }

        private void UserPrefForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnSendBack != null)
                OnSendBack(curr);

            if (OnExit != null)
                OnExit();
        }
    }
}
