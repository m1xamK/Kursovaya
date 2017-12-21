using System.Text;

namespace Manager
{
	public static class NextWord
	{
		//алфавит, из которого может состоять хеш
		public const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// Функция предназначена для нахождения смещения до которого считаем хеш
		/// </summary>
		/// <param name="word">Слово, для которого находим смещение</param>
		/// <returns>Смещение до которого считаем хеш</returns>
		public static string Get(string word)
		{
			const int minLength = 4;
			const string tail = "000";

			char lastAlphabetSymbol = Alphabet[Alphabet.Length - 1];

			// Первое смещение
			if (word.Length < minLength)
				return "1" + tail;

			StringBuilder temp = new StringBuilder(word.Substring(0, word.Length - minLength + 1));

			for (int i = temp.Length - 1; i >= 0; --i)
			{
				if (temp[i] != lastAlphabetSymbol)
				{
					int index = Alphabet.IndexOf(temp[i]);
					temp[i] = Alphabet[++index];

					break;
				}

				temp[i] = '0';

				if (i == 0)
					temp = new StringBuilder("1").Append(temp);
			}

			return temp + tail;
		}
	}
}
