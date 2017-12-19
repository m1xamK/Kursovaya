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
		public KeyValuePair<int, int> Range { get; private set; }
		public DateTime Time { get; private set; }
		public string[] Hashs { get; private set; }

		public MsgInProcess(string[] hashs, KeyValuePair<int, int> range, DateTime time)
		{
			Hashs = hashs;
			Range = range;
			Time = time;
		}
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
		public const int Step = 100;

		/// <summary>
		/// некий механизм осуществляющий передачу сообщений до агента
		/// </summary>
		private readonly MsmqRequestorAdapter _sender;

		//сообщения в обработке
		private Dictionary<string, MsgInProcess> _msgList; 

		//храним пару - md5 комбинация и ответ на нее, для которых мы узнали
		private Dictionary<string, string> _resultHashAnswer;

		public int Count { get; private set; }
		public int MsgInQueue { get; set; }
		public int PreviosEnd { get; private set; }

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
        /// <param name="requestResource"></param>
        /// <param name="replyResourсe"></param>
        public Manager(string requestResource, string replyResourсe, int count)
		{
			Count = (int)Math.Pow(AlphabetSize, QantityOfSymbols);;
			_msgList = new Dictionary<string, MsgInProcess>();
			_sender = new MsmqRequestorAdapter(requestResource, replyResourсe);
			_resultHashAnswer = new Dictionary<string, string>();
			_msgList = new Dictionary<string, MsgInProcess>();
		}

		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="start"> начальний сдвиг числа от которого пойдет перебор </param>
		/// <param name="count"> сколько надо посчитать агенту строк после</param>
		/// <param name="hash">праобразы md5 сверток</param>
		void Send(string[] hash)
		{
			//отправляем сообщение агенту через какое либо средство обмена сообщениями
			var msdId = _sender.Send(PreviosEnd.ToString(), Step, hash);

			PreviosEnd += Step;//сохраняем конец, отправленого диапазона 

			//отправляем сообщние в обрабатываемые
			_msgList.Add(msdId, new MsgInProcess(hash, new KeyValuePair<int, int>(PreviosEnd, PreviosEnd + Step), DateTime.Now));
		}

		/// <summary>
		/// ну берем и считаем сколько должно получиться различных чисел в к ичной системе счисления 
		/// длинной QantityOfSymbols знаков, разбиваем на отдельные промежутки и отправлем в очередь
		/// </summary>
		/// <param name="hash"></param>
		/// <returns></returns>
		public string FindHash(string[] hashs)
		{
			for(int i = 0; i < MsgInQueue; ++i)
				Send(hashs);

			return "";
		}

		public void NextMsgSend(string msgId)
		{
			if (PreviosEnd > Count)
				return;

			var msg = _msgList[msgId];

			Send(msg.Hashs);

			_msgList.Remove(msgId);
		}

		public string ReciveSync()  // changed by m1xamk void -> string
		{
			var message = _sender.ReceiveSync();

			if (message == null)
				return null;

			if (message.Extension != null)
			{
				var a = message.Body;
				Console.WriteLine(a.ToString());
			}

			//Console.WriteLine(message.Body);
			Console.WriteLine("\t!!ReciveSync!!");

		    return "log";
		}

    }
}
