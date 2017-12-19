using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Agent
{
    public class Agent
    {
        private readonly char[] Alphabet =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        //    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
        //    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        /// <summary>
        /// Главная функция, обрабатывает сообщение и возвращает список из пар "хэш"-"пароль"
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> Calculate(string message)
        {
            var messageInfo = message.Split(' ');

			int num = Convert.ToInt32(messageInfo[0]);
			string from = FromNumToWord(num);
            int count = Convert.ToInt32(messageInfo[1]);

            List<string> hashSumList = new List<string>();
            for (int i = 2; i < messageInfo.Length; ++i)
                hashSumList.Add(messageInfo[i]);

            //string to = FromNumToWord(FromWordToNum(from) + count);

            // находим совпадения.
            return SearchPassword(from, count, hashSumList);
        }
        
        /// <summary>
        /// Ищет пароли чьи свертки лежат в массиве HashSum
        /// </summary>
        /// <param name="from"> начало поиска, от какого-то слова </param>
		/// <param name="count"> конец поиска,  до такого-то слова </param>
        /// <param name="hashSumList"> массив нужных нам сверток, к которым требуется подобрать пароль </param>
        /// <returns></returns>
        private List<KeyValuePair<string, string>> SearchPassword(string from, int count, List<string> hashSumList)
        {
            List<KeyValuePair<string, string>> passwdList =
                new List<KeyValuePair<string, string>>(); // требуемые пароли
            int numOfPasswd = hashSumList.Count;          // число искомых хэшей

            StringBuilder runner = new StringBuilder(from); // строка для которой генерируется хеш в цикле
            int beginSize = from.Length;
            char lastSymb = (beginSize > 0) ? from[beginSize - 1] : '0';		// WTF ?!


            // основной цикл, здесь происходит подбор паролей от и до
            for (int i = 0; i < count && passwdList.Count != numOfPasswd; ++i, NextSymb(ref runner))
            {
                string curMd5 = Md5Hash(runner.ToString());
                if (hashSumList.Contains(curMd5))
                {
                    hashSumList.Remove(curMd5);
                    KeyValuePair<string, string> temp = new KeyValuePair<string, string>(curMd5, runner.ToString());

                    passwdList.Add(temp);
                }
            }

            return passwdList;
        }

        /// <summary>
        /// ищется строковое представление md5 от строки.
        /// </summary>
        /// <param name="input"> пароль </param>
        /// <returns></returns>
        public string Md5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            foreach (byte t in bytes)
            {
                hash.Append(t.ToString("x2"));
            }
            return hash.ToString();
        }

        /// <summary>
        /// возвращает следудующую комбинацию из лексикографического порядка 
        /// </summary>
        /// <param name="runner"> бегунок, строка, которая изменяется по циклу</param>
        /// <param name="lastSymb"> последний символ </param>
        private void NextSymb(ref StringBuilder runner)
        {
            int len = runner.Length;
            char lastSymb = runner[len - 1];
            // если последняя буква бегунка -- последняя буква алфавита -- 
            if (lastSymb == Alphabet.Last())
            {
                // дошли до начала, прошли весь цикл
                if (len == 1)
                {
                    runner.Replace(lastSymb, 'a', 0, 1);
                    lastSymb = 'a';
                    runner.Append('a');
                }
                // меняем последнюю букву
                else
                {
                    runner.Remove(len - 1, 1);
                    lastSymb = runner[len - 2];
                    NextSymb(ref runner);
                    runner.Append('a');
                }

            }
            else
            {
                int ind = Array.IndexOf(Alphabet, lastSymb); // индекс текущего последнего элемента комбинации
                lastSymb = Alphabet[ind + 1];
                runner.Remove(len - 1, 1);
                runner.Append(lastSymb); // заменяем на следующий символ
            }
        }


		// delete
        // перевод слова в систему счисления, мощность систему счисления = мощности алфавита
        public int FromWordToNum(string str)
        {
            int res = 0;
            char[] letterArr = str.ToCharArray();

            var alphabetLen = Alphabet.Length;

            for (int i = str.Length - 1; i >= 0; --i)
            {
                res += (Array.IndexOf(Alphabet, letterArr[i]) + 1) *	// need fix
                       (int) Math.Pow(alphabetLen, str.Length - 1 - i);
				//res +=
            }

            return res;
        }

        // перевод из алфавитной систему счисления в слово
        public string FromNumToWord(int num)
        {
	        if (num == 0)
		        return "0";

            List<char> wordArr = new List<char>();
            var alphabetLen = Alphabet.Length;

            for (int i = num % alphabetLen; num > 0; num /= alphabetLen, i = num % alphabetLen)
            {
                wordArr.Add(Alphabet[i]);
            }

            wordArr.Reverse();
            string word = string.Join("", wordArr);
            return word;
        }
    }
}