using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Otla.Net.Models;
using Otla.Net.Logic.Formats;

namespace Otla.Net.UI
{
    public partial class MainFrame : Form
    {
        private SbbHeader _currentHeader = new SbbHeader();
        private List<SbbBlock> _currentBlocks = new List<SbbBlock>();
        private Image[] _machineImages;
        private Image[] _waveImages;

        public MainFrame()
        {
            InitializeComponent();
            LoadAssets();
            SetupInitialState();
        }

        private void LoadAssets()
        {
            try
            {
                _machineImages = new Image[]
                {
                    GetResourceImage("machine_zx.bmp"),
                    GetResourceImage("machine_cpc.bmp"),
                    GetResourceImage("machine_msx.bmp"),
                    GetResourceImage("machine_81.bmp")
                };

                _waveImages = new Image[]
                {
                    GetResourceImage("wave_Square.bmp"),
                    GetResourceImage("wave_Cubic.bmp"),
                    GetResourceImage("wave_SqrSin.bmp"),
                    GetResourceImage("wave_Shaw.bmp"),
                    GetResourceImage("wave_Ecte.bmp"),
                    GetResourceImage("wave_Delta.bmp")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading assets: " + ex.Message);
            }
        }

        private Image GetResourceImage(string name)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            // In modern .csproj, resource names might include the path
            string resourceName = $"Otla.Net.Assets.{name}";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;
                return Image.FromStream(stream);
            }
        }

        private void SetupInitialState()
        {
            this.maquinaCmbBx.SelectedIndex = 0; // Default ZX
            this.frecuenciaCmbBx.Items.AddRange(new object[] { "44100 Hz", "48000 Hz" });
            this.frecuenciaCmbBx.SelectedIndex = 0;
            this.formaCmbBx.Items.AddRange(new object[] { "Square", "Cubic", "SqrSin", "Shaw", "E=cte", "Delta" });
            this.formaCmbBx.SelectedIndex = 0;
            this.muestrasCmbBx.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6" });
            this.muestrasCmbBx.SelectedIndex = 1;

            this.statusLabel.Text = "Ready";

            // Event bindings
            this.addBtn.Click += AddBtn_Click;
            this.newMenuItem.Click += (s, e) => ClearData();
            this.quitMenuItem.Click += (s, e) => Application.Exit();
            this.wavBtn.Click += WavBtn_Click;
            this.playBtn.Click += PlayBtn_Click;

            this.maquinaCmbBx.DrawItem += MaquinaCmbBx_DrawItem;
            this.formaCmbBx.DrawItem += FormaCmbBx_DrawItem;
        }

        private void MaquinaCmbBx_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            if (_machineImages != null && e.Index < _machineImages.Length && _machineImages[e.Index] != null)
            {
                e.Graphics.DrawImage(_machineImages[e.Index], e.Bounds.Left, e.Bounds.Top);
            }
            else
            {
                e.Graphics.DrawString(maquinaCmbBx.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void FormaCmbBx_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            int imgWidth = 0;
            if (_waveImages != null && e.Index < _waveImages.Length && _waveImages[e.Index] != null)
            {
                e.Graphics.DrawImage(_waveImages[e.Index], e.Bounds.Left, e.Bounds.Top);
                imgWidth = _waveImages[e.Index].Width + 5;
            }

            string text = formaCmbBx.Items[e.Index].ToString();
            using (Brush brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds.Left + imgWidth, e.Bounds.Top + (e.Bounds.Height - e.Font.Height) / 2);
            }
            e.DrawFocusRectangle();
        }

        private void ClearData()
        {
            _currentBlocks.Clear();
            _currentHeader = new SbbHeader();
            fileEdt.Text = "";
            nameEdt.Text = "";
            blocksLV.Items.Clear();
            statusLabel.Text = "Data cleared";
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Legacy Files|*.rom;*.tap;*.sna;*.z80;*.cas;*.p;*.z81|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    statusLabel.Text = $"Loading {ofd.FileName}...";
                    try {
                        if (ofd.FileName.EndsWith(".rom", StringComparison.OrdinalIgnoreCase))
                        {
                            var blocks = MsxRomConverter.ConvertRomToBlocks(ofd.FileName);
                            _currentBlocks.AddRange(blocks);
                            UpdateListView();
                            statusLabel.Text = "MSX ROM loaded (16-bit bypass)";
                        }
                        else if (ofd.FileName.EndsWith(".tap", StringComparison.OrdinalIgnoreCase))
                        {
                            var result = ZxSpectrumFiles.LoadTap(ofd.FileName);
                            _currentHeader = result.Header;
                            _currentBlocks.AddRange(result.Blocks);
                            UpdateListView();
                            statusLabel.Text = "ZX Tap loaded";
                        }
                    } catch (Exception ex) {
                        MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UpdateListView()
        {
            blocksLV.Items.Clear();
            foreach (var block in _currentBlocks)
            {
                var item = new ListViewItem(block.BlockName);
                item.SubItems.Add(block.Size.ToString());
                item.SubItems.Add(block.BlockType.ToString());
                item.SubItems.Add(block.Ini.ToString("X4"));
                item.SubItems.Add(block.Jump.ToString("X2"));
                item.SubItems.Add(block.Exec.ToString("X4"));
                blocksLV.Items.Add(item);
            }
        }

        private void WavBtn_Click(object sender, EventArgs e)
        {
            DoConversion(null);
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            string tempWav = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "otla_play.wav");
            if (DoConversion(tempWav))
            {
                statusLabel.Text = "Playing...";
                try
                {
                    using (var player = new System.Media.SoundPlayer(tempWav))
                    {
                        player.PlaySync();
                    }
                    statusLabel.Text = "Playback finished";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Playback error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool DoConversion(string targetPath)
        {
            if (_currentBlocks.Count == 0)
            {
                MessageBox.Show("No blocks to convert.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            string outputPath = targetPath;
            bool isTemp = false;

            if (string.IsNullOrEmpty(outputPath))
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "WAV Audio|*.wav";
                    if (sfd.ShowDialog() != DialogResult.OK) return false;
                    outputPath = sfd.FileName;
                }
            }
            else
            {
                isTemp = true;
            }

            statusLabel.Text = "Generating WAV...";
            try
            {
                int freq = frecuenciaCmbBx.SelectedIndex == 1 ? 48000 : 44100;
                WaveformType waveform = (WaveformType)formaCmbBx.SelectedIndex;

                var generator = new Otla.Net.Logic.Audio.WavGenerator(freq, waveform);
                generator.GenerateZxtapWav(outputPath, _currentBlocks);

                if (!isTemp)
                    statusLabel.Text = $"WAV saved to {outputPath}";

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating WAV: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error in generation";
                return false;
            }
        }
    }
}
