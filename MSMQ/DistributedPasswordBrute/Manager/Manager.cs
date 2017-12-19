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
		public const int AlphabetSize = 36;

		//предполагаемое количестао символов в будующей подюорке
		public const int QantityOfSymbols = 3;

		//кол-во комбинаций, которые нужно проверить, отправляемые в одном сообщениии
		public const int Step = 200;

		//Сколько в очереди может быть одновременно сообщений
		public const int MsgInQueue = 10;

		/// некий механизм осуществляющий передачу сообщений до агента
		private readonly MsmqRequestorAdapter _sender;

		//сообщения находящиеся в обработке агентами
		private Dictionary<string, MsgInProcess> _msgList; 

		//храним пару - md5 комбинация и ответ на нее, для которых мы узнали
		private Dictionary<string, string> _resultHashAnswer;

		//Сколько у нас различных комбинаций строк из К символов
		public int Count { get; private set; }

		//конец последнего диапазона, отправленого для просчета агенту
		public int PreviosEnd { get; private set; }

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
        /// <param name="requestResource"></param>
        /// <param name="replyResourсe"></param>
        public Manager(string requestResource, string replyResourсe)
		{
			Count = (int)Math.Pow(AlphabetSize, QantityOfSymbols);
			PreviosEnd = 0;
			_msgList = new Dictionary<string, MsgInProcess>();
			_sender = new MsmqRequestorAdapter(requestResource, replyResourсe);
			_resultHashAnswer = new Dictionary<string, string>();
			_msgList = new Dictionary<string, MsgInProcess>();
		}

		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="count"></param>
		/// <param name="hash">md5 свертки</param>
		/// <param name="start"></param>
		void Send(int start, int count, string[] hash)
		{
			//отправляем сообщение агенту через какое либо средство обмена сообщениями
			var msdId = _sender.Send(start.ToString(), count, hash);

			//отправляем сообщние в обрабатываемые
			_msgList.Add(msdId, new MsgInProcess(hash, new KeyValuePair<int, int>(start, count), DateTime.Now));
		}

		/// <summary>
		/// заполняем очередь сообщений
		/// </summary>
		/// <param name="hash"></param>
		/// <returns></returns>
		public string FindHash(string[] hashs)
		{
			for (int i = 0; i < MsgInQueue; ++i)
			{
				Send(PreviosEnd, Step, hashs);
				PreviosEnd += Step;
			}

			return "";
		}

		//отправляем сообщение на основе предыдущего
		public void NextMsgSend(string msgId)
		{
			if (PreviosEnd > Count)
				return;

			var msg = _msgList[msgId];

			Send(PreviosEnd, Step, msg.Hashs);

			PreviosEnd += Step;//сохраняем конец, отправленого диапазона 
		}

		public void ReciveSync()  // changed by m1xamk void -> string
		{
			var message = _sender.ReceiveSync();

			if (message == null)
				return ;
			
			//если у агента получилось посчитать ответ хоть на один hash
			if (message.Body.ToString() != "") //я хз какое условие
			{
				var messageBody = message.Body.ToString();

				var pairs = messageBody.Split(' ');

				for (int i = 0; i < pairs.Length - 1; i += 2)
				{
					Console.WriteLine("\t pair of md5 and password :" + pairs[i] + " "+ pairs[i + 1] + "\n");

					if (!_resultHashAnswer.ContainsKey(pairs[i]))
						_resultHashAnswer.Add(pairs[i], pairs[i + 1]);
				}
			}

			NextMsgSend(message.CorrelationId);

			_msgList.Remove(message.CorrelationId);

			//проверяем "свежесть" сообщений, отправляем еще раз, если кто-то испортился
			foreach (var msgInProcess in _msgList)
			{
				if (msgInProcess.Value.Time.Ticks - DateTime.Now.Ticks > 10000)
				{
					Send(msgInProcess.Value.Range.Key, msgInProcess.Value.Range.Value, msgInProcess.Value.Hashs);

					_msgList.Remove(msgInProcess.Key);
				}
			}

			if (_msgList.Count == 0)
			{
				foreach (var pair in _resultHashAnswer)
					Console.WriteLine("\t pair of md5 and password :" + pair.Key + "\t"+ pair.Value + "\n");

				Console.WriteLine("\t Misha is pidor \n");

				return;
			}
				
			ReciveSync();
		}

    }
}
