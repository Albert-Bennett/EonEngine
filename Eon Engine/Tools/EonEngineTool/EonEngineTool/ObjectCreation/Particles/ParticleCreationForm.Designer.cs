namespace EonEngineTool.ObjectCreation.Particles
{
    partial class ParticleCreationForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParticleCreationForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tabExit = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLyr = new System.Windows.Forms.TextBox();
            this.chkPostDraw = new System.Windows.Forms.CheckBox();
            this.lstEmitters = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.saveDia = new System.Windows.Forms.SaveFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnNew = new System.Windows.Forms.Button();
            this.table = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.openDia = new System.Windows.Forms.OpenFileDialog();
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
            this.tabTest,
            this.tabSave,
            this.tabExit});
            this.menuStrip1.Location = new System.Drawing.Point(625, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(195, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabTest
            // 
            this.tabTest.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabTest.ForeColor = System.Drawing.Color.Red;
            this.tabTest.Name = "tabTest";
            this.tabTest.Size = new System.Drawing.Size(65, 22);
            this.tabTest.Text = "Test";
            this.tabTest.Click += new System.EventHandler(this.tabTest_Click);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Draw Layer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(56, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Emitters";
            // 
            // txtLyr
            // 
            this.txtLyr.BackColor = System.Drawing.Color.White;
            this.txtLyr.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLyr.Location = new System.Drawing.Point(140, 54);
            this.txtLyr.MaxLength = 2;
            this.txtLyr.Name = "txtLyr";
            this.txtLyr.Size = new System.Drawing.Size(48, 22);
            this.txtLyr.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtLyr, "The layer to render the Particle System on.");
            // 
            // chkPostDraw
            // 
            this.chkPostDraw.AutoSize = true;
            this.chkPostDraw.BackColor = System.Drawing.Color.Transparent;
            this.chkPostDraw.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPostDraw.ForeColor = System.Drawing.Color.Red;
            this.chkPostDraw.Location = new System.Drawing.Point(17, 89);
            this.chkPostDraw.Name = "chkPostDraw";
            this.chkPostDraw.Size = new System.Drawing.Size(123, 21);
            this.chkPostDraw.TabIndex = 3;
            this.chkPostDraw.Text = "Post draw";
            this.toolTip.SetToolTip(this.chkPostDraw, "Wheather the Particle System will be drawn at the same time as the game.");
            this.chkPostDraw.UseVisualStyleBackColor = false;
            // 
            // lstEmitters
            // 
            this.lstEmitters.BackColor = System.Drawing.Color.White;
            this.lstEmitters.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEmitters.FormattingEnabled = true;
            this.lstEmitters.ItemHeight = 15;
            this.lstEmitters.Location = new System.Drawing.Point(15, 167);
            this.lstEmitters.Name = "lstEmitters";
            this.lstEmitters.Size = new System.Drawing.Size(171, 244);
            this.lstEmitters.TabIndex = 4;
            this.toolTip.SetToolTip(this.lstEmitters, "The list of  Particle Emitters in the Particle System");
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(192, 200);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(86, 27);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove";
            this.toolTip.SetToolTip(this.btnRemove, "Removes the selected particle emitter from the Particle System");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // toolTip
            // 
            this.toolTip.BackColor = System.Drawing.Color.Transparent;
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(192, 167);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(86, 27);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "New";
            this.toolTip.SetToolTip(this.btnNew, "Removes the selected particle emitter from the Particle System");
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // table
            // 
            this.table.AutoScroll = true;
            this.table.BackColor = System.Drawing.Color.Black;
            this.table.Location = new System.Drawing.Point(295, 57);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(522, 421);
            this.table.TabIndex = 7;
            this.table.WrapContents = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(10, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(285, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "2D Particle System Creation";
            // 
            // ParticleCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(829, 515);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.table);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lstEmitters);
            this.Controls.Add(this.chkPostDraw);
            this.Controls.Add(this.txtLyr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ParticleCreationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ParticleCreationForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tabTest;
        private System.Windows.Forms.ToolStripMenuItem tabSave;
        private System.Windows.Forms.ToolStripMenuItem tabExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLyr;
        private System.Windows.Forms.CheckBox chkPostDraw;
        private System.Windows.Forms.ListBox lstEmitters;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.SaveFileDialog saveDia;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.FlowLayoutPanel table;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openDia;
    }
}