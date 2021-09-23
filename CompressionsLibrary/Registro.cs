using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace CompressionsLibrary
{
    class Record
    {
        public byte symbol;
        public int apparition_account;
        public double probability;
        public string prefix;
        public Record left_son;
        public Record rigth_son;

        //LZW

        public byte[] Cadena;
        public int Id;

        public static bool Compardor_Bytes(byte[] Uno, byte[] Dos)
        {
            if (Uno.Length == Dos.Length)
            {
                for (int i = 0; i < Uno.Length; i++)
                {
                    if (Uno[i] != Dos[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    public static Comparison<Record> Comparar_Prioridad = delegate (Record Symb1, Record Symb2)
        {
            return Symb1.probability > Symb2.probability ? 1 : Symb1.probability < Symb2.probability ? -1 : 0;
        };


        public static Func<Record, Record, bool> Determinar_Prioridad = delegate (Record Symb1, Record Symb2)
        {
            return (Symb1.probability > Symb2.probability);
        };

        public static Comparison<Record> Comparar_symbol = delegate (Record Symb1, Record Symb2)
        {
            return Symb1.symbol.CompareTo(Symb2.symbol);
        };

        public void Asig_prefix(string prefix_Binario)
        {
            if (symbol != 0)
            {
                prefix = prefix_Binario;
            }
        }

    }
}
