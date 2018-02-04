using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_DES
{
    class Program
    {
        public static void Print(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static string LeftShift(string text)
        {
            char[] t = new char[5];
            for (int i = 0; i < text.Length; i++)
            {
                t[i] = text[i];
            }
            char bit = t[0];
            t[0] = t[1];
            t[1] = t[2];
            t[2] = t[3];
            t[3] = t[4];
            t[4] = bit;
            string newText = "";
            foreach (char item in t)
            {
                newText += item;
            }
            return newText;
        }

        public static string[] SplitTo(string text, int count)
        {
            string[] texts = new string[2];
            int j = 0;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = "";
                for (int k = 0; k < count; k++)
                {
                    texts[i] += text[j++];
                }
            }
            return texts;
        }

        public static void ShowMatrix(int[,] matrix)
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

        public static string[] GetKey(string key)
        {
            int[] indexes1 = { 2, 4, 1, 6, 3, 9, 0, 8, 7, 6 };
            string keyRearrenged = "";
            for (int i = 0; i < 10; i++)
            {
                keyRearrenged += key[indexes1[i]];
            }
            Print("\nKey after rearrengment with indexes 2 4 1 6 3 9 0 8 7 6 : ");
            Console.WriteLine(keyRearrenged);

            string[] keys = SplitTo(keyRearrenged, 5);
            Print("\nKey after division:");
            Console.WriteLine(keys[0] + "\t" + keys[1]);

            keys[0]= LeftShift(keys[0]);
            keys[1] = LeftShift(keys[1]);
            Print("\nDevided key after Left Shift:");
            Console.WriteLine(keys[0] + "\t" + keys[1]);
            int[] indexes2 = { 5, 2, 6, 3, 7, 4, 9, 8 };
            keyRearrenged = keys[0] + keys[1];

            string key1 = "";
            for (int i = 0; i < indexes2.Length; i++)
            {
                key1 += keyRearrenged[indexes2[i]];
            }
            Print("\nKey after compression with indexes 5 2 6 3 7 4 9 8\nKey1:");
            Console.WriteLine(key1);

            keys[0] = LeftShift(LeftShift(keys[0]));
            keys[1] = LeftShift(LeftShift(keys[1]));
            Print("\nDevided key after Left Dubble Shift:");
            Console.WriteLine(keys[0] + "\t" + keys[1]);
            keyRearrenged = keys[0] + keys[1];

            string key2 = "";
            for (int i = 0; i < indexes2.Length; i++)
            {
                key2 += keyRearrenged[indexes2[i]];
            }
            Print("\nKey after compression with indexes 5 2 6 3 7 4 9 8\nKey2:");
            Console.WriteLine(key2);
            keys[0] = key1;
            keys[1] = key2;
            return keys;
        }

        public static void S_DesEncryption(string key, string text)
        {
            string[] keys = GetKey(key);
            string textRearrenged = "";
            int[] indexes1 = { 1, 5, 2, 0, 3, 7, 4, 6 };
            for (int i = 0; i < indexes1.Length; i++)
            {
                textRearrenged += text[indexes1[i]];
            }
            Print("\nText after rearrengment with indexes 1 5 2 0 3 7 4 6");
            Console.WriteLine(textRearrenged);

            string[] texts = SplitTo(textRearrenged, 4);
            Print("\nL0\tR0");
            Console.WriteLine(texts[0] + "\t" + texts[1]);

            Print("\nROUND 1");
            int[] indexes2 = { 3, 0, 1, 2, 1, 2, 3, 0 };
            string R0 = "";
            for (int i = 0; i < indexes2.Length; i++)
            {
                R0 += texts[1][indexes2[i]];
            }
            Print("\nR0 after extension with indexes 3 0 1 2 1 2 3 0");
            Console.WriteLine(R0);

            int number = Convert.ToInt32(R0, 2) ^ Convert.ToInt32(keys[0], 2);
            R0 = Convert.ToString(number, 2).PadLeft(8, '0');//toBinary
            Print("\nR0 after XOR with key1 " + keys[0] + ":");
            Console.WriteLine(R0);

            int[,] matrix1 = { { 1, 0, 3, 2 }, { 3, 2, 1, 0 }, { 0, 2, 1, 3 }, { 3, 1, 3, 2 } };
            int[,] matrix2 = { { 0, 1, 2, 3 }, { 2, 0, 1, 3 }, { 3, 0, 1, 0 }, { 2, 1, 0, 3 } };
            string[] R0Split = SplitTo(R0, 4);
            Print("\nR0 after Split:");
            Console.WriteLine(R0Split[0] + " " + R0Split[1]);

            Print("\nMatrix1: ");
            ShowMatrix(matrix1);
            Print("\nMatrix2: ");
            ShowMatrix(matrix2);
            int index1 = Convert.ToInt32(R0Split[0][0].ToString() + R0Split[0][3].ToString(), 2);
            int index2 = Convert.ToInt32(R0Split[0][1].ToString() + R0Split[0][2].ToString(), 2);
            Print("\nIndexes for getting matrix's element: ");
            Console.WriteLine(index1 + "," + index2);
            string forFunction = Convert.ToString(matrix1[index1, index2], 2).PadLeft(2, '0');

            index1 = Convert.ToInt32(R0Split[1][0].ToString() + R0Split[1][3].ToString(), 2);
            index2 = Convert.ToInt32(R0Split[1][1].ToString() + R0Split[1][2].ToString(), 2);
            Print("\nIndexes for getting matrix's element: ");
            Console.WriteLine(index1 + "," + index2);
            forFunction += Convert.ToString(matrix2[index1, index2], 2).PadLeft(2, '0');
            Print("\nFunction before rearrengement:");
            Console.WriteLine(forFunction);

            string function = "";
            int[] indexes3 = { 1, 3, 2, 0 };
            for (int i = 0; i < indexes3.Length; i++)
            {
                function += forFunction[indexes3[i]];
            }
            Print("\nFunction after rearrengement with indexes 1 3 2 0:");
            Console.WriteLine(function);

            string R1 = Convert.ToString(Convert.ToInt32(function, 2) ^ Convert.ToInt32(texts[0], 2), 2).PadLeft(4, '0');
            string L1 = texts[1];
            Print("\nL1\tR1");
            Console.WriteLine(L1 + "\t" + R1);


            Print("\nROUND 2");
            string R2 = R1; 
            string R1_1 = "";
            for (int i = 0; i < indexes2.Length; i++)
            {
                R1_1 += R1[indexes2[i]];
            }
            Print("\nR1 after extension with indexes 3 0 1 2 1 2 3 0");
            Console.WriteLine(R1_1);

            number = Convert.ToInt32(R1_1, 2) ^ Convert.ToInt32(keys[1], 2);
            R1 = Convert.ToString(number, 2).PadLeft(8, '0');//toBinary
            Print("\nR1 after XOR with key1 " + keys[1] + ":");
            Console.WriteLine(R1);

            string[] R1Split = SplitTo(R1, 4);
            Print("\nR1 after Split:");
            Console.WriteLine(R1Split[0] + " " + R1Split[1]);

            Print("\nMatrix1: ");
            ShowMatrix(matrix1);
            Print("\nMatrix2: ");
            ShowMatrix(matrix2);
            index1 = Convert.ToInt32(R1Split[0][0].ToString() + R1Split[0][3].ToString(), 2);
            index2 = Convert.ToInt32(R1Split[0][1].ToString() + R1Split[0][2].ToString(), 2);
            Print("\nIndexes for getting matrix's element: ");
            Console.WriteLine(index1 + "," + index2);
            forFunction = Convert.ToString(matrix1[index1, index2], 2).PadLeft(2, '0');

            index1 = Convert.ToInt32(R1Split[1][0].ToString() + R1Split[1][3].ToString(), 2);
            index2 = Convert.ToInt32(R1Split[1][1].ToString() + R1Split[1][2].ToString(), 2);
            Print("\nIndexes for getting matrix's element: ");
            Console.WriteLine(index1 + "," + index2);
            forFunction += Convert.ToString(matrix2[index1, index2], 2).PadLeft(2, '0');
            Print("\nFunction before rearrengement:");
            Console.WriteLine(forFunction);

            function = "";
            for (int i = 0; i < indexes3.Length; i++)
            {
                function += forFunction[indexes3[i]];
            }
            Print("\nFunction after rearrengement with indexes 1 3 2 0:");
            Console.WriteLine(function);

            string L2 = Convert.ToString(Convert.ToInt32(function, 2) ^ Convert.ToInt32(L1, 2), 2).PadLeft(4, '0');
            Print("\nL2\tR2");
            Console.WriteLine(L2 + "\t" + R2);

            int[] indexes4 = { 3, 0, 2, 4, 6, 1, 7, 5 };
            string forText = L2 + R2;
            string newText = "";
            for (int i = 0; i < indexes4.Length; i++)
            {
                newText += forText[indexes4[i]];
            }
            Print("\nEncrypted Text(Reaarrenged with indexes  3 0 2 4 6 1 7 5 ): ");
            Console.WriteLine(newText);
        }

        static void Main(string[] args)
        {
            //key = 1011100110
            //text = 11110010
            Print("Enter key for Encryotion!");
            string key = Console.ReadLine();
            Print("Enter text for Encryotion!");
            string text = Console.ReadLine();
            S_DesEncryption(key,text);
        }
    }
}
