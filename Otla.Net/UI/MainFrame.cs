using System;
using System.Windows.Forms;
using System.Drawing;
using Otla.Net.Models;
using Otla.Net.Logic.Formats;

namespace Otla.Net.UI
{
    public partial class MainFrame : Form
    {
        private SbbHeader _currentHeader = new SbbHeader();

        public MainFrame()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "o.t.l.a. .NET Modernized";
            this.Size = new Size(400, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Layout based on original DFM
            var builderGroup = new GroupBox { Text = "Builder", Location = new Point(10, 10), Size = new Size(370, 320) };
            var playerGroup = new GroupBox { Text = "Player", Location = new Point(10, 340), Size = new Size(370, 200) };

            var addBtn = new Button { Text = "Add blocks...", Location = new Point(10, 130), Size = new Size(100, 30) };
            addBtn.Click += AddBtn_Click;

            builderGroup.Controls.Add(addBtn);

            this.Controls.Add(builderGroup);
            this.Controls.Add(playerGroup);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "MSX ROM (*.rom)|*.rom|ZX Tap (*.tap)|*.tap|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.FileName.EndsWith(".rom", StringComparison.OrdinalIgnoreCase))
                    {
                        var blocks = MsxRomConverter.ConvertRomToBlocks(ofd.FileName);
                        MessageBox.Show($"Loaded {blocks.Count} blocks from MSX ROM (16-bit dependency bypassed!)");
                    }
                }
            }
        }
    }
}
