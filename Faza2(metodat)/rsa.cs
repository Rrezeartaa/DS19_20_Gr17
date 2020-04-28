using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace ds
{
    class rsa
    {
        private static RSAParameters publicKey;
        private static RSAParameters privateKey;
        static string CONTAINER_NAME = "MyGeneratedKeys";
        public enum KeySizes { 
           SIZE_512=512,
           SIZE_1024=1024,
            SIZE_2048=2048,
            SIZE_952 = 952,
            SIZE_1369 = 1369

        };
        public static void GenKey(string name)
        {
            string publicKeyFile = "keys/" + name + ".pub.xml";
            string privateKeyFile= "keys/" + name + ".xml";
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                if (!File.Exists(publicKeyFile) && !File.Exists(privateKeyFile))
                {
                    rsa.PersistKeyInCsp = false;
                    if (File.Exists(privateKeyFile))
                        File.Delete(privateKeyFile);
                    if (File.Exists(publicKeyFile))
                        File.Delete(publicKeyFile);
                    string publicKey = rsa.ToXmlString(false);
                    File.WriteAllText(publicKeyFile, publicKey);
                    string privateKey = rsa.ToXmlString(true);
                    File.WriteAllText(privateKeyFile, privateKey);

                    Console.WriteLine("Eshte krijuar celesi privat '" + publicKeyFile + "'");
                    Console.WriteLine("Eshte krijuar celesi publik '" + privateKeyFile + "'");
                }
                else
                    Console.WriteLine("Gabim: Celesi '" + name + "' ekziston paraprakisht.");
            }
      
        }
        
         public static void DeleteKey(string name)
        {
            string publicKeyFile = "keys/" + name + ".pub.xml";
            string privateKeyFile = "keys/" + name + ".xml";
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                if (File.Exists(publicKeyFile) && File.Exists(privateKeyFile))
                {
                    rsa.PersistKeyInCsp = false;
                    if (File.Exists(privateKeyFile))
                        File.Delete(privateKeyFile);
                    if (File.Exists(publicKeyFile))
                        File.Delete(publicKeyFile);
                    Console.WriteLine("Eshte larguar celesi privat '" + privateKeyFile + "'");
                    Console.WriteLine("Eshte larguar celesi publik '" + publicKeyFile + "'");
                }
                else if (File.Exists(publicKeyFile)) {
                    rsa.PersistKeyInCsp = false;
                    if (File.Exists(publicKeyFile))
                        File.Delete(publicKeyFile);
                    Console.WriteLine("Eshte larguar celesi publik '" + publicKeyFile + "'");

                }
                else if (File.Exists(privateKeyFile)) {
                    rsa.PersistKeyInCsp = false;
                    if (File.Exists(privateKeyFile))
                        File.Delete(privateKeyFile);
                    Console.WriteLine("Eshte larguar celesi privat '" + privateKeyFile + "'");
                }
                else
                    Console.WriteLine("Gabim: Celesi '" + name + "' nuk ekziston.");

            }
        }
         public static void ExportKey(string public_private, string name)
        {
            string publicKeyFile = "keys/" + name + ".pub.xml";
            string privateKeyFile = "keys/" + name + ".xml";
             using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                if (public_private.Equals("public"))
                {
                    if (File.Exists(publicKeyFile))

                    {
                        RSAParameters objParameters = rsa.ExportParameters(false);                      
                        Console.WriteLine(rsa.ToXmlString(false));
                    }
             else
                        Console.WriteLine("Gabim:Celesi publik '"+name+"' nuk ekziston");
                   
                }
                             else if (public_private.Equals("private"))
                {
                    if (File.Exists(privateKeyFile))

                    {
                        RSAParameters objParameters = rsa.ExportParameters(true);                      
                        Console.WriteLine(rsa.ToXmlString(true));
                    }
                    else
                        Console.WriteLine("Gabim:Celesi privat '" + name + "' nuk ekziston");


                }
            }
        }
             
        public static void ExportKey1(string public_private, string name, string file)
        {
            string publicKeyFile = "keys/" + name + ".pub.xml";
            string privateKeyFile = "keys/" + name + ".xml";
            CspParameters csp = new CspParameters();
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                if (public_private.Equals("public"))
                {
                    if (File.Exists(publicKeyFile))

                    {
                        if (!(File.Exists(publicKeyFile)))

                            File.Create(file);
                        using (StreamReader reader = new StreamReader(publicKeyFile))
                        {
                            csp.KeyContainerName = file;
                            string filetext = reader.ReadToEnd();
                            using (StreamWriter writer = new StreamWriter(file))
                            {

                                writer.Write(filetext);
                            }
                            rsa.PersistKeyInCsp = true;
                            Console.WriteLine("Celesi publik u ruajt ne fajllin '" + file + "'");
                        }
                    }

                    else
                        Console.WriteLine("Gabim: Celesi publik '" + name + "' nuk ekziston");
                }

                else if (public_private.Equals("private"))
                {
                    if (File.Exists(privateKeyFile))
                    {
                        if (!(File.Exists(privateKeyFile)))
                            File.Create(file);
                        using (StreamReader reader = new StreamReader(privateKeyFile)) {
                            csp.KeyContainerName = file;
                            string filetext = reader.ReadToEnd();
                            using (StreamWriter writer = new StreamWriter(file))
                            {

                                writer.Write(filetext);
                            }
                            rsa.PersistKeyInCsp = true;
                            Console.WriteLine("Celesi privat u ruajt ne fajllin '" + file + "'");
                        }
                    
                    }
                    else
                        Console.WriteLine("Gabim:Celesi privat '" + name + "' nuk ekziston");
                }
            }


        } 
        
        public static void ImportKey(string name, string path)
        {


            string publicKeyFile = "keys/" + name + ".pub.xml";
            string privateKeyFile = "keys/" + name + ".xml";
            CspParameters csp = new CspParameters();
            using (var rsa = new RSACryptoServiceProvider(csp))
            {
                if (File.Exists(path))
                {
                    if (path.EndsWith(".png"))
                    {
                        Console.WriteLine("Gabim: Fajlli i dhene nuk eshte celes valid.");
                    }
                    else if (File.Exists(publicKeyFile))
                        Console.WriteLine("Celesi publik vecse ekziston!");
                    else

               

                    using (StreamReader reader = new StreamReader(path))

                    {

                        csp.KeyContainerName = publicKeyFile;

                        string publicKeyText = reader.ReadToEnd();
                        if (!Text.Contains("InverseQ"))
                            {
                                using (StreamWriter writer = new StreamWriter(publicKeyFile))
                                {
                                    writer.Write(Text);
                                }
                        

                        rsa.PersistKeyInCsp = true;
                        Console.WriteLine("Celesi publik u ruajt ne fajllin '" + publicKeyFile + "'.");    

                    }
                   
                }

               
                else Console.WriteLine("...");

            }


        }
     

    }
}
