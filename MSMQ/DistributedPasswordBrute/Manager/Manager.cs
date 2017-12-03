using MsmqAdapters;

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
		private MsmqRequestorAdapter _sender;

		/// <summary>
		/// указываем пути до ресурсов обмена
		/// </summary>
        /// <param name="requestResource"></param>
        /// <param name="replayResourse"></param>
        Manager(string requestResource, string replayResourse)
		{
            _sender = new MsmqRequestorAdapter(requestResource, replayResourse);
		}

		//не ну а чо тут непонятно?????
		/// <summary>
		/// отправка сообщения агенту 
		/// </summary>
		/// <param name="start"> начальний сдвиг числа от которого пойдет перебор </param>
		/// <param name="count"> сколько надо посчитать агенту строк после</param>
		/// <param name="hash">праобразы md5 сверток</param>
		void Send(string start, int count, string[] hash)
		{
			_sender.Send(start, count, hash);
		}


		string GetRange(string startNumber)
		{
            // todo;
		    return "";
		}
    }
}
