using System;
using System.Messaging;
using System.Text;

// ".\private$\InvalidMessages"

namespace MsmqAdapters
{
    /// <summary>
    /// Используется приложением которому необходимо получать запросы и отправлять ответы
    /// </summary>
    public class MsmqReplierAdapter
    {
        private MessageQueue _invalidQueue;

        /// <summary>
        /// Для инициализации объекта MsmqReplierAdapter следует передать имена очереди запросов и очереди сообщений недопустимого формата
        /// объект очереди ответов указывается с помощью "обратного адреса" сообзения с запросом.
        /// </summary>
        /// <param name="requestQueueName">имя очереди запросов</param>
        /// <param name="invalidQueueName">имя очереди сообщений недопустимого формата</param>
        public MsmqReplierAdapter(String requestQueueName, String invalidQueueName)
        {
            MessageQueue requestQueue = new MessageQueue(requestQueueName);
            _invalidQueue = new MessageQueue(invalidQueueName);

            requestQueue.MessageReadPropertyFilter.SetAll();
            ((XmlMessageFormatter)requestQueue.Formatter).TargetTypeNames = new string[]{"System.String,mscorlib"};

            // Создаем экземпляр делегата ReceiveCompletedEventHandler
            // Система сообщений будет автоматически вызывать метод OnReceiveCompleted при поступлении нового сообщения в очередь
            requestQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveCompleted);

            // Переходим в режим ожидания.
            requestQueue.BeginReceive();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="asyncResult"></param>
        public void OnReceiveCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue requestQueue = (MessageQueue)source;

            // Извлекаем сообщение из очереди
            Message requestMessage = requestQueue.EndReceive(asyncResult.AsyncResult);

            try
            {
                StringBuilder resultStr = new StringBuilder();
                Console.WriteLine("Received request");
                Console.WriteLine("Time:\t{0}", DateTime.Now.ToString("HH:mm:ss"));
                Console.WriteLine("Message ID:\t{0}", requestMessage.Id);
                Console.WriteLine(requestMessage.Body);

                string messageBody = requestMessage.Body.ToString();

                // Реализация "обратного адреса"
                MessageQueue replyQueue = requestMessage.ResponseQueue;
                
                // Формируем новое сообщение
                Message replyMessage = new Message();
                replyMessage.Body = messageBody;
                replyMessage.CorrelationId = requestMessage.Id;

                // Отправляем сообщение
                replyQueue.Send(replyMessage);

                Console.WriteLine("Message sent");
            }
            // Если пришел объект не типа Message или отсутствует обратный адрес, то попадаем сюда.
            catch (Exception)
            {
                StringBuilder resultStr = new StringBuilder();
                Console.WriteLine("Invalid message!");
                Console.WriteLine("Time:\t{0}", DateTime.Now.ToString("HH:mm:ss"));
                Console.WriteLine("Message ID:\t{0}", requestMessage.Id);

                requestMessage.CorrelationId = requestMessage.Id;

                _invalidQueue.Send(requestMessage);

                Console.WriteLine("Message sent to invalid message queue");
            }
            // Снова переходим в режим ожидания.
            requestQueue.BeginReceive();
        }
    }
}
