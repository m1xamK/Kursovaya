using System;
using System.Collections.Generic;
using System.Messaging;

namespace MsmqAdapters
{
    /// <summary>
    /// Используется приложением которому необходимо получать запросы и отправлять ответы
    /// </summary>
    public class MsmqReplierAdapter
    {
        private readonly MessageQueue _invalidQueue;
        private readonly Agent.Agent _agent;

	    /// <summary>
	    /// Для инициализации объекта MsmqReplierAdapter следует передать имена очереди запросов и очереди сообщений недопустимого формата
	    /// объект очереди ответов указывается с помощью "обратного адреса" сообщения с запросом.
	    /// </summary>
	    /// <param name="requestQueueName">имя очереди запросов</param>
	    /// <param name="invalidQueueName">имя очереди сообщений недопустимого формата</param>
	    /// <param name="agent">экземпляр класса предназначенный непосредственно для обработки сообщения</param>
	    public MsmqReplierAdapter(String requestQueueName, String invalidQueueName, Agent.Agent agent)
        {
            _agent = agent;
            var machineName = Environment.MachineName;
            MessageQueue requestQueue = !MessageQueue.Exists(requestQueueName) ? MessageQueue.Create(requestQueueName) : new MessageQueue(requestQueueName);
            _invalidQueue = !MessageQueue.Exists(invalidQueueName) ? MessageQueue.Create(invalidQueueName) : new MessageQueue(invalidQueueName);

            requestQueue.MessageReadPropertyFilter.SetAll();
            ((XmlMessageFormatter)requestQueue.Formatter).TargetTypeNames = new[] { "System.String" };

            // Создаем экземпляр делегата ReceiveCompletedEventHandler
            // Система сообщений будет автоматически вызывать метод OnReceiveCompleted при поступлении нового сообщения в очередь
            requestQueue.ReceiveCompleted += OnReceiveCompleted;

            // Переходим в режим ожидания.
            requestQueue.BeginReceive();
        }

        /// <summary>
        /// Обрабатывает то что пришло в очередь.
        /// </summary>
        /// <param name="source">Объект из очереди запросов.</param>
		/// <param name="asyncResult">Объект класса ReceiveCompletedEventArgs</param>
        public void OnReceiveCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue requestQueue = (MessageQueue)source;

            // Извлекаем сообщение из очереди
            Message requestMessage = requestQueue.EndReceive(asyncResult.AsyncResult);

            try
            {
				Console.WriteLine("Message ID:\t{0}", requestMessage.Id);				

                string messageBody = requestMessage.Body.ToString();

				// Разбор сообщения
				var messageInfo = messageBody.Split(' ');

				string start = messageInfo[0];
				string finish = messageInfo[1];

				List<string> hashSumList = new List<string>();
				for (int i = 2; i < messageInfo.Length; ++i)
					hashSumList.Add(messageInfo[i]);

				// Находим пароли
                List<KeyValuePair<string, string>> passwdList = _agent.SearchPassword(start, finish, hashSumList);
                
                // Устанавливаем обратный адрес
                MessageQueue replyQueue = requestMessage.ResponseQueue;
                
                // Формируем новое сообщение
                Message replyMessage = new Message();

				string str = "";
                if (passwdList.Count != 0)
                {
	                foreach (var pair in passwdList)
		                str += pair.Key + " " + pair.Value + " ";

	                str = str.Substring(0, str.Length - 1);
                }
				replyMessage.Body = str;

                replyMessage.CorrelationId = requestMessage.Id;

                // Отправляем сообщение
                replyQueue.Send(replyMessage);
            }
            // Если пришел объект не типа Message или отсутствует обратный адрес, то попадаем сюда.
            catch (Exception)
            {
                requestMessage.CorrelationId = requestMessage.Id;

                _invalidQueue.Send(requestMessage);
            }

            // Снова переходим в режим ожидания.
            requestQueue.BeginReceive();
        }
    }
}
