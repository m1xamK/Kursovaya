using System;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
			MsmqReplierAdapter replier = new MsmqReplierAdapter("DESKTOP-OUP4I3U\\Private$\\RequestQueue",
				"DESKTOP-OUP4I3U\\Private$\\InvalidQueue", new Agent.Agent());

			Console.ReadLine();
        }
    }
}
