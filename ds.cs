using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ds
{
    public static class Cipher
    {
        public static void Main(string[] args)
        {
            if (args[0].Equals("rail-fence"))
            {
                if (args[1].Equals("encrypt"))
                {
                    string plaintext = args[3];
                    string result = Encrypt(plaintext, int.Parse(args[2]));

                    Console.WriteLine(result);
                    Console.ReadKey();
                }
                else if (args[1].Equals("decrypt"))
                {
                    string plaintext = args[3];
                    string result = Decrypt(plaintext, int.Parse(args[2]));

                    Console.WriteLine(result);
                    Console.ReadKey();
                }

                
            }

        }


        public static string Encrypt(string plaintext, int rails)
        {
            char[] plain = plaintext.ToCharArray();              
            char[,] cipher = new char[rails,plain.Length];  
            for (int i = 0; i < rails; i++)
                for (int j = 0; j < plain.Length; j++)
                    cipher[i,j] = '#';

            int level = 0;
            int flag = 0;

            for (int i = 0; i < plain.Length; i++)
            {
                cipher[level,i] = plain[i];

                if (flag == 0)
                {
                    level++;

                    if (level == rails - 1)
                        flag = 1;
                }
                else
                {
                    level--;

                    if (level == 0)
                        flag = 0;
                }
            }

            String cipher_text = "";
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < plain.Length; j++)
                {
                    if (cipher[i,j] != '#')
                        cipher_text += cipher[i,j];
                }
            }
            return cipher_text;
        }


        public static String Decrypt(string plaintext, int rails)
        {

            char[] plain = plaintext.ToCharArray();               
            char[,] cipher = new char[rails,plain.Length];  
            for (int i = 0; i < rails; i++)
                for (int j = 0; j < plain.Length; j++)
                    cipher[i,j] = '#';

            int level = 0;
            int flag = 0;
            int index = 0;

            for (int i = 0; i < plain.Length; i++)
            {
                cipher[level,i] = '*';
                if (flag == 0)
                {
                    level++;

                    
                    if (level == rails - 1)
                        flag = 1;
                }
                else
                {
                    level--;

                    if (level == 0)
                        flag = 0;
                }
            }

            
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < plain.Length; j++)
                {
                    if (cipher[i,j] == '*')
                    {
                        cipher[i,j] = plain[index];
                        index++;
                    }
                }
            }

            String cipher_text = "";
            level = 0;
            flag = 0;
            for (int i = 0; i < plain.Length; i++)
            {
                if (flag == 0)
                {
                    level++;
                    if (level == rails - 1)
                        flag = 1;
                }
                else
                {
                    level--;
                    if (level == 0)
                        flag = 0;
                }
            }
            return cipher_text;

        }

    }
}
