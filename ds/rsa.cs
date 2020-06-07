using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Collections;
using System.Security.Policy;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Numerics;
using System.Data.SqlClient;
using Org.BouncyCastle.Security;
using java.security;
using System.Buffers.Text;

namespace ds
{
    class rsa
    {
        private static RSAParameters publicKey;
        private static RSAParameters privateKey;
        public enum KeySizes { 
           SIZE_512=512,
           SIZE_1024=1024,
           SIZE_2048=2048,
           SIZE_952 = 952,
           SIZE_1369 = 1369
        };
        public static void GenKey(string name)
        {
            string strRegexi = @"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{8,}$";
            Regex ree = new Regex(strRegexi);
            Console.WriteLine("Jepni fjalekalimin:");
            string password = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            Console.WriteLine();
            Console.WriteLine("Perserit fjalekalimin:");
            string confpassword = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    confpassword += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && confpassword.Length > 0)
                    {
                        confpassword = confpassword.Substring(0, (confpassword.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            Console.WriteLine();
            
            string publicKeyFile = "keys/" + name + ".pub.xml";
            string privateKeyFile= "keys/" + name + ".xml";
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                if (!File.Exists(publicKeyFile) && !File.Exists(privateKeyFile))
                {
                     if (password != confpassword)
                    {
                        Console.WriteLine("Fjalekalimet nuk perputhen!");
                    }
                    else if(!ree.IsMatch(password))
                    {
                        Console.WriteLine("Gabim: Fjalekalimi duhet te permbaje se paku nje numer ose simbol.");
                    }
                    else
                    {
                    string publicKey = rsa.ToXmlString(false);
                    File.WriteAllText(publicKeyFile, publicKey);
                    string privateKey = rsa.ToXmlString(true);
                    File.WriteAllText(privateKeyFile, privateKey);
                    Org.BouncyCastle.Security.SecureRandom secure = new Org.BouncyCastle.Security.SecureRandom();
                    byte[] salt = new byte[24];
                    secure.NextBytes(salt);
                    String salt_encoded = System.Convert.ToBase64String(salt);

                    MessageDigest messageDigest = MessageDigest.getInstance("SHA-512");
                    messageDigest.update(Encoding.UTF8.GetBytes(salt_encoded));
                    byte[] bytes = messageDigest.digest(Encoding.UTF8.GetBytes(password));
                    String encodedHash = System.Convert.ToBase64String(bytes);
                    string ConnectionString = @"Data Source=RREZEARTA-DESKT\SQLEXPRESS;Initial Catalog=celesat;Integrated Security=True;Pooling=False"; 
                     SqlConnection objConn = new SqlConnection(ConnectionString);
                     string command = "Insert into celesi (emri,salt,encodedhash) values('" + name+ "','" + salt_encoded + "','" + encodedHash + "')";
                     SqlCommand objCommand = new SqlCommand(command, objConn);
                     try
                      {
                          objConn.Open();
                          int AffectedRows = objCommand.ExecuteNonQuery();
                          if (AffectedRows == 1)
                              Console.WriteLine("Eshte krijuar shfrytezuesi '" + name + "'");
                          else
                              Console.WriteLine("Nuk eshte krijuar shfrytezuesi '" + name + "'");

                       }
                      catch (Exception ex)
                       {
                          Console.WriteLine("Ka ndodhur nje gabim: " + ex.Message);
                          objConn.Close();
                            
                       }
                    Console.WriteLine("Eshte krijuar celesi privat '" + privateKeyFile + "'");
                    Console.WriteLine("Eshte krijuar celesi publik '" + publicKeyFile + "'");
                   }
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
                     File.Delete(privateKeyFile);
                     File.Delete(publicKeyFile);
                    string ConnectionString = @"Data Source=RREZEARTA-DESKT\SQLEXPRESS;Initial Catalog=celesat;Integrated Security=True;Pooling=False";
                    SqlConnection objConn = new SqlConnection(ConnectionString);
                    string command = "Delete from celesi where emri='"+name+"'";
                    SqlCommand objCommand = new SqlCommand(command, objConn);
                    try
                    {
                        objConn.Open();
                        int AffectedRows = objCommand.ExecuteNonQuery();
                        if (AffectedRows == 1)
                            Console.WriteLine("Eshte larguar shfrytezuesi '" + name + "'");
                        else
                            Console.WriteLine("Nuk eshte larguar shfrytezuesi '" + name + "'");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ka ndodhur nje gabim: " + ex.Message);
                        objConn.Close();

                    }
                    Console.WriteLine("Eshte larguar celesi privat '" + privateKeyFile + "'");
                    Console.WriteLine("Eshte larguar celesi publik '" + publicKeyFile + "'");
                }
                else if (File.Exists(publicKeyFile)) {
                        File.Delete(publicKeyFile);
                    Console.WriteLine("Eshte larguar celesi publik '" + publicKeyFile + "'");
                }
                else if (File.Exists(privateKeyFile)) {
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
                            string filetext = reader.ReadToEnd();
                            Console.WriteLine(filetext);
                        }                        
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
                            string filetext = reader.ReadToEnd();
                            Console.WriteLine(filetext);
                        }
                    }
                    else
                        Console.WriteLine("Gabim:Celesi privat '" + name + "' nuk ekziston");
                }
                 else
                    Console.WriteLine("Specifikoni cilin celes doni ta eksportoni (publik apo privat)!");
            }
        }
             
        public static void ExportKey1(string public_private, string name, string file)
        {
            string publicKeyFile = "keys/" + name + ".pub.xml";
            string privateKeyFile = "keys/" + name + ".xml";
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                if (public_private.Equals("public"))
                {
                    if (File.Exists(publicKeyFile))
                    {
                        if (!(File.Exists(publicKeyFile)))
                            File.Create(file);
                        else if (!file.Contains(".xml") || !file.Contains("."))
                        {
                            Console.WriteLine("Nuk keni dhene format te duhur te fajllit!");
                        }
                        else{
                        using (StreamReader reader = new StreamReader(publicKeyFile))
                        {
                            string filetext = reader.ReadToEnd();
                            using (StreamWriter writer = new StreamWriter(file))
                            {
                                writer.Write(filetext);
                            }
                            Console.WriteLine("Celesi publik u ruajt ne fajllin '" + file + "'");
                        }
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
                        else if (!file.Contains(".xml") || !file.Contains("."))
                        {
                            Console.WriteLine("Nuk keni dhene format te duhur te fajllit!");
                        }
                        else{
                        using (StreamReader reader = new StreamReader(privateKeyFile)) {
                            string filetext = reader.ReadToEnd();
                            using (StreamWriter writer = new StreamWriter(file))
                            {
                                writer.Write(filetext);
                            }
                            Console.WriteLine("Celesi privat u ruajt ne fajllin '" + file + "'");
                        }                    
                      }
                    }
                    else
                        Console.WriteLine("Gabim:Celesi privat '" + name + "' nuk ekziston");
                }
                else
                    Console.WriteLine("Specifikoni cilin celes doni ta eksportoni (publik apo privat)!");
            }
        } 
        
        public static void ImportKey(string name, string path)
        {
            string publicKeyFile = "keys/" + name + ".pub.xml";
            string privateKeyFile = "keys/" + name + ".xml";
            RSACryptoServiceProvider obj = new RSACryptoServiceProvider();
                if (File.Exists(path))
                {
                    if (!path.Contains(".xml") || !path.Contains("."))
                {
                    Console.WriteLine("Gabim: Fajlli i dhene nuk eshte celes valid.");
                }
                  else{
                      if (File.Exists(publicKeyFile) || File.Exists(privateKeyFile))
                    { 
                        Console.WriteLine("Gabim: Celesi '" + name + "' ekziston paraprakisht.");
                    }
                    else{
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string Text = reader.ReadToEnd();
                        if (!Text.Contains("<InverseQ>"))
                            {
                                using (StreamWriter writer = new StreamWriter(publicKeyFile))
                                {
                                    writer.Write(Text);
                                }
                        Console.WriteLine("Celesi publik u ruajt ne fajllin '" + publicKeyFile + "'."); 
                         } 
                        }

                    using (StreamReader reader = new StreamReader(path))
                    {
                          string Text = reader.ReadToEnd();
                            if (Text.Contains("<InverseQ>"))
                            {
                                File.WriteAllText(privateKeyFile, Text);
                                string param = obj.ToXmlString(false);
                                StreamWriter sw = new StreamWriter(publicKeyFile);
                                sw.Write(param);
                                sw.Close();
                                Console.WriteLine("Celesi privat u ruajt ne fajllin '" + privateKeyFile + "'");
                                Console.WriteLine("Celesi publik u ruajt ne fajllin '" + publicKeyFile + "'");
                            }
                         }
                      }
                   }
                }
                
                else if (path.StartsWith("http://") || path.StartsWith("https://"))
                {
                    if (File.Exists(publicKeyFile) || File.Exists(privateKeyFile))
                    { 
                      Console.WriteLine("Gabim: Celesi '" + name + "' ekziston paraprakisht.");
                    }

                    else{
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                    request.Method = "Get";
                    request.KeepAlive = true;
                    request.ContentType = "appication/json";
                    request.Headers.Add("ContentType", "appication/json");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string myResponse = "";
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        myResponse = sr.ReadToEnd();
                        if (!myResponse.Contains("<InverseQ>")&& myResponse.Contains("Modulus"))
                        {
                            using (StreamWriter writer = new StreamWriter(publicKeyFile))
                            {
                                writer.Write(myResponse);
                            }
                            Console.WriteLine("Celesi publik u ruajt ne fajllin '" + publicKeyFile + "'.");
                         }
                        else if (myResponse.Contains("<InverseQ>"))
                        {
                            File.WriteAllText(privateKeyFile, myResponse);
                            string param = obj.ToXmlString(false);
                            StreamWriter sw = new StreamWriter(publicKeyFile);
                            sw.Write(param);
                            sw.Close();
                            Console.WriteLine("Celesi privat u ruajt ne fajllin " + privateKeyFile);
                            Console.WriteLine("Celesi publik u ruajt ne fajllin " + publicKeyFile);
                      }
                        else
                            Console.WriteLine("Linku i dhene nuk permban ndonje celes publik apo privat!");
                    }   
                 }
              }
                 else if (!File.Exists(path))
                {
                    Console.WriteLine("Ky fajll nuk ekziston ose keni dhene pathin gabim!");
                }
            }
       
         public static void encrypt(string name, string message)
        {
            string publicKeyFile = "keys/" + name + ".pub.xml";
            if (File.Exists(publicKeyFile))
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(name);
                var test = System.Convert.ToBase64String(plainTextBytes);

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
                encryptedText = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                string result = test + "." + IV + "." + rsakey + "." + encryptedText;
                
                Console.Write(result); 
            }
            else
                Console.WriteLine("Gabim: Celesi publik '" + name + "' nuk ekziston");
        }
        
        public static void encrypt(string name,string message,string file)
        {
            string publicKeyFile = "keys/" + name + ".pub.xml";
            if (File.Exists(publicKeyFile))
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(name);
                var test = System.Convert.ToBase64String(plainTextBytes);

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
            if (!file.Contains(".txt"))
                {
                    Console.WriteLine("Mesazhi i enkriptuar mund te ruhet vetem ne tekst file!");
                }
                else
                {
                    File.WriteAllText(file, result);
                    Console.WriteLine("Mesazhi i enkriptuar u ruajt ne fajllin '" + file + "'.");
                }              
            }
            else
                Console.WriteLine("Gabim: Celesi publik '" + name + "' nuk ekziston");
        }
       public static void decrypt(string encryptedtext)
        {
            byte[] decemri;
            byte[] decIV;
            byte[] decEncryptedKey;
            byte[] decEncryptedMsg;
            byte[] decSender;
            byte[] verify;
            String emri;
            try
            {
                if (encryptedtext.EndsWith(".txt"))
                {
                    if (!File.Exists(encryptedtext))
                    {
                        Console.WriteLine("Ky fajll nuk ekziston!");
                    }
                    else
                    {
                        using (StreamReader readeri = new StreamReader(encryptedtext))
                        {
                            string Texti = readeri.ReadToEnd();
                            String[] b = Texti.Split('.');
                            decemri = System.Convert.FromBase64String(b[0]);
                            emri = System.Text.ASCIIEncoding.ASCII.GetString(decemri);
                            decIV = System.Convert.FromBase64String(b[1]);
                            decEncryptedKey = System.Convert.FromBase64String(b[2]);
                            decEncryptedMsg = System.Convert.FromBase64String(b[3]);




                            string privateKeyFile = "keys/" + emri + ".xml";
                            if (File.Exists(privateKeyFile))
                            {
                                byte[] DesKey = RSAdecrypt(decEncryptedKey, privateKeyFile, emri);

                                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                                MemoryStream memoryStream = new MemoryStream
                                        (Convert.FromBase64String(b[3]));
                                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                    cryptoProvider.CreateDecryptor(DesKey, decIV), CryptoStreamMode.Read);
                                StreamReader reader = new StreamReader(cryptoStream);
                                string result = reader.ReadToEnd();

                                String M = result;

                               

                                if (b.Length > 4)
                                {
                                    decSender = System.Convert.FromBase64String(b[4]);
                                    String sender = System.Text.ASCIIEncoding.ASCII.GetString(decSender);
                                    verify = System.Convert.FromBase64String(b[5]);
                                    RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
                                    string path = "keys/" + sender + ".pub.xml";

                                    string strXmlParameters = "";
                                    StreamReader sr = new StreamReader(path);
                                    strXmlParameters = sr.ReadToEnd();
                                    sr.Close();

                                    objRSA.FromXmlString(strXmlParameters);

                                    byte[] byteSignedValue = verify;
                                    byte[] bytePlaintexti = decEncryptedMsg;

                                    bool Verified = objRSA.VerifyData(bytePlaintexti, new SHA1CryptoServiceProvider(), byteSignedValue);
                                    string v = "";
                                    if (Verified)
                                        v = "jovalid";
                                    else if (!File.Exists(path))
                                        v = " mungon celesi publik '" + sender + "'";
                                    else
                                        v = "valid";

                                    Console.WriteLine("Marresi: " + emri);
                                    Console.WriteLine("Mesazhi: " + M);
                                    Console.WriteLine("Derguesi: " + sender);
                                    Console.WriteLine("Nenshkrimi: " + v);

                                }
                                else
                                {
                                    Console.WriteLine("Marresi: " + emri);
                                    Console.WriteLine("Mesazhi: " + M);
                                }
                            }
                            else
                                Console.WriteLine("Gabim: Celesi privat " + privateKeyFile + " nuk ekziston");
                        }
                    }
                }
                else if (!encryptedtext.Contains(".txt"))
                {
                    String[] a = encryptedtext.Split('.');
                    decemri = System.Convert.FromBase64String(a[0]);
                    emri = System.Text.ASCIIEncoding.ASCII.GetString(decemri);
                    decIV = System.Convert.FromBase64String(a[1]);
                    decEncryptedKey = System.Convert.FromBase64String(a[2]);
                    decEncryptedMsg = System.Convert.FromBase64String(a[3]);
                    
                    string privateKeyFile = "keys/" + emri + ".xml";
                    if (File.Exists(privateKeyFile))
                    {
                        byte[] DesKey = RSAdecrypt(decEncryptedKey, privateKeyFile, emri);

                        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                        MemoryStream memoryStream = new MemoryStream
                                (Convert.FromBase64String(a[3]));
                        CryptoStream cryptoStream = new CryptoStream(memoryStream,
                            cryptoProvider.CreateDecryptor(DesKey, decIV), CryptoStreamMode.Read);
                        StreamReader reader = new StreamReader(cryptoStream);
                        string result = reader.ReadToEnd();

                        String M = result;

                        if (a.Length > 4)
                        {
                            decSender = System.Convert.FromBase64String(a[4]);
                            String sender = System.Text.ASCIIEncoding.ASCII.GetString(decSender);
                            verify = System.Convert.FromBase64String(a[5]);
                            RSACryptoServiceProvider objRSAa = new RSACryptoServiceProvider();
                            string path = "keys/" + sender + ".pub.xml";

                            string strXmlParameters = "";
                            StreamReader sr = new StreamReader(path);
                            strXmlParameters = sr.ReadToEnd();
                            sr.Close();

                            objRSAa.FromXmlString(strXmlParameters);
                            byte[] byteSignedValue = System.Text.Encoding.UTF8.GetBytes(a[5]);
                            byte[] bytePlaintexti = System.Text.Encoding.UTF8.GetBytes(a[3]);

                            bool Verified = objRSAa.VerifyData(bytePlaintexti, new SHA1CryptoServiceProvider(), byteSignedValue);
                            string v = "";
                            if (Verified)
                                v = "jovalid";
                            else if (!File.Exists(path))
                                v = " mungon celesi publik '" + sender + "'";
                            else
                                v = "valid";

                            Console.WriteLine("Marresi: " + emri);
                            Console.WriteLine("Mesazhi: " + M);
                            Console.WriteLine("Derguesi: " + sender);
                            Console.WriteLine("Nenshkrimi: " + v);
                        }
                        else
                        {

                            Console.WriteLine("Marresi: " + emri);
                            Console.WriteLine("Mesazhi: " + M);
                        }
                    }
                    else
                        Console.WriteLine("Gabim: Celesi privat " + privateKeyFile + " nuk ekziston");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Mesazhi qe doni te dekriptoni nuk eshte ne formatin e duhur!");
            }
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
