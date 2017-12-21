using System.Text;

namespace Manager
{
	public static class NextDiapason
	{
		public const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";	//алфавит, из которого может состоять хеш
		private const int MinLength = 4;	// Минимальная длина диапазона (она же шаг с которым происходит смещение слова)
		private const string FirstDiapasone = "1000";	// Конец первого диапазона

		/// <summary>
		/// Функция предназначена для нахождения смещения до которого считаем хеш 
		/// </summary>
		/// <param name="word">Слово, для которого находим смещение</param>
		/// <returns>Смещение до которого считаем хеш</returns>
		public static string Get(string word)
		{
			var tail = FirstDiapasone.Substring(1);

			var lastAlphabetSymbol = Alphabet[Alphabet.Length - 1];

			// Первое смещение
			if (word.Length < MinLength)
				return FirstDiapasone;

			StringBuilder temp = new StringBuilder(word.Substring(0, word.Length - MinLength + 1));                                                                                                                  
			for (int i = temp.Length - 1; i >= 0; --i)
			{
				if (temp[i] != lastAlphabetSymbol)
				{
					var index = Alphabet.IndexOf(temp[i]);
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
