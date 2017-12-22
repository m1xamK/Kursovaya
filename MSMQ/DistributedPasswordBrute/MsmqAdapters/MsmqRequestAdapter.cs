using System;
using System.Messaging;
using Message = System.Messaging.Message;

namespace MsmqAdapters
{
    public class MsmqRequestorAdapter
    {
        private MessageQueue[] _requestQueue;
        private readonly MessageQueue _replyQueue;
	    private int _requestCount;

        /// <summary>
        /// Инициализирует объект класса MsmqRequestorAdapter
        /// </summary>
        /// <param name="requestQueueName">Имя очереди запросов</param>
        /// <param name="replyQueueName">Имя очереди ответов</param>
        public MsmqRequestorAdapter() //string requestQueueName, string replyQueueName)
        {
			var managerIp = "192.168.0.100";
	        string[] agentsIp = {"192.168.0.101", "192.168.0.100"};
			string[] requestQueueName = { "FormatName:Direct=TCP:" + agentsIp[0] + "\\Private$\\RequestQueue", "FormatName:Direct=TCP:" + agentsIp[1] + "\\Private$\\RequestQueue" };
			string replyQueueName = "FormatName:Direct=TCP:" + managerIp + "\\Private$\\ReplyQueue";

	        _requestQueue = new[]
	        {
		        new MessageQueue(requestQueueName[0]),
		        new MessageQueue(requestQueueName[1])
	        };
	        
			_replyQueue = new MessageQueue(replyQueueName);

            // Фильтр для считывания сообщения со всеми свойствами
            _replyQueue.MessageReadPropertyFilter.SetAll();

            // Задаем формат содержимого сообщения как строку.
            // ReSharper disable once RedundantExplicitArrayCreation
            ((XmlMessageFormatter)_replyQueue.Formatter).TargetTypeNames = new string[] { "System.String" };
        }

		/// <summary>
		/// Отправляет сообщение в очередь запросов.
		/// </summary>
		/// <param name="start">Строка от которой агент начнет подбирать хеши</param>
		/// <param name="finish">Строка до которой агент подбирает хеши</param>
		/// <param name="hashSumArr">Хеши, которые пытается найти</param>
	    /// <returns>Идентификатор отправленного сообщения</returns>
	    public string Send(string start, string finish, string[] hashSumArr, MessageQueue queue = null)
        {
            Message requestMessage = new Message();

			string msgBody = start + " " + finish;

            foreach (var hash in hashSumArr)
            {
                msgBody += ' ';
                msgBody += hash;
            }
			
            requestMessage.Body = msgBody;              // Задаем содержимое сообщения
            
            requestMessage.ResponseQueue = _replyQueue; // Задаем обратный адрес
			
			requestMessage.TimeToBeReceived = TimeSpan.FromMinutes(1);

            // Отправляем сообщение
			if (queue == null)
				_requestQueue[++_requestCount % _requestQueue.Length].Send(requestMessage);
			else
				queue.Send(requestMessage);


			// для дебага
			Console.WriteLine("Sent request message");
			Console.WriteLine(start + "\n");
            
            return requestMessage.Id;
        }

		/// <summary>
		/// Ожидает сообщение из очереди ответов.
		/// </summary>
		/// <returns>Полученное сообщение.</returns>
        public Message ReceiveSync()
		{
			try
			{
				var waitTimeOut = TimeSpan.FromMilliseconds(10);
				Message replyMessage = _replyQueue.Receive(waitTimeOut);

				return replyMessage;
			}
			catch (Exception)
			{
				return null;
			}
        }

		// Освобождает ресурсы, выделенные для _replyQueue
	    public void StopSession()
	    {
		    _replyQueue.Close();
	    }
    }
}
