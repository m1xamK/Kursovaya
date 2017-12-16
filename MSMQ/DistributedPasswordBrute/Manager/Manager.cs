using System;
﻿using System.Collections.Generic;
﻿using System.Linq;
﻿using MsmqAdapters;

namespace Manager
{
	class MessageInProcess
	{
		private DateTime _time;
		private KeyValuePair<int, int> _range;
		private const int Overdude = 1000;

		public MessageInProcess(long time, KeyValuePair<int, int> range)
		{
			_time = new DateTime(time);
			_range = range;
		}

		bool IsValid()
		{
			return _time.Ticks - Overdude <= DateTime.Now.Ticks;
		}
	}

	/// <summary>
	/// класс осуществляющий равномерное распределение вычисления md5 праобраза 
	/// </summary>
	public class Manager
    {
		/// <summary>
		/// некий механизм осуществляющий передачу сообщений до агента
		/// </summary>
		private readonly MsmqRequestorAdapter _sender;

		private Dictionary<string, MessageInProcess> _agents;
 
		//Наверное надо брать константы из файла конфига или как-то задавать, крч надо подумать а пока и так сойдет

		//количетво бук в алфавите
		public const int AlphabetSize = 60;
		//предполагаемое количестао символов в будующей подюорке
		public const int QantityOfSymbols = 6;

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
		/// <param name="requestResurce"></param>
		/// <param name="replayResurse"></param>
		public Manager(string requestResurce, string replayResurse)
		{
			//_requestResurce = requestResurce;
			_sender = new MsmqRequestorAdapter(requestResurce, replayResurse);
			_agents = new Dictionary<string, MessageInProcess>();
		}

		//не ну а чо тут непонятно?????
		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="start"> начальний сдвиг числа от которого пойдет перебор </param>
		/// <param name="count"> сколько надо посчитать агенту строк после</param>
		/// <param name="hash">праобразы md5 сверток</param>
		void Send(int start, int count, string[] hash)
		{
			var msgId = DateTime.Now.Ticks;
			var newMsg = new MessageInProcess(msgId, new KeyValuePair<int, int>(start, start + count));
			
			_agents.Add(msgId.ToString(), newMsg);

			_sender.Send(start.ToString(), count, hash);
		}

		/// <summary>
		/// ну берем и считаем сколько должно получиться различных чисел в к ичной системе счисления 
		/// длинной QantityOfSymbols знаков, разбиваем на отдельные промежутки и отправлем в очередь
		/// </summary>
		/// <param name="hash"></param>
		/// <returns></returns>
		private string FindHashs(string[] hashs)
		{
			string result = "";

			int count = (int)Math.Pow(AlphabetSize, QantityOfSymbols);
			int step = 10000000;

			for (int i = 1; i < count; i += step)
			{
				if (i + step >= count)
					step = count - i;

				Send(i, i + step, hashs);

				result = hashs.Aggregate(result, (current, hash) => current + ("Send msg for " + hash + " from " + i + " to " + i + step + "\n"));
			}

			return result;
		}

		public void ReciveSync()
		{
			_sender.ReceiveSync();
		}

    }
}
