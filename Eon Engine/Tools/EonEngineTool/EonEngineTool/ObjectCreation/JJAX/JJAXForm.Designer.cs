namespace EonEngineTool.ObjectCreation.JJAX
{
    partial class JJAXForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JJAXForm));
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tabSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tabExit = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnRemoveCat = new System.Windows.Forms.Button();
            this.lstCategories = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRemoveCue = new System.Windows.Forms.Button();
            this.lstCues = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCueName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.txtFilepath = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.cboLoops = new System.Windows.Forms.ComboBox();
            this.cboSingle = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.saveDia = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowzer = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "J-Jax Creation Tool";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip2.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.Font = new System.Drawing.Font("Furore", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabSave,
            this.tabExit});
            this.menuStrip2.Location = new System.Drawing.Point(674, 9);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(130, 26);
            this.menuStrip2.TabIndex = 16;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // tabSave
            // 
            this.tabSave.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabSave.ForeColor = System.Drawing.Color.Red;
            this.tabSave.Name = "tabSave";
            this.tabSave.Size = new System.Drawing.Size(65, 22);
            this.tabSave.Text = "Save";
            this.tabSave.Click += new System.EventHandler(this.tabSave_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "File Name";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(12, 98);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(330, 22);
            this.txtName.TabIndex = 19;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRemoveCat
            // 
            this.btnRemoveCat.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveCat.Location = new System.Drawing.Point(50, 437);
            this.btnRemoveCat.Name = "btnRemoveCat";
            this.btnRemoveCat.Size = new System.Drawing.Size(86, 27);
            this.btnRemoveCat.TabIndex = 22;
            this.btnRemoveCat.Text = "Remove";
            this.btnRemoveCat.UseVisualStyleBackColor = true;
            this.btnRemoveCat.Click += new System.EventHandler(this.btnRemoveCat_Click);
            // 
            // lstCategories
            // 
            this.lstCategories.BackColor = System.Drawing.Color.White;
            this.lstCategories.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCategories.FormattingEnabled = true;
            this.lstCategories.ItemHeight = 15;
            this.lstCategories.Location = new System.Drawing.Point(12, 173);
            this.lstCategories.Name = "lstCategories";
            this.lstCategories.Size = new System.Drawing.Size(155, 244);
            this.lstCategories.TabIndex = 21;
            this.lstCategories.SelectedIndexChanged += new System.EventHandler(this.lstCategories_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(35, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "categories";
            // 
            // btnRemoveCue
            // 
            this.btnRemoveCue.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveCue.Location = new System.Drawing.Point(225, 437);
            this.btnRemoveCue.Name = "btnRemoveCue";
            this.btnRemoveCue.Size = new System.Drawing.Size(86, 27);
            this.btnRemoveCue.TabIndex = 25;
            this.btnRemoveCue.Text = "Remove";
            this.btnRemoveCue.UseVisualStyleBackColor = true;
            this.btnRemoveCue.Click += new System.EventHandler(this.btnRemoveCue_Click);
            // 
            // lstCues
            // 
            this.lstCues.BackColor = System.Drawing.Color.White;
            this.lstCues.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCues.FormattingEnabled = true;
            this.lstCues.ItemHeight = 15;
            this.lstCues.Location = new System.Drawing.Point(187, 173);
            this.lstCues.Name = "lstCues";
            this.lstCues.Size = new System.Drawing.Size(155, 244);
            this.lstCues.TabIndex = 24;
            this.lstCues.SelectedIndexChanged += new System.EventHandler(this.lstCues_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(239, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Cues";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(541, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 27;
            this.label5.Text = "Cue";
            // 
            // txtCueName
            // 
            this.txtCueName.BackColor = System.Drawing.Color.White;
            this.txtCueName.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCueName.Location = new System.Drawing.Point(517, 194);
            this.txtCueName.MaxLength = 50;
            this.txtCueName.Name = "txtCueName";
            this.txtCueName.Size = new System.Drawing.Size(171, 22);
            this.txtCueName.TabIndex = 26;
            this.txtCueName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(387, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "Category";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(389, 221);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 17);
            this.label7.TabIndex = 29;
            this.label7.Text = "Filepath";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(361, 345);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 17);
            this.label8.TabIndex = 30;
            this.label8.Text = "Single Instance";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(395, 282);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "Volume";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(402, 313);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 17);
            this.label10.TabIndex = 32;
            this.label10.Text = "Loops";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(402, 194);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 17);
            this.label11.TabIndex = 33;
            this.label11.Text = "Name";
            // 
            // txtVolume
            // 
            this.txtVolume.BackColor = System.Drawing.Color.White;
            this.txtVolume.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVolume.Location = new System.Drawing.Point(517, 282);
            this.txtVolume.MaxLength = 50;
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(171, 22);
            this.txtVolume.TabIndex = 34;
            this.txtVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVolume.TextChanged += new System.EventHandler(this.txtVolume_TextChanged);
            // 
            // txtFilepath
            // 
            this.txtFilepath.BackColor = System.Drawing.Color.White;
            this.txtFilepath.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilepath.Location = new System.Drawing.Point(517, 223);
            this.txtFilepath.MaxLength = 50;
            this.txtFilepath.Name = "txtFilepath";
            this.txtFilepath.Size = new System.Drawing.Size(171, 22);
            this.txtFilepath.TabIndex = 35;
            this.txtFilepath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCategory
            // 
            this.txtCategory.BackColor = System.Drawing.Color.White;
            this.txtCategory.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategory.Location = new System.Drawing.Point(517, 253);
            this.txtCategory.MaxLength = 50;
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(171, 22);
            this.txtCategory.TabIndex = 36;
            this.txtCategory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboLoops
            // 
            this.cboLoops.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoops.FormattingEnabled = true;
            this.cboLoops.Items.AddRange(new object[] {
            "true",
            "false"});
            this.cboLoops.Location = new System.Drawing.Point(544, 313);
            this.cboLoops.Name = "cboLoops";
            this.cboLoops.Size = new System.Drawing.Size(121, 23);
            this.cboLoops.TabIndex = 37;
            // 
            // cboSingle
            // 
            this.cboSingle.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSingle.FormattingEnabled = true;
            this.cboSingle.Items.AddRange(new object[] {
            "true",
            "false"});
            this.cboSingle.Location = new System.Drawing.Point(544, 341);
            this.cboSingle.Name = "cboSingle";
            this.cboSingle.Size = new System.Drawing.Size(121, 23);
            this.cboSingle.TabIndex = 38;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(468, 390);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 27);
            this.btnAdd.TabIndex = 39;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(574, 390);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(86, 27);
            this.btnClear.TabIndex = 40;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // JJAXForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(813, 476);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cboSingle);
            this.Controls.Add(this.cboLoops);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.txtFilepath);
            this.Controls.Add(this.txtVolume);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCueName);
            this.Controls.Add(this.btnRemoveCue);
            this.Controls.Add(this.lstCues);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRemoveCat);
            this.Controls.Add(this.lstCategories);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "JJAXForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JJAX Creation Tool";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tabSave;
        private System.Windows.Forms.ToolStripMenuItem tabExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnRemoveCat;
        private System.Windows.Forms.ListBox lstCategories;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRemoveCue;
        private System.Windows.Forms.ListBox lstCues;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCueName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.TextBox txtFilepath;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.ComboBox cboLoops;
        private System.Windows.Forms.ComboBox cboSingle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.SaveFileDialog saveDia;
        private System.Windows.Forms.FolderBrowserDialog folderBrowzer;
    }
}