using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using System.Text;
using DataStructures;
using System.Text.Json;


namespace CompressionsLibrary
{
    public class Huffman : Compressor
    {
        private Heap<Record> Line;
        private Dictionary<byte, Record> Table;
        private Dictionary<string, Record> Table_Decompres;
        private int Tam_Original;
        private int Tam_Compress;
        private bool Compres;

        public byte[] Compression(byte[] Data)
        {
            Compres = true;
            Tam_Original = Data.Length;
            Table = new Dictionary<byte, Record>();
            CreateRecord(Data);
            byte[] Meta_Data = Send_MetaData();
            int Tam_Data = Meta_Data.Length;
            Create_tree();
            Add_prefixs(Line.raiz.Valor);
            byte[] Result = Convert_Ascii(Binario_Convert(Data));
            Tam_Compress = Result.Length;
            Array.Resize(ref Meta_Data, Meta_Data.Length + Result.Length);
            Result.CopyTo(Meta_Data, Tam_Data);
            return Meta_Data;
        }

        public void WriteRegistry(string OriginalName, string CompressedFilePath, double CompressionRatio, double CompressionFactor, double ReductionPercentage)
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\") + "\\LAB-03-ED2-URL\\test.json");
            using (var reader = new StreamReader(path))
            {
                JsonSerializerOptions rule = new JsonSerializerOptions { IgnoreNullValues = true };
                var All = JsonSerializer.Deserialize<List<object>>(reader.ReadToEnd(), rule);
                var temp = new
                {
                    originalName = OriginalName,
                    compressedFilePath = CompressedFilePath,
                    compressionRatio = CompressionRatio,
                    compressionFactor = CompressionFactor,
                    reductionPercentage = ReductionPercentage,
                };
                All.Add(temp);
                reader.Close();
                using (var writer = new StreamWriter(path))
                {
                    var AllRegistries = JsonSerializer.Serialize<List<object>>(All, rule);
                    writer.Write(AllRegistries);
                }
            }
        }

        private byte[] Send_MetaData()
        {
            int tam = 256;
            int Cant_BFrec = 1;
            int Aux = 0;
            byte Estefue = 0;
            var Data = Table.Values;
            foreach (Record Item in Data)
            {
                while (tam < Item.apparition_account)
                {
                    tam += 256;
                    Cant_BFrec++;
                    Estefue = Item.symbol;
                }
            }
            byte[] Resul = new byte[2 + (Cant_BFrec + 1) * Data.Count];
            int posicion = 0;
            Resul[posicion++] = Convert.ToByte(Convert.ToChar(Data.Count));
            Resul[posicion++] = Convert.ToByte(Convert.ToChar(Cant_BFrec));
            int cantidaD = 0;
            foreach (Record Item in Data)
            {
                cantidaD += Item.apparition_account;
                Resul[posicion++] = Item.symbol;
                Aux = Item.apparition_account % 256;
                Resul[posicion++] = Convert.ToByte(Convert.ToChar(Aux));
                Aux = Item.apparition_account - Aux;
                for (int i = 1; i < Cant_BFrec; i++)
                {
                    if (Aux >= (256 * i))
                    {
                        Resul[posicion++] = Convert.ToByte(Convert.ToChar(255));
                    }
                    else
                    {
                        Resul[posicion++] = Convert.ToByte(Convert.ToChar(" "));
                    }
                }
            }
            return Resul;
        }
        private void CreateRecord(byte[] Tex)
        {

            Line = new Heap<Record>();
            Record New;
            int Total = Tex.Length;
            for (int i = 0; i < Total; i++)
            {
                New = new Record();
                New.symbol = Tex[i];
                if (New.symbol == 0)
                {
                    string text = "fracaso rotundo";
                }
                if (Table.ContainsKey(New.symbol))
                {
                    Table[New.symbol].apparition_account++;
                }
                else
                {
                    Table.Add(Tex[i], New);
                    Table[New.symbol].apparition_account++;
                }
            }
            var Contenido = Table.Values;
            foreach (Record Item in Contenido)
            {
                Item.probability = Convert.ToDouble(Item.apparition_account) / Convert.ToDouble(Total);
                Line.Agregar(Item, Record.Determinar_Prioridad);
            }
        }

        private void Create_tree()
        {
            while (Line.Cant_Nodos > 1)
            {
                Record Aux = new Record();
                Aux.symbol = 0;
                Record One = Line.Eliminar(Record.Comparar_Prioridad, Record.Determinar_Prioridad, Record.Comparar_symbol);
                Record Two = Line.Eliminar(Record.Comparar_Prioridad, Record.Determinar_Prioridad, Record.Comparar_symbol);
                Aux.probability = One.probability + Two.probability;
                Aux.rigth_son = One;
                Aux.left_son = Two;
                //Aux.IsNode = true;
                Line.Agregar(Aux, Record.Determinar_Prioridad);
            }
        }

        private void Add_prefixs(Record raiz)
        {
            try
            {
                if (Line.raiz.Valor != null)
                {
                    if (raiz.left_son != null)
                    {
                        Inorder(raiz.left_son, "0");
                    }
                    raiz.Asig_prefix("1");
                    if (!string.IsNullOrEmpty(raiz.prefix))
                    {
                        if (Compres)
                        {
                            Table[raiz.symbol].Asig_prefix("1");

                        }
                        else { Table_Decompres.Add(raiz.prefix, raiz); }
                    }
                    if (raiz.rigth_son != null)
                    {
                        Inorder(raiz.rigth_son, "1");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Inorder(Record Record_Nodo, string prefix_Binario)
        {
            if (Record_Nodo.left_son != null)
            {
                Inorder(Record_Nodo.left_son, prefix_Binario + "0");
            }
            Record_Nodo.Asig_prefix(prefix_Binario);
            if (!string.IsNullOrEmpty(Record_Nodo.prefix))
            {
                if (Compres)
                {
                    Table[Record_Nodo.symbol].Asig_prefix(prefix_Binario);
                }
                else
                {
                    Table_Decompres.Add(Record_Nodo.prefix, Record_Nodo);
                }
            }
            if (Record_Nodo.rigth_son != null)
            {
                Inorder(Record_Nodo.rigth_son, prefix_Binario + "1");
            }
        }

        private string Binario_Convert(byte[] Cadena)
        {
            string Binario = "";
            for (int i = 0; i < Cadena.Length; i++)
            {
                Record aux = new Record();
                Binario += Table[Cadena[i]].prefix;
            }
            return Binario;
        }

        private byte[] Convert_Ascii(string Text_Binario)
        {
            int Cantidad_Byte = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Text_Binario.Length) / 8));
            string[] Byte = new string[Cantidad_Byte];
            int posicion = 0;
            while (!string.IsNullOrEmpty(Text_Binario))
            {
                string aux = "";
                if (Text_Binario.Length >= 8)
                {
                    aux = Text_Binario.Substring(0, 8);
                    Text_Binario = Text_Binario.Remove(0, 8);
                    Byte[posicion] = aux;
                }
                else
                {
                    aux = Text_Binario;
                    Text_Binario = "";
                    while (aux.Length < 8)
                    {
                        aux += "0";
                    }
                    Byte[posicion] = aux;
                }
                posicion++;
            }
            char[] Caracteres_Resul = new char[Cantidad_Byte];
            byte[] Comprimido = new byte[Cantidad_Byte];
            for (int i = 0; i < Cantidad_Byte; i++)
            {
                //Duda
                Caracteres_Resul[i] = Convert.ToChar(Convert.ToInt32(Byte[i], 2));
                Comprimido[i] = Convert.ToByte(Caracteres_Resul[i]);
            }
            return Comprimido;
        }

//to insert descompresion, binary, and ascii methods


    }

}
