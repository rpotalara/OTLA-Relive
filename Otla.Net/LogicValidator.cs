using System;
using System.Collections.Generic;
using Otla.Net.Logic.Audio;

namespace Otla.Net.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Otla.Net Logic Validator");

            // Test audio generation with some dummy data
            var generator = new WavGenerator(44100);
            byte[] dummyData = new byte[] { 0x55, 0xAA, 0x00, 0xFF };

            string testPath = "test_output.wav";
            generator.GenerateWav(testPath, dummyData);

            if (System.IO.File.Exists(testPath))
            {
                Console.WriteLine($"SUCCESS: Wav file generated at {testPath}");
                Console.WriteLine($"Size: {new System.IO.FileInfo(testPath).Length} bytes");
            }
            else
            {
                Console.WriteLine("FAILURE: Wav file not generated.");
            }
        }
    }
}
