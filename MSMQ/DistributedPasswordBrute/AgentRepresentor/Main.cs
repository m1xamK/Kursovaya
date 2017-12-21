using System;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
			MsmqReplierAdapter replier = new MsmqReplierAdapter(Environment.MachineName + "\\Private$\\RequestQueue",
                Environment.MachineName + "\\Private$\\InvalidQueue", new Agent.Agent());

			Console.ReadLine();
        }
    }
}
