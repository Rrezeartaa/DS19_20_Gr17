using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ds
{
	public static class Cipher
	{
		public static void Main(string[] args)
		{
			if (args[0].Equals("rail-fence")){
			if (args[1].Equals("encrypt"))
			{
				string plaintext = args[3];
				string result = Encrypt(plaintext, int.Parse(args[2]));

				Console.WriteLine(result);
				Console.ReadKey();
			}
			else if (args[1].Equals("decrypt"))
			{
				string plaintext = args[3];
				string result = Decrypt(plaintext, int.Parse(args[2]));

				Console.WriteLine(result);
				Console.ReadKey();
			}
			}
			//else if(args[0].Equals("..."))
			//{
			
			//}
		}


		public static string Encrypt(string plaintext, int rails)
		{

			plaintext = plaintext.ToUpper();
			plaintext = Regex.Replace(plaintext, @"[^A-Z0-9]", string.Empty);

			var lines = new List<StringBuilder>();

			for (int i = 0; i < rails; i++)
				lines.Add(new StringBuilder());

			int currentLine = 0;
			int direction = 1;

			for (int i = 0; i < plaintext.Length; i++)
			{
				lines[currentLine].Append(plaintext[i]);

				if (currentLine == 0)
					direction = 1;
				else if (currentLine == rails - 1)
					direction = -1;

				currentLine += direction;
			}

			StringBuilder result = new StringBuilder();

			for (int i = 0; i < rails; i++)
				result.Append(lines[i].ToString());

			return result.ToString();
		}

		public static string Decrypt(string plaintext, int rails)
		{
			plaintext = plaintext.ToUpper();
			plaintext = Regex.Replace(plaintext, @"[^A-Z0-9]", string.Empty);

			var lines = new List<StringBuilder>();

			for (int i = 0; i < rails; i++)
				lines.Add(new StringBuilder());

			int[] linesLenght = Enumerable.Repeat(0, rails).ToArray();

			int currentLine = 0;
			int direction = 1;

			for (int i = 0; i < plaintext.Length; i++)
			{
				linesLenght[currentLine]++;

				if (currentLine == 0)
					direction = 1;
				else if (currentLine == rails - 1)
					direction = -1;

				currentLine += direction;
			}

			int currentChar = 0;

			for (int line = 0; line < rails; line++)
			{
				for (int c = 0; c < linesLenght[line]; c++)
				{
					lines[line].Append(plaintext[currentChar]);
					currentChar++;
				}
			}

			StringBuilder result = new StringBuilder();

			currentLine = 0;
			direction = 1;

			int[] currentReadLine = Enumerable.Repeat(0, rails).ToArray();

			for (int i = 0; i < plaintext.Length; i++)
			{

				result.Append(lines[currentLine][currentReadLine[currentLine]]);
				currentReadLine[currentLine]++;

				if (currentLine == 0)
					direction = 1;
				else if (currentLine == rails - 1)
					direction = -1;

				currentLine += direction;
			}

			return result.ToString();

		}

		//public static void show(string text, int rails)
		//{
			//for (int i = 0; i < text.Length; i++)
			//{
				
			//		Console.WriteLine(text[i]);
				
			//}
			
		//}
	}

}
