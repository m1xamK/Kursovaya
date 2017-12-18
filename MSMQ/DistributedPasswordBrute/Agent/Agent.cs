using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Agent
{
    public class Agent
    {
        private readonly char[] Alphabet =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public List<KeyValuePair<string, string>> Calculate(string message)
        {
            // парсим сообщение.
            // todo
            string from = message.Substring(0, 4);
            string to = message.Substring(0, 4);
            string[] hash = message.Split(' ');

            // находим совпадения.
            return SearchPassword(from, to, hash);
        }

        /*
         * Ищет пароли ЧЬИ СВЕРТКИ В МАССИВЕ HashSum
         * string from      -- начало поиска, ОТ такого-то слова
         * string to        -- конец поиска,  ДО такого-то слова
         * string[] hashSum_arr -- массив нужных нам сверток
         */
        private List<KeyValuePair<string, string>> SearchPassword(string from, string to, string[] hashSumArr)
        {
            List<KeyValuePair<string, string>> passwdList = new List<KeyValuePair<string, string>>(); // требуемые пароли
            int numOfPasswd = hashSumArr.Length;
            List<string> hashSumList = hashSumArr.ToList();

            // если в лексикографическом порядке комбинация ОТ идет ДО комбинации после -- ошибка
            if (String.CompareOrdinal(from, to) > 0)
                throw new Exception("From > to in lexicographic order!");

            StringBuilder runner = new StringBuilder(from);      // строка для которой генерируется хеш в цикле
            int beginSize = from.Length;
            char lastSymb = (beginSize > 0) ? from[beginSize - 1] : 'A';


            // основной цикл, здесь происходит подбор паролей от и до
            for (;
                runner.ToString() != to && passwdList.Count != numOfPasswd;
                NextSymb(ref runner, ref lastSymb))
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

        //собственно, ищется строковое представление md5 от строки. Честно спеижженно
        private string Md5Hash(string input)
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

        // возвращает следудующую комбинацию из лексикографического порядка
        private void NextSymb(ref StringBuilder runner, ref char lastSymb)
        {
            int len = runner.Length;
            lastSymb = runner[len - 1];
            // если последняя буква бегунка -- последняя буква алфавита -- 
            if (lastSymb == Alphabet.Last())
            {
                // дошли до начала
                if (len == 1)
                {
                    runner.Replace(lastSymb, 'a', 0, 1);
                    lastSymb = 'a';
                    runner.Append('a');
                }
                else
                {
                    runner.Remove(len - 1, 1);
                    lastSymb = runner[len - 2];
                    NextSymb(ref runner, ref lastSymb);
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
    }
}