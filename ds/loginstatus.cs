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
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Security;
using System.Diagnostics;
using CryptoSysPKI;
using System.Xml;
using Newtonsoft.Json;
using System.Configuration;

namespace ds
{
    class loginstatus
    {
        private const string _company = "Example";
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
                            DateTime.Now,
                            DateTime.Now.AddMinutes(20));

                        var secToken = new JwtSecurityToken(header, payload);
                         var handler = new JwtSecurityTokenHandler();
                        var tokenString = handler.WriteToken(secToken);
                    
                    Console.WriteLine("Token: "+tokenString);
                    
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
        
        public static void statusToken(string tokeni)
        {
            string[] tokenArr = tokeni.Split('.');
            byte[] decpay;
            decpay= System.Convert.FromBase64String(tokenArr[1]);
            string pay= System.Text.Encoding.UTF8.GetString(decpay);
            string[] name = pay.Split(',');
            string ds = name[0];
            string[] dsd = ds.Split(':');
            string emri = dsd[1];
            emri = emri.Replace("\"","");

            string koha= name[2];
            string[] dsdi = koha.Split(':');
            string timee = dsdi[1];
            timee = timee.Replace("\"", "");

            var startdate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(timee)*1000+7200000).UtcDateTime;
            
            RsaSecurityKey publicKeyii;
            RSA rsa= RSA.Create();
            string pubkke = "";
            string key = "keys/"+emri+".pub.xml";
            using (StreamReader reader = new StreamReader(key))
            {
                pubkke = reader.ReadToEnd();
            }
            rsa.FromXML(pubkke);
            publicKeyii = new RsaSecurityKey(rsa);
            
            var prms = new TokenValidationParameters()
            {
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                IssuerSigningKey = publicKeyii,
                ValidateAudience = false,
                ValidIssuer = emri
            };
            var handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken(tokeni, prms, out SecurityToken token);
                var tok = handler.ReadJwtToken(tokeni);
                Console.WriteLine("User:" + emri);
                Console.WriteLine("Valid:Po");
                Console.WriteLine("Skadimi:"+startdate);
            }
            catch
            {
                Console.WriteLine("User:" + emri);
                Console.WriteLine("Valid:Jo");
                Console.WriteLine("Skadimi:" + startdate);
            }

        }       
    }
}
