﻿using System;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
			MsmqReplierAdapter replier = new MsmqReplierAdapter("FormatName:Direct=TCP:192.168.43.233\\Private$\\RequestQueue",
				"FormatName:Direct=TCP:192.168.43.233\\Private$\\InvalidQueue", new Agent.Agent());

			Console.ReadLine();
        }
    }
}
