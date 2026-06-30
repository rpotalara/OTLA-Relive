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

        public MainFrame()
        {
            InitializeComponent();
            SetupInitialState();
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
                            var (header, blocks) = ZxSpectrumFiles.LoadTap(ofd.FileName);
                            _currentHeader = header;
                            _currentBlocks.AddRange(blocks);
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
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "WAV Audio|*.wav";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    statusLabel.Text = "Generating WAV...";
                    // Logic for WAV generation would be called here
                    statusLabel.Text = $"WAV saved to {sfd.FileName}";
                }
            }
        }
    }
}
