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
            railfence railfence=new railfence();
            
            if (args.Length < 3 )
            {
                throw new IndexOutOfRangeException("\n\tNuk ka argumente te mjaftueshme!" +
                                                        "\n\tFunksionet e ketij programi pranojne 4 ose 5 argumente!");
               
            }
            if (args.Length > 4)
            {
                throw new IndexOutOfRangeException("\nNumri i argumenteve eshte i tepert!");
                
            }

            
            if ("rail-fence".Equals(args[0]))
            {
                if ("encrypt".Equals(args[1]))
                {
                    string plaintext = args[3];
                    string result =railfence.Encrypt(plaintext, int.Parse(args[2]));

                    Console.WriteLine(result);
                    Console.ReadKey();
                }
                else if ("decrypt".Equals(args[1]))
                {
                    string plaintext = args[3];
                    string result =railfence.Decrypt(plaintext, int.Parse(args[2]));

                    Console.WriteLine(result);
                    Console.ReadKey();
                }
                else if("numerical".Equals(args[0])){
                    if("encode".Equals(args[1]))
                    {
                        string input=args[2];
                        string result=Numerical.Encode(input);
                        Console.WriteLine(result);
                        Console.ReadKey();
                    }

                
            }

        }


       

    }
}
