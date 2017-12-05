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

        public void Send(string start, int count, string[] hashSumArr)
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

            Console.WriteLine("msg send");
        }

        public void ReceiveSync()
        {
            Message replyMessage = _replyQueue.Receive();
        }
    }
}
