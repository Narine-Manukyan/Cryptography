using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePad
{
    class Program
    {
        public static Random rd = new Random();
        public static string GenerateKey(int keyLength)
        {
            int k;
            string strKey = string.Empty;
            for (int x = 0; x < keyLength; x++)
            {
                k = rd.Next(0,10);
                strKey = strKey + k;
                if ((strKey.Length + 1) % 6 == 0)
                { 
                    strKey += " ";
                }
            }
            return strKey;
        }

        public static string[] GenerateKeys(int count)
        {
            string[] strKeys = new string[count];
            for (int i = 0; i < count; i++)
            {
                strKeys[i] = GenerateKey(130);
            }
            return strKeys;
        }

        public static void ShowKeys(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                Console.WriteLine("Key: " + (i + 1) + "\n" + str[i] + "\n");
            }
        }

        public static string[] BinKey()
        {
            string startKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ,.";
            string []binKey = new string[startKey.Length];
            for (int i = 0; i < startKey.Length; i++)
            {
                if (i >= 0 && i <= 9)
                    binKey[i] += '0' + i.ToString();
                else
                    binKey[i] += i.ToString();
            }
            return binKey;
        }

        public static void ShowBinKey()
        {
            string[] binKey = BinKey();
            foreach (string item in binKey)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        public static string ForEncrypt(string text)
        {
            string startKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ,.";
            string[] binKey = BinKey();
            string forEncrypt = "";
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < startKey.Length; j++)
                {
                    if(text[i] == startKey[j])
                    {
                        forEncrypt += binKey[j];
                        break;
                    }
                }
            }
            return forEncrypt;
        }

        public static void Encrypt(string text, string[] strKeys)
        {
            Print("\nAll  Generated Keys...");
            ShowKeys(strKeys);
            string forEncrypt = ForEncrypt(text);
            Print("\nDecimal for Text: ");
            Console.WriteLine(forEncrypt);
            int k = rd.Next(0, 5);
            string newText = "";
            for (int i = 0; i < 5; i++)
            {
                newText += strKeys[k][i];
            }
            int j = 6;
            for (int i = 0; i < forEncrypt.Length; i++)
            {
                if (strKeys[k][j] == ' ')
                    j++;
                int mod = int.Parse(strKeys[k][j].ToString()) + int.Parse(forEncrypt[i].ToString());
                if (mod >= 10)
                    mod -= 10;
                newText += mod;
                j++;
            }
            Print("\nEncryption: "); 
            Console.WriteLine(newText);

        }

        public static void Decrypt(string text, string[] strKeys)
        {
            string startKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ,.";
            string[] binKey = BinKey();
            int k = -1;
            for (int i = 0; i < strKeys.GetLength(0); i++)
            {
                int count = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (text[j] == strKeys[i][j])
                        ++count;
                }
                if (count == 5)
                {
                    k = i;
                    break;
                }
            }
            string key = "";
            for (int i = 0; i < 5; i++)
            {
                key += text[i];
            }
            int c = 6;
            for (int i = 5; i < text.Length; i++)
            {
                if (strKeys[k][c] == ' ')
                    c++;
                int mod = int.Parse(text[i].ToString()) - int.Parse(strKeys[k][c].ToString());
                if (mod < 0)
                    mod += 10;
                key += mod;
                c++;
            }
            Print("\nKey for Decryption: ");
            Console.WriteLine(key);
            string newText = "";
            for (int i = 5; i < key.Length - 1; i+=2)
            {
                for (int j = 0; j < binKey.Length; j++)
                {
                    string keys = "";
                    keys += key[i];
                    keys += key[i + 1];
                    if (keys == binKey[j])
                    {
                        newText += startKey[j];
                        break;
                    }
                }
            }
            Print("\nDecryption: ");
            Console.WriteLine(newText);
        }

        public static void Print(string s)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Print("Used Symbols: ");
            Console.WriteLine("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ,.");
            Print("\nCodes for Symbols: ");
            ShowBinKey();
            Print("\nEnter text for Encryption: ");
            string text1 = Console.ReadLine();
            string[] strKeys = GenerateKeys(5);
            Encrypt(text1, strKeys);

            Print("\nEnter text for Decryption: ");
            string text2 = Console.ReadLine();
            Decrypt(text2, strKeys);
        }
    }
}
