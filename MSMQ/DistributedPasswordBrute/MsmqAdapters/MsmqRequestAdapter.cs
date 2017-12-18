using System;
using System.Messaging;
using System.Text;
using Message = System.Messaging.Message;
using System.Runtime.Serialization;
using System.IO;

// ".\private$\ReplyQueue"
// ".\private$\RequestQueue"

namespace MsmqAdapters
{
    public class MsmqRequestorAdapter
    {
        private readonly MessageQueue _requestQueue;
        private readonly MessageQueue _replyQueue;

        /// <summary>
        /// Инициализирует объект класса MsmqRequestorAdapter
        /// </summary>
        /// <param name="requestQueueName">имя очереди запросов</param>
        /// <param name="replyQueueName">имя очереди ответов</param>
        public MsmqRequestorAdapter(string requestQueueName, string replyQueueName)
        {
            _requestQueue = !MessageQueue.Exists(requestQueueName) ? MessageQueue.Create(requestQueueName) : new MessageQueue(requestQueueName);

            _replyQueue = !MessageQueue.Exists(replyQueueName) ? MessageQueue.Create(replyQueueName) : new MessageQueue(replyQueueName);

            // Фильтр для считывания сообщения со всеми свойствами
            _replyQueue.MessageReadPropertyFilter.SetAll();

            // Задаем формат содержимого сообщения как строку.
            // ReSharper disable once RedundantExplicitArrayCreation
            ((XmlMessageFormatter)_replyQueue.Formatter).TargetTypeNames = new string[] { "System.String" };
        }

        /// <summary>
        /// Отправляет сообщение
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="hashSumArr"></param>
        public string Send(string start, int count, string[] hashSumArr)
        {
            Message requestMessage = new Message();

	        string msgBody = start + " " + count;

            foreach (var hash in hashSumArr)
            {
                msgBody += ' ';
                msgBody += hash;
            }
			
            requestMessage.Body = msgBody;              // Задаем содержимое сообщения
            
            requestMessage.ResponseQueue = _replyQueue; // Задаем обратный адрес
			
            // Отправляем сообщение
			_requestQueue.Send(requestMessage);

            // для дебага
            Console.WriteLine("Sent request message");
            Console.WriteLine("Message ID: {0}", requestMessage.Id);
            Console.WriteLine("Reply to: {0}", requestMessage.ResponseQueue.Path);
            Console.WriteLine("Message Body: {0}",requestMessage.Body);
            
            return requestMessage.Id;
        }

        public Message ReceiveSync()
        {
            Message replyMessage = _replyQueue.Receive();

            if (replyMessage == null) 
                return null;
            
            // для дебага
            Console.WriteLine("Received reply");
            Console.WriteLine("Message ID:" + replyMessage.Id);
            Console.WriteLine("Message Correlation ID:" + replyMessage.CorrelationId);
            Console.WriteLine("Message Body:" + replyMessage.Body + "\n");

            return replyMessage;
        }
    }
}
