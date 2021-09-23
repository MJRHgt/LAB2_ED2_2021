using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CompressionsLibrary
{
    public class LZW : Compressor
    {
        Dictionary<byte[], Record> Tabla;
        Dictionary<int, Record> Tabla_Descompres;
        int Tam_Original;
        int Tam_Comprimido;


    }
}
