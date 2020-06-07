using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Numerics;
using System.Buffers.Text;
namespace ds
{
    class encdecwithtoken
    {
        public static void encryptii(string name, string message, string token)
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

                string[] tokenArr = token.Split('.');
                //string pay = tokenArr[1];
                byte[] decpay = System.Convert.FromBase64String(tokenArr[1]);
                string pay = System.Text.ASCIIEncoding.ASCII.GetString(decpay);
                string[] sender = pay.Split(',');
                string ds = sender[0];
                string[] dsd = ds.Split(':');
                string emrii = dsd[1];
                emrii = emrii.Replace("\"", "");

                string koha = sender[2];
                string[] dsdi = koha.Split(':');
                string timee = dsdi[1];
                timee = timee.Replace("\"", "");

                var startdate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(timee) * 1000).UtcDateTime;

                var plainTextBytess = System.Text.Encoding.UTF8.GetBytes(emrii);
                var testi = System.Convert.ToBase64String(plainTextBytess);

                RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
                SHA1CryptoServiceProvider hashFunksioni = new SHA1CryptoServiceProvider();

                string path = "keys/" + emrii + ".xml";
                string sign = Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptedText));
                string strXmlParametersi = "";
                StreamReader sr = new StreamReader(path);
                strXmlParametersi = sr.ReadToEnd();
                sr.Close();
                objRSA.FromXmlString(strXmlParametersi);
                byte[] byteSignedText = objRSA.SignData(Encoding.UTF8.GetBytes(sign), new SHA1CryptoServiceProvider());
                string signed = Convert.ToBase64String(byteSignedText);
                string result = "\n" + test + "." + IV + "." + rsakey + "." + encryptedText + "." + testi + "." + signed;
                if (startdate < DateTime.Now)
                {
                    Console.WriteLine("Tokeni nuk eshte valid");
                }
                else
                {
                    Console.Write(result);
                }
            }
            else
                Console.WriteLine("Gabim: Celesi publik '" + name + "' nuk ekziston");
        }
        public static void encrypttok(string name, string message, string file, string token)
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
        string[] tokenArr = token.Split('.');

        byte[] decpay = System.Convert.FromBase64String(tokenArr[1]);
        string pay = System.Text.ASCIIEncoding.ASCII.GetString(decpay);
        string[] sender = pay.Split(',');
        string ds = sender[0];
        string[] dsd = ds.Split(':');
        string emrii = dsd[1];
        emrii = emrii.Replace("\"", "");
                 
        string koha = sender[2];
        string[] dsdi = koha.Split(':');
        string timee = dsdi[1];
        timee = timee.Replace("\"", "");

        var startdate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(timee) * 1000).UtcDateTime;

        var plainTextBytess = System.Text.Encoding.UTF8.GetBytes(emrii);
        var testi = System.Convert.ToBase64String(plainTextBytess);
                 
        string path = "keys/" + emrii + ".xml";

        string sign = Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptedText));
        string strXmlParametersi = "";
        StreamReader sr = new StreamReader(path);
        strXmlParametersi = sr.ReadToEnd();
        sr.Close();
        objRSA.FromXmlString(strXmlParametersi);
        byte[] byteSignedText = objRSA.SignData(Encoding.UTF8.GetBytes(sign), new SHA1CryptoServiceProvider());
        string signed = Convert.ToBase64String(byteSignedText);
    }
}
