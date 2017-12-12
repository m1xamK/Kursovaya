using System;
using System.Messaging;
using System.Text;
using Message = System.Messaging.Message;

namespace MsmqAdapters
{
    public class MsmqRequestorAdapter
    {
        private MessageQueue _requestQueue;
        private MessageQueue _replyQueue;

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
        public string Send(string msgId, string start, int count, string[] hashSumArr)
        {
            Message requestMessage = new Message();
            StringBuilder msgBody = new StringBuilder(msgId + " " + start + " " + count.ToString());
            foreach (var hash in hashSumArr)
            {
                msgBody.Append(' ');
                msgBody.Append(hash);
            }

            requestMessage.Body = msgBody;
            requestMessage.ResponseQueue = _replyQueue;
            _requestQueue.Send(requestMessage);

            return "msg " + msgId + " send";
        }

        public void ReceiveSync()
        {
            Message replyMessage = _replyQueue.Receive();
        }
    }
}
