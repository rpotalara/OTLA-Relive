using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using Otla.Net.Models;
using Otla.Net.Logic.Formats;
using Otla.Net.Logic.Persistence;

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
            this.frecuenciaCmbBx.Items.Clear();
            this.frecuenciaCmbBx.Items.AddRange(new object[] { "44100 Hz", "48000 Hz" });
            this.frecuenciaCmbBx.SelectedIndex = 0;

            this.formaCmbBx.Items.Clear();
            this.formaCmbBx.Items.AddRange(new object[] { "Square", "Cubic", "SqrSin", "Shaw", "E=cte", "Delta" });
            this.formaCmbBx.SelectedIndex = 1; // Default to Cubic as in screenshot

            this.maquinaCmbBx.SelectedIndex = 0; // Default ZX

            this.statusLabel.Text = "Ready";

            // Event bindings
            this.addBtn.Click += AddBtn_Click;

            // File Menu
            this.newMenuItem.Click += (s, e) => ClearData();
            this.newAddMenuItem.Click += (s, e) => { ClearData(); AddBtn_Click(s, e); };
            this.openMenuItem.Click += (s, e) => OpenSbb();
            this.saveMenuItem.Click += (s, e) => SaveSbb(false);
            this.saveAsMenuItem.Click += (s, e) => SaveSbb(true);
            this.quitMenuItem.Click += (s, e) => Application.Exit();

            // Convert Menu
            this.toSoundMenuItem.Click += PlayBtn_Click;
            this.toWavMenuItem.Click += WavBtn_Click;
            this.toMp3MenuItem.Click += (s, e) => MessageBox.Show("MP3 conversion requires external LAME encoder. Feature coming soon in .NET port.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.toTzxMenuItem.Click += (s, e) => MessageBox.Show("TZX conversion is not yet implemented in the .NET port.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Tools Menu
            this.editBlocksMenuItem.Click += (s, e) => MessageBox.Show("Block editing is available via the 'Add blocks' workflow. Full editor coming soon.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.batchSbbMenuItem.Click += (s, e) => MessageBox.Show("Batch .sbb processing not yet implemented.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.batchWavMenuItem.Click += (s, e) => MessageBox.Show("Batch .wav processing not yet implemented.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.adjustAudioMenuItem.Click += (s, e) => MessageBox.Show("Audio adjustment settings not yet implemented.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.wavPlayerMenuItem.Click += PlayBtn_Click;

            // Settings Menu
            this.settingsMenu.Click += (s, e) => MessageBox.Show("General settings not yet implemented.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Help Menu
            this.wikiMenuItem.Click += (s, e) => System.Diagnostics.Process.Start("https://code.google.com/archive/p/otla/");
            this.aboutMenuItem.Click += (s, e) => MessageBox.Show("OTLA .NET Modernized v2.2\n\nPorted from Borland C++ Builder 6 project.\n\nAuthor: original by Hernán Moraldo, port by Jules.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.wavBtn.Click += WavBtn_Click;
            this.playBtn.Click += PlayBtn_Click;

            this.maquinaCmbBx.DrawItem += MaquinaCmbBx_DrawItem;
            this.formaCmbBx.DrawItem += FormaCmbBx_DrawItem;

            this.maquinaCmbBx.SelectedIndexChanged += MaquinaCmbBx_SelectedIndexChanged;
            this.frecuenciaCmbBx.SelectedIndexChanged += (s, e) => UpdateMuestras();

            ClearData();

            // Trigger initial state
            MaquinaCmbBx_SelectedIndexChanged(null, null);
        }

        private void UpdateMuestras()
        {
            int selectedIndex = muestrasCmbBx.SelectedIndex;
            muestrasCmbBx.Items.Clear();

            int machineIdx = maquinaCmbBx.SelectedIndex;
            bool is48k = frecuenciaCmbBx.SelectedIndex == 1;

            if (machineIdx == 3) // 81
            {
                if (is48k)
                {
                    muestrasCmbBx.Items.Add("6 ( 8000 bps)");
                    muestrasCmbBx.Items.Add("5 ( 9600 bps)");
                    muestrasCmbBx.Items.Add("4 (12000 bps)");
                    muestrasCmbBx.Items.Add("3 (16200 bps)");
                }
                else
                {
                    muestrasCmbBx.Items.Add("6 ( 7350 bps)");
                    muestrasCmbBx.Items.Add("5 ( 8820 bps)");
                    muestrasCmbBx.Items.Add("4 (11025 bps)");
                    muestrasCmbBx.Items.Add("3 (14700 bps)");
                }
            }
            else
            {
                if (is48k)
                {
                    muestrasCmbBx.Items.Add("4   (12000 bps)");
                    muestrasCmbBx.Items.Add("3.5 (13600 bps)");
                    muestrasCmbBx.Items.Add("3   (16200 bps)");
                    muestrasCmbBx.Items.Add("2.5 (19200 bps)");
                    if (machineIdx != 0) // Not ZX
                    {
                        muestrasCmbBx.Items.Add("2.75 (17454 bps)");
                        muestrasCmbBx.Items.Add("2.25 (21333 bps)");
                        muestrasCmbBx.Items.Add("1.75 (27428 bps)");
                    }
                }
                else
                {
                    muestrasCmbBx.Items.Add("4   (11025 bps)");
                    muestrasCmbBx.Items.Add("3.5 (12600 bps)");
                    muestrasCmbBx.Items.Add("3   (14700 bps)");
                    muestrasCmbBx.Items.Add("2.5 (17640 bps)");
                    if (machineIdx != 0) // Not ZX
                    {
                        muestrasCmbBx.Items.Add("2.75 (16036 bps)");
                        muestrasCmbBx.Items.Add("2.25 (19600 bps)");
                        muestrasCmbBx.Items.Add("1.75 (25200 bps)");
                    }
                }
            }

            if (selectedIndex >= 0 && selectedIndex < muestrasCmbBx.Items.Count)
                muestrasCmbBx.SelectedIndex = selectedIndex;
            else if (muestrasCmbBx.Items.Count > 1)
                muestrasCmbBx.SelectedIndex = 1; // Default to 3.5 or equivalent
            else if (muestrasCmbBx.Items.Count > 0)
                muestrasCmbBx.SelectedIndex = 0;
        }

        private void MaquinaCmbBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = maquinaCmbBx.SelectedIndex;
            if (idx < 0) return;

            // Update Model ComboBox
            modelCmbBx.Items.Clear();
            switch (idx)
            {
                case 0: // ZX
                    modelCmbBx.Items.AddRange(new object[] { "48k", "128k", "+2a" });
                    break;
                case 1: // CPC
                    modelCmbBx.Items.AddRange(new object[] { "464", "6128", "664" });
                    break;
                case 2: // MSX
                    modelCmbBx.Items.AddRange(new object[] { "16k", "32k", "64k" });
                    break;
                case 3: // 81
                    modelCmbBx.Items.AddRange(new object[] { "1k", "2k", "16k", "48k", "64k" });
                    break;
            }
            if (modelCmbBx.Items.Count > 0) modelCmbBx.SelectedIndex = 0;

            // Reset defaults
            locateLbl.Enabled = false;
            reubicaEdt.Enabled = false;
            pokeLbl.Enabled = false;
            pokeEdt.Enabled = false;
            clearLbl.Enabled = true;
            clearEdt.Enabled = true;
            usrLbl.Enabled = true;
            usrEdt.Enabled = true;

            checkLoadErrorChkBx.Enabled = true;
            enableIntChkBx.Enabled = true;

            switch (idx)
            {
                case 0: // ZX
                    nameEdt.MaxLength = 10;
                    clearLbl.Text = "CLEAR";
                    usrLbl.Text = "USR";
                    releMotorChkBx.Enabled = false;
                    tzxBtn.Text = "SBB => TZX";
                    reubicaEdt.Text = "255";
                    break;
                case 1: // CPC
                    nameEdt.MaxLength = 16;
                    clearLbl.Text = "SP";
                    usrLbl.Text = "JP";
                    locateLbl.Enabled = true;
                    reubicaEdt.Enabled = true;
                    releMotorChkBx.Enabled = true;
                    tzxBtn.Text = "SBB => CDT";
                    reubicaEdt.Text = "255";
                    break;
                case 2: // MSX
                    nameEdt.MaxLength = 6;
                    clearLbl.Text = "CLEAR";
                    usrLbl.Text = "JP";
                    locateLbl.Enabled = true;
                    reubicaEdt.Enabled = true;
                    pokeLbl.Enabled = true;
                    pokeEdt.Enabled = true;
                    releMotorChkBx.Enabled = true;
                    tzxBtn.Text = "SBB => ¿TZX?";
                    reubicaEdt.Text = "244"; // 0xf4
                    pokeEdt.Text = "0";
                    break;
                case 3: // 81
                    nameEdt.MaxLength = 2;
                    clearLbl.Text = "SP";
                    usrLbl.Text = "USR";
                    clearLbl.Enabled = false;
                    clearEdt.Enabled = false;
                    usrLbl.Enabled = false;
                    usrEdt.Enabled = false;
                    checkLoadErrorChkBx.Enabled = false;
                    enableIntChkBx.Enabled = false;
                    releMotorChkBx.Enabled = false;
                    tzxBtn.Text = "SBB => TZX";
                    reubicaEdt.Text = "0";
                    break;
            }

            UpdateMuestras();
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
            fileEdt.Text = "new.SBB";
            nameEdt.Text = "";
            aditionalInfoEdt.Text = "";
            clearEdt.Text = "0";
            usrEdt.Text = "0";
            pokeEdt.Text = "0";
            bloksEdt.Text = "0";
            originEdt.Text = "";
            reubicaEdt.Text = "255";
            blocksLV.Items.Clear();
            statusLabel.Text = "Ready";
            maquinaCmbBx.Enabled = true;
        }

        private void FillData()
        {
            fileEdt.Text = _currentHeader.Name + ".SBB"; // Approximation or actual path
            UpdateListView();

            // Set machine and trigger its UI logic
            int machineIdx = 0;
            switch (_currentHeader.Machine.Trim().ToUpper())
            {
                case "ZX": case "ZXS": machineIdx = 0; break;
                case "CPC": machineIdx = 1; break;
                case "MSX": machineIdx = 2; break;
                case "81": case "81-": machineIdx = 3; break;
            }
            maquinaCmbBx.SelectedIndex = machineIdx;
            MaquinaCmbBx_SelectedIndexChanged(null, null);

            modelCmbBx.SelectedIndex = Math.Max(0, _currentHeader.Model - 1);
            nameEdt.Text = _currentHeader.Name;
            aditionalInfoEdt.Text = _currentHeader.ExtraInfo;
            bloksEdt.Text = _currentBlocks.Count.ToString();
            originEdt.Text = _currentHeader.Origin.ToString();

            enableIntChkBx.Checked = (_currentHeader.EiDi & 1) != 0;
            reubicaEdt.Text = FormatValue(_currentHeader.Locate);
            pokeEdt.Text = FormatValue(_currentHeader.PokeFfff);
            clearEdt.Text = FormatValue(_currentHeader.ClearSp);
            usrEdt.Text = FormatValue(_currentHeader.UsrPc);
        }

        private void UpdateHeader()
        {
            _currentHeader.Machine = maquinaCmbBx.Text;
            _currentHeader.Model = (sbyte)(modelCmbBx.SelectedIndex + 1);
            _currentHeader.ExtraInfo = aditionalInfoEdt.Text;
            _currentHeader.EiDi = (byte)(enableIntChkBx.Checked ? 1 : 0);
            _currentHeader.Name = nameEdt.Text;
            _currentHeader.Locate = (byte)ParseValue(reubicaEdt.Text);
            _currentHeader.ClearSp = (ushort)ParseValue(clearEdt.Text);
            _currentHeader.UsrPc = (ushort)ParseValue(usrEdt.Text);
            _currentHeader.PokeFfff = (byte)ParseValue(pokeEdt.Text);
            _currentHeader.NBlocks = (byte)_currentBlocks.Count;
        }

        private int ParseValue(string val)
        {
            if (string.IsNullOrEmpty(val)) return 0;
            if (val.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                int.TryParse(val.Substring(2), NumberStyles.HexNumber, null, out int hex);
                return hex;
            }
            int.TryParse(val, out int dec);
            return dec;
        }

        private string FormatValue(int val)
        {
            return val.ToString(); // TODO: check if hex output is desired as in n2s
        }

        private void OpenSbb()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "SBB files (*.sbb)|*.sbb|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var result = SbbPersistence.LoadSbb(ofd.FileName);
                        _currentHeader = result.header;
                        _currentBlocks = result.blocks;
                        fileEdt.Text = ofd.FileName;
                        FillData();
                        maquinaCmbBx.Enabled = false;
                        statusLabel.Text = $"Loaded {ofd.FileName}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading SBB: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SaveSbb(bool saveAs)
        {
            string path = fileEdt.Text;
            if (saveAs || string.IsNullOrEmpty(path) || path == "new.SBB")
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "SBB files (*.sbb)|*.sbb";
                    if (sfd.ShowDialog() != DialogResult.OK) return;
                    path = sfd.FileName;
                }
            }

            try
            {
                UpdateHeader();
                SbbPersistence.SaveSbb(path, _currentHeader, _currentBlocks);
                fileEdt.Text = path;
                statusLabel.Text = $"Saved to {path}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving SBB: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                statusLabel.Text = "Opening Player Monitor...";
                try
                {
                    using (var monitor = new PlayerMonitor(tempWav))
                    {
                        monitor.StartPlayback();
                        monitor.ShowDialog(this);
                    }
                    statusLabel.Text = "Ready";
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
