using System;
using System.Collections.Generic;
using System.IO;
using System.Messaging;
using System.Text.RegularExpressions;
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
        public MsmqRequestorAdapter() //string requestQueueName, string replyQueueName)
        {
			string configPath = "ip_config_ma.txt";

			string[] lines = File.ReadAllLines(configPath);

			var managerIp = lines[0];

			List<string> agentsIp = new List<string>();

			for (int i = 0; i < lines.Length - 1; ++i)
				agentsIp.Add(lines[i]);

	     	string[] requestQueueName = { "FormatName:Direct=TCP:" + agentsIp[0] + "\\Private$\\RequestQueue", "FormatName:Direct=TCP:" + agentsIp[1] + "\\Private$\\RequestQueue" };
			string replyQueueName = "FormatName:Direct=TCP:" + managerIp + "\\Private$\\ReplyQueue";

	        _requestQueue = new[]
	        {
		        new MessageQueue(requestQueueName[0]),
		        new MessageQueue(requestQueueName[1])
	        };
	        
			_replyQueue = new MessageQueue(replyQueueName);

	        try
	        {
				_replyQueue.Purge();
	        }
	        catch (Exception)
	        {}
		
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
			
			requestMessage.TimeToBeReceived = TimeSpan.FromMinutes(1);	// Время жизни сообщения в очереди

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
            Message replyMessage = _replyQueue.Receive();

            if (replyMessage == null) 
                return null;

            return replyMessage;
        }

		// Освобождает ресурсы, выделенные для _replyQueue
	    public void StopSession()
	    {
			_replyQueue.Purge();
		    _replyQueue.Close();
	    }
    }
}
