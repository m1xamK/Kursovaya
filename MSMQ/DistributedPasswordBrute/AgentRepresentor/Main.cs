using System;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
	        new MsmqReplierAdapter("FormatName:Direct=TCP:192.168.0.102\\private$\\RequestQueue",
		        "FormatName:Direct=TCP:192.168.0.102\\private$\\InvalidQueue", new Agent.Agent());

	        Console.ReadLine();
        }
    }
}
