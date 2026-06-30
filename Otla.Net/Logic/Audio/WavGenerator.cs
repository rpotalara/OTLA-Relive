using System;
using System.IO;
using Otla.Net.Models;

namespace Otla.Net.Logic.Audio
{
    public class WavGenerator
    {
        private int _sampleRate = 44100;
        private byte[] _oneCycle;
        private byte[] _zeroCycle;

        public WavGenerator(int sampleRate)
        {
            _sampleRate = sampleRate;
            InitializeWaveforms();
        }

        private void InitializeWaveforms()
        {
            // Simplified square wave initialization based on wav2.cpp
            // In a real implementation, we'd use the precise cycle lengths from the original code
            int samplesPerBit = 20; // Example for 44100Hz
            _oneCycle = new byte[samplesPerBit];
            _zeroCycle = new byte[samplesPerBit / 2];

            for (int i = 0; i < samplesPerBit; i++)
                _oneCycle[i] = (i < samplesPerBit / 2) ? (byte)255 : (byte)0;

            for (int i = 0; i < samplesPerBit / 2; i++)
                _zeroCycle[i] = (i < samplesPerBit / 4) ? (byte)255 : (byte)0;
        }

        public void GenerateWav(string outputPath, byte[] rawData)
        {
            using (var fs = new FileStream(outputPath, FileMode.Create))
            using (var bw = new BinaryWriter(fs))
            {
                // RIFF Header
                bw.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
                bw.Write(0); // Placeholder for chunk size
                bw.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));

                // fmt chunk
                bw.Write(System.Text.Encoding.ASCII.GetBytes("fmt "));
                bw.Write(16); // fmt chunk size
                bw.Write((short)1); // AudioFormat (PCM)
                bw.Write((short)1); // NumChannels (Mono)
                bw.Write(_sampleRate); // SampleRate
                bw.Write(_sampleRate); // ByteRate (SampleRate * NumChannels * BitsPerSample / 8)
                bw.Write((short)1); // BlockAlign
                bw.Write((short)8); // BitsPerSample

                // data chunk
                bw.Write(System.Text.Encoding.ASCII.GetBytes("data"));
                bw.Write(0); // Placeholder for data size

                long dataStart = fs.Position;

                // Write actual wave data
                foreach (byte b in rawData)
                {
                    WriteByteAsAudio(bw, b);
                }

                long dataEnd = fs.Position;
                int dataSize = (int)(dataEnd - dataStart);

                // Update sizes
                fs.Seek(4, SeekOrigin.Begin);
                bw.Write(dataSize + 36);
                fs.Seek(40, SeekOrigin.Begin);
                bw.Write(dataSize);
            }
        }

        private void WriteByteAsAudio(BinaryWriter bw, byte b)
        {
            for (int i = 7; i >= 0; i--)
            {
                bool bit = ((b >> i) & 1) == 1;
                bw.Write(bit ? _oneCycle : _zeroCycle);
            }
        }
    }
}
