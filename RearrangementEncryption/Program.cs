using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RearrangementEncryption
{
    class Program
    {
        public static char[,] CreateMatrix(string text, int key)
        {
            string newText = "";
            int index = 0;
            foreach (char item in text)
            {
                if (item == ' ')
                    continue;
                newText += item;
            }
            char[,] matrix = new char[(int)Math.Ceiling((double)newText.Length / key), key];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (index >= newText.Length)
                    {
                        matrix[i, j] = 'z';
                        index++;
                    }
                    else
                        matrix[i, j] = newText[index++];
                }
            }
            return matrix;
        }

        public static void ShowMatrix(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static string Encryption(string text, int key)
        {
            char[,] matrix = CreateMatrix(text, key);
            string newText = "";
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    newText += matrix[j, i];
                }
            }
            return newText;
        }

        public static char[,] CreateMatrixD(string text, int key)
        {
            int index = 0;
            char[,] matrix = new char[key, (int)Math.Ceiling((double)text.Length / key)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (index >= text.Length)
                    break;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = text[index++];
                }
            }
            return matrix;
        }

        public static string Decryption(string text, int key)
        {
            char[,] matrix = CreateMatrixD(text, key);
            string newText = "";
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    newText += matrix[j, i];
                }
            }
            return newText;
        }

        public static void Coloring(string s)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Coloring("Enter text for Encryption: ");
            string text = Console.ReadLine();

            Coloring("Enter key: ");
            int key = int.Parse(Console.ReadLine());

            Coloring("Matrix: ");
            ShowMatrix(CreateMatrix(text, key));

            Coloring("Encription: ");
            Console.WriteLine(Encryption(text, key));

            Coloring("Enter text for Decryption: ");
            string text1 = Console.ReadLine();

            Coloring("Enter key: ");
            int key1 = int.Parse(Console.ReadLine());

            Coloring("Matrix: ");
            ShowMatrix(CreateMatrixD(text1, key1));

            Coloring("Decription: ");
            Console.WriteLine(Decryption(text1, key1));
        }
    }
}
