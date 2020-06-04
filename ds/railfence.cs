using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
public class railfence
{
    public static void encrypt(String plaintext, int rails)
    {
        String ciphertext = "";
        plaintext = Regex.Replace(plaintext, @"\s+", "");
        int n = rails-plaintext.Length % rails;
        plaintext= plaintext + new String('w', n);
        bool kontrollo = false;
        int j = 0;
        int rreshta = rails;
        int kolona = plaintext.Length;

        char[,] a = new char[rreshta, kolona];

        for (int i = 0; i < kolona; i++)
        {
            if (j == 0 || j == rails - 1)
                kontrollo = !kontrollo;

            a[j, i] = plaintext[i];

            if (kontrollo)
                j++;
            else
                j = 0;
        }

        for (int i = 0; i < rreshta; i++)
        {
            for (int k = 0; k < kolona; k++)
            {
                if (a[i, k] != 0)
                    ciphertext += a[i, k];
            }
        }
        Console.WriteLine(ciphertext);
    }
    
    public static String decrypt(String ciphertext, int rails)
    {
        String deciphertext = "";
        ciphertext = Regex.Replace(ciphertext, "w", "");
        bool kontrollo = false;
        int j = 0;
        int rreshta = rails;
        int kolona = ciphertext.Length;

        char[,] a = new char[rreshta, kolona];

        for (int i = 0; i < kolona; i++)
        {
            if (j == 0 || j == rails - 1)
                kontrollo = !kontrollo;

            a[j, i] = '*';
            if (kontrollo) j++;
            else j = 0;
        }
        int index = 0;

        for (int i = 0; i < rreshta; i++)
        {
            for (int k = 0; k < kolona; k++)
            {
                if (a[i, k] == '*' && index < ciphertext.Length)
                {
                    a[i, k] = ciphertext[index++];
                }
            }
        }
        
        kontrollo = false;
        j = 0;

        for (int i = 0; i < kolona; i++)
        {
            if (j == 0 || j == rreshta - 1)
                kontrollo = !kontrollo;

            deciphertext += a[j, i];

            if (kontrollo) j++;
            else j = 0;

        }
        return deciphertext;
    }
    public static void show( String text,int rails)
    {
        text = Regex.Replace(text, @"\s+", "");
        int n = rails - text.Length % rails;
        text = text + new String('w', n);
        int length =text.Length / rails;
        String [,] cipherText = new String[rails,length];
        int l = 0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < rails; j++)
            {
                cipherText[j, i] = "" + text[l];
                l++;
            }
        }
        for (int i = 0; i < cipherText.GetLength(0); i++)
        {
            for (int u = 0; u < cipherText.GetLength(1); u++)
            {
               Console.Write(cipherText[i,u] + " ");
            }
           Console.WriteLine();
        }
        Console.WriteLine();    
    }
}
