<<<<<<< HEAD
﻿using System;
using System.Security.Permissions;
using RequestManager;
=======
﻿using MsmqAdapters;
>>>>>>> b9f0bc3bcc549387989859d19bcd35c2c7a7c9e7

namespace Manager
{
	/// <summary>
	/// класс осуществляющий равномерное распределение вычисления md5 праобраза 
	/// </summary>
	public class Manager
    {
		/// <summary>
		/// некий механизм осуществляющий передачу сообщений до агента
		/// </summary>
<<<<<<< HEAD
		private readonly RequestorAdapter _sender;


		//Наверное надо брать константы из файла конфига или как-то задавать, крч надо подумать а пока и так сойдет

		//количетво бук в алфавите
		public const int AlphabetSize = 60;
		
		//предполагаемое количестао символов в будующей подюорке
		public const int QantityOfSymbols = 6;
=======
		private MsmqRequestorAdapter _sender;
>>>>>>> b9f0bc3bcc549387989859d19bcd35c2c7a7c9e7

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
<<<<<<< HEAD
		/// <param name="requestResurce"></param>
		/// <param name="replayResurse"></param>
		Manager(string requestResurce, string replayResurse)
		{
			_sender = new RequestorAdapter(requestResurce, replayResurse);
		}
	
=======
        /// <param name="requestResource"></param>
        /// <param name="replayResourse"></param>
        Manager(string requestResource, string replayResourse)
		{
            _sender = new MsmqRequestorAdapter(requestResource, replayResourse);
		}

		//не ну а чо тут непонятно?????
>>>>>>> b9f0bc3bcc549387989859d19bcd35c2c7a7c9e7
		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="start"> начальний сдвиг числа от которого пойдет перебор </param>
		/// <param name="count"> сколько надо посчитать агенту строк после</param>
		/// <param name="hash">праобразы md5 сверток</param>
<<<<<<< HEAD
		private void Send(string start, int count, string[] hash)
=======
		void Send(string start, int count, string[] hash)
>>>>>>> b9f0bc3bcc549387989859d19bcd35c2c7a7c9e7
		{
			_sender.Send(start, count, hash);
		}

<<<<<<< HEAD
		/// <summary>
		/// ну берем и считаем сколько должно получитья различных чисел в к ичной системе счисления 
		/// длинной QantityOfSymbols знаков, разбиваем на отдельные промежутки и отправлем в очередь
		/// </summary>
		/// <param name="hash"></param>
		/// <returns></returns>
		private string FindHashs(string[] hash)
		{
			int count = (int)Math.Pow(AlphabetSize, QantityOfSymbols);
			int step = 10000000;

			for (int i = 1; i < count; i += step)
				Send(i.ToString(), i + step, hash);

			return "Zaebca";
		}

		public void ReciveSync()
		{
			_sender.ReceiveSync();
=======

		string GetRange(string startNumber)
		{
            // todo;
		    return "";
>>>>>>> b9f0bc3bcc549387989859d19bcd35c2c7a7c9e7
		}
    }
}
