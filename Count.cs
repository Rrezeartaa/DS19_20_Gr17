using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    class Program
    {
        public static void Main()
        {
            string str1;
            
            int j, fja, e ;
            string str;
            int i, wrd, l;

            Console.Write("\n\nShkruaj fjal apo fjali :\n");
            Console.Write("------------------------------------------------------\n");
            Console.Write("Input the string : ");
            str = Console.ReadLine();

            l = 0;
            wrd = 0;

          
            while (l <= str.Length - 1)
            {
               
                if (str[l] == ' ' || str[l] == '\n' || str[l] == '\t')
                {
                    wrd++;
                }
                
                l++;
            }
            
            e = 0;
            fja = 1;

            while (e <= str.Length - 1)
            {

                if (str[e] == '.' || str[e] == '\n' || str[e] == '\t')
                {
                    fja++;
                }

                e++;
            }










            Console.Write("Numri i fjalive te dhena : {0}\n", wrd);
            Console.Write("Numri i fjalive te dhena : {0}\n", fja);
        }
    }
}
