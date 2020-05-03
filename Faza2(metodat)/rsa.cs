using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections;
using System.Security.Policy;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Numerics;
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
                        using (StreamReader reader = new StreamReader(publicKeyFile))
                        {
                            //csp.KeyContainerName = file;
                            string filetext = reader.ReadToEnd();
                            Console.WriteLine(filetext);
                        }
                           // publicKey = rsa.ExportParameters(false);
                        
                    }
                  else
                        Console.WriteLine("Gabim:Celesi publik '"+name+"' nuk ekziston");
                   
                }
                             else if (public_private.Equals("private"))
                {
                    if (File.Exists(privateKeyFile))

                    {
                        using (StreamReader reader = new StreamReader(privateKeyFile))
                        {
                            //csp.KeyContainerName = file;
                            string filetext = reader.ReadToEnd();
                            Console.WriteLine(filetext);
                        }
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

                    using (StreamReader reader = new StreamReader(path))
                    {
                        csp.KeyContainerName = privateKeyFile;

                        string Text = reader.ReadToEnd(); 
                        if (Text.Contains("InverseQ"))
                    {
                    using (StreamWriter writer = new StreamWriter(publicKeyFile))
                            {
                                
                                writer.Write("?");

                             }
                     using (StreamWriter writer = new StreamWriter(privateKeyFile))
                            {
                                writer.Write(Text);
                            }
                            rsa.PersistKeyInCsp = true;
                               Console.WriteLine("Celesi publik u ruajt ne fajllin '" + publicKeyFile + "'");
                            Console.WriteLine("Celesi privat u ruajt ne fajllin '" + privateKeyFile + "'");
                        }
                    }
                }
                 // else if (!File.Exists(path))
               // {
                //    Console.WriteLine("Fajlli qe doni te importoni nuk ekziston!");
                //}

                else if (path.StartsWith("http://") || path.StartsWith("https://"))
                {

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                    request.Method = "Post";
                    request.KeepAlive = true;
                    request.ContentType = "appication/json";
                    request.Headers.Add("ContentType", "appication/json");
                    //  request.ContentType = "application/x-www-form-urlencoded";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string myResponse = "";
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        myResponse = sr.ReadToEnd();

                        if (!myResponse.Contains("InverseQ"))
                        {
                            using (StreamWriter writer = new StreamWriter(publicKeyFile))

                            {
                                writer.Write(myResponse);
                            }

                            Console.WriteLine("Celesi publik u ruajt ne fajllin '" + publicKeyFile + "'.");
                             }
                        else if (myResponse.Contains("InverseQ"))
                        {


                            using (StreamWriter writer = new StreamWriter(privateKeyFile))

                            {
                                writer.Write(myResponse);
                            }
                          //  Console.WriteLine("Celesi publik u ruajt ne fajllin '" + publicKeyFile + "'.");
                            Console.WriteLine("Celesi privat u ruajt ne fajllin '" + privateKeyFile + "'.");
                        }
                    }
                }
                
            }
        }
                       
               

         
        public static void encrypt(string name,string message)
        {

            string publicKeyFile = "keys/" + name + ".pub.xml";

            if (File.Exists(publicKeyFile)){
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(name);
            var user = System.Convert.ToBase64String(plainTextBytes);

            DES DESalg = DES.Create();
            byte[] keyb = new byte[8];
            byte[] ivb = new byte[8];
            keyb = DESalg.Key;

            ivb = DESalg.IV;

            string KEY = Convert.ToBase64String(keyb);
            string IV = Convert.ToBase64String(ivb);
            
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            var pubkey = File.ReadAllText(publicKeyFile);
            rsa.FromXmlString(pubkey);
            byte[] keybytes = Convert.FromBase64String(KEY);
            string rsakey = Convert.ToBase64String(rsa.Encrypt(keybytes, true));
             string encryptedText = string.Empty;
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
            cryptoProvider.CreateEncryptor(keybytes, ivb), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(message);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            encryptedText=Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            string result = test + "." + IV + "." + rsakey + "." + encryptedText;
             Console.Write(result);
            }
            else
                Console.WriteLine("Gabim: Celesi publik '" + name + "' nuk ekziston");
            
        }
        public static void decrypt(string encryptedtext)
        {

            byte[] decemri;
            String emri;
            String[] a = encryptedtext.Split('.');
            //System.Convert.FromBase64String
            decemri = System.Convert.FromBase64String(a[0]);
            emri = System.Text.ASCIIEncoding.ASCII.GetString(decemri);
             string privateKeyFile = "keys/" + emri + ".xml";
              if (File.Exists(privateKeyFile))
            {

                byte[] decIV;

                byte[] decEncryptedKey;

                byte[] decEncryptedMsg;

                decIV = System.Convert.FromBase64String(a[1]);

                decEncryptedKey = System.Convert.FromBase64String(a[2]);

                decEncryptedMsg = System.Convert.FromBase64String(a[3]);

                byte[] DesKey = RSAdecrypt(decEncryptedKey, privateKeyFile, emri);

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream
                        (Convert.FromBase64String(a[3]));
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    cryptoProvider.CreateDecryptor(DesKey, decIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);
                string result = reader.ReadToEnd();

                String M = result;

                Console.WriteLine("Marresi: " + emri);
                Console.WriteLine("Mesazhi: " + M);
            }
              else
                Console.WriteLine("Gabim: Celesi privat " + privateKeyFile + " nuk ekziston");

        }
        public static byte[] RSAdecrypt(byte[] DESKey, string privatei, string emri)
        {
            byte[] decrypted;

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                string privateFile = "keys/" + emri + ".xml";
                
                    privatei = File.ReadAllText(privateFile);
                    rsa.FromXmlString(privatei);
                    decrypted = rsa.Decrypt(DESKey, true);
                    return decrypted;
                
                
            }
        } 

          

        }

    }
}
