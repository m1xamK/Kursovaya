using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rotors
{
	using System.Diagnostics;

	public class Mashine
	{
		public Mashine(string fileName, string combination,int []startPos, int shift = 0)
		{            
			MashineSz = new List<IRotor>();

            List<List<int>> term = Initialize(fileName + combination + ".txt");

			for (int i = 0; i < 6; ++i)
				MashineSz.Add(new XRotor(term[i], startPos[i], shift));

			for (int i = 6; i < 12; ++i)
				MashineSz.Add(new PRotor(term[i], startPos[i], shift));
		}

        private List<IRotor> MashineSz;

		/// <summary>
		/// Считывание ключей из файла
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
        private List<List<int>> Initialize(string fileName)
        {
            List<List<int>> result = new List<List<int>>();

            int[] sizes = { 41, 31, 29, 26, 23, 61, 37, 43, 47, 51, 53, 59 };


            var strArr = File.ReadAllLines(fileName);

            for (int i = 0; i < 12; ++i)
            {
                result.Add(new List<int>(sizes[i]));

                for (int j = 0; j < sizes[i]; ++j)
                    result[i].Add(Int32.Parse(strArr[i][j].ToString()));
            }

            return result;
        }          

		/// <summary>
		/// Один такт машины
		/// </summary>
		public void Turn()
		{
			var firstMashineRotor = MashineSz[5].GetValue();
			var secondMashineRotor = MashineSz[6].GetValue();

			for (int i = 0; i < 6; ++i)
				MashineSz[i].Turn(1);

			MashineSz[6].Turn(firstMashineRotor);

			for (int i = 7; i < 12; ++i)
				MashineSz[i].Turn(secondMashineRotor);
		}

		/// <summary>
		/// Считывание гаммы 
		/// </summary>
		/// <returns></returns>
		public List<int> GetResult()
		{
			var result = new List<int>();

			for (int i = 4; 0 <= i; --i)
				result.Add((MashineSz[i].GetValue() + MashineSz[i + 7].GetValue()) % 2);

			return result;
		}

		public char Tact()
		{
			StringBuilder str = new StringBuilder();

			var arr = GetResult();

			for (int i = 0; i < 5; ++i)
				str.Append(arr[i].ToString());

			for (int i = 0; i < 12; ++i)
				MashineSz[i].GetValue();
			
			Turn();

			return InitTable.ArrDictionary[str.ToString()];
		}

        public char GetNextSymbol(char ch)
        {
            return Summ.GetSum(Tact(), ch);
        }
	}
}