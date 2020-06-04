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
namespace ds
{
    class loginstatus
    {
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

        
        }
     }
 }
