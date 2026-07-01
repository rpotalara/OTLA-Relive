namespace Otla.Net.UI
{
    partial class PlayerMonitor
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

        private void InitializeComponent()
        {
            this.btnFile = new System.Windows.Forms.Button();
            this.labelFilePath = new System.Windows.Forms.Label();
            this.labelFormat = new System.Windows.Forms.Label();
            this.labelSoundCard = new System.Windows.Forms.Label();
            this.comboBoxSoundCard = new System.Windows.Forms.ComboBox();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.labelMute = new System.Windows.Forms.Label();
            this.checkBoxMute = new System.Windows.Forms.CheckBox();
            this.pictureBoxWave = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelTime = new System.Windows.Forms.Label();
            this.checkBoxCloseOnFinished = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWave)).BeginInit();
            this.SuspendLayout();
            //
            // btnFile
            //
            this.btnFile.Enabled = false;
            this.btnFile.Location = new System.Drawing.Point(12, 12);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(55, 23);
            this.btnFile.TabIndex = 0;
            this.btnFile.Text = "File...";
            this.btnFile.UseVisualStyleBackColor = true;
            //
            // labelFilePath
            //
            this.labelFilePath.AutoEllipsis = true;
            this.labelFilePath.Location = new System.Drawing.Point(73, 12);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(305, 13);
            this.labelFilePath.TabIndex = 1;
            this.labelFilePath.Text = "path...";
            //
            // labelFormat
            //
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new System.Drawing.Point(73, 27);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(95, 13);
            this.labelFormat.TabIndex = 2;
            this.labelFormat.Text = "44100 Hz 8 bit mono";
            //
            // labelSoundCard
            //
            this.labelSoundCard.AutoSize = true;
            this.labelSoundCard.Location = new System.Drawing.Point(12, 45);
            this.labelSoundCard.Name = "labelSoundCard";
            this.labelSoundCard.Size = new System.Drawing.Size(61, 13);
            this.labelSoundCard.TabIndex = 3;
            this.labelSoundCard.Text = "SoundCard";
            //
            // comboBoxSoundCard
            //
            this.comboBoxSoundCard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSoundCard.FormattingEnabled = true;
            this.comboBoxSoundCard.Location = new System.Drawing.Point(76, 42);
            this.comboBoxSoundCard.Name = "comboBoxSoundCard";
            this.comboBoxSoundCard.Size = new System.Drawing.Size(194, 21);
            this.comboBoxSoundCard.TabIndex = 4;
            //
            // btnPlayPause
            //
            this.btnPlayPause.Location = new System.Drawing.Point(276, 40);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(55, 23);
            this.btnPlayPause.TabIndex = 5;
            this.btnPlayPause.Text = "Pause";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            //
            // btnStop
            //
            this.btnStop.Location = new System.Drawing.Point(337, 40);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(41, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            //
            // trackBarVolume
            //
            this.trackBarVolume.Location = new System.Drawing.Point(15, 80);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarVolume.Size = new System.Drawing.Size(45, 75);
            this.trackBarVolume.TabIndex = 7;
            this.trackBarVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarVolume.Scroll += new System.EventHandler(this.trackBarVolume_Scroll);
            //
            // labelMute
            //
            this.labelMute.AutoSize = true;
            this.labelMute.Location = new System.Drawing.Point(35, 66);
            this.labelMute.Name = "labelMute";
            this.labelMute.Size = new System.Drawing.Size(31, 13);
            this.labelMute.TabIndex = 8;
            this.labelMute.Text = "Mute";
            //
            // checkBoxMute
            //
            this.checkBoxMute.AutoSize = true;
            this.checkBoxMute.Location = new System.Drawing.Point(15, 66);
            this.checkBoxMute.Name = "checkBoxMute";
            this.checkBoxMute.Size = new System.Drawing.Size(15, 14);
            this.checkBoxMute.TabIndex = 9;
            this.checkBoxMute.UseVisualStyleBackColor = true;
            //
            // pictureBoxWave
            //
            this.pictureBoxWave.BackColor = System.Drawing.Color.Black;
            this.pictureBoxWave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxWave.Location = new System.Drawing.Point(73, 67);
            this.pictureBoxWave.Name = "pictureBoxWave";
            this.pictureBoxWave.Size = new System.Drawing.Size(305, 88);
            this.pictureBoxWave.TabIndex = 10;
            this.pictureBoxWave.TabStop = false;
            this.pictureBoxWave.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxWave_Paint);
            //
            // progressBar
            //
            this.progressBar.Location = new System.Drawing.Point(73, 161);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(305, 15);
            this.progressBar.TabIndex = 11;
            //
            // labelTime
            //
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(12, 161);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(48, 13);
            this.labelTime.TabIndex = 12;
            this.labelTime.Text = "Time :0.0";
            //
            // checkBoxCloseOnFinished
            //
            this.checkBoxCloseOnFinished.AutoSize = true;
            this.checkBoxCloseOnFinished.Checked = true;
            this.checkBoxCloseOnFinished.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCloseOnFinished.Location = new System.Drawing.Point(15, 185);
            this.checkBoxCloseOnFinished.Name = "checkBoxCloseOnFinished";
            this.checkBoxCloseOnFinished.Size = new System.Drawing.Size(107, 17);
            this.checkBoxCloseOnFinished.TabIndex = 13;
            this.checkBoxCloseOnFinished.Text = "Close on finished";
            this.checkBoxCloseOnFinished.UseVisualStyleBackColor = true;
            //
            // btnClose
            //
            this.btnClose.Location = new System.Drawing.Point(257, 182);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(121, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // PlayerMonitor
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 217);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.checkBoxCloseOnFinished);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBoxWave);
            this.Controls.Add(this.checkBoxMute);
            this.Controls.Add(this.labelMute);
            this.Controls.Add(this.trackBarVolume);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlayPause);
            this.Controls.Add(this.comboBoxSoundCard);
            this.Controls.Add(this.labelSoundCard);
            this.Controls.Add(this.labelFormat);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this.btnFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlayerMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Player Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerMonitor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.Label labelSoundCard;
        private System.Windows.Forms.ComboBox comboBoxSoundCard;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.Label labelMute;
        private System.Windows.Forms.CheckBox checkBoxMute;
        private System.Windows.Forms.PictureBox pictureBoxWave;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.CheckBox checkBoxCloseOnFinished;
        private System.Windows.Forms.Button btnClose;
    }
}
