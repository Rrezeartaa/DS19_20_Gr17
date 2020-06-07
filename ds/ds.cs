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
        private const int ERROR_BAD_ARGUMENTS = 0xA0;
        public static void Main(string[] args)
        {
            if (args.Length < 2|| args.Length > 7)
            {
                throw new IndexOutOfRangeException("\n\tFunksionet e ketij programi pranojne 2 deri ne 5 argumente!");
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
                    Console.WriteLine("\nArgumentet jane jovalide! \nSigurohuni qe keni shkruar argumentet si ne vijim: \n" +
                            "ds count lines <text>: per te llogaritur numrin e rreshtave\n" +
                            "ds count words <text>: per te llogaritur numrin e fjaleve\n" +
                            "ds count letters <text>: per te llogaritur numrin e shkronjave\n" +
                            "ds count symbols <text>: per te llogaritur numrin e simboleve\n" +
                            "ds count vowels <text>: per te llogaritur numrin e zanoreve\n" +
                            "ds count consonants <text>: per te llogaritur numrin e bashketingelloreve\n"+
                            "ds count sentences <text>: per te llogaritur numrin e fjalive\n");
                    Environment.Exit(2);
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
                            " te karakterit te specifikuar.\n");    
                  Environment.Exit(2);
              }

            }
               
            else if ("rail-fence".Equals(args[0]))
            {
                if ("encrypt".Equals(args[1]))
                {
                    if ("--show".Equals(args[2]))
                    {
                        string plaintext = args[4];  
                        railfence.show(plaintext, int.Parse(args[3]));
                        Console.ReadKey();
                    }
                    else
                    {
                        string plaintext = args[3];
                        railfence.encrypt(plaintext, int.Parse(args[2]));                       
                        Console.ReadKey();
                    }

                }
                else if ("decrypt".Equals(args[1]))
                {                 
                        string ciphertext = args[3];
                        string result = railfence.decrypt(ciphertext, int.Parse(args[2]));
                        Console.WriteLine(result);
                        Console.ReadKey();   
                }
                else
                {
                    Console.WriteLine("\nArgumentet jane jovalide! \nSigurohuni qe keni shkruar argumentet si ne vijim:  \n" +
                            "ds rail-fence encrypt <rails> <plaintext>: per enkriptimin e plaintext-it\n" +
                            "ds rail-fence decrypt <rails> <ciphertext>: per dekriptimin e ciphertext-it\n" +
                            "ds rail-fence encrypt --show <rails> <text>: per te pare tekstin e enkriptuar te organizuar ne shirita\n");
                    Environment.Exit(2);
                }
            }
            //Faza e dyte
            else if ("create-user".Equals(args[0]))
            {
                if (args.Length == 2)
                {
                    string strRegex = @"^[a-zA-Z0-9_]*$";
                    Regex re = new Regex(strRegex);
                    if (re.IsMatch(args[1]))
                    {
                        rsa.GenKey(args[1]);
                    }
                    else
                        Console.WriteLine("Keni dhene karaktere qe nuk lejohen!");
                }
                else
                    Console.WriteLine("Keni dhene argumente te teperta per kete komande!");               
            }
            else if ("delete-user".Equals(args[0]))
            {
                if (args.Length == 2)
                {
                    string strRegex = @"^[a-zA-Z0-9_]*$";
                    Regex re = new Regex(strRegex);
                    if (re.IsMatch(args[1]))
                    {
                        rsa.DeleteKey(args[1]);
                    }
                    else
                        Console.WriteLine("Keni dhene karaktere qe nuk lejohen!");
                }
                else
                    Console.WriteLine("Keni dhene argumente te teperta per kete komande!");

            }
            else if ("export-key".Equals(args[0]))
            {               
                    if (args.Length == 3)
                    {
                        rsa.ExportKey(args[1], args[2]);
                    }
                    else if(args.Length == 4)
                        rsa.ExportKey1(args[1], args[2], args[3]);              
                else
                    Console.WriteLine("Nuk i keni dhene argumentet ne rregull!");
            }
            else if ("import-key".Equals(args[0])) {
                if (args.Length == 3)
                {
                    rsa.ImportKey(args[1], args[2]);
                }
                else
                    Console.WriteLine("Nuk i keni dhene argumentet ne rregull!");                
            }
            else if ("write-message".Equals(args[0]))
            {
                if (args.Length == 3)
                {
                    rsa.encrypt(args[1], args[2]);
                }
                else if (args.Length == 4)
                    rsa.encrypt(args[1], args[2], args[3]);
                else if (args[3].Equals("--sender"))
                {
                    encdecwithtoken.encryptii(args[1], args[2], args[4]);              
                }
                else if (args[4].Equals("--sender"))
                {
                    encdecwithtoken.encrypttok(args[1], args[2], args[3], args[5]);
                }
                else
                    Console.WriteLine("Nuk i keni dhene argumentet ne rregull!");

            }
            else if ("read-message".Equals(args[0]))
            {
                if (args.Length == 2)
                    rsa.decrypt(args[1]);
                else
                    Console.WriteLine("Nuk i keni dhene argumentet ne rregull!");
            }
            else if ("login".Equals(args[0]))
            {
                if (args.Length == 2)
                    loginstatus.loginGenerateToken(args[1]);
                else
                    Console.WriteLine("Nuk i keni dhene argumentet ne rregull!");
            }
            else if ("status".Equals(args[0]))
            {
                if (args.Length == 2)
                    loginstatus.statusToken(args[1]);
                else
                    Console.WriteLine("Nuk i keni dhene argumentet ne rregull!");
            }
            else
            {
                Console.WriteLine("\nArgumentet jane jovalide. Argumenti i pare eshte njeri prej funksioneve tona: \n" +
                        "count\n" +
                        "numerical\n" +
                        "rail-fence\n"+
                        "create-user\n"+
                        "delete-user\n"+
                        "export-key\n"+
                        "write-message\n"+
                        "read-message\n"+
                        "login\n"+
                        "status\n\n");
                Environment.ExitCode = ERROR_BAD_ARGUMENTS;
            }
        }
    }
}
