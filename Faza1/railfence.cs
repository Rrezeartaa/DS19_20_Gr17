using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


public class Railfence
{
    public static string encrypt(string plaintext, int rails)
    {
        char[] plain = plaintext.ToCharArray();
        char[,] cipher = new char[rails, plain.Length];

        for (int i = 0; i < rails; i++)
            for (int j = 0; j < plain.Length; j++)
                cipher[i, j] = '#';

        int level = 0;
        int flag = 0;

        for (int i = 0; i < plain.Length; i++)
        {
            cipher[level, i] = plain[i];

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
                if (cipher[i, j] != '#')
                    cipher_text += cipher[i, j];
            }
        }
        return cipher_text;
    }

    public static string decrypt(string plaintext, int rails)
    {

        char[] plain = plaintext.ToCharArray();
        char[,] cipher = new char[rails, plain.Length];

        for (int i = 0; i < rails; i++)
            for (int j = 0; j < plain.Length; j++)
                cipher[i, j] = '#';

        int level = 0;
        int flag = 0;
        int index = 0;

        for (int i = 0; i < plain.Length; i++)
        {
            cipher[level, i] = '*';

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
                if (cipher[i, j] == '*')
                {
                    cipher[i, j] = plain[index];
                    index++;
                }
            }
        }

        String ciphertext = "";
        level = 0;
        flag = 0;

        for (int i = 0; i < plain.Length; i++)
        {

            if (cipher[level, i] != '#')
                ciphertext += cipher[level, i];
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

        return ciphertext;

    }
   public static void show(string plaintext, int rails)
    {

        if (rails == 1)
        {
            
            Console.Write(plaintext);
            return;
        }
 
        int len = plaintext.Length;

        char[,] a = new char[len, len];
        char[] c = plaintext.ToCharArray();

        int row = 0;
        bool down = true;

        for (int i = 0; i < len; i++)
        {
            a[row, i] = c[i];

            if (row == rails - 1)
                down = false;
            else if (row == 0)
                down = true;

            if (down)
                row++;
            else
                row--;
        }

        for (int i = 0; i < rails; i++)
        {
            for (int j = 0; j < len; j++)
            {
                Console.Write(a[i, j] + " ");
            }
            Console.Write("\n");
        }
    }



}
