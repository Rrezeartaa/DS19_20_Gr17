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
                     if ("show".Equals(args[2]))
                    {
                        string plaintext = args[4];
                        string result = Railfence.encrypt(plaintext, int.Parse(args[3]));
                        Console.WriteLine(result); 
                        Railfence.show(plaintext, int.Parse(args[3]));Console.ReadKey();
                    }
                    else
                    {
                        string plaintext = args[3];
                        string result = Railfence.encrypt(plaintext, int.Parse(args[2]));
                        Console.WriteLine(result);
                        Console.ReadKey();
                    }
                }
                else if ("decrypt".Equals(args[1]))
                {
                   if ("show".Equals(args[2]))
                    {
                        string ciphertext = args[4];
                        string result = Railfence.decrypt(ciphertext, int.Parse(args[3]));
                        Console.WriteLine(result); Console.ReadKey();
                        Railfence.show(ciphertext, int.Parse(args[3]));
                    }
                    else
                    {
                        string ciphertext = args[3];
                        string result = Railfence.decrypt(ciphertext, int.Parse(args[2]));
                        Console.WriteLine(result);
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("\nArgumente jovalide! \nSiguroheni qe keni shkruar argumentet si ne vijim:  \n" +
                            "ds rail-fence encrypt <rails> <text>: per enkriptimin e plaintext-it\n" +
                            "ds rail-fence decrypt <rails> <text>: per dekriptimin e ciphertext-it\n"+
                            "ds rail-fence encrypt show <rails> <text>: per te pare tekstin e enkriptuar te organizuar ne shirita\n" +
                            "ds rail-fence decrypt show <rails> <text>: per te pare tekstin e dekriptuar te organizuar ne shirita\n");

                }
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
