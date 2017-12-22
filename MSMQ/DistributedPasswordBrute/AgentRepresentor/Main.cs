using System;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
	        new MsmqReplierAdapter("FormatName:Direct=TCP:169.254.241.60\\private$\\RequestQueue",
                "FormatName:Direct=TCP:169.254.241.60\\private$\\InvalidQueue", new Agent.Agent());

	        Console.ReadLine();
        }
    }
}
