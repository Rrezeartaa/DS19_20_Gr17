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
    }

}
