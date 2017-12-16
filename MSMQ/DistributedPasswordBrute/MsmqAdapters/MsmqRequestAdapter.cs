using System.Messaging;
using System.Text;
using Message = System.Messaging.Message;

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
            _requestQueue = new MessageQueue(requestQueueName);
            _replyQueue = new MessageQueue(replyQueueName);

            // Фильтр для считывания сообщения со всеми свойствами
            _replyQueue.MessageReadPropertyFilter.SetAll();

            // Задаем формат содержимого сообщения как строку.
            // ReSharper disable once RedundantExplicitArrayCreation
            ((XmlMessageFormatter)_replyQueue.Formatter).TargetTypeNames = new string[] { "System.String,mscorlib" };
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
            StringBuilder msgBody = new StringBuilder(start + " " + count.ToString());
            foreach (var hash in hashSumArr)
            {
                msgBody.Append(' ');
                msgBody.Append(hash);
            }
            
            requestMessage.Body = msgBody;              // Задаем содержимое сообщения
            requestMessage.ResponseQueue = _replyQueue; // Задаем обратный адрес

            // Отправляем сообщение
            _requestQueue.Send(requestMessage);

            // для дебага
            StringBuilder str = new StringBuilder();
            str.AppendLine("Sent request message");
            str.AppendLine("Message ID:" + requestMessage.Id);
            str.AppendLine("Reply to:" + requestMessage.ResponseQueue.Path);
            str.AppendLine("Message Body:" + requestMessage.Body);

            return str.ToString();
            return requestMessage.Id;
        }

        public string ReceiveSync()
        {
            Message replyMessage = _replyQueue.Receive();

            if (replyMessage == null) 
                return null;

            StringBuilder info = new StringBuilder();
            info.AppendLine("Received reply");
            info.AppendLine("Message ID:" + replyMessage.Id);
            info.AppendLine("Message Correlation ID:" + replyMessage.CorrelationId);
            info.AppendLine("Message Body:" + replyMessage.Body);

            return info.ToString();
        }
    }
}
