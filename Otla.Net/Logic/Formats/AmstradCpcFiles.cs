using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Otla.Net.Models;

namespace Otla.Net.Logic.Formats
{
    public static class AmstradCpcFiles
    {
        public class LoaderResult
        {
            public SbbHeader Header { get; set; }
            public List<SbbBlock> Blocks { get; set; }
        }

        public static LoaderResult LoadAmsdos(string filename)
        {
            byte[] data = File.ReadAllBytes(filename);
            var header = new SbbHeader
            {
                Machine = "CPC",
                EiDi = 1,
                Locate = 0xFF,
                Origin = 'B', // _binary_
                Name = Path.GetFileName(filename)
            };

            var blocks = new List<SbbBlock>();

            if (data.Length >= 128)
            {
                // Amsdos header starts at 0
                string name = Encoding.ASCII.GetString(data, 1, 16).Trim('\0');
                ushort ini = BitConverter.ToUInt16(data, 21);
                ushort len = BitConverter.ToUInt16(data, 24);
                ushort exec = BitConverter.ToUInt16(data, 26);
                byte type = data[18];

                char blockType = '-';
                switch (type)
                {
                    case 0: blockType = '-'; break; // basic
                    case 1: blockType = 'A'; break; // ascii
                    case 2: blockType = 'B'; break; // binary
                    case 3: blockType = 'P'; break; // protected
                }

                var block = new SbbBlock
                {
                    BlockName = name,
                    Ini = ini,
                    Size = len,
                    Exec = exec,
                    BlockType = blockType
                };

                int dataLen = Math.Min((int)len, data.Length - 128);
                Array.Copy(data, 128, block.Data, 0, dataLen);
                blocks.Add(block);
            }

            header.NBlocks = (byte)blocks.Count;
            return new LoaderResult { Header = header, Blocks = blocks };
        }
    }
}
