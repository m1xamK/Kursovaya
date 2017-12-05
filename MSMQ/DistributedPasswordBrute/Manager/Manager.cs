using System;
using System.Security.Permissions;
using RequestManager;

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
		private readonly RequestorAdapter _sender;


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
		Manager(string requestResurce, string replayResurse)
		{
			_sender = new RequestorAdapter(requestResurce, replayResurse);
		}
	
		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="start"> начальний сдвиг числа от которого пойдет перебор </param>
		/// <param name="count"> сколько надо посчитать агенту строк после</param>
		/// <param name="hash">праобразы md5 сверток</param>
		private void Send(string start, int count, string[] hash)
		{
			_sender.Send(start, count, hash);
		}

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
		}
    }
}
