using System;
using System.Net;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
			const string managerIp = "192.168.0.101";	// IPv4 менеджера сообщений

#pragma warning disable 618
			var agentIp = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();	// наш IPv4 (агента)
#pragma warning restore 618

			new MsmqReplierAdapter(managerIp, agentIp, new Agent.Agent());

	        Console.ReadLine();
        }
    }
}
