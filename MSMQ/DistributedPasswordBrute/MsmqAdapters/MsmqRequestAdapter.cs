using System;
using System.Messaging;
using Message = System.Messaging.Message;

namespace MsmqAdapters
{
    public class MsmqRequestorAdapter
    {
        private readonly MessageQueue _requestQueue;
        private readonly MessageQueue _replyQueue;

        /// <summary>
        /// Инициализирует объект класса MsmqRequestorAdapter
        /// </summary>
        /// <param name="requestQueueName">Имя очереди запросов</param>
        /// <param name="replyQueueName">Имя очереди ответов</param>
        public MsmqRequestorAdapter(string requestQueueName, string replyQueueName)
        {
	        if (MessageQueue.Exists(requestQueueName))
	        {
		        _requestQueue = new MessageQueue(requestQueueName);
		        _requestQueue.Purge();
	        }
	        else
		        _requestQueue = MessageQueue.Create(requestQueueName);

	        if (MessageQueue.Exists(requestQueueName))
	        {
		        _replyQueue = new MessageQueue(replyQueueName);
		        _replyQueue.Purge();
	        }
	        else
		        _replyQueue = MessageQueue.Create(replyQueueName);

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
	    public string Send(string start, string finish, string[] hashSumArr)
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
			_requestQueue.Send(requestMessage);

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
            Message replyMessage = _replyQueue.Receive();

            if (replyMessage == null) 
                return null;

            return replyMessage;
        }
    }
}
