namespace EonEngineTool.ObjectCreation.Tilemap
{
    partial class TileLayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileLayerForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tabExit = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNormal = new System.Windows.Forms.TextBox();
            this.txtCols = new System.Windows.Forms.TextBox();
            this.txtRows = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtDistortion = new System.Windows.Forms.TextBox();
            this.lstLyrs = new System.Windows.Forms.ListBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.openDia = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnShow = new System.Windows.Forms.Button();
            this.txtOffsetY = new System.Windows.Forms.TextBox();
            this.txtOffsetX = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabSave,
            this.tabExit});
            this.menuStrip1.Location = new System.Drawing.Point(690, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(130, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabSave
            // 
            this.tabSave.ForeColor = System.Drawing.Color.Red;
            this.tabSave.Name = "tabSave";
            this.tabSave.Size = new System.Drawing.Size(65, 22);
            this.tabSave.Text = "Save";
            this.tabSave.Click += new System.EventHandler(this.tabSave_Click);
            // 
            // tabExit
            // 
            this.tabExit.ForeColor = System.Drawing.Color.Red;
            this.tabExit.Name = "tabExit";
            this.tabExit.Size = new System.Drawing.Size(57, 22);
            this.tabExit.Text = "Exit";
            this.tabExit.Click += new System.EventHandler(this.tabExit_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Black;
            this.panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel.BackgroundImage")));
            this.panel.Location = new System.Drawing.Point(276, 57);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(544, 416);
            this.panel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tile Layer Creation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(81, 316);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tile Layers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(12, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Columns";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Rows";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(12, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "Total Tiles";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(12, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "Distortion Map";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(12, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 18);
            this.label7.TabIndex = 8;
            this.label7.Text = "Normal Map";
            // 
            // txtNormal
            // 
            this.txtNormal.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNormal.Location = new System.Drawing.Point(161, 97);
            this.txtNormal.Name = "txtNormal";
            this.txtNormal.Size = new System.Drawing.Size(100, 22);
            this.txtNormal.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtNormal, "The file path of the current tile layer\'s normal map.");
            this.txtNormal.Click += new System.EventHandler(this.txtNormal_Click);
            // 
            // txtCols
            // 
            this.txtCols.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCols.Location = new System.Drawing.Point(161, 215);
            this.txtCols.Name = "txtCols";
            this.txtCols.Size = new System.Drawing.Size(100, 22);
            this.txtCols.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtCols, "The total number of columns in the image.");
            this.txtCols.TextChanged += new System.EventHandler(this.txtCols_TextChanged);
            // 
            // txtRows
            // 
            this.txtRows.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRows.Location = new System.Drawing.Point(161, 187);
            this.txtRows.Name = "txtRows";
            this.txtRows.Size = new System.Drawing.Size(100, 22);
            this.txtRows.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtRows, "The total number of rows in the image.");
            this.txtRows.TextChanged += new System.EventHandler(this.txtRows_TextChanged);
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(161, 159);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 22);
            this.txtTotal.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtTotal, "The total number of tiles in the image.");
            this.txtTotal.TextChanged += new System.EventHandler(this.txtTotal_TextChanged);
            // 
            // txtDistortion
            // 
            this.txtDistortion.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistortion.Location = new System.Drawing.Point(161, 125);
            this.txtDistortion.Name = "txtDistortion";
            this.txtDistortion.Size = new System.Drawing.Size(100, 22);
            this.txtDistortion.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtDistortion, "The file path of the current tile layer\'s distortion map.");
            this.txtDistortion.Click += new System.EventHandler(this.txtDistortion_Click);
            // 
            // lstLyrs
            // 
            this.lstLyrs.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLyrs.FormattingEnabled = true;
            this.lstLyrs.ItemHeight = 15;
            this.lstLyrs.Location = new System.Drawing.Point(15, 350);
            this.lstLyrs.Name = "lstLyrs";
            this.lstLyrs.Size = new System.Drawing.Size(130, 124);
            this.lstLyrs.TabIndex = 14;
            this.toolTip1.SetToolTip(this.lstLyrs, "A list of all of the current tile layers.");
            this.lstLyrs.SelectedIndexChanged += new System.EventHandler(this.lstLyrs_SelectedIndexChanged);
            // 
            // txtNum
            // 
            this.txtNum.Enabled = false;
            this.txtNum.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNum.Location = new System.Drawing.Point(161, 57);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(100, 22);
            this.txtNum.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(12, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 18);
            this.label8.TabIndex = 15;
            this.label8.Text = "Layer number";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(161, 350);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 27);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add new";
            this.toolTip1.SetToolTip(this.btnAdd, "Adds a new tile layer.");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(161, 383);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(100, 27);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "remove";
            this.toolTip1.SetToolTip(this.btnRemove, "Removes a tile layer.");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnShow
            // 
            this.btnShow.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Location = new System.Drawing.Point(161, 447);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(100, 27);
            this.btnShow.TabIndex = 16;
            this.btnShow.Text = "Show";
            this.toolTip1.SetToolTip(this.btnShow, "Shows/ hides image indexing.");
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // txtOffsetY
            // 
            this.txtOffsetY.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOffsetY.Location = new System.Drawing.Point(161, 282);
            this.txtOffsetY.Name = "txtOffsetY";
            this.txtOffsetY.Size = new System.Drawing.Size(100, 22);
            this.txtOffsetY.TabIndex = 18;
            this.toolTip1.SetToolTip(this.txtOffsetY, "The offset between tile images along the Y axis.");
            this.txtOffsetY.TextChanged += new System.EventHandler(this.txtOffsetY_TextChanged);
            // 
            // txtOffsetX
            // 
            this.txtOffsetX.Font = new System.Drawing.Font("Furore", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOffsetX.Location = new System.Drawing.Point(161, 254);
            this.txtOffsetX.Name = "txtOffsetX";
            this.txtOffsetX.Size = new System.Drawing.Size(100, 22);
            this.txtOffsetX.TabIndex = 17;
            this.toolTip1.SetToolTip(this.txtOffsetX, "The offset between tile images along the X axis. ");
            this.txtOffsetX.TextChanged += new System.EventHandler(this.txtOffsetX_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(12, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 18);
            this.label9.TabIndex = 20;
            this.label9.Text = "Offset x";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(12, 282);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 18);
            this.label10.TabIndex = 19;
            this.label10.Text = "offset Y";
            // 
            // TileLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(829, 515);
            this.Controls.Add(this.txtOffsetY);
            this.Controls.Add(this.txtOffsetX);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lstLyrs);
            this.Controls.Add(this.txtDistortion);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtRows);
            this.Controls.Add(this.txtCols);
            this.Controls.Add(this.txtNormal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TileLayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TileLayerForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tabSave;
        private System.Windows.Forms.ToolStripMenuItem tabExit;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNormal;
        private System.Windows.Forms.TextBox txtCols;
        private System.Windows.Forms.TextBox txtRows;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtDistortion;
        private System.Windows.Forms.ListBox lstLyrs;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.OpenFileDialog openDia;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.TextBox txtOffsetY;
        private System.Windows.Forms.TextBox txtOffsetX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}