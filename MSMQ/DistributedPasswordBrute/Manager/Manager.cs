using System;
﻿using System.Collections.Generic;
using System.Text;
using MsmqAdapters;

namespace Manager
{
	/// <summary>
	/// структура данных, хранящая инфу о комбинациях, находящихся в обработке у агентов
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
		//Наверное надо брать константы из файла конфига или как-то задавать, крч надо подумать а пока и так сойдет

		//предполагаемое количестао символов в будующей подборке
		public const int QantityOfSymbols = 3;

		//алфавит, из которого может состоять hash
		public const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyz"; 

		//Сколько в очереди может быть одновременно сообщений
		public const int MsgInQueue = 10;

		/// некий механизм осуществляющий передачу сообщений до агента
		private readonly MsmqRequestorAdapter _sender;

		//сообщения находящиеся в обработке агентами
		private Dictionary<string, MsgInProcess> _msgList; 

		//храним пару - md5 комбинация и ответ на нее, для которых мы узнали
		private Dictionary<string, string> _resultHashAnswer;

		//конец последнего диапазона, отправленого для просчета агенту
		public string PreviosEnd { get; private set; }

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
        /// <param name="requestResource"></param>
        /// <param name="replyResourсe"></param>
        public Manager(string requestResource, string replyResourсe)
		{
			PreviosEnd = "0";
			_msgList = new Dictionary<string, MsgInProcess>();
			_sender = new MsmqRequestorAdapter(requestResource, replyResourсe);
			_resultHashAnswer = new Dictionary<string, string>();
			_msgList = new Dictionary<string, MsgInProcess>();
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
		/// <param name="hashs"></param>
		/// <returns></returns>
		public string FindHash(string[] hashs)
		{
			for (int i = 0; i < MsgInQueue; ++i)
			{
				string finish = NextWord(PreviosEnd);

				Send(PreviosEnd, finish, hashs);

				PreviosEnd = finish;
			}

			return "";
		}

		string NextWord(string word)
		{
			if (word[0] == 'z')
			{
				word = word.Replace('z', '1');
				word = word.Insert(word.Length, "0");
				return word;
			}

			int index = Alphabet.IndexOf(word[0]);

			word = word.Replace(word[0], Alphabet[++index]);

			return word;
		}

		//отправляем сообщение на основе предыдущего
		public void NextMsgSend(string msgId)
		{
			if (String.Compare(PreviosEnd, "zzzzzz", StringComparison.Ordinal) < 0)
				return;

			var msg = _msgList[msgId];

			string finish = NextWord(PreviosEnd);

			Send(PreviosEnd, finish, msg.Hashs);

			PreviosEnd = finish; //сохраняем конец, отправленого диапазона 
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
