using System;
﻿using System.Collections.Generic;
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

    public delegate void CustomEventHandler(object sender, LogArgs logArgs);

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

		//последняя комбинация которая должна общитаться
		private const string LastRange = "zzzzzz";

		// Количество хешей, для которых ищем пароли
		private int _hashCount;

		//массив общитываемых хешей
		private string[] _hashArr;

        public event EventHandler<LogArgs> logEvent;

        protected virtual void OnLogEvent(LogArgs e)
        {
            var handler = logEvent;

            if (handler != null)
            {
                e.Message += String.Format("at {0}", DateTime.Now.ToString());
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
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="hash">md5 свертки</param>
		void Send(string start, string end, string[] hash)
		{
			//отправляем сообщение агенту через какое либо средство обмена сообщениями
			var msdId = _sender.Send(start, end, hash);

			//отправляем сообщние в обрабатываемые
			_msgList.Add(msdId, new MsgInProcess(new KeyValuePair<string, string>(start, end), DateTime.Now));
		}

		/// <summary>
		/// Первоначальное заполнение очереди нашеми сообщениями
		/// </summary>
		/// <param name="hashs">Хеш свертки, для которых ищем пароли.</param>
		public void InitialFillingOfTheQueue(string[] hashs)
		{
			_hashCount = hashs.Length;
			_hashArr = hashs;

			for (int i = 0; i < MsgInQueue; ++i)
			{
				string finish = NextDiapazone.Get(PreviousEnd);

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

			string finish = NextDiapazone.Get(PreviousEnd);

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
                // тут эвент 
                
                //
			    for (int i = 0; i < pairs.Length - 1; i += 2)
			    {
			        --_hashCount;

			        if (!_resultHashAnswer.ContainsKey(pairs[i]))
			        {
			            OnLogEvent(new LogArgs("Hash:" + pairs[i] + "\nPassword:" + pairs[i+1] + "\n"));
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

			if (_msgList.Count == 0 || _hashCount == 0)
			{
				var resultStr = "";

				foreach (var pair in _resultHashAnswer)
					resultStr += "pair of md5 and password :" + pair.Key + "\t" + pair.Value + "\n";
				
				return resultStr;
			}

			return "";
		}

    }
}
