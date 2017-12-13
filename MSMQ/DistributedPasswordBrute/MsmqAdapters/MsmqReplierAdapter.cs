using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MsmqAdapters
{
    public class MsmqReplierAdapter
    {
        private MessageQueue _invalidQueue;

        public MsmqReplierAdapter(String requestQueueName, String invalidQueueName)
        {
            MessageQueue requestQueue = new MessageQueue(requestQueueName);
            _invalidQueue = new MessageQueue(invalidQueueName);

            requestQueue.MessageReadPropertyFilter.SetAll();
            ((XmlMessageFormatter)requestQueue.Formatter).TargetTypeNames = new string[]{"System.String,mscorlib"};

            requestQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveCompleted);
            requestQueue.BeginReceive();
        }

        public void OnReceiveCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue requestQueue = (MessageQueue) source;
            Message requestMessage = requestQueue.EndReceive(asyncResult.AsyncResult);

            try
            {

            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}
