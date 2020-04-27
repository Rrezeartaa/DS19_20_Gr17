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
        public static void ImportKey(string name, string path)
        {


            string publicKeyFile = "keys/" + name + ".pub.xml";
            string privateKeyFile = "keys/" + name + ".xml";
            CspParameters csp = new CspParameters();
            using (var rsa = new RSACryptoServiceProvider(csp))
            {
                FileInfo fi = new FileInfo(Path.Combine(path));
                 if (File.Exists(path))

                {

                    using (StreamReader reader = new StreamReader(path))

                    {

                        csp.KeyContainerName = publicKeyFile;

                        string publicKeyText = reader.ReadToEnd();
     

    }
}
