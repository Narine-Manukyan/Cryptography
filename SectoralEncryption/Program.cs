using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace SectoralEncryption
{
    class Program
    {
        public static void ShowArray(string[] array)
        {
            foreach (string item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        public static void Print(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static string ToBinary(int number)
        {
            string binary = Convert.ToString(number, 2);
            return binary;
        }

        public static string LeftOrRightShift(string text, string leftOrRight)
        {
            char[] t = new char[4];
            for (int i = 0; i < text.Length; i++)
            {
                t[i] = text[i];
            }
            if (leftOrRight == "left")
            {
                char bit = t[0];
                t[0] = t[1];
                t[1] = t[2];
                t[2] = t[3];
                t[3] = bit;
            } else
            {
                char bit = t[3];
                t[3] = t[2];
                t[2] = t[1];
                t[1] = t[0];
                t[0] = bit;
            }
            string newText = "";
            foreach (char item in t)
            {
                newText += item;
            }
            return newText;
        }

        public static string[] DevideString(string text, string key)
        {
            string[] texts = new string[text.Length / 4];
            int j = 0;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = "";
                for (int k = 0; k < key.Length; k++)
                {
                    texts[i] += text[j++];
                }
            }
            return texts;
        }

        //Electronic Code Book 
        public static void ECBEncrypt(string text, string key)
        {
            string[] texts = DevideString(text, key);
            Print("\nDevided string: ");
            ShowArray(texts);
            for (int i = 0; i < texts.Length; i++)
            {
                int number = Convert.ToInt32(texts[i], 2);
                number ^= Convert.ToInt32(key, 2);
                texts[i] = ToBinary(number).PadLeft(4, '0');
            }
            Print("\nXOR: ");
            ShowArray(texts);
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = LeftOrRightShift(texts[i], "left");
            }
            Print("\nAfter <<: ");
            ShowArray(texts);
        }

        public static void ECBDecrypt(string text, string key)
        {
            string[] texts = DevideString(text, key);
            Print("\nDevided string: ");
            ShowArray(texts);
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = LeftOrRightShift(texts[i], "right");
            }
            Print("\nAfter >>: ");
            ShowArray(texts);
            for (int i = 0; i < texts.Length; i++)
            {
                int number = Convert.ToInt32(texts[i], 2);
                number ^= Convert.ToInt32(key, 2);
                texts[i] = ToBinary(number).PadLeft(4, '0');
            }
            Print("\nXOR: ");
            ShowArray(texts);
        }
          
        //Cipher Block Chaining
        public static void CBCEncrypt(string text, string key)
        {
            string[] texts = DevideString(text, key);
            Print("\nDevided string: ");
            ShowArray(texts);
            int number = Convert.ToInt32(texts[0], 2);
            number ^= Convert.ToInt32(key, 2);
            texts[0] = ToBinary(number).PadLeft(4, '0');
            texts[0] = LeftOrRightShift(texts[0], "left");
            string keyForXor = texts[0];
            for (int i = 1; i < texts.Length; i++)
            {
                number = Convert.ToInt32(texts[i], 2);
                number ^= Convert.ToInt32(keyForXor, 2);
                number ^= Convert.ToInt32(key, 2);
                texts[i] = ToBinary(number).PadLeft(4, '0');
                texts[i] = LeftOrRightShift(texts[i],"left");
                keyForXor = texts[i];
            }
            Print("\nXOR: ");
            ShowArray(texts);
        }       

        //Plaintext Block Chaining 
        public static void PBCEncrypt(string text, string key)
        {
            string[] texts = DevideString(text, key);
            Print("\nDevided string: ");
            ShowArray(texts);
            int keyForXor = Convert.ToInt32(texts[0], 2);
            int number = Convert.ToInt32(texts[0], 2);
            number ^= Convert.ToInt32(key, 2);
            texts[0] = ToBinary(number).PadLeft(4, '0');
            texts[0] = LeftOrRightShift(texts[0],"left");
            for (int i = 1; i < texts.Length; i++)
            {
                number = Convert.ToInt32(texts[i], 2);
                number ^= keyForXor;
                keyForXor = number;
                number ^= Convert.ToInt32(key, 2);
                texts[i] = ToBinary(number).PadLeft(4, '0');
                texts[i] = LeftOrRightShift(texts[i],"left");
            }
            Print("\nXOR: ");
            ShowArray(texts);
        }

        //Cipher Feedback
        public static void CFBEncrypt(string text, string key)
        {
            string[] texts = DevideString(text, key);
            Print("\nDevided string: ");
            ShowArray(texts);
            int n = texts.Length - 1;
            int number = Convert.ToInt32(texts[n], 2);
            number ^= Convert.ToInt32(key, 2);
            texts[n] = ToBinary(number).PadLeft(4, '0');
            texts[n] = LeftOrRightShift(texts[n], "left");
            string keyForXor = texts[n];
            for (int i = n - 1; i >= 0; i--)
            {
                number = Convert.ToInt32(texts[i], 2);
                number ^= Convert.ToInt32(keyForXor, 2);
                number ^= Convert.ToInt32(key, 2);
                texts[i] = ToBinary(number).PadLeft(4, '0');
                texts[i] = LeftOrRightShift(texts[i], "left");
                keyForXor = texts[i];
            }
            Print("\nXOR: ");
            ShowArray(texts);
        }

        //Plaintext Feedback
        public static void PFBEncrypt(string text, string key)
        {
            string[] texts = DevideString(text, key);
            Print("\nDevided string: ");
            ShowArray(texts);
            int n = texts.Length - 1;
            int keyForXor = Convert.ToInt32(texts[n], 2);
            int number = Convert.ToInt32(texts[n], 2);
            number ^= Convert.ToInt32(key, 2);
            texts[n] = ToBinary(number).PadLeft(4, '0');
            texts[n] = LeftOrRightShift(texts[n], "left");
            for (int i = n - 1; i >= 0; i--)
            {
                number = Convert.ToInt32(texts[i], 2);
                number ^= keyForXor;
                keyForXor = number;
                number ^= Convert.ToInt32(key, 2);
                texts[i] = ToBinary(number).PadLeft(4, '0');
                texts[i] = LeftOrRightShift(texts[i], "left");
            }
            Print("\nXOR: ");
            ShowArray(texts);
        }

        static void Main(string[] args)
        {
            Print("Enter key: ");
            //string key = Console.ReadLine();
            string key = "1011";
            Print("\nEnter text for Encrytion: ");
            //string text = Console.ReadLine();
            string text = "10100010001110101001";

            Print("\n\nECB");
            ECBEncrypt(text,key);

            Print("\n\nCBC");
            CBCEncrypt(text, key);

            Print("\n\nPBC");
            PBCEncrypt(text, key);

            Print("\n\nCFB");
            CFBEncrypt(text, key);

            Print("\n\nPFB");
            PFBEncrypt(text, key);

            Print("\nEnter text for Decrytion: ");
            //string text1 = Console.ReadLine();
            string text1 = "00100011000100100100";

            Print("\n\nECB");
            ECBDecrypt(text1, key);
        }
    }
}
