using System;
using System.Messaging;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
            
            //string requestQueueName = ".\\Private$\\RequestMessages";

           //MessageQueue myQueue = MessageQueue.Create(requestQueueName);
            //myQueue.Send("Public queue by path name.");
            //var requestQueue = MessageQueue.Create(requestQueueName);

            MsmqReplierAdapter replier = new MsmqReplierAdapter(".\\Private$\\RequestQueue", ".\\Private$\\InvalidQueue", new Agent.Agent());

            while (true)
            {
                var command = Console.ReadLine(); //Считывание команды с консоли
                if (command != null)
                    return;
            }
        }
    }
}
