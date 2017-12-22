using System;
using System.Net;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
			var managerIp = "192.168.0.100";
			var agentIp = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
			new MsmqReplierAdapter("FormatName:Direct=TCP:" + managerIp + "\\private$\\RequestQueue",	// da da delaem share request y managera
				"FormatName:Direct=TCP:" + agentIp +"\\private$\\InvalidQueue", new Agent.Agent());

	        Console.ReadLine();
        }
    }
}
