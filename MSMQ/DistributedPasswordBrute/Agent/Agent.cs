using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Agent
{
    public class Agent
    {
        private readonly char[] _alphabet =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        //    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
        //    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };
		
	    /// <summary>
		/// Ищет пароли чьи свертки лежат в hashSumList
	    /// </summary>
		/// /// <param name="start">Строка начиная с которой считаем хеш суммы</param>
		/// <param name="finish">Строка до которой считаем хеш суммы</param>
	    /// <param name="hashSumList">Лист нужных нам сверток, к которым требуется подобрать пароль </param>
	    /// <returns></returns>
	    public List<KeyValuePair<string, string>> SearchPassword(string start, string finish, List<string> hashSumList)
        {
            List<KeyValuePair<string, string>> passwdList = new List<KeyValuePair<string, string>>(); // лист из найденных пар {хеш сумма, пароль}
            int passCount = hashSumList.Count;          // число искомых хэшей

			StringBuilder tempStr = new StringBuilder(start); // строка для которой генерируется хеш в цикле

	        // основной цикл, здесь происходит подбор паролей от и до
            while (tempStr.ToString() != finish && passwdList.Count != passCount)
            {
				string tempMd5 = Md5Hash(tempStr.ToString());

				if (hashSumList.Contains(tempMd5))
                {
					hashSumList.Remove(tempMd5);
					KeyValuePair<string, string> temp = new KeyValuePair<string, string>(tempMd5, tempStr.ToString());

                    passwdList.Add(temp);
                }

				NextSymb(ref tempStr);
            }

            return passwdList;
        }

        /// <summary>
        /// Ищется строковое представление md5 от строки.
        /// </summary>
        /// <param name="input"> пароль </param>
        /// <returns></returns>
        public string Md5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            foreach (byte t in bytes)
                hash.Append(t.ToString("x2"));
            
            return hash.ToString();
        }

	    /// <summary>
	    /// Возвращает следудующую комбинацию из лексикографического порядка 
	    /// </summary>
		/// <param name="str">Строка в которой изменяем символ</param>
	    private void NextSymb(ref StringBuilder str)
        {
			int len = str.Length;
			char lastSymb = str[len - 1];

            // Если последняя буква бегунка последняя буква алфавита.
            if (lastSymb == _alphabet.Last())
            {
                // Дошли до начала, прошли весь цикл.
                if (len == 1)
                {
					str.Replace(lastSymb, _alphabet.First(), 0, 1);
					str.Append(_alphabet.First());
                }
                // меняем последнюю букву
                else
                {
					str.Remove(len - 1, 1);
					NextSymb(ref str);
					str.Append(_alphabet.First());
                }

            }
            else
            {
                int ind = Array.IndexOf(_alphabet, lastSymb); // Индекс текущего последнего элемента комбинации
                lastSymb = _alphabet[ind + 1];

				// Заменяем на следующий символ.
				str.Remove(len - 1, 1);
				str.Append(lastSymb);
            }
        }
		
        // перевод слова в систему счисления, мощность систему счисления = мощности алфавита
        public int FromWordToNum(string str)
        {
            int res = 0;
            char[] letterArr = str.ToCharArray();

            var alphabetLen = _alphabet.Length;

            for (int i = str.Length - 1; i >= 0; --i)
            {
                res += (Array.IndexOf(_alphabet, letterArr[i]) + 1) *	// need fix
                       (int) Math.Pow(alphabetLen, str.Length - 1 - i);
            }

            return res;
        }

        // перевод из алфавитной систему счисления в слово
        public string FromNumToWord(int num)
        {
	        if (num == 0)
		        return "0";

            List<char> wordArr = new List<char>();
            var alphabetLen = _alphabet.Length;

            for (int i = num % alphabetLen; num > 0; num /= alphabetLen, i = num % alphabetLen)
            {
                wordArr.Add(_alphabet[i]);
            }

            wordArr.Reverse();
            string word = string.Join("", wordArr);
            return word;
        }
    }
}