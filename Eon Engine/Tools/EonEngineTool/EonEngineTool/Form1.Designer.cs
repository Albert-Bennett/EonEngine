namespace EonEngineTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabNew = new System.Windows.Forms.ToolStripMenuItem();
            this.newProj2D = new System.Windows.Forms.ToolStripMenuItem();
            this.tabNewProj3D = new System.Windows.Forms.ToolStripMenuItem();
            this.tabNewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tabLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPro2DjTab = new System.Windows.Forms.ToolStripMenuItem();
            this.tabLoadProj3D = new System.Windows.Forms.ToolStripMenuItem();
            this.tabLoadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tabContent = new System.Windows.Forms.ToolStripMenuItem();
            this.tabEditContent = new System.Windows.Forms.ToolStripMenuItem();
            this.EditShader = new System.Windows.Forms.ToolStripMenuItem();
            this.Load2DParticleSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditJJaxTool = new System.Windows.Forms.ToolStripMenuItem();
            this.newContentFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewShader = new System.Windows.Forms.ToolStripMenuItem();
            this.new2DParticleSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.newJJaxFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTestLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTestObj = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTestParticles = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTestMenuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTestAni = new System.Windows.Forms.ToolStripMenuItem();
            this.tabView = new System.Windows.Forms.ToolStripMenuItem();
            this.tabViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tabOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPref = new System.Windows.Forms.ToolStripMenuItem();
            this.tabExit = new System.Windows.Forms.ToolStripMenuItem();
            this.openDia = new System.Windows.Forms.OpenFileDialog();
            this.browzeDia = new System.Windows.Forms.FolderBrowserDialog();
            this.processContentFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Process3DAni = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("Furore", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabNew,
            this.tabLoad,
            this.tabContent,
            this.tabTest,
            this.tabView,
            this.tabSave,
            this.tabOptions,
            this.tabExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabNew
            // 
            this.tabNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProj2D,
            this.tabNewProj3D,
            this.tabNewMenu});
            this.tabNew.ForeColor = System.Drawing.Color.Red;
            this.tabNew.Name = "tabNew";
            this.tabNew.Size = new System.Drawing.Size(70, 27);
            this.tabNew.Text = "New";
            // 
            // newProj2D
            // 
            this.newProj2D.Name = "newProj2D";
            this.newProj2D.Size = new System.Drawing.Size(240, 28);
            this.newProj2D.Text = "2D Project";
            this.newProj2D.Click += new System.EventHandler(this.newProj2D_Click);
            // 
            // tabNewProj3D
            // 
            this.tabNewProj3D.Name = "tabNewProj3D";
            this.tabNewProj3D.Size = new System.Drawing.Size(240, 28);
            this.tabNewProj3D.Text = "3D Project";
            this.tabNewProj3D.Click += new System.EventHandler(this.tabNewProj3D_Click);
            // 
            // tabNewMenu
            // 
            this.tabNewMenu.Name = "tabNewMenu";
            this.tabNewMenu.Size = new System.Drawing.Size(240, 28);
            this.tabNewMenu.Text = "Menu System";
            this.tabNewMenu.Click += new System.EventHandler(this.tabNewMenu_Click);
            // 
            // tabLoad
            // 
            this.tabLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPro2DjTab,
            this.tabLoadProj3D,
            this.tabLoadMenu});
            this.tabLoad.ForeColor = System.Drawing.Color.Red;
            this.tabLoad.Name = "tabLoad";
            this.tabLoad.Size = new System.Drawing.Size(81, 27);
            this.tabLoad.Text = "Load";
            // 
            // loadPro2DjTab
            // 
            this.loadPro2DjTab.Name = "loadPro2DjTab";
            this.loadPro2DjTab.Size = new System.Drawing.Size(240, 28);
            this.loadPro2DjTab.Text = "2D Project";
            this.loadPro2DjTab.Click += new System.EventHandler(this.loadPro2DjTab_Click);
            // 
            // tabLoadProj3D
            // 
            this.tabLoadProj3D.Name = "tabLoadProj3D";
            this.tabLoadProj3D.Size = new System.Drawing.Size(240, 28);
            this.tabLoadProj3D.Text = "3D project";
            this.tabLoadProj3D.Click += new System.EventHandler(this.tabLoadProj3D_Click);
            // 
            // tabLoadMenu
            // 
            this.tabLoadMenu.Name = "tabLoadMenu";
            this.tabLoadMenu.Size = new System.Drawing.Size(240, 28);
            this.tabLoadMenu.Text = "Menu System";
            this.tabLoadMenu.Click += new System.EventHandler(this.tabLoadMenu_Click);
            // 
            // tabContent
            // 
            this.tabContent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabEditContent,
            this.newContentFileToolStripMenuItem,
            this.processContentFileToolStripMenuItem});
            this.tabContent.ForeColor = System.Drawing.Color.Red;
            this.tabContent.Name = "tabContent";
            this.tabContent.Size = new System.Drawing.Size(124, 27);
            this.tabContent.Text = "Content";
            // 
            // tabEditContent
            // 
            this.tabEditContent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditShader,
            this.Load2DParticleSystem,
            this.EditJJaxTool});
            this.tabEditContent.Name = "tabEditContent";
            this.tabEditContent.Size = new System.Drawing.Size(349, 28);
            this.tabEditContent.Text = "Edit Content File";
            this.tabEditContent.Click += new System.EventHandler(this.tabEditContent_Click);
            // 
            // EditShader
            // 
            this.EditShader.Name = "EditShader";
            this.EditShader.Size = new System.Drawing.Size(322, 28);
            this.EditShader.Text = "Shader";
            this.EditShader.Click += new System.EventHandler(this.EditShader_Click);
            // 
            // Load2DParticleSystem
            // 
            this.Load2DParticleSystem.Name = "Load2DParticleSystem";
            this.Load2DParticleSystem.Size = new System.Drawing.Size(322, 28);
            this.Load2DParticleSystem.Text = "2D Particle System";
            this.Load2DParticleSystem.Click += new System.EventHandler(this.Load2DParticleSystem_Click);
            // 
            // EditJJaxTool
            // 
            this.EditJJaxTool.Name = "EditJJaxTool";
            this.EditJJaxTool.Size = new System.Drawing.Size(322, 28);
            this.EditJJaxTool.Text = "J-Jax File";
            this.EditJJaxTool.Click += new System.EventHandler(this.EditJJaxTool_Click);
            // 
            // newContentFileToolStripMenuItem
            // 
            this.newContentFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewShader,
            this.new2DParticleSystem,
            this.newJJaxFile});
            this.newContentFileToolStripMenuItem.Name = "newContentFileToolStripMenuItem";
            this.newContentFileToolStripMenuItem.Size = new System.Drawing.Size(349, 28);
            this.newContentFileToolStripMenuItem.Text = "New Content File";
            // 
            // NewShader
            // 
            this.NewShader.Name = "NewShader";
            this.NewShader.Size = new System.Drawing.Size(322, 28);
            this.NewShader.Text = "Shader";
            this.NewShader.Click += new System.EventHandler(this.NewShader_Click);
            // 
            // new2DParticleSystem
            // 
            this.new2DParticleSystem.Name = "new2DParticleSystem";
            this.new2DParticleSystem.Size = new System.Drawing.Size(322, 28);
            this.new2DParticleSystem.Text = "2D Particle System";
            this.new2DParticleSystem.Click += new System.EventHandler(this.new2DParticleSystem_Click);
            // 
            // newJJaxFile
            // 
            this.newJJaxFile.Name = "newJJaxFile";
            this.newJJaxFile.Size = new System.Drawing.Size(322, 28);
            this.newJJaxFile.Text = "J-Jax file";
            this.newJJaxFile.Click += new System.EventHandler(this.newJJaxFile_Click);
            // 
            // tabTest
            // 
            this.tabTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabTestLevel,
            this.tabTestObj,
            this.tabTestMenuSystem,
            this.tabTestAni});
            this.tabTest.ForeColor = System.Drawing.Color.Red;
            this.tabTest.Name = "tabTest";
            this.tabTest.Size = new System.Drawing.Size(79, 27);
            this.tabTest.Text = "Test";
            // 
            // tabTestLevel
            // 
            this.tabTestLevel.Name = "tabTestLevel";
            this.tabTestLevel.Size = new System.Drawing.Size(240, 28);
            this.tabTestLevel.Text = "Level";
            this.tabTestLevel.Click += new System.EventHandler(this.tabTestLevel_Click);
            // 
            // tabTestObj
            // 
            this.tabTestObj.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabTestParticles});
            this.tabTestObj.Name = "tabTestObj";
            this.tabTestObj.Size = new System.Drawing.Size(240, 28);
            this.tabTestObj.Text = "Object";
            // 
            // tabTestParticles
            // 
            this.tabTestParticles.Name = "tabTestParticles";
            this.tabTestParticles.Size = new System.Drawing.Size(285, 28);
            this.tabTestParticles.Text = "Particle System";
            this.tabTestParticles.Click += new System.EventHandler(this.tabTestParticles_Click);
            // 
            // tabTestMenuSystem
            // 
            this.tabTestMenuSystem.Name = "tabTestMenuSystem";
            this.tabTestMenuSystem.Size = new System.Drawing.Size(240, 28);
            this.tabTestMenuSystem.Text = "Menu System";
            this.tabTestMenuSystem.Click += new System.EventHandler(this.tabTestMenuSystem_Click);
            // 
            // tabTestAni
            // 
            this.tabTestAni.Name = "tabTestAni";
            this.tabTestAni.Size = new System.Drawing.Size(240, 28);
            this.tabTestAni.Text = "Animation";
            this.tabTestAni.Click += new System.EventHandler(this.tabTestAni_Click);
            // 
            // tabView
            // 
            this.tabView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabViewMenu});
            this.tabView.ForeColor = System.Drawing.Color.Red;
            this.tabView.Name = "tabView";
            this.tabView.Size = new System.Drawing.Size(75, 27);
            this.tabView.Text = "View";
            // 
            // tabViewMenu
            // 
            this.tabViewMenu.Name = "tabViewMenu";
            this.tabViewMenu.Size = new System.Drawing.Size(240, 28);
            this.tabViewMenu.Text = "Menu System";
            this.tabViewMenu.Click += new System.EventHandler(this.tabViewMenu_Click);
            // 
            // tabSave
            // 
            this.tabSave.ForeColor = System.Drawing.Color.Red;
            this.tabSave.Name = "tabSave";
            this.tabSave.Size = new System.Drawing.Size(81, 27);
            this.tabSave.Text = "Save";
            // 
            // tabOptions
            // 
            this.tabOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabPref});
            this.tabOptions.ForeColor = System.Drawing.Color.Red;
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Size = new System.Drawing.Size(116, 27);
            this.tabOptions.Text = "Options";
            // 
            // tabPref
            // 
            this.tabPref.Name = "tabPref";
            this.tabPref.Size = new System.Drawing.Size(323, 28);
            this.tabPref.Text = "Change Prefrences";
            this.tabPref.Click += new System.EventHandler(this.tabPref_Click);
            // 
            // tabExit
            // 
            this.tabExit.ForeColor = System.Drawing.Color.Red;
            this.tabExit.Name = "tabExit";
            this.tabExit.Size = new System.Drawing.Size(70, 27);
            this.tabExit.Text = "Exit";
            this.tabExit.Click += new System.EventHandler(this.tabExit_Click);
            // 
            // openDia
            // 
            this.openDia.FileName = "openFileDialog1";
            this.openDia.Title = "Browze Files";
            // 
            // processContentFileToolStripMenuItem
            // 
            this.processContentFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Process3DAni});
            this.processContentFileToolStripMenuItem.Name = "processContentFileToolStripMenuItem";
            this.processContentFileToolStripMenuItem.Size = new System.Drawing.Size(349, 28);
            this.processContentFileToolStripMenuItem.Text = "Process Content File";
            // 
            // Process3DAni
            // 
            this.Process3DAni.Name = "Process3DAni";
            this.Process3DAni.Size = new System.Drawing.Size(235, 28);
            this.Process3DAni.Text = "3D Animation";
            this.Process3DAni.Click += new System.EventHandler(this.Process3DAni_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1008, 681);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eon Engine Creation Tool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tabNew;
        private System.Windows.Forms.ToolStripMenuItem newProj2D;
        private System.Windows.Forms.ToolStripMenuItem tabLoad;
        private System.Windows.Forms.ToolStripMenuItem loadPro2DjTab;
        private System.Windows.Forms.ToolStripMenuItem tabContent;
        private System.Windows.Forms.ToolStripMenuItem tabEditContent;
        private System.Windows.Forms.ToolStripMenuItem tabSave;
        private System.Windows.Forms.ToolStripMenuItem tabOptions;
        private System.Windows.Forms.ToolStripMenuItem tabPref;
        private System.Windows.Forms.ToolStripMenuItem tabExit;
        private System.Windows.Forms.ToolStripMenuItem tabTest;
        private System.Windows.Forms.ToolStripMenuItem tabNewProj3D;
        private System.Windows.Forms.ToolStripMenuItem tabLoadProj3D;
        private System.Windows.Forms.ToolStripMenuItem tabTestLevel;
        private System.Windows.Forms.ToolStripMenuItem tabTestObj;
        private System.Windows.Forms.ToolStripMenuItem tabTestMenuSystem;
        private System.Windows.Forms.OpenFileDialog openDia;
        private System.Windows.Forms.FolderBrowserDialog browzeDia;
        private System.Windows.Forms.ToolStripMenuItem tabNewMenu;
        private System.Windows.Forms.ToolStripMenuItem tabLoadMenu;
        private System.Windows.Forms.ToolStripMenuItem tabView;
        private System.Windows.Forms.ToolStripMenuItem tabViewMenu;
        private System.Windows.Forms.ToolStripMenuItem tabTestParticles;
        private System.Windows.Forms.ToolStripMenuItem tabTestAni;
        private System.Windows.Forms.ToolStripMenuItem EditShader;
        private System.Windows.Forms.ToolStripMenuItem newContentFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewShader;
        private System.Windows.Forms.ToolStripMenuItem Load2DParticleSystem;
        private System.Windows.Forms.ToolStripMenuItem new2DParticleSystem;
        private System.Windows.Forms.ToolStripMenuItem EditJJaxTool;
        private System.Windows.Forms.ToolStripMenuItem newJJaxFile;
        private System.Windows.Forms.ToolStripMenuItem processContentFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Process3DAni;
    }
}

