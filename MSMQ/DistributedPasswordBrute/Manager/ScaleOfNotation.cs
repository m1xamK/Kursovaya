using System.Text;

namespace Manager
{
	public class ScaleOfNotation
	{
		private string _alphabet;
		private StringBuilder _value;

		public ScaleOfNotation(string alphabet, string number = "0")
		{
			_alphabet = alphabet;
			_value = new StringBuilder(number);
		}

		public static ScaleOfNotation operator++(ScaleOfNotation number)
		{
			for (int i = 0; i < number._value.Length - 1; ++i)
			{
				int posOfCurentSymbol = number._alphabet.IndexOf(number._value[i]);
				number._value[i] = number._alphabet[++posOfCurentSymbol%number._alphabet.Length];

				if (number._value[i] != number._alphabet[0])
					break;
			}
		}
	}

	public static class Notation
	{
		public static ScaleOfNotation Shift(this ScaleOfNotation number, int count)
		{
			for (int i = 0; i < count; ++i)
				number++;

			return number;
		}
	}
}
