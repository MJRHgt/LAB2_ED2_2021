using System;
using System.Collections.Generic;
using System.Text;

namespace CompressionsLibrary
{
    interface Compressor
    {
        public byte[] Compression(byte[] Tex_Original);
        public byte[] Descompression(byte[] Tex_Comprimido);
    }
}
