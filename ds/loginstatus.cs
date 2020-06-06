using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using java.security;
using System.Reflection.Metadata;
using Aspose.Words;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Data;
using System.Security.Cryptography;
using System.Xml;
using Newtonsoft.Json;
using System.Configuration;
namespace ds
{
    class loginstatus
    {
        private const string _company = "Shembull";
        public static void loginGenerateToken(string name)
        {
            Console.WriteLine("Jepni fjalekalimin:");
            string password = "";
            do
            {
                ConsoleKeyInfo keyy = Console.ReadKey(true);
                if (keyy.Key != ConsoleKey.Backspace && keyy.Key != ConsoleKey.Enter)
                {
                    password += keyy.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (keyy.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (keyy.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            Console.WriteLine();
            
            string ConnectionString = @"Data Source=RREZEARTA-DESKT\SQLEXPRESS;Initial Catalog=celesat;Integrated Security=True;Pooling=False";
            SqlConnection objConn = new SqlConnection(ConnectionString);
             //objConn.Open();
            string command = "select * from celesi where emri='" + name + "'";
            SqlCommand objCommand = new SqlCommand(command, objConn);

            DataSet ds = new DataSet();
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            try
            {
                objAdapter.Fill(ds);
                string hash = "";
                string salt_encoded = "";
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    salt_encoded = ds.Tables[0].Rows[0]["salt"].ToString();
                    hash = ds.Tables[0].Rows[0]["encodedhash"].ToString(); 
                }
                 
                    MessageDigest messageDigest = MessageDigest.getInstance("SHA-512");
                    messageDigest.update(Encoding.UTF8.GetBytes(salt_encoded));
                    byte[] bytes = messageDigest.digest(Encoding.UTF8.GetBytes(password));
                    String encodedHash = System.Convert.ToBase64String(bytes);

                if (encodedHash.Equals(hash))
                {
                    RsaSecurityKey privateKeyi;
                    RSA privateRsa = RSA.Create();                        
                    string privatekke = "";
                    string key = "keys/" + name + ".xml";
                    using (StreamReader reader = new StreamReader(key))
                    {
                      privatekke = reader.ReadToEnd();
                    }
                   privateRsa.FromXML(privatekke);
                   privateKeyi = new RsaSecurityKey(privateRsa);
                   var credentials = new SigningCredentials(privateKeyi, SecurityAlgorithms.RsaSha256);

                        var header = new JwtHeader(credentials);
                        var payload = new JwtPayload(
                            name,
                            _company,
                            new List<Claim>()
                            {
                             new Claim("sub", name)
                            },
                            DateTime.UtcNow,
                            DateTime.UtcNow.AddMinutes(20));

                        var secToken = new JwtSecurityToken(header, payload);
                         var handler = new JwtSecurityTokenHandler();
                        var tokenString = handler.WriteToken(secToken);
                    
                    Console.WriteLine(tokenString);
                }
                else if (!encodedHash.Equals(hash))
                    Console.WriteLine("Gabim:Shfrytezuesi ose fjalekalimi i gabuar.");               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ka ndodhur nje gabim: " + ex.Message);
                objConn.Close(); 
         }
                
        }
        public static void statusToken(string token)
{
             string publickey = "";
    //string name = "";
    string key = "keys/haliti.pub.xml";
    using (StreamReader reader = new StreamReader(key))
    {
        publickey = reader.ReadToEnd();
    }
    string[] tokenParts = token.Split('.');
    String unsignedToken = token[0] + "." + token[1] + ".";
    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    rsa.ImportParameters(
      new RSAParameters()
                      {
                  Modulus = Encoding.ASCII.GetBytes("0WG3GMAH0nRTHKgSnrAjNNuBLxxikLG0XyY73OfuHn6JcRuRUyh1MvS0Y5BxX9FcC32l6Er8UcHOLX/WraNZclBexF1TnuUarRB82alkB0UMxBrtP+qgfOrsUek/orpDwdXkANW6oGB0aTZ5tdC7r/MI7nbCCwcQU7tQ2BtIdVHb/Q9QfFMtMO5sbf5y8GhvvcyNbJ6Mb0pyeFGZNoC2ISLqQmGxzNJmFSofbUusq3P5VmzjFprvfoPcklCmTi8rthR69lrWlc/RDWAhJPOodZqBVu3keV0UPD1hxY4mFDj8NvSvKo+/Yq1FCX/xrO0Of/lBDlhtY8rJebeRxDcCpQ=="),
                  Exponent = Encoding.ASCII.GetBytes("AQAB")
              });
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(tokenParts[0] + '.' + tokenParts[1]));

            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm("SHA256");
            byte[] h = Encoding.ASCII.GetBytes(tokenParts[2]);
            if (rsaDeformatter.VerifySignature(hash, Encoding.ASCII.GetBytes(tokenParts[2])))
                Console.WriteLine("Po");
            else
                Console.WriteLine("Jo");
        }

     }
 }
 
