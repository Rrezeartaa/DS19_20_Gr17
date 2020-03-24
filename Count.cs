using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace ds
{
    class count
    {
        public static int countlines(String text)
        {
            int lines = 1;
            int start = 0;
            
            while ((start = text.IndexOf('\n', start)) != -1)
            {
                lines++;
                start++;
            }
            
            return lines;
  
        }

        
        public static int countwords(String text)
        {
            int l = 0;
            int word = 1;

            while (l <= text.Length - 1)
            {
                if (text[l] == ' ' || text[l] == '\n' || text[l] == '\t')
                {
                    word++;
                }

                l++;
            }
            return word;
        }
    }
}
