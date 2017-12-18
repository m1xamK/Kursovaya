using System;
﻿using System.Collections.Generic;
﻿using System.Linq;
﻿using MsmqAdapters;

namespace Manager
{
	/// <summary>
	/// класс осуществляющий равномерное распределение вычисления md5 праобраза 
	/// </summary>
	public class Manager
    {
		//Наверное надо брать константы из файла конфига или как-то задавать, крч надо подумать а пока и так сойдет

		//количетво бук в алфавите
		public const int AlphabetSize = 30;
		//предполагаемое количестао символов в будующей подюорке
		public const int QantityOfSymbols = 3;

		/// <summary>
		/// некий механизм осуществляющий передачу сообщений до агента
		/// </summary>
		private readonly MsmqRequestorAdapter _sender;

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
        /// <param name="requestResource"></param>
        /// <param name="replyResourсe"></param>
        public Manager(string requestResource, string replyResourсe)
		{
            _sender = new MsmqRequestorAdapter(requestResource, replyResourсe);
		}

		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="start"> начальний сдвиг числа от которого пойдет перебор </param>
		/// <param name="count"> сколько надо посчитать агенту строк после</param>
		/// <param name="hash">праобразы md5 сверток</param>
		void Send(int start, int count, string[] hash)
		{	
			_sender.Send(start.ToString(), count, hash);
		}

		/// <summary>
		/// ну берем и считаем сколько должно получиться различных чисел в к ичной системе счисления 
		/// длинной QantityOfSymbols знаков, разбиваем на отдельные промежутки и отправлем в очередь
		/// </summary>
		/// <param name="hash"></param>
		/// <returns></returns>
		public string FindHash(string[] hashs)
		{
			string result = "";

			int count = (int)Math.Pow(AlphabetSize, QantityOfSymbols);
			int step = 10000;

			for (int i = 1; i < count; i += step)
			{
				if (i + step >= count)
					step = count - i;

				Send(i, i + step, hashs);

				//result = hashs.Aggregate(result, (current, hash) => current + ("Send msg for " + hash + " from " + i + " to " + i + step + "\n"));
			}

			return result;
		}

		public string ReciveSync()  // changed by m1xamk void -> string
		{
			var message = _sender.ReceiveSync();

			//Console.WriteLine(message.Body);
			Console.WriteLine("\t!!ReciveSync!!");

		    return "log";
		}

    }
}
