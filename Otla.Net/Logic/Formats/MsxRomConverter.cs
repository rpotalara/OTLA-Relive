using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Otla.Net.Models;

namespace Otla.Net.Logic.Formats
{
    public static class MsxRomConverter
    {
        /// <summary>
        /// Emulates the functionality of msxr2b.exe by extracting blocks from an MSX ROM.
        /// MSX ROMs don't have a single standard header, they often use mappers.
        /// However, simple ROMs might be 8k, 16k, 32k or 48k.
        /// The original code seems to expect the tool to produce one or more .bin files
        /// which are then loaded by lee_msx_bin.
        /// </summary>
        public static List<SbbBlock> ConvertRomToBlocks(string romPath)
        {
            byte[] romData = File.ReadAllBytes(romPath);
            var blocks = new List<SbbBlock>();

            // Basic logic for MSX ROM to BIN:
            // Often we just need to chunk the ROM into 16KB pages if it's large,
            // or if it's a simple 32KB ROM, it might be mapped to 0x4000-0xBFFF.

            // For now, let's implement a simple heuristic:
            // If ROM is <= 32KB, treat it as a single block starting at 0x4000 or 0x8000.

            if (romData.Length <= 32768)
            {
                var block = new SbbBlock
                {
                    BlockName = Path.GetFileNameWithoutExtension(romPath),
                    Size = (ushort)romData.Length,
                    Ini = 0x4000, // Common start for MSX ROMs
                    Exec = 0,     // ROMs usually don't have an 'exec' in the same sense as BIN
                    BlockType = (char)0xFE // BIN type
                };
                Array.Copy(romData, block.Data, romData.Length);
                blocks.Add(block);
            }
            else
            {
                // Multi-page ROM. We split in 16KB chunks.
                int numPages = (romData.Length + 16383) / 16384;
                for (int i = 0; i < numPages; i++)
                {
                    int offset = i * 16384;
                    int size = Math.Min(16384, romData.Length - offset);

                    var block = new SbbBlock
                    {
                        BlockName = $"{Path.GetFileNameWithoutExtension(romPath)}_{i+1}",
                        Size = (ushort)size,
                        Ini = 0x8000, // Map each page to 0x8000 for loading
                        Exec = 0,
                        BlockType = (char)0xFE
                    };
                    Array.Copy(romData, offset, block.Data, 0, size);
                    blocks.Add(block);
                }
            }

            return blocks;
        }
    }
}
