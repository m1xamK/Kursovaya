using System.Text;

namespace Manager
{
	/// <summary>
	/// класс реализующий к ичную систему счисления
	/// </summary>
	public class ScaleOfNotation
	{
		private string _alphabet; //алвавит системы счисления
		private StringBuilder _value;// зничение

		public ScaleOfNotation(string alphabet, string number = "0")
		{
			_alphabet = alphabet;
			_value = new StringBuilder(number);
		}

		//реализация опрератора инкремента
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

	// класс нужный для сдвига числа на n позиций
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
