using System;
﻿using System.Collections.Generic;
using System.Linq;
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
		private Dictionary<string, MsgInProcess> _msgList;	// словарь типа: {идентификатор сообщения, информация об сообщении}

		// Храним пару - md5 комбинация и ответ на нее, для которых мы узнали
		private Dictionary<string, string> _resultHashAnswer;

		// Последняя комбинация которая должна быть посчитана
		private const string LastRange = "zzzzzz";

		// Массив хешей, для которых ищем свертки
		private string[] _hashArr;

		// Событие для вывода логов
        public event EventHandler<LogArgs> LogEvent;

        protected virtual void OnLogEvent(LogArgs e)
        {
            var handler = LogEvent;

            if (handler != null)
            {
                e.Message += String.Format("at {0}", DateTime.Now);
                handler(this, e);
            }
        }

		//конец последнего диапазона, отправленого для просчета агенту
		public string PreviousEnd { get; private set; }

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
        /// <param name="requestResource">имя очереди запросов</param>
        /// <param name="replyResourсe">имя очереди ответов</param>
        public Manager(string requestResource, string replyResourсe)
		{
			PreviousEnd = "0";
			_msgList = new Dictionary<string, MsgInProcess>();
			_sender = new MsmqRequestorAdapter(requestResource, replyResourсe);
			_resultHashAnswer = new Dictionary<string, string>();
		}

		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="start">От этой строки начинаем считать хеши</param>
		/// <param name="end">До этой строки считаем хеш</param>
		/// <param name="hash">md5 свертки</param>
		void Send(string start, string end, string[] hash)
		{
			//отправляем сообщение агенту через какое либо средство обмена сообщениями
			var msdId = _sender.Send(start, end, hash);

			//отправляем сообщние в обрабатываемые
			_msgList.Add(msdId, new MsgInProcess(new KeyValuePair<string, string>(start, end), DateTime.Now));
		}

		/// <summary>
		/// Первоначальное заполнение очереди нашими сообщениями
		/// </summary>
		/// <param name="hashs">Хеш свертки, для которых ищем пароли.</param>
		public void InitialFillingOfTheQueue(string[] hashs)
		{
			_hashArr = hashs;

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
		/// <param name="msgId">Идентификатор сообщения</param>
		public void NextMsgSend(string msgId)
		{
			if (String.Compare(PreviousEnd, LastRange, StringComparison.Ordinal) > 0)
				return;

			string finish = NextDiapason.Get(PreviousEnd);

			Send(PreviousEnd, finish, _hashArr);

			PreviousEnd = finish; //сохраняем конец, отправленого диапазона 
		}

		public string ReciveSync() 
		{
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
					//if (_hashArr.Contains(pairs[i]))
					//{
					//	_hashArr.Remove(pairs[i]);

					//	// все остальное

					//}

			        if (!_resultHashAnswer.ContainsKey(pairs[i]))
			        {
			            OnLogEvent(new LogArgs("Hash: " + pairs[i] + "\tPassword: " + pairs[i+1] + "\n"));
			            _resultHashAnswer.Add(pairs[i], pairs[i + 1]);
			        }
			    }
			}

			NextMsgSend(message.CorrelationId);

			_msgList.Remove(message.CorrelationId);

			//проверяем "свежесть" сообщений, отправляем еще раз, если кто-то испортился
			foreach (var msgInProcess in _msgList)
			{
				if (msgInProcess.Value.Time.Ticks - DateTime.Now.Ticks > 10000)
				{
					Send(msgInProcess.Value.Range.Key, msgInProcess.Value.Range.Value, _hashArr);
 
					_msgList.Remove(msgInProcess.Key);
				}
			}

			if (_msgList.Count == 0 || _hashArr.Length == 0)
			{
				// Освобождаем ресурсы очереди.
				_sender.StopSession();

				return _resultHashAnswer.Aggregate("", (current, pair) => current + "pair of md5 and password :" + pair.Key + "\t" + pair.Value + "\n");
			}
			
			return "";
		}

    }
}
