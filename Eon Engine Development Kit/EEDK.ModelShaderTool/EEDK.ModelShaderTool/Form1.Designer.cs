namespace EEDK.ModelShaderTool
{
    partial class Form1
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
            this.openDia = new System.Windows.Forms.OpenFileDialog();
            this.saveDia = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.txtMdlPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCollisionMdlPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPosZ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRotX = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRotY = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRotZ = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cboStatic = new System.Windows.Forms.ComboBox();
            this.btnAddLOD = new System.Windows.Forms.Button();
            this.lstLOD = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.table = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openDia
            // 
            this.openDia.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model Shader Tool";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoad,
            this.btnSave,
            this.btnExit});
            this.menuStrip1.Location = new System.Drawing.Point(662, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(259, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnLoad
            // 
            this.btnLoad.ForeColor = System.Drawing.Color.Red;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(55, 20);
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Red;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 20);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 20);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtMdlPath
            // 
            this.txtMdlPath.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMdlPath.Location = new System.Drawing.Point(16, 95);
            this.txtMdlPath.Name = "txtMdlPath";
            this.txtMdlPath.Size = new System.Drawing.Size(204, 23);
            this.txtMdlPath.TabIndex = 2;
            this.txtMdlPath.Click += new System.EventHandler(this.PathClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(71, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Model Filepath";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(39, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Collision Model Filepath";
            // 
            // txtCollisionMdlPath
            // 
            this.txtCollisionMdlPath.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCollisionMdlPath.Location = new System.Drawing.Point(16, 144);
            this.txtCollisionMdlPath.Name = "txtCollisionMdlPath";
            this.txtCollisionMdlPath.Size = new System.Drawing.Size(205, 23);
            this.txtCollisionMdlPath.TabIndex = 4;
            this.txtCollisionMdlPath.Click += new System.EventHandler(this.PathClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(31, 365);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Position Z";
            // 
            // txtPosZ
            // 
            this.txtPosZ.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosZ.Location = new System.Drawing.Point(16, 384);
            this.txtPosZ.Name = "txtPosZ";
            this.txtPosZ.Size = new System.Drawing.Size(99, 23);
            this.txtPosZ.TabIndex = 6;
            this.txtPosZ.TextChanged += new System.EventHandler(this.txtFloatChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(31, 311);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Position Y";
            // 
            // txtPosY
            // 
            this.txtPosY.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosY.Location = new System.Drawing.Point(16, 330);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(99, 23);
            this.txtPosY.TabIndex = 8;
            this.txtPosY.TextChanged += new System.EventHandler(this.txtFloatChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(31, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Position X";
            // 
            // txtPosX
            // 
            this.txtPosX.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosX.Location = new System.Drawing.Point(16, 275);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(99, 23);
            this.txtPosX.TabIndex = 10;
            this.txtPosX.TextChanged += new System.EventHandler(this.txtFloatChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(248, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Scale";
            // 
            // txtScale
            // 
            this.txtScale.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScale.Location = new System.Drawing.Point(227, 275);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(86, 23);
            this.txtScale.TabIndex = 16;
            this.txtScale.TextChanged += new System.EventHandler(this.txtFloatChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(132, 256);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "Rotation X";
            // 
            // txtRotX
            // 
            this.txtRotX.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRotX.Location = new System.Drawing.Point(121, 275);
            this.txtRotX.Name = "txtRotX";
            this.txtRotX.Size = new System.Drawing.Size(99, 23);
            this.txtRotX.TabIndex = 22;
            this.txtRotX.TextChanged += new System.EventHandler(this.txtFloatChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(132, 311);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 16);
            this.label11.TabIndex = 21;
            this.label11.Text = "Rotation Y";
            // 
            // txtRotY
            // 
            this.txtRotY.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRotY.Location = new System.Drawing.Point(121, 330);
            this.txtRotY.Name = "txtRotY";
            this.txtRotY.Size = new System.Drawing.Size(99, 23);
            this.txtRotY.TabIndex = 20;
            this.txtRotY.TextChanged += new System.EventHandler(this.txtFloatChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(132, 365);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 16);
            this.label12.TabIndex = 19;
            this.label12.Text = "Rotation Z";
            // 
            // txtRotZ
            // 
            this.txtRotZ.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRotZ.Location = new System.Drawing.Point(121, 384);
            this.txtRotZ.Name = "txtRotZ";
            this.txtRotZ.Size = new System.Drawing.Size(99, 23);
            this.txtRotZ.TabIndex = 18;
            this.txtRotZ.TextChanged += new System.EventHandler(this.txtFloatChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(232, 311);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 16);
            this.label13.TabIndex = 24;
            this.label13.Text = "Is Static ?";
            // 
            // cboStatic
            // 
            this.cboStatic.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatic.FormattingEnabled = true;
            this.cboStatic.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboStatic.Location = new System.Drawing.Point(227, 330);
            this.cboStatic.Name = "cboStatic";
            this.cboStatic.Size = new System.Drawing.Size(86, 24);
            this.cboStatic.TabIndex = 25;
            // 
            // btnAddLOD
            // 
            this.btnAddLOD.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddLOD.Location = new System.Drawing.Point(120, 173);
            this.btnAddLOD.Name = "btnAddLOD";
            this.btnAddLOD.Size = new System.Drawing.Size(100, 23);
            this.btnAddLOD.TabIndex = 26;
            this.btnAddLOD.Text = "Add LOD";
            this.btnAddLOD.UseVisualStyleBackColor = true;
            this.btnAddLOD.Click += new System.EventHandler(this.btnAddLOD_Click);
            // 
            // lstLOD
            // 
            this.lstLOD.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLOD.FormattingEnabled = true;
            this.lstLOD.ItemHeight = 16;
            this.lstLOD.Location = new System.Drawing.Point(236, 95);
            this.lstLOD.Name = "lstLOD";
            this.lstLOD.Size = new System.Drawing.Size(77, 100);
            this.lstLOD.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(233, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 28;
            this.label8.Text = "LOD Levels";
            // 
            // table
            // 
            this.table.AutoScroll = true;
            this.table.BackColor = System.Drawing.Color.Black;
            this.table.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.table.Location = new System.Drawing.Point(351, 76);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(567, 381);
            this.table.TabIndex = 29;
            this.table.WrapContents = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(930, 500);
            this.Controls.Add(this.table);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lstLOD);
            this.Controls.Add(this.btnAddLOD);
            this.Controls.Add(this.cboStatic);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtRotX);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRotY);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtRotZ);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtScale);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPosX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPosY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPosZ);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCollisionMdlPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMdlPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model Shader Tool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openDia;
        private System.Windows.Forms.SaveFileDialog saveDia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnLoad;
        private System.Windows.Forms.ToolStripMenuItem btnSave;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.TextBox txtMdlPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCollisionMdlPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPosZ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPosX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRotX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRotY;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRotZ;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboStatic;
        private System.Windows.Forms.Button btnAddLOD;
        private System.Windows.Forms.ListBox lstLOD;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel table;
    }
}

