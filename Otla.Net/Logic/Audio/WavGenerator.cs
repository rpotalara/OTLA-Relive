using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Otla.Net.Models;

namespace Otla.Net.Logic.Audio
{
    public class WavGenerator
    {
        private int _sampleRate = 44100;

        // ZX Spectrum standard timings (approximated in samples at 44.1kHz)
        // Based on original c_pilot_zx, c_sync_zx, etc.
        private byte[] _pilotCycle;
        private byte[] _sync1Cycle;
        private byte[] _sync2Cycle;
        private byte[] _zeroCycle;
        private byte[] _oneCycle;

        public WavGenerator(int sampleRate)
        {
            _sampleRate = sampleRate;
            InitializeStandardZxWaveforms();
        }

        private void InitializeStandardZxWaveforms()
        {
            // 2168 T-states for pilot, 667/735 for sync, 855 for zero, 1710 for one.
            // At 44100Hz, 1ms is 44.1 samples.
            // 3.5MHz CPU -> 1 T-state is 1/3,500,000 sec.
            // Pilot pulse (2168 T-states) = 2168 / 3,500,000 = 0.000619s = ~27 samples.

            _pilotCycle = CreateSquareWaveCycle(27);
            _sync1Cycle = CreateSquareWaveCycle(8);
            _sync2Cycle = CreateSquareWaveCycle(9);
            _zeroCycle = CreateSquareWaveCycle(11);
            _oneCycle = CreateSquareWaveCycle(22);
        }

        private byte[] CreateSquareWaveCycle(int length)
        {
            byte[] cycle = new byte[length];
            for (int i = 0; i < length; i++)
                cycle[i] = (i < length / 2) ? (byte)220 : (byte)35; // Standard amplitude
            return cycle;
        }

        public void GenerateZxtapWav(string outputPath, List<SbbBlock> blocks)
        {
            using (var fs = new FileStream(outputPath, FileMode.Create))
            using (var bw = new BinaryWriter(fs))
            {
                // Write placeholder RIFF header
                bw.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
                bw.Write(0);
                bw.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));
                bw.Write(System.Text.Encoding.ASCII.GetBytes("fmt "));
                bw.Write(16);
                bw.Write((short)1); // PCM
                bw.Write((short)1); // Mono
                bw.Write(_sampleRate);
                bw.Write(_sampleRate); // ByteRate
                bw.Write((short)1);
                bw.Write((short)8);
                bw.Write(System.Text.Encoding.ASCII.GetBytes("data"));
                bw.Write(0);

                long dataStart = fs.Position;

                foreach (var block in blocks)
                {
                    // Pilot tone: 8064 pulses for header, 3220 for data
                    int pilotCount = (block.BlockType == '0') ? 8064 : 3220;
                    for (int i = 0; i < pilotCount; i++) bw.Write(_pilotCycle);

                    // Sync pulses
                    bw.Write(_sync1Cycle);
                    bw.Write(_sync2Cycle);

                    // Data
                    // Note: Original OTLA includes the flag byte and checksum in the WAV
                    // Here we assume block.Data contains the raw payload.
                    // We'd need to add the flag and checksum here for a perfect port.
                    foreach (byte b in block.Data.Take(block.Size))
                    {
                        WriteByte(bw, b);
                    }

                    // Pause between blocks
                    for (int i = 0; i < _sampleRate / 2; i++) bw.Write((byte)128);
                }

                long dataEnd = fs.Position;
                int dataSize = (int)(dataEnd - dataStart);

                fs.Seek(4, SeekOrigin.Begin);
                bw.Write(dataSize + 36);
                fs.Seek(40, SeekOrigin.Begin);
                bw.Write(dataSize);
            }
        }

        private void WriteByte(BinaryWriter bw, byte b)
        {
            for (int i = 7; i >= 0; i--)
            {
                bool bit = ((b >> i) & 1) == 1;
                bw.Write(bit ? _oneCycle : _zeroCycle);
            }
        }
    }
}
