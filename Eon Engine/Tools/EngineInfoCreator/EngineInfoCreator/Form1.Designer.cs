namespace EngineInfoCreator
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEngCompAdd = new System.Windows.Forms.Button();
            this.btnAssemAddManual = new System.Windows.Forms.Button();
            this.btnEngAssemRemove = new System.Windows.Forms.Button();
            this.btnEngRemove = new System.Windows.Forms.Button();
            this.btnEngCompManualAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstEngComps = new System.Windows.Forms.ListBox();
            this.lstEngAssem = new System.Windows.Forms.ListBox();
            this.txtManualAssemRef = new System.Windows.Forms.TextBox();
            this.txtManualEngCompAdd = new System.Windows.Forms.TextBox();
            this.cboEngComps = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add Engine Component";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Assembly References";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(340, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Engine Components";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(157, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Manual Add Engine Component";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Manual Add Assembly Reference";
            // 
            // btnEngCompAdd
            // 
            this.btnEngCompAdd.Location = new System.Drawing.Point(212, 84);
            this.btnEngCompAdd.Name = "btnEngCompAdd";
            this.btnEngCompAdd.Size = new System.Drawing.Size(75, 23);
            this.btnEngCompAdd.TabIndex = 12;
            this.btnEngCompAdd.Text = "Add";
            this.btnEngCompAdd.UseVisualStyleBackColor = true;
            this.btnEngCompAdd.Click += new System.EventHandler(this.btnEngCompAdd_Click);
            // 
            // btnAssemAddManual
            // 
            this.btnAssemAddManual.Location = new System.Drawing.Point(212, 287);
            this.btnAssemAddManual.Name = "btnAssemAddManual";
            this.btnAssemAddManual.Size = new System.Drawing.Size(75, 23);
            this.btnAssemAddManual.TabIndex = 13;
            this.btnAssemAddManual.Text = "Add";
            this.btnAssemAddManual.UseVisualStyleBackColor = true;
            this.btnAssemAddManual.Click += new System.EventHandler(this.btnAssemAddManual_Click);
            // 
            // btnEngAssemRemove
            // 
            this.btnEngAssemRemove.Location = new System.Drawing.Point(505, 241);
            this.btnEngAssemRemove.Name = "btnEngAssemRemove";
            this.btnEngAssemRemove.Size = new System.Drawing.Size(75, 23);
            this.btnEngAssemRemove.TabIndex = 14;
            this.btnEngAssemRemove.Text = "Remove";
            this.btnEngAssemRemove.UseVisualStyleBackColor = true;
            this.btnEngAssemRemove.Click += new System.EventHandler(this.btnEngAssemRemove_Click);
            // 
            // btnEngRemove
            // 
            this.btnEngRemove.Location = new System.Drawing.Point(505, 38);
            this.btnEngRemove.Name = "btnEngRemove";
            this.btnEngRemove.Size = new System.Drawing.Size(75, 23);
            this.btnEngRemove.TabIndex = 15;
            this.btnEngRemove.Text = "Remove";
            this.btnEngRemove.UseVisualStyleBackColor = true;
            this.btnEngRemove.Click += new System.EventHandler(this.btnEngRemove_Click);
            // 
            // btnEngCompManualAdd
            // 
            this.btnEngCompManualAdd.Location = new System.Drawing.Point(212, 154);
            this.btnEngCompManualAdd.Name = "btnEngCompManualAdd";
            this.btnEngCompManualAdd.Size = new System.Drawing.Size(75, 23);
            this.btnEngCompManualAdd.TabIndex = 16;
            this.btnEngCompManualAdd.Text = "Add";
            this.btnEngCompManualAdd.UseVisualStyleBackColor = true;
            this.btnEngCompManualAdd.Click += new System.EventHandler(this.btnEngCompManualAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(571, 436);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lstEngComps
            // 
            this.lstEngComps.FormattingEnabled = true;
            this.lstEngComps.Location = new System.Drawing.Point(293, 38);
            this.lstEngComps.Name = "lstEngComps";
            this.lstEngComps.Size = new System.Drawing.Size(206, 173);
            this.lstEngComps.TabIndex = 18;
            // 
            // lstEngAssem
            // 
            this.lstEngAssem.FormattingEnabled = true;
            this.lstEngAssem.Location = new System.Drawing.Point(293, 241);
            this.lstEngAssem.Name = "lstEngAssem";
            this.lstEngAssem.Size = new System.Drawing.Size(206, 186);
            this.lstEngAssem.TabIndex = 19;
            // 
            // txtManualAssemRef
            // 
            this.txtManualAssemRef.Location = new System.Drawing.Point(8, 289);
            this.txtManualAssemRef.Name = "txtManualAssemRef";
            this.txtManualAssemRef.Size = new System.Drawing.Size(198, 20);
            this.txtManualAssemRef.TabIndex = 20;
            // 
            // txtManualEngCompAdd
            // 
            this.txtManualEngCompAdd.Location = new System.Drawing.Point(12, 154);
            this.txtManualEngCompAdd.Name = "txtManualEngCompAdd";
            this.txtManualEngCompAdd.Size = new System.Drawing.Size(198, 20);
            this.txtManualEngCompAdd.TabIndex = 21;
            // 
            // cboEngComps
            // 
            this.cboEngComps.FormattingEnabled = true;
            this.cboEngComps.Items.AddRange(new object[] {
            "None",
            "Eon.Game.LevelManagement.LevelManager",
            "Eon.Physics2D.Framework",
            "Eon.Rendering2D.Framework.Framework",
            "Eon.Physics3D.Framework",
            "Eon.Rendering3D.Framework.Framework",
            "Eon.UIApi.MenuManager"});
            this.cboEngComps.Location = new System.Drawing.Point(16, 84);
            this.cboEngComps.Name = "cboEngComps";
            this.cboEngComps.Size = new System.Drawing.Size(190, 21);
            this.cboEngComps.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 471);
            this.Controls.Add(this.cboEngComps);
            this.Controls.Add(this.txtManualEngCompAdd);
            this.Controls.Add(this.txtManualAssemRef);
            this.Controls.Add(this.lstEngAssem);
            this.Controls.Add(this.lstEngComps);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEngCompManualAdd);
            this.Controls.Add(this.btnEngRemove);
            this.Controls.Add(this.btnEngAssemRemove);
            this.Controls.Add(this.btnAssemAddManual);
            this.Controls.Add(this.btnEngCompAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEngCompAdd;
        private System.Windows.Forms.Button btnAssemAddManual;
        private System.Windows.Forms.Button btnEngAssemRemove;
        private System.Windows.Forms.Button btnEngRemove;
        private System.Windows.Forms.Button btnEngCompManualAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstEngComps;
        private System.Windows.Forms.ListBox lstEngAssem;
        private System.Windows.Forms.TextBox txtManualAssemRef;
        private System.Windows.Forms.TextBox txtManualEngCompAdd;
        private System.Windows.Forms.ComboBox cboEngComps;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

