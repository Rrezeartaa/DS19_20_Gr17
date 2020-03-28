using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
public class railfence1
{
    public static void encrypt(String plaintext, int key)
    {
        String ciphertext = "";
        plaintext = Regex.Replace(plaintext, @"\s+", "");
        bool kontrollo = false;
        int j = 0;
        int rreshta = key;
        int kolona = plaintext.Length;

        char[,] a = new char[rreshta, kolona];

        for (int i = 0; i < kolona; i++)
        {
            if (j == 0 || j == key - 1)
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

    }



    public static String decrypt(String ciphertext, int key)
    {
        String deciphertext = "";

        bool kontrollo = false;
        int j = 0;
        int rreshta = key;
        int kolona = ciphertext.Length;

        char[,] a = new char[rreshta, kolona];


        for (int i = 0; i < kolona; i++)
        {
            if (j == 0 || j == key - 1)
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
    public static void show( String text,int key)
    {
        
        int length =text.Length / key;
        text = Regex.Replace(text, @"\s+", "");
        StringBuilder s = new StringBuilder();
        String [,] cipherText = new String[key,length];
        int l = 0;
        text.Replace(" ","");
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < key; j++)
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
