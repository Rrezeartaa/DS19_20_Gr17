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
        public static int countletters(String text)
        {
            int nriShkronjave = 0;

            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if ((text[i] >= 'a' && text[i] <= 'z'))
                {
                    nriShkronjave++;
                }
            }
            return nriShkronjave;
        }

        

        public static int countsymbols(String text)
        {
            int nrSimboleve = 0;

            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if ((text[i] >= 'a' && text[i] <= 'z') || text[i] == ' '
                        || (text[i] >= '0' && text[i] <= '9'))
                {
                    continue;
                }
                else
                {
                    nrSimboleve++;
                }
            }
            return nrSimboleve;
        }

        public static int countvowels(String text)
        {
            int nrZanoreve = 0;
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case 'a':
                        nrZanoreve++;
                        break;
                    case 'e':
                        nrZanoreve++;
                        break;
                    case 'o':
                        nrZanoreve++;
                        break;
                    case 'u':
                        nrZanoreve++;
                        break;
                    case 'i':
                        nrZanoreve++;
                        break;
                }
            }
            return nrZanoreve;
        }

        public static int countspaces(String text)
        {
            int numriHaps = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                    numriHaps++;
            }
            return numriHaps;
        }

        public static int countconsonants(String text)
        {
            return text.Length - countspaces(text) - countvowels(text) - countsymbols(text);
        }
public static int countsent(String strin)
        {
            char[] chars = strin.ToLower().ToCharArray();
            int sent=0;
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '.' || chars[i] == '?' || chars[i] == '!')
                {
                    sent++;
                }
            }
            return sent;
        }
    }
}

