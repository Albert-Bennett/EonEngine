namespace EonEngineTool.ObjectCreation.Tilemap
{
    partial class TileMapForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileMapForm));
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tabExit = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDia = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.chkPostRender = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelImg = new System.Windows.Forms.FlowLayoutPanel();
            this.lstlayers = new System.Windows.Forms.ListBox();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTextEdittor = new System.Windows.Forms.Button();
            this.flowShow = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tile Map Creator";
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
            this.menuStrip1.Location = new System.Drawing.Point(885, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(130, 26);
            this.menuStrip1.TabIndex = 1;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(62, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tile Layers";
            // 
            // chkPostRender
            // 
            this.chkPostRender.AutoSize = true;
            this.chkPostRender.BackColor = System.Drawing.Color.Transparent;
            this.chkPostRender.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPostRender.ForeColor = System.Drawing.Color.Red;
            this.chkPostRender.Location = new System.Drawing.Point(12, 59);
            this.chkPostRender.Name = "chkPostRender";
            this.chkPostRender.Size = new System.Drawing.Size(148, 22);
            this.chkPostRender.TabIndex = 3;
            this.chkPostRender.Text = "Post Render";
            this.toolTip1.SetToolTip(this.chkPostRender, "Wheather or not the tile map should be rendered after all other objects in the le" +
        "vel.");
            this.chkPostRender.UseVisualStyleBackColor = false;
            this.chkPostRender.CheckedChanged += new System.EventHandler(this.chkPostRender_CheckedChanged);
            // 
            // panelImg
            // 
            this.panelImg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelImg.BackgroundImage")));
            this.panelImg.Location = new System.Drawing.Point(44, 140);
            this.panelImg.Name = "panelImg";
            this.panelImg.Size = new System.Drawing.Size(156, 97);
            this.panelImg.TabIndex = 4;
            this.toolTip1.SetToolTip(this.panelImg, "Thumbnail image of the current tile layer\'s tile sheet.");
            // 
            // lstlayers
            // 
            this.lstlayers.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstlayers.FormattingEnabled = true;
            this.lstlayers.ItemHeight = 18;
            this.lstlayers.Location = new System.Drawing.Point(12, 337);
            this.lstlayers.Name = "lstlayers";
            this.lstlayers.Size = new System.Drawing.Size(156, 220);
            this.lstlayers.TabIndex = 5;
            this.toolTip1.SetToolTip(this.lstlayers, "A list of all of the usable tile layers.");
            this.lstlayers.SelectedIndexChanged += new System.EventHandler(this.lstlayers_SelectedIndexChanged);
            // 
            // btnHide
            // 
            this.btnHide.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.Location = new System.Drawing.Point(175, 337);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(94, 28);
            this.btnHide.TabIndex = 6;
            this.btnHide.Text = "Hide";
            this.toolTip1.SetToolTip(this.btnHide, "Hides/ shows the currently selected tile layer.");
            this.btnHide.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(174, 529);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(94, 28);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Edit";
            this.toolTip1.SetToolTip(this.btnEdit, "Edits the tile layers available.");
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtWidth
            // 
            this.txtWidth.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWidth.Location = new System.Drawing.Point(136, 258);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(100, 25);
            this.txtWidth.TabIndex = 10;
            this.toolTip1.SetToolTip(this.txtWidth, "The width of each tile in the tile layer.");
            this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            // 
            // txtHeight
            // 
            this.txtHeight.Font = new System.Drawing.Font("Furore", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeight.Location = new System.Drawing.Point(136, 289);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(100, 25);
            this.txtHeight.TabIndex = 11;
            this.toolTip1.SetToolTip(this.txtHeight, "The height of each tile in the tile layer.");
            this.txtHeight.TextChanged += new System.EventHandler(this.txtHeight_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(9, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tile height";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(9, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tile width";
            // 
            // btnTextEdittor
            // 
            this.btnTextEdittor.Font = new System.Drawing.Font("Furore", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTextEdittor.Location = new System.Drawing.Point(15, 563);
            this.btnTextEdittor.Name = "btnTextEdittor";
            this.btnTextEdittor.Size = new System.Drawing.Size(257, 28);
            this.btnTextEdittor.TabIndex = 12;
            this.btnTextEdittor.Text = "Open Text edittor";
            this.toolTip1.SetToolTip(this.btnTextEdittor, "Opens the text edittor, this is used to manually edit the locations of tiles in a" +
        " tile layer. ");
            this.btnTextEdittor.UseVisualStyleBackColor = true;
            this.btnTextEdittor.Click += new System.EventHandler(this.btnTextEdittor_Click);
            // 
            // flowShow
            // 
            this.flowShow.Location = new System.Drawing.Point(275, 108);
            this.flowShow.Name = "flowShow";
            this.flowShow.Size = new System.Drawing.Size(737, 483);
            this.flowShow.TabIndex = 13;
            // 
            // TileMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 720);
            this.Controls.Add(this.flowShow);
            this.Controls.Add(this.btnTextEdittor);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.lstlayers);
            this.Controls.Add(this.panelImg);
            this.Controls.Add(this.chkPostRender);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TileMapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TileMapForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tabSave;
        private System.Windows.Forms.ToolStripMenuItem tabExit;
        private System.Windows.Forms.SaveFileDialog saveDia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkPostRender;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel panelImg;
        private System.Windows.Forms.ListBox lstlayers;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Button btnTextEdittor;
        private System.Windows.Forms.FlowLayoutPanel flowShow;
    }
}