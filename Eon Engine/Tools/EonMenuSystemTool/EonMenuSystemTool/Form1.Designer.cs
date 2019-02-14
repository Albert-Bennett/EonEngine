namespace EonMenuSystemTool
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
            this.txtScreenName = new System.Windows.Forms.TextBox();
            this.txtAssem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.btnAddScreen = new System.Windows.Forms.Button();
            this.btnAddAssembly = new System.Windows.Forms.Button();
            this.lslScreens = new System.Windows.Forms.ListBox();
            this.lslAssemblies = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtObjectType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtScreenName
            // 
            this.txtScreenName.Location = new System.Drawing.Point(95, 93);
            this.txtScreenName.Name = "txtScreenName";
            this.txtScreenName.Size = new System.Drawing.Size(100, 20);
            this.txtScreenName.TabIndex = 0;
            // 
            // txtAssem
            // 
            this.txtAssem.Location = new System.Drawing.Point(95, 222);
            this.txtAssem.Name = "txtAssem";
            this.txtAssem.Size = new System.Drawing.Size(100, 20);
            this.txtAssem.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Screen Name";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(13, 225);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(82, 13);
            this.label.TabIndex = 3;
            this.label.Text = "Assembly Name";
            // 
            // btnAddScreen
            // 
            this.btnAddScreen.Location = new System.Drawing.Point(214, 64);
            this.btnAddScreen.Name = "btnAddScreen";
            this.btnAddScreen.Size = new System.Drawing.Size(75, 23);
            this.btnAddScreen.TabIndex = 4;
            this.btnAddScreen.Text = "Add";
            this.btnAddScreen.UseVisualStyleBackColor = true;
            this.btnAddScreen.Click += new System.EventHandler(this.btnAddScreen_Click);
            // 
            // btnAddAssembly
            // 
            this.btnAddAssembly.Location = new System.Drawing.Point(214, 220);
            this.btnAddAssembly.Name = "btnAddAssembly";
            this.btnAddAssembly.Size = new System.Drawing.Size(75, 23);
            this.btnAddAssembly.TabIndex = 5;
            this.btnAddAssembly.Text = "Add";
            this.btnAddAssembly.UseVisualStyleBackColor = true;
            this.btnAddAssembly.Click += new System.EventHandler(this.btnAddAssembly_Click);
            // 
            // lslScreens
            // 
            this.lslScreens.FormattingEnabled = true;
            this.lslScreens.Location = new System.Drawing.Point(321, 64);
            this.lslScreens.Name = "lslScreens";
            this.lslScreens.Size = new System.Drawing.Size(218, 95);
            this.lslScreens.TabIndex = 6;
            // 
            // lslAssemblies
            // 
            this.lslAssemblies.FormattingEnabled = true;
            this.lslAssemblies.Location = new System.Drawing.Point(321, 220);
            this.lslAssemblies.Name = "lslAssemblies";
            this.lslAssemblies.Size = new System.Drawing.Size(218, 95);
            this.lslAssemblies.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(743, 389);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtObjectType
            // 
            this.txtObjectType.Location = new System.Drawing.Point(95, 67);
            this.txtObjectType.Name = "txtObjectType";
            this.txtObjectType.Size = new System.Drawing.Size(100, 20);
            this.txtObjectType.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Screen Type";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 424);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtObjectType);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lslAssemblies);
            this.Controls.Add(this.lslScreens);
            this.Controls.Add(this.btnAddAssembly);
            this.Controls.Add(this.btnAddScreen);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAssem);
            this.Controls.Add(this.txtScreenName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtScreenName;
        private System.Windows.Forms.TextBox txtAssem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button btnAddScreen;
        private System.Windows.Forms.Button btnAddAssembly;
        private System.Windows.Forms.ListBox lslScreens;
        private System.Windows.Forms.ListBox lslAssemblies;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtObjectType;
        private System.Windows.Forms.Label label2;
    }
}

