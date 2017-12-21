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
	/// структура данных, хранящая информацию о комбинациях, находящихся в обработке у агентов
	/// </summary>
	public class MsgInProcess
	{
	    
		public KeyValuePair<string, string> Range { get; private set; }
		public DateTime Time { get; private set; }
		public string[] Hashs { get; private set; }

		public MsgInProcess(string[] hashs, KeyValuePair<string, string> range, DateTime time)
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
		//предполагаемое количестао символов в будующей подборке
		public const int QantityOfSymbols = 3;

		//Сколько в очереди может быть одновременно сообщений
		public const int MsgInQueue = 10;

		/// некий механизм осуществляющий передачу сообщений до агента
		private readonly MsmqRequestorAdapter _sender;

		//сообщения находящиеся в обработке агентами
		private Dictionary<string, MsgInProcess> _msgList; 

		//храним пару - md5 комбинация и ответ на нее, для которых мы узнали
		private Dictionary<string, string> _resultHashAnswer;

		// Количество хешей, для которых ищем пароли
		private int _hashCount;

		private string[] _hashArr;

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
		public string PreviosEnd { get; private set; }

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
        /// <param name="requestResource">имя очереди запросов</param>
        /// <param name="replyResourсe">имя очереди ответов</param>
        public Manager(string requestResource, string replyResourсe)
		{
			PreviosEnd = "0";
			_msgList = new Dictionary<string, MsgInProcess>();
			_sender = new MsmqRequestorAdapter(requestResource, replyResourсe);
			_resultHashAnswer = new Dictionary<string, string>();
		}

		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="hash">md5 свертки</param>
		void Send(string start, string end, string[] hash)
		{
			//отправляем сообщение агенту через какое либо средство обмена сообщениями
			var msdId = _sender.Send(start, end, hash);

			//отправляем сообщние в обрабатываемые
			_msgList.Add(msdId, new MsgInProcess(hash, new KeyValuePair<string, string>(start, end), DateTime.Now));
		}

		/// <summary>
		/// заполняем очередь сообщений
		/// </summary>
		/// <param name="hashs">Хеш свертки для которых ищем пароли.</param>
		public void FindHash(string[] hashs)
		{
			_hashCount = hashs.Length;
			_hashArr = hashs;

			for (int i = 0; i < MsgInQueue; ++i)
			{
				string finish = NextWord.Get(PreviosEnd);

				Send(PreviosEnd, finish, hashs);

				PreviosEnd = finish;
			}
		}

		/// <summary>
        /// Отправляем сообщение на основе предыдущего
		/// </summary>
		/// <param name="msgId">Идентификатор сообщения</param>
		public void NextMsgSend(string msgId)
		{
			if (String.Compare(PreviosEnd, "zzzzzz", StringComparison.Ordinal) > 0)
				return;

			string finish = NextWord.Get(PreviosEnd);

			Send(PreviosEnd, finish, _hashArr);

			PreviosEnd = finish; //сохраняем конец, отправленого диапазона 
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
                // тут эвент 
                
                //
			    for (int i = 0; i < pairs.Length - 1; i += 2)
			    {
			        --_hashCount;

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
					Send(msgInProcess.Value.Range.Key, msgInProcess.Value.Range.Value, msgInProcess.Value.Hashs);
 
					_msgList.Remove(msgInProcess.Key);
				}
			}

			if (_msgList.Count == 0 || _hashCount == 0)
				return _resultHashAnswer.Aggregate("", (current, pair) => current + "pair of md5 and password :" + pair.Key + "\t" + pair.Value + "\n");
			
			return "";
		}

    }
}
