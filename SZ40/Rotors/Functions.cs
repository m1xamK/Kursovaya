namespace Rotors
{
    public class Summ
    {
        /// <summary>
        /// заполняю таблицу соответсвия символов бинарным представления чтобы сформировать
        /// таблицу сумм и составляю таблицу сумм
        /// </summary>
        static Summ()
        {
            _symbolFromBitRepresentation = new char[33];
            _summTable = new char[256][];
            _symbolBitRepresentation = new int[256];

			//pзвполнение нулями
            for (int i = 0; i < 256; ++i)
            {
                _summTable[i] = new char[256];
                    
                for (int j = 0; j < 256; ++j)
                {
                    _summTable[i][j] = '_';
                }
            }

            ///
            /// символам в соответствие числа из бинарного представления
            /// 
#region SymbolRepresentation
            _symbolBitRepresentation['3'] = 2;
            _symbolBitRepresentation['4'] = 8;
            _symbolBitRepresentation['5'] = 27;
            _symbolBitRepresentation['8'] = 31;
            _symbolBitRepresentation['9'] = 4;
            _symbolBitRepresentation['/'] = 0;

            _symbolBitRepresentation['A'] = 3;                  _symbolBitRepresentation['G'] = 26;
            _symbolBitRepresentation['B'] = 25;                 _symbolBitRepresentation['H'] = 20;
            _symbolBitRepresentation['C'] = 14;                 _symbolBitRepresentation['I'] = 6;
            _symbolBitRepresentation['D'] = 9;                  _symbolBitRepresentation['J'] = 11;
            _symbolBitRepresentation['E'] = 1;                  _symbolBitRepresentation['K'] = 15;
            _symbolBitRepresentation['F'] = 13;                 _symbolBitRepresentation['L'] = 18;

            _symbolBitRepresentation['M'] = 28;                 _symbolBitRepresentation['T'] = 16;
            _symbolBitRepresentation['N'] = 12;                 _symbolBitRepresentation['U'] = 7;
            _symbolBitRepresentation['O'] = 24;                 _symbolBitRepresentation['V'] = 30;
            _symbolBitRepresentation['P'] = 22;                 _symbolBitRepresentation['W'] = 19;
            _symbolBitRepresentation['Q'] = 23;                 _symbolBitRepresentation['X'] = 29;
            _symbolBitRepresentation['R'] = 10;                 _symbolBitRepresentation['Y'] = 21;
            _symbolBitRepresentation['S'] = 5;                  _symbolBitRepresentation['Z'] = 17;
               
#endregion

#region SymbolFromRepresentation

            _symbolFromBitRepresentation[0] = '/';  
            _symbolFromBitRepresentation[2] = '3';  
            _symbolFromBitRepresentation[4] = '9';  
            _symbolFromBitRepresentation[8] = '4';  
            _symbolFromBitRepresentation[27] = '5'; 
            _symbolFromBitRepresentation[31] = '8'; 

            _symbolFromBitRepresentation[3] = 'A'; _symbolFromBitRepresentation[26] = 'G';
            _symbolFromBitRepresentation[25] = 'B'; _symbolFromBitRepresentation[20] = 'H';
            _symbolFromBitRepresentation[14] = 'C'; _symbolFromBitRepresentation[6] = 'I';
            _symbolFromBitRepresentation[9] = 'D'; _symbolFromBitRepresentation[11] = 'J';
            _symbolFromBitRepresentation[1] = 'E'; _symbolFromBitRepresentation[15] = 'K';
            _symbolFromBitRepresentation[13] = 'F'; _symbolFromBitRepresentation[18] = 'L';

            _symbolFromBitRepresentation[28] = 'M'; _symbolFromBitRepresentation[16] = 'T';
            _symbolFromBitRepresentation[12] = 'N'; _symbolFromBitRepresentation[7] = 'U';
            _symbolFromBitRepresentation[24] = 'O'; _symbolFromBitRepresentation[30] = 'V';
            _symbolFromBitRepresentation[22] = 'P'; _symbolFromBitRepresentation[19] = 'W';
            _symbolFromBitRepresentation[23] = 'Q'; _symbolFromBitRepresentation[29] = 'X';
            _symbolFromBitRepresentation[10] = 'R'; _symbolFromBitRepresentation[21] = 'Y';
            _symbolFromBitRepresentation[5] = 'S'; _symbolFromBitRepresentation[17] = 'Z';
         
#endregion



            ///
            /// Формирую Таблицу сумм
            /// 
            char ch1;
            char ch2;
            ///
            /// заполнил для алфавита
            /// 
            for (ch1 = 'A'; ch1 <= 'Z'; ++ch1)
            {
                for (ch2 = 'A'; ch2 <= 'Z'; ++ch2)
                {
                    _summTable[ch1][ch2] = _symbolFromBitRepresentation[_symbolBitRepresentation[ch1] ^ _symbolBitRepresentation[ch2]];
                    _summTable[ch2][ch1] = _symbolFromBitRepresentation[_symbolBitRepresentation[ch1] ^ _symbolBitRepresentation[ch2]];
                }
            }

            fillForUnnormal('/');
            fillForUnnormal('3');
            fillForUnnormal('4');
            fillForUnnormal('5');
            fillForUnnormal('8');
            fillForUnnormal('9');

        }

        /// <summary>
        /// заполнение таблицы для управляющих символов
        /// </summary>
		private static void fillForUnnormal(char symbol)
        {
            char ch2;

            for (ch2 = 'A'; ch2 <= 'Z'; ++ch2)
            {
                _summTable[symbol][ch2] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];
                _summTable[ch2][symbol] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];
            }

            ch2 = '/';
            _summTable[symbol][ch2] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];
            _summTable[ch2][symbol] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];

            ch2 = '3';
            _summTable[symbol][ch2] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];
            _summTable[ch2][symbol] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];

            ch2 = '4';
            _summTable[symbol][ch2] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];
            _summTable[ch2][symbol] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];

            ch2 = '5';
            _summTable[symbol][ch2] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];
            _summTable[ch2][symbol] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];

            ch2 = '8';
            _summTable[symbol][ch2] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];
            _summTable[ch2][symbol] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];

            ch2 = '9';
            _summTable[symbol][ch2] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];
            _summTable[ch2][symbol] = _symbolFromBitRepresentation[_symbolBitRepresentation[symbol] ^ _symbolBitRepresentation[ch2]];
        }


        /// <summary>
        /// Функция для получения суммы сивола first и second
        /// </summary>
		public static char GetSum(char first, char second)
        {
            return _summTable[first][second];
        }

		private static char[][] _summTable;
		private static int[] _symbolBitRepresentation;
        private static char[] _symbolFromBitRepresentation;
    }

}

//пробный мусор
/*
 *
        //public static StringBuilder Summ(StringBuilder first, StringBuilder second)
        //{
        //    StringBuilder temp = new StringBuilder();
        //    int i = 0;

        //    while (i < first.Length && i < second.Length)
        //    {
        //        temp[i] = (char)(first[i] + second[i]);
        //        ++i;
        //    }

        //    return temp;
        //} 
 * int[] temp1 = _summTable[first];
                int[] temp2 = _summTable[second];

                int[] result = new int[5];

                for (int i = 1; i < 5; ++i)
                {
                    if (temp1[i] == 1 && temp2[i] == 1)
                    {
                        if (result[i - 1] == 0)
                            result[i - 1] = 1;
                        else
                            result[i - 1] = 0;

                        result[i] = 0;
                    }
                    else
                    {
                        result[i] = temp2[i];
                        result[i] += temp1[i];
                    }
                }

                return ' ';//нужно както получить нужный чар
*/ 


