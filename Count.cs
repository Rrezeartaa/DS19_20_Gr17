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
            string str;
            int i, wrd, l;
            //numerimi i fjalive 

            Console.Write("\n\nJepe nje fjali :\n");
            Console.Write("------------------------------------------------------\n");
            Console.Write("Input the string : ");
            str = Console.ReadLine();

            l = 0;
            wrd = 1;

          
            while (l <= str.Length - 1)
            {
               
                if (str[l] == ' ' || str[l] == '\n' || str[l] == '\t')
                {
                    wrd++;
                }

                l++;
            }

            Console.Write("Numri i fjalive te dhena : {0}\n", wrd);
        }
    }
}
