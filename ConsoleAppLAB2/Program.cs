using System;
using System.IO;
using CompressionsLibrary;
using DataStructures;

namespace ConsoleAppLAB2
{
    class Program
    {

        public static void Header()
        {
            Console.Clear();
            string textToEnter = "--PRÁCTICA DE LABORATORIO #3 - ESTRUCTURA DE DATOS 2--";
            string textToEnter2 = "----- Javier Andrés Morales González - 1210219 | Mario José Roldán  Hernández - 1117517 -----";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter2.Length / 2)) + "}", textToEnter2));
            Console.WriteLine("\n");
            Console.ResetColor();
        }

        public static void TitleOption1()
        {
            string t = "--COMPRESIÓN HUFFMAN--";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (t.Length / 2)) + "}", t) + "\n");
            Console.ResetColor();
        }
        public static void TitleOption2()
        {
            string t = "--DESCOMPRESIÓN HUFFMAN--";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (t.Length / 2)) + "}", t) + "\n");
            Console.ResetColor();
        }

        public static void TitleOption3()
        {
            string t = "--COMPRESIÓN LZW--";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (t.Length / 2)) + "}", t) + "\n");
            Console.ResetColor();
        }
        public static void TitleOption4()
        {
            string t = "--DESCOMPRESIÓN LZW--";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (t.Length / 2)) + "}", t) + "\n");
            Console.ResetColor();
        }


        static void Main(string[] args)
        {
            LZW CompresorLZW = new LZW();
            Huffman CompresorCrack = new Huffman();
            Header();
            bool exit = false;
            while (!exit)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Ingrese H para dirigirse al método Huffman o ingrese L para dirigirse al método LZW:\n\n");
                    Console.ResetColor();
                    string Selected = Console.ReadLine();
                    if (String.IsNullOrEmpty(Selected))
                    {
                        throw new FormatException();
                    }
                    if (Selected.Equals("H") || Selected.Equals("h"))
                    {
                        try
                        {
                            Header();
                            TitleOption1();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Ingrese el texto a comprimir:\n\n");
                            Console.ResetColor();
                            string Text = Console.ReadLine();
                            if (String.IsNullOrEmpty(Text))
                            {
                                throw new FormatException();
                            }
                            byte[] texto = new byte[Text.Length];
                            for (int i = 0; i < Text.Length; i++)
                            {
                                texto[i] = Convert.ToByte(Convert.ToChar(Text[i]));
                            }
                            byte[] Comprimido = CompresorCrack.Compression(texto);
                            string result = "";
                            foreach (byte bit in Comprimido)
                            {
                                result += Convert.ToString(Convert.ToChar(bit));
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n\nEl texto comprimido es el siguiente:\n\n");
                            Console.ResetColor();
                            Console.WriteLine(result);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n\nPresione cualquier tecla para ver el mismo texto descompreso.");
                            Console.ResetColor();
                            Console.ReadKey();
                            Console.Clear();
                            Header();
                            TitleOption2();
                            result = "";
                            byte[] Descomprimido = CompresorCrack.Descompression(Comprimido);
                            foreach (byte bit in Descomprimido)
                            {
                                result += Convert.ToString(Convert.ToChar(bit));
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n\nEl texto descomprimido es el siguiente:");
                            Console.ResetColor();
                            Console.WriteLine("\n" + result + "\n\n");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Proceso finalizado. Presione una tecla.");
                            Console.ResetColor();
                            Console.ReadKey();
                            Header();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            string e = "Ingrese S para volver al menú principal o cualquier cosa para salir del programa.";
                            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (e.Length / 2)) + "}", e) + "\n");
                            Console.ResetColor();
                            if (!Convert.ToString(Console.ReadLine()).Equals("S"))
                            {
                                exit = true;
                            }
                        }
                        catch
                        {
                            Header();
                            string e = "Ocurrió un error. Presione una tecla para volver al menú principal.";
                            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (e.Length / 2)) + "}", e) + "\n");
                            Console.ReadKey();
                        }
                    }
                    else if (Selected.Equals("L") || Selected.Equals("l"))
                    {
                        try
                        {
                            Header();
                            TitleOption3();
                            Console.ForegroundColor = ConsoleColor.Yellow;

                            Console.WriteLine("Ingrese el texto a comprimir:\n\n");
                            Console.ResetColor();
                            string Text = Console.ReadLine();
                            if (String.IsNullOrEmpty(Text))
                            {
                                throw new FormatException();
                            }
                            byte[] texto = new byte[Text.Length];
                            for (int i = 0; i < Text.Length; i++)
                            {
                                texto[i] = Convert.ToByte(Convert.ToChar(Text[i]));
                            }
                            byte[] Comprimido = CompresorLZW.Compression(texto);
                            string result = "";
                            foreach (byte bit in Comprimido)
                            {
                                result += Convert.ToString(Convert.ToChar(bit));
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n\nEl texto comprimido es el siguiente:\n\n");
                            Console.ResetColor();
                            Console.WriteLine(result);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n\nPresione cualquier tecla para ver el mismo texto descompreso.");
                            Console.ResetColor();
                            Console.ReadKey();
                            Console.Clear();
                            Header();
                            TitleOption4();
                            result = "";
                            byte[] Descomprimido = CompresorLZW.Descompression(Comprimido);
                            foreach (byte bit in Descomprimido)
                            {
                                result += Convert.ToString(Convert.ToChar(bit));
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n\nEl texto descomprimido es el siguiente:");
                            Console.ResetColor();
                            Console.WriteLine("\n" + result + "\n\n");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Proceso finalizado. Presione una tecla.");
                            Console.ResetColor();
                            Console.ReadKey();
                            Header();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            string e = "Ingrese S para volver al menú principal o cualquier cosa para salir del programa.";
                            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (e.Length / 2)) + "}", e) + "\n");
                            Console.ResetColor();
                            if (!Convert.ToString(Console.ReadLine()).Equals("S"))
                            {
                                exit = true;
                            }
                        }
                        catch
                        {
                            Header();
                            string e = "Ocurrió un error. Presione una tecla para volver al menú principal.";
                            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (e.Length / 2)) + "}", e) + "\n");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch
                {
                    Header();
                    string e = "Ocurrió un error. Presione una tecla para volver a la selección.";
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (e.Length / 2)) + "}", e) + "\n");
                    Console.ReadKey();
                }
            }
        }
    }
}
