using System.Windows.Forms;

namespace Otla.Net.UI
{
    partial class MainFrame
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAddMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toSoundMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toWavMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toMp3MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toTzxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editBlocksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchSbbMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchWavMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adjustAudioMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wavPlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.wikiMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.builderGB = new System.Windows.Forms.GroupBox();
            this.fileEdt = new System.Windows.Forms.TextBox();
            this.headerGB = new System.Windows.Forms.GroupBox();
            this.nameLbl = new System.Windows.Forms.Label();
            this.nameEdt = new System.Windows.Forms.TextBox();
            this.aditionalLbl = new System.Windows.Forms.Label();
            this.aditionalInfoEdt = new System.Windows.Forms.TextBox();
            this.modelLbl = new System.Windows.Forms.Label();
            this.modelCmbBx = new System.Windows.Forms.ComboBox();
            this.maquinaCmbBx = new System.Windows.Forms.ComboBox();
            this.enableIntChkBx = new System.Windows.Forms.CheckBox();
            this.locateLbl = new System.Windows.Forms.Label();
            this.reubicaEdt = new System.Windows.Forms.TextBox();
            this.pokeLbl = new System.Windows.Forms.Label();
            this.pokeEdt = new System.Windows.Forms.TextBox();
            this.clearLbl = new System.Windows.Forms.Label();
            this.clearEdt = new System.Windows.Forms.TextBox();
            this.usrLbl = new System.Windows.Forms.Label();
            this.usrEdt = new System.Windows.Forms.TextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.blocksLbl = new System.Windows.Forms.Label();
            this.bloksEdt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.originEdt = new System.Windows.Forms.TextBox();

            this.blocksLV = new System.Windows.Forms.ListView();
            this.colBlockName = (System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader());
            this.colSize = (System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader());
            this.colType = (System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader());
            this.colStart = (System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader());
            this.colJump = (System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader());
            this.colExec = (System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader());

            this.playerGB = new System.Windows.Forms.GroupBox();
            this.loaderGB = new System.Windows.Forms.GroupBox();
            this.makeLoaderChkBx = new System.Windows.Forms.CheckBox();
            this.accelerateChkBx = new System.Windows.Forms.CheckBox();
            this.checkLoadErrorChkBx = new System.Windows.Forms.CheckBox();
            this.releMotorChkBx = new System.Windows.Forms.CheckBox();

            this.wavGB = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.frecuenciaCmbBx = new System.Windows.Forms.ComboBox();
            this.bpsLbl = new System.Windows.Forms.Label();
            this.muestrasCmbBx = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.formaCmbBx = new System.Windows.Forms.ComboBox();
            this.invertChkBx = new System.Windows.Forms.CheckBox();
            this.pausaEdt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();

            this.playBtn = new System.Windows.Forms.Button();
            this.wavBtn = new System.Windows.Forms.Button();
            this.mp3Btn = new System.Windows.Forms.Button();
            this.tzxBtn = new System.Windows.Forms.Button();

            this.statusBar1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            this.menuStrip1.SuspendLayout();
            this.builderGB.SuspendLayout();
            this.headerGB.SuspendLayout();
            this.playerGB.SuspendLayout();
            this.loaderGB.SuspendLayout();
            this.wavGB.SuspendLayout();
            this.statusBar1.SuspendLayout();
            this.SuspendLayout();

            // Menu
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu, this.convertMenu, this.settingsMenu, this.toolsMenu, this.helpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(386, 24);
            this.menuStrip1.TabIndex = 0;

            this.fileMenu.Text = "File";
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem, this.newAddMenuItem, this.openMenuItem, this.saveMenuItem, this.saveAsMenuItem, this.toolStripSeparator1, this.quitMenuItem});

            this.newMenuItem.Text = "New";
            this.newAddMenuItem.Text = "New+Add";
            this.openMenuItem.Text = "Open...";
            this.saveMenuItem.Text = "Save";
            this.saveAsMenuItem.Text = "Save as...";
            this.quitMenuItem.Text = "Quit";

            this.convertMenu.Text = "Convert";
            this.convertMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toSoundMenuItem, this.toWavMenuItem, this.toMp3MenuItem, this.toTzxMenuItem});

            this.settingsMenu.Text = "Settings";

            this.toolsMenu.Text = "Tools";
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editBlocksMenuItem, this.batchSbbMenuItem, this.batchWavMenuItem, this.adjustAudioMenuItem, this.wavPlayerMenuItem});

            this.editBlocksMenuItem.Text = "Edit blocks";
            this.batchSbbMenuItem.Text = "Batch .sbb";
            this.batchWavMenuItem.Text = "Batch .wav";
            this.adjustAudioMenuItem.Text = "Adjust audio";
            this.wavPlayerMenuItem.Text = "Wav player";

            this.helpMenu.Text = "Help";
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wikiMenuItem, this.aboutMenuItem});

            this.wikiMenuItem.Text = "Wiki";
            this.aboutMenuItem.Text = "About";

            // BuilderGB
            this.builderGB.Controls.Add(this.fileEdt);
            this.builderGB.Controls.Add(this.headerGB);
            this.builderGB.Controls.Add(this.blocksLV);
            this.builderGB.Location = new System.Drawing.Point(8, 27);
            this.builderGB.Name = "builderGB";
            this.builderGB.Size = new System.Drawing.Size(369, 329);
            this.builderGB.TabIndex = 1;
            this.builderGB.TabStop = false;
            this.builderGB.Text = "Builder";

            this.fileEdt.Location = new System.Drawing.Point(8, 16);
            this.fileEdt.Name = "fileEdt";
            this.fileEdt.ReadOnly = true;
            this.fileEdt.Size = new System.Drawing.Size(345, 20);
            this.fileEdt.TabIndex = 2;

            // HeaderGB
            this.headerGB.Controls.Add(this.nameLbl);
            this.headerGB.Controls.Add(this.nameEdt);
            this.headerGB.Controls.Add(this.aditionalLbl);
            this.headerGB.Controls.Add(this.aditionalInfoEdt);
            this.headerGB.Controls.Add(this.modelLbl);
            this.headerGB.Controls.Add(this.modelCmbBx);
            this.headerGB.Controls.Add(this.maquinaCmbBx);
            this.headerGB.Controls.Add(this.enableIntChkBx);
            this.headerGB.Controls.Add(this.locateLbl);
            this.headerGB.Controls.Add(this.reubicaEdt);
            this.headerGB.Controls.Add(this.pokeLbl);
            this.headerGB.Controls.Add(this.pokeEdt);
            this.headerGB.Controls.Add(this.clearLbl);
            this.headerGB.Controls.Add(this.clearEdt);
            this.headerGB.Controls.Add(this.usrLbl);
            this.headerGB.Controls.Add(this.usrEdt);
            this.headerGB.Controls.Add(this.addBtn);
            this.headerGB.Controls.Add(this.blocksLbl);
            this.headerGB.Controls.Add(this.bloksEdt);
            this.headerGB.Controls.Add(this.label2);
            this.headerGB.Controls.Add(this.originEdt);
            this.headerGB.Location = new System.Drawing.Point(8, 40);
            this.headerGB.Name = "headerGB";
            this.headerGB.Size = new System.Drawing.Size(353, 161);
            this.headerGB.TabIndex = 1;
            this.headerGB.TabStop = false;
            this.headerGB.Text = "Header";

            this.nameLbl.Location = new System.Drawing.Point(16, 72);
            this.nameLbl.Size = new System.Drawing.Size(70, 13);
            this.nameLbl.Text = "Loading name";

            this.nameEdt.Location = new System.Drawing.Point(88, 72);
            this.nameEdt.Size = new System.Drawing.Size(89, 20);

            this.maquinaCmbBx.Items.AddRange(new object[] { "ZX", "CPC", "MSX", "81" });
            this.maquinaCmbBx.Location = new System.Drawing.Point(32, 16);
            this.maquinaCmbBx.Size = new System.Drawing.Size(153, 21);

            this.aditionalLbl.Location = new System.Drawing.Point(16, 96);
            this.aditionalLbl.Size = new System.Drawing.Size(70, 13);
            this.aditionalLbl.Text = "Aditional info";

            this.aditionalInfoEdt.Location = new System.Drawing.Point(88, 96);
            this.aditionalInfoEdt.Size = new System.Drawing.Size(89, 20);

            this.modelLbl.Location = new System.Drawing.Point(200, 16);
            this.modelLbl.Size = new System.Drawing.Size(40, 13);
            this.modelLbl.Text = "Model";

            this.modelCmbBx.Location = new System.Drawing.Point(248, 16);
            this.modelCmbBx.Size = new System.Drawing.Size(73, 21);

            this.enableIntChkBx.Location = new System.Drawing.Point(216, 40);
            this.enableIntChkBx.Size = new System.Drawing.Size(113, 17);
            this.enableIntChkBx.Text = "Enable interrupts";

            this.locateLbl.Location = new System.Drawing.Point(209, 64);
            this.locateLbl.Size = new System.Drawing.Size(50, 13);
            this.locateLbl.Text = "Locate at";

            this.reubicaEdt.Location = new System.Drawing.Point(264, 64);
            this.reubicaEdt.Size = new System.Drawing.Size(37, 20);

            this.pokeLbl.Location = new System.Drawing.Point(216, 88);
            this.pokeLbl.Size = new System.Drawing.Size(50, 13);
            this.pokeLbl.Text = "POKE -1";

            this.pokeEdt.Location = new System.Drawing.Point(264, 88);
            this.pokeEdt.Size = new System.Drawing.Size(41, 20);

            this.clearLbl.Location = new System.Drawing.Point(224, 112);
            this.clearLbl.Size = new System.Drawing.Size(40, 13);
            this.clearLbl.Text = "CLEAR";

            this.clearEdt.Location = new System.Drawing.Point(264, 112);
            this.clearEdt.Size = new System.Drawing.Size(65, 20);

            this.usrLbl.Location = new System.Drawing.Point(232, 136);
            this.usrLbl.Size = new System.Drawing.Size(30, 13);
            this.usrLbl.Text = "USR";

            this.usrEdt.Location = new System.Drawing.Point(264, 136);
            this.usrEdt.Size = new System.Drawing.Size(65, 20);

            this.blocksLbl.Location = new System.Drawing.Point(88, 128);
            this.blocksLbl.Size = new System.Drawing.Size(40, 13);
            this.blocksLbl.Text = "Blocks";

            this.bloksEdt.Location = new System.Drawing.Point(120, 128);
            this.bloksEdt.Size = new System.Drawing.Size(25, 20);
            this.bloksEdt.ReadOnly = true;

            this.label2.Location = new System.Drawing.Point(152, 128);
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.Text = "Origin";

            this.originEdt.Location = new System.Drawing.Point(184, 128);
            this.originEdt.Size = new System.Drawing.Size(25, 20);
            this.originEdt.ReadOnly = true;

            this.addBtn.Location = new System.Drawing.Point(8, 128);
            this.addBtn.Size = new System.Drawing.Size(73, 25);
            this.addBtn.Text = "Add blocks...";

            // BlocksLV
            this.blocksLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colBlockName, this.colSize, this.colType, this.colStart, this.colJump, this.colExec});
            this.blocksLV.Location = new System.Drawing.Point(8, 208);
            this.blocksLV.Name = "blocksLV";
            this.blocksLV.Size = new System.Drawing.Size(353, 113);
            this.blocksLV.TabIndex = 0;
            this.blocksLV.View = System.Windows.Forms.View.Details;

            this.colBlockName.Text = "Block name";
            this.colBlockName.Width = 100;
            this.colSize.Text = "Size";
            this.colSize.Width = 46;
            this.colType.Text = "Type";
            this.colType.Width = 80;
            this.colStart.Text = "Start";
            this.colJump.Text = "Jump";
            this.colJump.Width = 0;
            this.colExec.Text = "Exec";

            // PlayerGB
            this.playerGB.Controls.Add(this.loaderGB);
            this.playerGB.Controls.Add(this.wavGB);
            this.playerGB.Controls.Add(this.playBtn);
            this.playerGB.Controls.Add(this.wavBtn);
            this.playerGB.Controls.Add(this.mp3Btn);
            this.playerGB.Controls.Add(this.tzxBtn);
            this.playerGB.Location = new System.Drawing.Point(8, 355);
            this.playerGB.Name = "playerGB";
            this.playerGB.Size = new System.Drawing.Size(369, 206);
            this.playerGB.TabIndex = 2;
            this.playerGB.TabStop = false;
            this.playerGB.Text = "Player";

            // LoaderGB
            this.loaderGB.Controls.Add(this.makeLoaderChkBx);
            this.loaderGB.Controls.Add(this.accelerateChkBx);
            this.loaderGB.Controls.Add(this.checkLoadErrorChkBx);
            this.loaderGB.Controls.Add(this.releMotorChkBx);
            this.loaderGB.Location = new System.Drawing.Point(8, 16);
            this.loaderGB.Name = "loaderGB";
            this.loaderGB.Size = new System.Drawing.Size(129, 153);
            this.loaderGB.Text = "Loader Settings";

            this.makeLoaderChkBx.Location = new System.Drawing.Point(8, 24);
            this.makeLoaderChkBx.Size = new System.Drawing.Size(97, 17);
            this.makeLoaderChkBx.Text = "Make loader";
            this.makeLoaderChkBx.Checked = true;

            this.accelerateChkBx.Location = new System.Drawing.Point(8, 56);
            this.accelerateChkBx.Size = new System.Drawing.Size(105, 17);
            this.accelerateChkBx.Text = "Accelerate";

            this.checkLoadErrorChkBx.Location = new System.Drawing.Point(8, 88);
            this.checkLoadErrorChkBx.Size = new System.Drawing.Size(113, 17);
            this.checkLoadErrorChkBx.Text = "Check loading error";
            this.checkLoadErrorChkBx.Checked = true;

            this.releMotorChkBx.Location = new System.Drawing.Point(8, 120);
            this.releMotorChkBx.Size = new System.Drawing.Size(113, 17);
            this.releMotorChkBx.Text = "Cassette motor rele";
            this.releMotorChkBx.Checked = true;

            // WavGB
            this.wavGB.Controls.Add(this.label4);
            this.wavGB.Controls.Add(this.frecuenciaCmbBx);
            this.wavGB.Controls.Add(this.bpsLbl);
            this.wavGB.Controls.Add(this.muestrasCmbBx);
            this.wavGB.Controls.Add(this.label3);
            this.wavGB.Controls.Add(this.formaCmbBx);
            this.wavGB.Controls.Add(this.invertChkBx);
            this.wavGB.Controls.Add(this.pausaEdt);
            this.wavGB.Controls.Add(this.label9);
            this.wavGB.Location = new System.Drawing.Point(144, 16);
            this.wavGB.Name = "wavGB";
            this.wavGB.Size = new System.Drawing.Size(217, 153);
            this.wavGB.Text = "Wav Settings";

            this.label4.Location = new System.Drawing.Point(8, 19);
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.Text = "Sampling frequency";

            this.frecuenciaCmbBx.Location = new System.Drawing.Point(104, 19);
            this.frecuenciaCmbBx.Size = new System.Drawing.Size(105, 21);
            this.frecuenciaCmbBx.DropDownStyle = ComboBoxStyle.DropDownList;

            this.bpsLbl.Location = new System.Drawing.Point(16, 43);
            this.bpsLbl.Size = new System.Drawing.Size(80, 13);
            this.bpsLbl.Text = "samples/bit (bps)";

            this.muestrasCmbBx.Location = new System.Drawing.Point(104, 43);
            this.muestrasCmbBx.Size = new System.Drawing.Size(105, 21);
            this.muestrasCmbBx.DropDownStyle = ComboBoxStyle.DropDownList;

            this.label3.Location = new System.Drawing.Point(48, 67);
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.Text = "Waveform";

            this.formaCmbBx.Location = new System.Drawing.Point(104, 67);
            this.formaCmbBx.Size = new System.Drawing.Size(105, 21);
            this.formaCmbBx.DropDownStyle = ComboBoxStyle.DropDownList;

            this.invertChkBx.Location = new System.Drawing.Point(8, 101);
            this.invertChkBx.Size = new System.Drawing.Size(121, 17);
            this.invertChkBx.Text = "Inverse wav polarity";

            this.pausaEdt.Location = new System.Drawing.Point(8, 125);
            this.pausaEdt.Size = new System.Drawing.Size(41, 20);
            this.pausaEdt.Text = "500";

            this.label9.Location = new System.Drawing.Point(56, 128);
            this.label9.Size = new System.Drawing.Size(123, 13);
            this.label9.Text = "ms pause between blocks";

            this.playBtn.Text = "PLAY";
            this.playBtn.Location = new System.Drawing.Point(16, 175);
            this.playBtn.Size = new System.Drawing.Size(73, 24);

            this.wavBtn.Text = "SBB => WAV";
            this.wavBtn.Location = new System.Drawing.Point(104, 176);
            this.wavBtn.Size = new System.Drawing.Size(73, 24);

            this.mp3Btn.Text = "SBB => MP3";
            this.mp3Btn.Location = new System.Drawing.Point(192, 176);
            this.mp3Btn.Size = new System.Drawing.Size(73, 24);

            this.tzxBtn.Text = "SBB => TZX";
            this.tzxBtn.Location = new System.Drawing.Point(277, 176);
            this.tzxBtn.Size = new System.Drawing.Size(76, 24);

            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusLabel });
            this.statusBar1.Location = new System.Drawing.Point(0, 578);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(386, 22);
            this.statusBar1.TabIndex = 3;

            // MainFrame
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 600);
            this.Controls.Add(this.builderGB);
            this.Controls.Add(this.playerGB);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFrame";
            this.Text = "o.t.l.a. .NET Modernized";

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.builderGB.ResumeLayout(false);
            this.builderGB.PerformLayout();
            this.headerGB.ResumeLayout(false);
            this.headerGB.PerformLayout();
            this.playerGB.ResumeLayout(false);
            this.loaderGB.ResumeLayout(false);
            this.wavGB.ResumeLayout(false);
            this.wavGB.PerformLayout();
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newAddMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertMenu;
        private System.Windows.Forms.ToolStripMenuItem toSoundMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toWavMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toMp3MenuItem;
        private System.Windows.Forms.ToolStripMenuItem toTzxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenu;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem editBlocksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchSbbMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchWavMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adjustAudioMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wavPlayerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem wikiMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;

        private System.Windows.Forms.GroupBox builderGB;
        private System.Windows.Forms.TextBox fileEdt;
        private System.Windows.Forms.GroupBox headerGB;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.TextBox nameEdt;
        private System.Windows.Forms.Label aditionalLbl;
        private System.Windows.Forms.TextBox aditionalInfoEdt;
        private System.Windows.Forms.Label modelLbl;
        private System.Windows.Forms.ComboBox modelCmbBx;
        private System.Windows.Forms.ComboBox maquinaCmbBx;
        private System.Windows.Forms.CheckBox enableIntChkBx;
        private System.Windows.Forms.Label locateLbl;
        private System.Windows.Forms.TextBox reubicaEdt;
        private System.Windows.Forms.Label pokeLbl;
        private System.Windows.Forms.TextBox pokeEdt;
        private System.Windows.Forms.Label clearLbl;
        private System.Windows.Forms.TextBox clearEdt;
        private System.Windows.Forms.Label usrLbl;
        private System.Windows.Forms.TextBox usrEdt;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Label blocksLbl;
        private System.Windows.Forms.TextBox bloksEdt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox originEdt;

        private System.Windows.Forms.ListView blocksLV;
        private System.Windows.Forms.ColumnHeader colBlockName;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colStart;
        private System.Windows.Forms.ColumnHeader colJump;
        private System.Windows.Forms.ColumnHeader colExec;

        private System.Windows.Forms.GroupBox playerGB;
        private System.Windows.Forms.GroupBox loaderGB;
        private System.Windows.Forms.CheckBox makeLoaderChkBx;
        private System.Windows.Forms.CheckBox accelerateChkBx;
        private System.Windows.Forms.CheckBox checkLoadErrorChkBx;
        private System.Windows.Forms.CheckBox releMotorChkBx;

        private System.Windows.Forms.GroupBox wavGB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox frecuenciaCmbBx;
        private System.Windows.Forms.Label bpsLbl;
        private System.Windows.Forms.ComboBox muestrasCmbBx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox formaCmbBx;
        private System.Windows.Forms.CheckBox invertChkBx;
        private System.Windows.Forms.TextBox pausaEdt;
        private System.Windows.Forms.Label label9;

        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button wavBtn;
        private System.Windows.Forms.Button mp3Btn;
        private System.Windows.Forms.Button tzxBtn;

        private System.Windows.Forms.StatusStrip statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}
