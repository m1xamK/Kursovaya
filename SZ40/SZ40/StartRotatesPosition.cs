using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SZ40
{
    class StartRotatesPosition
    {
        private string _str;
        public StartRotatesPosition(string str)
        {
            _str = str;
        }

        public int[] GetStartPositions()
        {
            int[] result = new int[12];
            int index = 0;
            StringBuilder str = new StringBuilder(_str);
            StringBuilder temp = new StringBuilder("");

            for (int i = 0; i < str.Length; ++i)
            {
                if (i == str.Length - 1)
                {
                    temp.Append(str[i]);
                    result[index++] = Convert.ToInt32(temp.ToString());

                    return result;
                }                    

                if (str[i] == ' ')
                {
                    result[index++] = Convert.ToInt32(temp.ToString());
                    temp.Clear();
                }
                else
                    temp.Append(str[i]);
            }

            return result;
        }
    }

    //class RotatesPositions
    //{
    //    NumericUpDown[] _rotatesPositions;
    //    public RotatesPositions(NumericUpDown[] rotatesPositions)
    //    {
    //        _rotatesPositions = rotatesPositions;
    //    }
    //    public string GetPositions()
    //    {
    //        //StringBuilder str = new StringBuilder();
    //        string str = "";
    //        for (int i = 0; i < 12; ++i)
    //            str += _rotatesPositions[i].Value.ToString();

    //        return str;
    //    }        
    //}

    /*class Identificators
    {
        Dictionary<string, string> _identificatorsString;
        Dictionary<string, int[]> _identificatorsInt;

        /// <summary>
        /// Конструктор для класса, хранящий в себе идентификаторы.
        /// Ставит в соответствие каждому идентификатору "key" - массив из 12 int.
        /// </summary>
        public Identificators()
        {
            _identificatorsString = new Dictionary<string, string>();
            _identificatorsInt = new Dictionary<string,int[]>();

            using (StreamReader sr = new StreamReader("../Identificators.txt")) // Считывает ключ
            {
                while (sr.Peek() >= 0)
                {
                    string key = sr.ReadLine();
                    string array = sr.ReadLine();

                    _identificatorsString[key] = array;
                    _identificatorsInt[key] = ConvertToIntArray(array);
                }                    
            }
        }

        /// <summary>
        /// Преобразует строку в массив, состоящий из 12 чисел (начальные сдвиги).
        /// </summary>
        /// <param name="str">Строка, которую нужно преобразовать в массив 12 интов.</param>
        /// <returns></returns>
        private int[] ConvertToIntArray(string str)
        {
            int[] arr = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            for (int i = 0; i < 12; ++i)
            {
                int j = 2 * i;
                arr[i] = str[j] * 10 + str[j + 1] - '0';
            }

            return arr;
        }

        /// <summary>
        /// Возвращает строковое значение ключа переданного в качестве аргумента (массив из 12 интов в типе string).
        /// </summary>
        /// <param name="key">Строка чье значение нужно узнать.</param>
        /// <returns></returns>
        public string GetStringArray(string key)
        {
            return _identificatorsString[key];
        }

        /// <summary>
        /// Возвращает масив 12 интов соответствующий идентификатору. 
        /// </summary>
        /// <param name="key">Идентификатор, чей массив нужно получить.</param>
        /// <returns></returns>
        public int[] GetIntArray(string key)
        {
            return _identificatorsInt[key];
        }
    }*/
}
