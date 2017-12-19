using System;
﻿using System.Collections.Generic;
﻿using System.Linq;
﻿using MsmqAdapters;

namespace Manager
{
	/// <summary>
	/// структура данных ,хранящая инфу о комбинациях, находящихся в обработке у агентов
	/// </summary>
	public class MsgInProcess
	{
		KeyValuePair<int, int> _range;
		DateTime _time;
		string[] hashs;
	}

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
		//кол-во комбинаций, которые нужно проверить, отправляемые в одном сообщениии
		public const int Step = 10000;

		/// <summary>
		/// некий механизм осуществляющий передачу сообщений до агента
		/// </summary>
		private readonly MsmqRequestorAdapter _sender;

		//сообщения в обработке
		private Dictionary<string, MsgInProcess> _msgList; 

		//храним пару - md5 комбинация и ответ на нее, для которых мы узнали
		private Dictionary<string, string> _resultHashAnswer;

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
        /// <param name="requestResource"></param>
        /// <param name="replyResourсe"></param>
        public Manager(string requestResource, string replyResourсe)
		{
			_msgList = new Dictionary<string, MsgInProcess>();
			_sender = new MsmqRequestorAdapter(requestResource, replyResourсe);
			_resultHashAnswer = new Dictionary<string, string>();
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

			for (int i = 1; i < count; i += Step)
			{
				var endOfIteration = 0;

				if (i + Step >= count)
					endOfIteration = count - i;
				else
					endOfIteration = Step;

				Send(i, i + endOfIteration, hashs);

				//result = hashs.Aggregate(result, (current, hash) => current + ("Send msg for " + hash + " from " + i + " to " + i + step + "\n"));
			}

			return result;
		}

		public string ReciveSync()  // changed by m1xamk void -> string
		{
			var message = _sender.ReceiveSync();

			if (message == null)
				return null;

			if (message.Extension[0] == 1)
			{
				var a = message.Body;
			}

			//Console.WriteLine(message.Body);
			Console.WriteLine("\t!!ReciveSync!!");

		    return "log";
		}

    }
}
