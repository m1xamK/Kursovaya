using System;
using System.Collections.Generic;
using System.IO;
using System.Messaging;
using Message = System.Messaging.Message;

namespace MsmqAdapters
{
    public class MsmqRequestorAdapter
    {
		private readonly List<MessageQueue> _requestQueue;
        private readonly MessageQueue _replyQueue;
	    private int _requestCount;

        /// <summary>
        /// Инициализирует объект класса MsmqRequestorAdapter
        /// </summary>
        public MsmqRequestorAdapter()
        {
	        _requestQueue = new List<MessageQueue>();
			string configPath = "ip_config_manager.txt";
				//"C:\\Users\\Mikhail\\Documents\\Visual Studio 2013\\Projects\\Kursovaya\\Kursovaya\\MSMQ\\DistributedPasswordBrute\\Tests\\Files\\ip_config_manager.txt";
	        string managerIp;
			List<string> agentsIp = new List<string>();

	        try
	        {
				string[] lines = File.ReadAllLines(configPath);
				managerIp = lines[0];
				
				for (int i = 1; i < lines.Length; ++i)
					agentsIp.Add(lines[i]);
	        }
	        catch (Exception)
	        {
				Console.WriteLine("no file ip_config_manager.txt exist");
				return;
	        }
			
	        foreach (var ip in agentsIp)
	        {
		        var requestQueueName = "FormatName:Direct=TCP:" + ip + "\\Private$\\RequestQueue";
				_requestQueue.Add(new MessageQueue(requestQueueName));
	        }

			var replyQueueName = "FormatName:Direct=TCP:" + managerIp + "\\Private$\\ReplyQueue";
			_replyQueue = new MessageQueue(replyQueueName);

	        try
	        {
				_replyQueue.Purge();
	        }
	        catch (Exception)
	        {
		        // ignored
	        }

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
	    /// <param name="queue">Очередь в которую отправляем сообщение.</param>
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
				_requestQueue[++_requestCount % _requestQueue.Count].Send(requestMessage);
			else
				queue.Send(requestMessage);

			// для дебага чтобы вы убиделись, что работает
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
				var timeOutWait = TimeSpan.FromMilliseconds(100);
				Message replyMessage = _replyQueue.Receive(timeOutWait);

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
			_replyQueue.Purge();
		    _replyQueue.Close();
	    }
    }
}
