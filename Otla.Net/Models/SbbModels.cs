using System;

namespace Otla.Net.Models
{
    public enum MachineType
    {
        ZXSpectrum,
        CPCAmstrad,
        MSX,
        ZX81,
        Unknown
    }

    public class SbbHeader
    {
        public string Version { get; set; } = "2.2";
        public string Machine { get; set; } = "???";
        public sbyte Model { get; set; } = -1;
        public string ExtraInfo { get; set; } = "";
        public byte EiDi { get; set; }
        public string Name { get; set; } = "";
        public byte Locate { get; set; } = 0xFF;
        public char Origin { get; set; }
        public byte NBlocks { get; set; }
        public byte PokeFfff { get; set; }
        public ushort ClearSp { get; set; }
        public ushort UsrPc { get; set; }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[46]; // ts_sbb_header size
            // Version "SB" + code
            bytes[0] = (byte)'S';
            bytes[1] = (byte)'B';
            bytes[2] = 0x22; // Version 2.2

            // Machine (4 bytes)
            byte[] machineBytes = System.Text.Encoding.ASCII.GetBytes(Machine.PadRight(4).Substring(0, 4));
            Array.Copy(machineBytes, 0, bytes, 3, 4);

            bytes[7] = (byte)Model;

            // ExtraInfo (7 bytes)
            byte[] extraBytes = System.Text.Encoding.ASCII.GetBytes(ExtraInfo.PadRight(7).Substring(0, 7));
            Array.Copy(extraBytes, 0, bytes, 8, 7);

            bytes[15] = EiDi;

            // Name (16 bytes)
            byte[] nameBytes = System.Text.Encoding.ASCII.GetBytes(Name.PadRight(16).Substring(0, 16));
            Array.Copy(nameBytes, 0, bytes, 16, 16);

            bytes[32] = Locate;
            bytes[33] = (byte)Origin;
            bytes[34] = NBlocks;
            bytes[35] = PokeFfff;

            BitConverter.GetBytes(ClearSp).CopyTo(bytes, 36);
            BitConverter.GetBytes(UsrPc).CopyTo(bytes, 38);
            // Rest are padding or unused in original struct?
            // sizeof(ts_sbb_header) was used in C++. Let's check sbb.h
            /*
            typedef struct
            {
                char sbb_version [3];      // 0-2
                char machine [4];          // 3-6
                char model;                // 7
                char extra_info [7];       // 8-14
                unsigned char  ei_di;      // 15
                char nombre [16];          // 16-31
                unsigned char  locate;     // 32
                char  origin;              // 33
                unsigned char  n_blocks;   // 34
                unsigned char  poke_ffff;  // 35
                unsigned short clear_sp;   // 36-37
                unsigned short usr_pc;     // 38-39
            } ts_sbb_header;
            */
            // Total size 40 bytes. Wait, 3+4+1+7+1+16+1+1+1+1+2+2 = 40.
            return bytes;
        }
    }

    public class SbbBlock
    {
        public string BlockName { get; set; } = "";
        public ushort Size { get; set; }
        public ushort Param3 { get; set; }
        public char BlockType { get; set; }
        public byte HeaderChecksum { get; set; }
        public ushort Ini { get; set; }
        public byte Jump { get; set; }
        public ushort Exec { get; set; }
        public byte DataChecksum { get; set; }
        public byte[] Data { get; set; } = new byte[0x10000];

        // Header size without data is 28 bytes
        // char blockname[16];   // 0-15
        // ushort size;          // 16-17
        // ushort param3;        // 18-19
        // char block_type;      // 20
        // byte h_chksum;        // 21
        // ushort ini;           // 22-23
        // byte jump;            // 24
        // ushort exec;          // 25-26
        // byte d_chksum;        // 27
    }
}
