using System;
﻿using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Threading;
using MsmqAdapters;

namespace Manager
{
	public class LogArgs : EventArgs
	{
		public LogArgs(string logMsg)
		{
			Message = logMsg;
		}

		public string Message { get; set; }
	}
	/// <summary>
	/// класс осуществляющий равномерное распределение вычисления md5 праобраза
	/// </summary>
	public class Manager
	{
		/// Механизм осуществляющий передачу сообщений до агента
		private readonly MsmqRequestorAdapter _sender;

		// Словарь сообщений, находящихся в обработке агентами
		private static Dictionary<string, MsgInProcess> _msgList;	// словарь типа: {идентификатор сообщения, информация об сообщении}

		// Храним пару - md5 комбинация и ответ на нее, для которых мы узнали
		private Dictionary<string, string> _resultHashAnswer;

		// Последняя комбинация которая должна быть посчитана
		private const string LastRange = "zzzzzz";

		// Массив хешей, для которых ищем свертки
		private static string[] _hashArr;

		// Количество оставшихся хешей
		private int _count = 0;

		// Событие для вывода логов
		public event EventHandler<LogArgs> LogEvent;

		//конец последнего диапазона, отправленого для просчета агенту
		public string PreviousEnd { get; private set; }

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
		/// <param name="requestResource">имя очереди запросов</param>
		/// <param name="replyResourсe">имя очереди ответов</param>
		public Manager()//string requestResource, string replyResourсe)
		{
			PreviousEnd = "0";
			_msgList = new Dictionary<string, MsgInProcess>();
			_sender = new MsmqRequestorAdapter(); //requestResource, replyResourсe);
			_resultHashAnswer = new Dictionary<string, string>();
		}

		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="start">От этой строки начинаем считать хеши</param>
		/// <param name="end">До этой строки считаем хеш</param>
		/// <param name="hash">md5 свертки</param>
		void Send(string start, string end, string[] hash, MessageQueue queue = null)
		{
			//отправляем сообщение агенту через какое либо средство обмена сообщениями
			var msgId = _sender.Send(start, end, hash, queue);

			//отправляем сообщние в обрабатываемые
			_msgList.Add(msgId, new MsgInProcess(new KeyValuePair<string, string>(start, end), DateTime.Now));
		}

		/// <summary>
		/// Первоначальное заполнение очереди нашими сообщениями
		/// </summary>
		/// <param name="hashs">Хеш свертки, для которых ищем пароли.</param>
		public void InitialFillingOfTheQueue(string[] hashs)
		{
			_hashArr = hashs;
			_count = hashs.Length;

			// Сколько добавляем сообщений в очередь
			const int msgInQueue = 10;

			for (int i = 0; i < msgInQueue; ++i)
			{
				string finish = NextDiapason.Get(PreviousEnd);

				Send(PreviousEnd, finish, hashs);

				PreviousEnd = finish;
			}
		}

		/// <summary>
		/// Отправляем сообщение на основе предыдущего
		/// </summary>
		public void NextMsgSend(MessageQueue queue)
		{
			if (String.Compare(PreviousEnd, LastRange, StringComparison.Ordinal) > 0)
				return;

			string finish = NextDiapason.Get(PreviousEnd);

			Send(PreviousEnd, finish, _hashArr, queue);

			PreviousEnd = finish; //сохраняем конец, отправленого диапазона 
		}

		public string ReciveSync()
		{
			Dictionary<string, MsgInProcess> arr = new Dictionary<string, MsgInProcess>();

			//смотрим какие сообщения истекли по времении, удаяем из массива сообщений, находящихся в обработке, и отправляем заново
			foreach (var msg in _msgList)
			{
				long ticksInOneSecond = 10000000;

				if (DateTime.Now.Ticks - msg.Value.Time.Ticks < ticksInOneSecond * 20)
					continue;

				arr.Add(msg.Key, msg.Value);
			}


			foreach (var msg in arr)
			{
				_msgList.Remove(msg.Key);

				//отправляем заново
				Send(msg.Value.Range.Key, msg.Value.Range.Value, _hashArr);
			}

			arr.Clear();

			var message = _sender.ReceiveSync();

			if (message == null)
				return "";

			//если у агента получилось посчитать ответ хоть на один хеш
			if (message.Body.ToString() != "")
			{
				var messageBody = message.Body.ToString();

				var pairs = messageBody.Split(' ');

				for (int i = 0; i < pairs.Length - 1; i += 2)
				{
					if (_resultHashAnswer.ContainsKey(pairs[i]))
						continue;

					OnLogEvent(new LogArgs("Hash: " + pairs[i] + "\tPassword: " + pairs[i + 1] + "\n"));
					_resultHashAnswer.Add(pairs[i], pairs[i + 1]);

					--_count;
				}
			}

			NextMsgSend(message.ResponseQueue);

			if (_msgList.ContainsKey(message.CorrelationId))
				_msgList.Remove(message.CorrelationId);

			if (_msgList.Count == 0 || _count == 0)
			{
				// Освобождаем ресурсы очереди.
				_sender.StopSession();

				return _resultHashAnswer.Aggregate("", (current, pair) => current + "pair of md5 and password :" + pair.Key + "\t" + pair.Value + "\n");
			}

			return "";
		}

		public virtual void OnLogEvent(LogArgs e)
		{
			var handler = LogEvent;

			if (handler != null)
			{
				e.Message += String.Format("at {0}", DateTime.Now);
				handler(this, e);
			}
		}
	}
}
