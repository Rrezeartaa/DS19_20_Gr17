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
                // Backspace Should Not Work
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
                    //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                                     
                    //string key = "<RSAKeyValue><Modulus>rIXjR3RjnTSCM4hIyMtEKmg0G0LfEVNejJ8NXiPYjir4hxb/HLMClNJE7jBeCUaP0/PaJUO4mImonPo2hMYMijh8/nHyQQlbU3SZEF7v+5ALPlmPqvemNLVBN0KYE7RnA7GC104/Vm1I8ae6OnaU91idXWWwvJ/Jf7kxoTDJw08pwnr7wIEZR5Jj7R+tt/n7nhubxPqzagWfCHDxpHTTsV+pVqKuTJxWsoC56g03wfNLjXLUm1E+FjL1D1YUiG6Eeuw3DSElohdwotHX6rpLqmsf9pezh3tRRfyS65eELLvnB7kGqKih36fU6frZG7E9gg5ioFY6EbGNBAvn6JkAFQ==</Modulus><Exponent>AQAB</Exponent><P>zwpxt87OyUmFWm719kbmC3tW8GgKT3xFH0K+J2Bfxu5NhopXxFiWSaDpRa5+pUBb/oBPbRsZM2Psqpfzgwn8XsOAMP3v1SOJtltKG0n/ZQO/eSrH100837zGSHy2c+x12qVcGh8SsVzKXJhUyBYz71pvArVN8270VKsSXrXiW18=</P><Q>1VHaD6LbQ2p4Qo9l6VbQasmB5l9sN7XR6e1E33QmHa7EkBHU1jx+Ycgq4AIo2SxshE+t1TsWsEstthcDmibgqO9RkcNoxxVifSrqf8LJaQbhBov+xEY49UPhP7BIpCz1dsGuDi+sOrJeYDaNi45Wh9kiwTSPoh894FzBVBvMzQs=</Q><DP>rKwnP4cpi1LX9x5H4Igs4DKTxZjf0H7yHypI8Qo9lum0mprSrBy96tZa9xSa5zOQBef5ViOdlvClt3lXTFiNtHMUfMesuHQVLJNicPP8HsFLdcCqPvRZ6rfEHzxz5qa1fA8hi1+S9X1QAH3DZ8Sst9kScI3JX1eQSvUKMxc6m70=</DP><DQ>ZcDGcGDxkAXQWYeHSDeF8PovwVDREP3kpF3uVVxU9iGwVzx6NrriWggeE35UN8uN88sCE009NFiX9Fyj9jsHPO3zDcGVUCluMmBvPQQQCM4kNng+zSbl8nAvmK6g59ceO0iFmvS4hcMha2l6ORBrTB+SCXDl9qJKZKQ5/8HG/iM=</DQ><InverseQ>uVT429MKSGkpKoggarPlEot9SNhpI21fkdp4FkQUaxJ8HXD6IjeB/ITyA7W2Ivg8jKEwq2uLfCCN2jO/BrtQ5Q6eGe+/Jyv0cObgANIce14VllL99jG/YjDKgaP+1HQSJnv0Jt/FEyVQTDtrWx2vcWz+Z1V/GfAqPR1ji8VabCQ=</InverseQ><D>LI6Pun6m040iK2kBU0qcGk+7VWcZ8YGUo8DYVP3xChazBUC9No5NFl6QCuxW/RJKRzVtm743yL2U3KYKupPj3TbloVmQdZeTDKTKe13poRD8tmevITrApFBDvZ/nYv9us5d+8Vh2Jz4mXrMw97R0mMU8L0j6Ml0aT3BnDvhow5Yb495BJjOIPf1ou3W6nzHdGMaT1d3zBacK8FDDGlzdRmMaiCnCwd26MUZnqcNJ8GMSYUwiMNSCYqoIeDNOskES9yNN8dyjEJRoPiGl/LRH+6L7bD/2bcGU7Nsf4UXAc+SWIF/JcXNYz7ZjZWwZ3EIZaV/qJbpW0tdqw+tIWtieKQ==</D></RSAKeyValue>";
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

            var startdate = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(timee)*1000+7200000).UtcDateTime;///<------
            
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
                ValidIssuer = _company
            };
            var handler = new JwtSecurityTokenHandler();

            try
            { handler.ValidateToken(tokeni, prms, out SecurityToken token);
                var tok=handler.ReadJwtToken(tokeni);
                Console.WriteLine("User:" + emri);
                Console.WriteLine("Valid:Jo");
                Console.WriteLine("Skadimi:");
            }

            catch
            {
                Console.WriteLine("User:" + emri);
                Console.WriteLine("Valid:Po");
                Console.WriteLine("Skadimi:" + startdate);
            }

        }

        
    }
}
