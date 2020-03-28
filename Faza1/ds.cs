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


            if (args.Length < 3)
            {
                throw new IndexOutOfRangeException("\n\tNuk ka argumente te mjaftueshme!" +
                                                        "\n\tFunksionet e ketij programi pranojne 4 ose 5 argumente!");
            }
            else if (args.Length > 5)
            {
                throw new IndexOutOfRangeException("\nNumri i argumenteve eshte i tepert!");
            }
            
            
            else if ("count".Equals(args[0]))
            {
                if ("lines".Equals(args[1]))
                {
                    Console.WriteLine(count.countlines(args[2]));
                }

                else if ("words".Equals(args[1]))
                {
                    Console.WriteLine(count.countwords(args[2]));
                }
                else if ("letters".Equals(args[1]))
                {
                    Console.WriteLine(count.countletters(args[2]));
                }
                
                else if ("symbols".Equals(args[1]))
                {
                    Console.WriteLine(count.countsymbols(args[2]));
                }
                else if ("vowels".Equals(args[1]))
                {
                    Console.WriteLine(count.countvowels(args[2]));
                }
                else if ("consonants".Equals(args[1]))
                {
                    Console.WriteLine(count.countconsonants(args[2]));
                }
                else if("sentences".Equals(args[1]))
                {
                    Console.WriteLine(count.countsent(args[2]));
                }

                else
                {
                    Console.WriteLine("\nArgumentet jane jovalide! \nSigurohuni qe keni shkruar argumentet si ne vijim:  \n" +
                            "ds count lines <text>: per te llogaritur numrin e rreshtave\n" +
                            "ds count words <text>: per te llogaritur numrin e fjaleve\n" +
                            "ds count letters <text>: per te llogaritur numrin e shkronjave\n" +
                            "ds count symbols <text>: per te llogaritur numrin e simboleve\n" +
                            "ds count vowels <text>: per te llogaritur numrin e zanoreve\n" +
                            "ds count consonants <text>: per te llogaritur numrin e bashketingelloreve\n"+
                            "ds count sentences <text>: per te llogaritur numrin e fjalive\n"
                            );

                }
            }
             else if ("numerical".Equals(args[0]))
            {
                if ("encode".Equals(args[1]))
                {
                    if ("--separator".Equals(args[2]))
                    {

                        string result = Numerical.Encode(args[4]);
                        string resultt = Numerical.separator(args[3], args[4]);
                        Console.WriteLine(result);
                        Console.WriteLine(resultt);
                        Console.ReadKey();


                    }

                    else
                    {
                        string text = args[2];
                        string result = Numerical.Encode(text);
                        Console.WriteLine(result);
                        Console.ReadKey();
                    }

                }
                else if ("decode".Equals(args[1]))
                {


                    string code = args[2];

                    Numerical.Decode(code);

                    Console.ReadKey();

                }
              else
              {
                Console.WriteLine("\nArgumentet jane jovalide! \nSigurohuni qe keni shkruar argumentet si ne vijim:  \n" +
                            "ds numerical encode <text>: per ta enkoduar tekstin ne pozitat alfabetike te shkronjave.\n" +
                            "ds numerical decode <code>: per ta dekoduar vargun <code> nga shifrat ne shkronjat perkatese.\n" +
                            "ds numerical encode --separator <char> <text>: per ta paraqitur tekstin e enkoduar te ndare me ane"+
                                  " te karakterit te specifikuar.\n"
                                 
                            );
              
              }

            }
            
            else if ("rail-fence".Equals(args[0]))
            {
                if ("encrypt".Equals(args[1]))
                {

                    if ("--show".Equals(args[2]))
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
                    Console.WriteLine("\nArgumentet jane jovalide! \nSigurohuni qe keni shkruar argumentet si ne vijim:  \n" +
                            "ds rail-fence encrypt <rails> <plaintext>: per enkriptimin e plaintext-it\n" +
                            "ds rail-fence decrypt <rails> <ciphertext>: per dekriptimin e ciphertext-it\n" +
                            "ds rail-fence encrypt --show <rails> <plaintext>: per te pare tekstin e enkriptuar te organizuar\n" +
                            "ds rail-fence decrypt --show <rails> <ciphertext>: per te pare tekstin e dekriptuar te organizuar\n");

                }
                

            }
            
            else if ("rail-fence1".Equals(args[0]))
            {
                if ("encrypt".Equals(args[1]))
                {

                    if ("--show".Equals(args[2]))
                    {
                        string plaintext = args[4];
                        
                        railfence1.encrypt1(plaintext, int.Parse(args[3]));
                        railfence1.show1(plaintext, int.Parse(args[3]));

                        Console.ReadKey();
                    }
                    else
                    {
                        string plaintext = args[3];
                       railfence1.encrypt1(plaintext, int.Parse(args[2]));
                        
                        Console.ReadKey();
                    }

                }
                else if ("decrypt".Equals(args[1]))
                {
                    if ("show".Equals(args[2]))
                    {
                        string ciphertext = args[4];
                        string result= railfence1.decrypt1(ciphertext, int.Parse(args[3]));
                        Console.WriteLine(result);
                        railfence1.show1(ciphertext, int.Parse(args[3]));
                      Console.ReadKey();
                      
                    }
                    else
                    {
                        string ciphertext = args[3];
                        string result = railfence1.decrypt1(ciphertext, int.Parse(args[2]));
                        Console.WriteLine(result);

                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("\nArgumentet jane jovalide! \nSigurohuni qe keni shkruar argumentet si ne vijim:  \n" +
                            "ds rail-fence1 encrypt <rails> <text>: per enkriptimin e plaintext-it\n" +
                            "ds rail-fence1 decrypt <rails> <text>: per dekriptimin e ciphertext-it\n" +
                            "ds rail-fence1 encrypt --show <rails> <text>: per te pare tekstin e enkriptuar te organizuar\n" +
                            "ds rail-fence1 decrypt --show <rails> <text>: per te pare tekstin e dekriptuar te organizuar\n" 
                            
                            );

                }

            }
            
            else
            {
                Console.WriteLine("\nArgumentet jane jovalide. Argumenti i pare eshte njeri prej funksioneve tona: \n" +
                        " count\n" +
                        " numerical\n" +
                        " rail-fence\n"+
                                  "rail-fence1\n\n");
                
            }



        }


        
    }
}
