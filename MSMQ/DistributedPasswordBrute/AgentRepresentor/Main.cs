using System;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
            MsmqReplierAdapter replier = new MsmqReplierAdapter(".\\private$\\RequestMessages", ".\\private$\\InvalidMessages", new Agent.Agent());

            while (true)
            {
                var command = Console.ReadLine(); //Считывание команды с консоли
                if (command != null)
                    return;
            }
        }
    }
}
