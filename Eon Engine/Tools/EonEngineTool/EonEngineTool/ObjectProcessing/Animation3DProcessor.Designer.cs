namespace EonEngineTool.ObjectProcessing
{
    partial class Animation3DProcessor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Animation3DProcessor));
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModelFilepath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tabExit = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowzer = new System.Windows.Forms.FolderBrowserDialog();
            this.btnExport = new System.Windows.Forms.Button();
            this.lstAnimations = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.openDia = new System.Windows.Forms.OpenFileDialog();
            this.txtFPS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(12, 89);
            this.txtPath.MaxLength = 50;
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(330, 22);
            this.txtPath.TabIndex = 22;
            this.txtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPath.Click += new System.EventHandler(this.txtPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Output Path";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "3D Animation Processor";
            // 
            // txtModelFilepath
            // 
            this.txtModelFilepath.BackColor = System.Drawing.Color.White;
            this.txtModelFilepath.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModelFilepath.Location = new System.Drawing.Point(12, 165);
            this.txtModelFilepath.MaxLength = 50;
            this.txtModelFilepath.Name = "txtModelFilepath";
            this.txtModelFilepath.Size = new System.Drawing.Size(330, 22);
            this.txtModelFilepath.TabIndex = 24;
            this.txtModelFilepath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtModelFilepath.Click += new System.EventHandler(this.txtModelFilepath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Model Filepath";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip2.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.Font = new System.Drawing.Font("Furore", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabExit});
            this.menuStrip2.Location = new System.Drawing.Point(723, 9);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(65, 26);
            this.menuStrip2.TabIndex = 25;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // tabExit
            // 
            this.tabExit.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabExit.ForeColor = System.Drawing.Color.Red;
            this.tabExit.Name = "tabExit";
            this.tabExit.Size = new System.Drawing.Size(57, 22);
            this.tabExit.Text = "Exit";
            this.tabExit.Click += new System.EventHandler(this.tabExit_Click);
            // 
            // folderBrowzer
            // 
            this.folderBrowzer.SelectedPath = "C:\\Users\\Develpement\\Desktop";
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(496, 365);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(95, 27);
            this.btnExport.TabIndex = 28;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lstAnimations
            // 
            this.lstAnimations.BackColor = System.Drawing.Color.White;
            this.lstAnimations.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstAnimations.FormattingEnabled = true;
            this.lstAnimations.ItemHeight = 15;
            this.lstAnimations.Location = new System.Drawing.Point(496, 98);
            this.lstAnimations.Name = "lstAnimations";
            this.lstAnimations.Size = new System.Drawing.Size(227, 244);
            this.lstAnimations.TabIndex = 27;
            this.lstAnimations.SelectedIndexChanged += new System.EventHandler(this.lstAnimations_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(552, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "Animations";
            // 
            // btnExportAll
            // 
            this.btnExportAll.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportAll.Location = new System.Drawing.Point(597, 365);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(126, 27);
            this.btnExportAll.TabIndex = 29;
            this.btnExportAll.Text = "Export All";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // txtFPS
            // 
            this.txtFPS.BackColor = System.Drawing.Color.White;
            this.txtFPS.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFPS.Location = new System.Drawing.Point(12, 365);
            this.txtFPS.MaxLength = 50;
            this.txtFPS.Name = "txtFPS";
            this.txtFPS.Size = new System.Drawing.Size(111, 22);
            this.txtFPS.TabIndex = 31;
            this.txtFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFPS.TextChanged += new System.EventHandler(this.txtFPS_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(12, 345);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 17);
            this.label5.TabIndex = 30;
            this.label5.Text = "Frame Rate";
            // 
            // txtRename
            // 
            this.txtRename.BackColor = System.Drawing.Color.White;
            this.txtRename.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRename.Location = new System.Drawing.Point(302, 317);
            this.txtRename.MaxLength = 50;
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(162, 22);
            this.txtRename.TabIndex = 33;
            this.txtRename.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRename.TextChanged += new System.EventHandler(this.txtRename_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(348, 297);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 32;
            this.label6.Text = "Rename";
            // 
            // Animation3DProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(797, 437);
            this.Controls.Add(this.txtRename);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFPS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnExportAll);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lstAnimations);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.txtModelFilepath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Animation3DProcessor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Animation2DProcessor";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtModelFilepath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tabExit;
        private System.Windows.Forms.FolderBrowserDialog folderBrowzer;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ListBox lstAnimations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.OpenFileDialog openDia;
        private System.Windows.Forms.TextBox txtFPS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRename;
        private System.Windows.Forms.Label label6;
    }
}