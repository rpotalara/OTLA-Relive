using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Otla.Net.Models;

namespace Otla.Net.Logic.Formats
{
    public static class ZxSpectrumFiles
    {
        public static (SbbHeader, List<SbbBlock>) LoadTap(string filename)
        {
            byte[] data = File.ReadAllBytes(filename);
            var header = new SbbHeader
            {
                Machine = "ZXS",
                EiDi = 1,
                Origin = 'T', // _tape_
                Name = Path.GetFileName(filename)
            };

            var blocks = new List<SbbBlock>();
            int pos = 0;

            while (pos < data.Length)
            {
                if (pos + 2 > data.Length) break;
                ushort blockSize = BitConverter.ToUInt16(data, pos);
                pos += 2;

                if (pos + blockSize > data.Length) break;

                byte flag = data[pos];
                if (flag == 0x00 && blockSize >= 19) // Header block
                {
                    var block = new SbbBlock
                    {
                        BlockType = (char)('0' + data[pos + 1]), // _zx_program_ + type
                        BlockName = Encoding.ASCII.GetString(data, pos + 2, 10).Trim(),
                        Size = BitConverter.ToUInt16(data, pos + 12),
                        Ini = BitConverter.ToUInt16(data, pos + 14),
                        Param3 = BitConverter.ToUInt16(data, pos + 16)
                    };
                    blocks.Add(block);
                }
                else if (flag == 0xFF) // Data block
                {
                    if (blocks.Count > 0 && blocks[blocks.Count - 1].DataChecksum == 0)
                    {
                        // Fill data for the last header
                        var lastBlock = blocks[blocks.Count - 1];
                        int actualDataSize = blockSize - 2; // flag and checksum
                        Array.Copy(data, pos + 1, lastBlock.Data, 0, Math.Min(actualDataSize, 0x10000));
                        lastBlock.DataChecksum = 0xFF; // Mark as filled
                    }
                    else
                    {
                        // Headless block
                        var block = new SbbBlock
                        {
                            BlockName = "Headless",
                            BlockType = '4', // _zx_tap_hdlss_
                            Size = (ushort)(blockSize - 2),
                            Ini = 0xFFFF
                        };
                        Array.Copy(data, pos + 1, block.Data, 0, Math.Min(block.Size, 0x10000));
                        blocks.Add(block);
                    }
                }
                pos += blockSize;
            }

            header.NBlocks = (byte)blocks.Count;
            return (header, blocks);
        }
    }
}
