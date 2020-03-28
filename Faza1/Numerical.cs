using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ds
{
    class Numerical
    {
        public static string Encode(string input)
        {
            string result = "";

            Regex rgx = new Regex("[^A-Za-z]");
            string s = rgx.Replace(input, "").ToString();

            int index = 0;

            for (int i = 0; i < s.Length; i++)
            {
                index = (int)s[i] % 32;

                if (index > 0 && index < 27)
                {
                    result += index + " ";
                }
            }
            return result.Trim();
        }
        public static void Decode(String ciphertext)
        {
            String[] nr = ciphertext.Split(' ');
            foreach (String s in nr)
            {
                int a = int.Parse(s);
                char c = (char)(a + 'a' - 1);
                Console.Write(c);
            }
        }
         public static string separator(string separator, string input)
        {
            string s = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]))
                {
                    int nr = (int)input[i] - 'a' + 1;
                    s = s + Convert.ToString(nr) + " ";
                }
                else
                {
                    s = s + separator[0] + " ";
                }
            }
            return s;
        }
    }
}
