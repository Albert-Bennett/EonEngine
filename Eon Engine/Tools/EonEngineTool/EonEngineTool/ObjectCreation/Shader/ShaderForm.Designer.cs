namespace EonEngineTool.ObjectCreation.Shader
{
    partial class ShaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShaderForm));
            this.openDia = new System.Windows.Forms.OpenFileDialog();
            this.saveDia = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tabExit = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstMeshParts = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.txtMeshPartName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.table = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.txtRotY = new System.Windows.Forms.TextBox();
            this.txtRotX = new System.Windows.Forms.TextBox();
            this.txtRotZ = new System.Windows.Forms.TextBox();
            this.txtPosZ = new System.Windows.Forms.TextBox();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPosZ2 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Furore", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabSave,
            this.tabExit});
            this.menuStrip1.Location = new System.Drawing.Point(674, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(130, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(232, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "Model Shader Creation";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(12, 79);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(263, 22);
            this.txtName.TabIndex = 14;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(114, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Name";
            // 
            // lstMeshParts
            // 
            this.lstMeshParts.BackColor = System.Drawing.Color.White;
            this.lstMeshParts.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMeshParts.FormattingEnabled = true;
            this.lstMeshParts.ItemHeight = 15;
            this.lstMeshParts.Location = new System.Drawing.Point(12, 223);
            this.lstMeshParts.Name = "lstMeshParts";
            this.lstMeshParts.Size = new System.Drawing.Size(155, 139);
            this.lstMeshParts.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(35, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Mesh Parts";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(101, 440);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 27);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(189, 223);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(86, 27);
            this.btnRemove.TabIndex = 19;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // txtMeshPartName
            // 
            this.txtMeshPartName.BackColor = System.Drawing.Color.White;
            this.txtMeshPartName.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMeshPartName.Location = new System.Drawing.Point(15, 412);
            this.txtMeshPartName.MaxLength = 500;
            this.txtMeshPartName.Name = "txtMeshPartName";
            this.txtMeshPartName.Size = new System.Drawing.Size(260, 22);
            this.txtMeshPartName.TabIndex = 20;
            this.txtMeshPartName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(69, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Mesh Part Name";
            // 
            // table
            // 
            this.table.AutoScroll = true;
            this.table.BackColor = System.Drawing.Color.Black;
            this.table.Location = new System.Drawing.Point(282, 43);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(522, 421);
            this.table.TabIndex = 22;
            this.table.WrapContents = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 137);
            this.textBox1.MaxLength = 50;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(263, 22);
            this.textBox1.TabIndex = 23;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(26, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(235, 17);
            this.label5.TabIndex = 24;
            this.label5.Text = "Collision Model Filepath";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(634, 517);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 25;
            this.label6.Text = "Scale";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(327, 517);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 17);
            this.label7.TabIndex = 26;
            this.label7.Text = "Rotational Offset";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(35, 517);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 17);
            this.label8.TabIndex = 27;
            this.label8.Text = "Positional Offset";
            // 
            // txtScale
            // 
            this.txtScale.BackColor = System.Drawing.Color.White;
            this.txtScale.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScale.Location = new System.Drawing.Point(674, 549);
            this.txtScale.MaxLength = 500;
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(58, 22);
            this.txtScale.TabIndex = 28;
            this.txtScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtScale.TextChanged += new System.EventHandler(this.textFloatChanged);
            // 
            // txtRotY
            // 
            this.txtRotY.BackColor = System.Drawing.Color.White;
            this.txtRotY.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRotY.Location = new System.Drawing.Point(384, 585);
            this.txtRotY.MaxLength = 500;
            this.txtRotY.Name = "txtRotY";
            this.txtRotY.Size = new System.Drawing.Size(81, 22);
            this.txtRotY.TabIndex = 29;
            this.txtRotY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRotY.TextChanged += new System.EventHandler(this.textFloatChanged);
            // 
            // txtRotX
            // 
            this.txtRotX.BackColor = System.Drawing.Color.White;
            this.txtRotX.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRotX.Location = new System.Drawing.Point(384, 548);
            this.txtRotX.MaxLength = 500;
            this.txtRotX.Name = "txtRotX";
            this.txtRotX.Size = new System.Drawing.Size(81, 22);
            this.txtRotX.TabIndex = 30;
            this.txtRotX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRotX.TextChanged += new System.EventHandler(this.textFloatChanged);
            // 
            // txtRotZ
            // 
            this.txtRotZ.BackColor = System.Drawing.Color.White;
            this.txtRotZ.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRotZ.Location = new System.Drawing.Point(384, 622);
            this.txtRotZ.MaxLength = 500;
            this.txtRotZ.Name = "txtRotZ";
            this.txtRotZ.Size = new System.Drawing.Size(81, 22);
            this.txtRotZ.TabIndex = 31;
            this.txtRotZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRotZ.TextChanged += new System.EventHandler(this.textFloatChanged);
            // 
            // txtPosZ
            // 
            this.txtPosZ.BackColor = System.Drawing.Color.White;
            this.txtPosZ.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosZ.Location = new System.Drawing.Point(429, 722);
            this.txtPosZ.MaxLength = 500;
            this.txtPosZ.Name = "txtPosZ";
            this.txtPosZ.Size = new System.Drawing.Size(74, 22);
            this.txtPosZ.TabIndex = 32;
            this.txtPosZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPosZ.TextChanged += new System.EventHandler(this.textFloatChanged);
            // 
            // txtPosY
            // 
            this.txtPosY.BackColor = System.Drawing.Color.White;
            this.txtPosY.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosY.Location = new System.Drawing.Point(77, 585);
            this.txtPosY.MaxLength = 500;
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(74, 22);
            this.txtPosY.TabIndex = 33;
            this.txtPosY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPosY.TextChanged += new System.EventHandler(this.textFloatChanged);
            // 
            // txtPosX
            // 
            this.txtPosX.BackColor = System.Drawing.Color.White;
            this.txtPosX.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosX.Location = new System.Drawing.Point(77, 548);
            this.txtPosX.MaxLength = 500;
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(74, 22);
            this.txtPosX.TabIndex = 34;
            this.txtPosX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPosX.TextChanged += new System.EventHandler(this.textFloatChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(52, 627);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 17);
            this.label9.TabIndex = 35;
            this.label9.Text = "Z";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(52, 586);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 17);
            this.label10.TabIndex = 36;
            this.label10.Text = "Y";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(52, 548);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 17);
            this.label11.TabIndex = 37;
            this.label11.Text = "X";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(359, 623);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 17);
            this.label12.TabIndex = 38;
            this.label12.Text = "Z";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(359, 585);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 17);
            this.label13.TabIndex = 39;
            this.label13.Text = "Y";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(359, 549);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 17);
            this.label14.TabIndex = 40;
            this.label14.Text = "X";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(589, 549);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 17);
            this.label15.TabIndex = 41;
            this.label15.Text = "Uniform";
            // 
            // txtPosZ2
            // 
            this.txtPosZ2.BackColor = System.Drawing.Color.White;
            this.txtPosZ2.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosZ2.Location = new System.Drawing.Point(77, 627);
            this.txtPosZ2.MaxLength = 500;
            this.txtPosZ2.Name = "txtPosZ2";
            this.txtPosZ2.Size = new System.Drawing.Size(74, 22);
            this.txtPosZ2.TabIndex = 42;
            this.txtPosZ2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPosZ2.TextChanged += new System.EventHandler(this.textFloatChanged);
            // 
            // ShaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(813, 704);
            this.Controls.Add(this.txtPosZ2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPosX);
            this.Controls.Add(this.txtPosY);
            this.Controls.Add(this.txtPosZ);
            this.Controls.Add(this.txtRotZ);
            this.Controls.Add(this.txtRotX);
            this.Controls.Add(this.txtRotY);
            this.Controls.Add(this.txtScale);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.table);
            this.Controls.Add(this.txtMeshPartName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lstMeshParts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShaderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shader Creation Form";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openDia;
        private System.Windows.Forms.SaveFileDialog saveDia;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tabSave;
        private System.Windows.Forms.ToolStripMenuItem tabExit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstMeshParts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox txtMeshPartName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel table;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.TextBox txtRotY;
        private System.Windows.Forms.TextBox txtRotX;
        private System.Windows.Forms.TextBox txtRotZ;
        private System.Windows.Forms.TextBox txtPosZ;
        private System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.TextBox txtPosX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPosZ2;
    }
}