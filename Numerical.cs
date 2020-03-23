using System;
using System.Text.RegularExpressions;
using System.Linq;

public class Numerical
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



/*public static string Encode(string input)
{

	return string.Join(" ", new Regex(@"[^A-Za-z]")
				.Replace(input, "")
				.Select(s => ((int)s % 32).ToString()));
}*/
}
