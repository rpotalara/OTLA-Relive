using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Otla.Net.UI
{
    public partial class PlayerMonitor : Form
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnValue, int returnLength, IntPtr winHandle);

        private string _wavePath;
        private Timer _timer;
        private bool _isPaused = false;
        private byte[] _audioData;
        private int _sampleRate;

        public PlayerMonitor(string wavePath)
        {
            InitializeComponent();
            _wavePath = wavePath;
            LoadAudioData();
            InitializePlayer();
        }

        private void LoadAudioData()
        {
            try
            {
                if (File.Exists(_wavePath))
                {
                    _audioData = File.ReadAllBytes(_wavePath);
                    // Basic WAV parsing to find sample rate and data start
                    // Assuming 8-bit mono for now as per image "44100 Hz 8 bit mono"
                    if (_audioData.Length > 44)
                    {
                        _sampleRate = BitConverter.ToInt32(_audioData, 24);
                        labelFormat.Text = $"{_sampleRate} Hz 8 bit mono";
                        labelFilePath.Text = _wavePath;
                    }
                }
            }
            catch { }
        }

        private void InitializePlayer()
        {
            mciSendString($"close myaudio", null, 0, IntPtr.Zero);
            mciSendString($"open \"{_wavePath}\" type waveaudio alias myaudio", null, 0, IntPtr.Zero);

            _timer = new Timer();
            _timer.Interval = 50;
            _timer.Tick += Timer_Tick;

            LoadSoundCards();

            trackBarVolume.Value = 80;
            UpdateVolume();
        }

        private void LoadSoundCards()
        {
            // Simplified: just show a placeholder as per original
            comboBoxSoundCard.Items.Add("Default Playback Device");
            comboBoxSoundCard.SelectedIndex = 0;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(128);
            mciSendString("status myaudio position", sb, 128, IntPtr.Zero);
            int pos = 0;
            int.TryParse(sb.ToString(), out pos);

            mciSendString("status myaudio length", sb, 128, IntPtr.Zero);
            int len = 1;
            int.TryParse(sb.ToString(), out len);

            if (len > 0)
            {
                progressBar.Value = Math.Min(100, (int)((pos * 100L) / len));
                labelTime.Text = $"Time :{(pos / 1000.0):F1}";
            }

            if (pos >= len && len > 0)
            {
                StopPlayback();
                if (checkBoxCloseOnFinished.Checked)
                {
                    this.Close();
                }
            }

            pictureBoxWave.Invalidate();
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (!_isPaused)
            {
                mciSendString("pause myaudio", null, 0, IntPtr.Zero);
                btnPlayPause.Text = "Resume";
                _isPaused = true;
            }
            else
            {
                mciSendString("resume myaudio", null, 0, IntPtr.Zero);
                btnPlayPause.Text = "Pause";
                _isPaused = false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        private void StopPlayback()
        {
            mciSendString("stop myaudio", null, 0, IntPtr.Zero);
            mciSendString("seek myaudio to start", null, 0, IntPtr.Zero);
            btnPlayPause.Text = "Pause";
            _isPaused = false;
            _timer.Stop();
            progressBar.Value = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PlayerMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            mciSendString("close myaudio", null, 0, IntPtr.Zero);
            _timer.Stop();
        }

        public void StartPlayback()
        {
            mciSendString("play myaudio", null, 0, IntPtr.Zero);
            _timer.Start();
        }

        private void UpdateVolume()
        {
            int vol = trackBarVolume.Value * 10; // 0-1000
            mciSendString($"setaudio myaudio volume to {vol}", null, 0, IntPtr.Zero);
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            UpdateVolume();
        }

        private void pictureBoxWave_Paint(object sender, PaintEventArgs e)
        {
            if (_audioData == null || _audioData.Length < 45) return;

            StringBuilder sb = new StringBuilder(128);
            mciSendString("status myaudio position", sb, 128, IntPtr.Zero);
            int posMs = 0;
            int.TryParse(sb.ToString(), out posMs);

            // Calculate current sample index
            long currentSample = (long)((posMs / 1000.0) * _sampleRate);
            int dataStart = 44; // Standard WAV header size

            e.Graphics.Clear(Color.Black);
            using (Pen pen = new Pen(Color.Green, 2))
            {
                int w = pictureBoxWave.Width;
                int h = pictureBoxWave.Height;
                int midY = h / 2;

                Point[] points = new Point[w];
                for (int x = 0; x < w; x++)
                {
                    long sampleIdx = currentSample + x * 10; // Zoom factor
                    if (sampleIdx + dataStart < _audioData.Length)
                    {
                        byte val = _audioData[dataStart + sampleIdx];
                        int y = midY + (int)((val - 128) * (h / 256.0));
                        points[x] = new Point(x, y);
                    }
                    else
                    {
                        points[x] = new Point(x, midY);
                    }
                }
                if (w > 1)
                    e.Graphics.DrawLines(pen, points);
            }
        }
    }
}
