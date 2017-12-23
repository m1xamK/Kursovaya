using System;
using System.Collections.Generic;
using System.Linq;
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
		/// Для инициализации объекта MsmqReplierAdapter следует передать IP адрес менеджера сообщений и IP адрес агента для формирования
	    /// имен очереди запросов и очереди сообщений недопустимого формата,
	    /// объект очереди ответов указывается с помощью "обратного адреса" сообщения с запросом.
	    /// </summary>
		/// <param name="managerIp">IPv4 адрес менеджера</param>
		/// <param name="agentIp">IPv4 адрес агента</param>
	    /// <param name="agent">экземпляр класса предназначенный непосредственно для обработки сообщения</param>
	    public MsmqReplierAdapter(String managerIp, String agentIp, Agent.Agent agent)
        {
            _agent = agent;

		    var requestQueueName = "FormatName:Direct=TCP:" + managerIp + "\\private$\\RequestQueue";
		    var requestQueue = new MessageQueue(requestQueueName, true);

		    var invalidQueueName = "FormatName:Direct=TCP:" + agentIp + "\\private$\\InvalidQueue";
            _invalidQueue = new MessageQueue(invalidQueueName);

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
				// что бы убедиться что работает.
				Console.WriteLine("Message ID:\t{0}", requestMessage.Id);				

                var messageBody = requestMessage.Body.ToString();

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
	                str = passwdList.Aggregate(str, (current, pair) => current + (pair.Key + " " + pair.Value + " "));

	                str = str.Substring(0, str.Length - 1);
                }
	            replyMessage.Body = str;
	            replyMessage.ResponseQueue = requestQueue;
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
