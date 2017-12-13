using System.Messaging;
using System.Text;
using Message = System.Messaging.Message;

namespace MsmqAdapters
{
    public class MsmqRequestorAdapter
    {
        private readonly MessageQueue _requestQueue;
        private readonly MessageQueue _replyQueue;

        public MsmqRequestorAdapter(string requestQueueName, string replyQueueName)
        {
            _requestQueue = new MessageQueue(requestQueueName);
            _replyQueue = new MessageQueue(replyQueueName);

            _replyQueue.MessageReadPropertyFilter.SetAll();
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
            
            requestMessage.Body = msgBody;
            requestMessage.ResponseQueue = _replyQueue;
            _requestQueue.Send(requestMessage);

            return requestMessage.Id;
        }

        public string ReceiveSync() //поменял тут с void на string lenin
        {
            Message replyMessage = _replyQueue.Receive();

			return null;
        }
    }
}
