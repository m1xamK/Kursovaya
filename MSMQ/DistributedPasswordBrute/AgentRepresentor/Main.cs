using System;
using System.IO;
using System.Net;
using MsmqAdapters;

namespace AgentRepresentor
{
    public static class AgentRepresentor
    {
        public static void Main()
        {
			string configPath = "ip_config_agent.txt";

			string managerIp = File.ReadAllLines(configPath)[0]; // IPv4 менеджера сообщений

#pragma warning disable 618
			var agentIp = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();	// наш IPv4 (агента)
#pragma warning restore 618

			new MsmqReplierAdapter(managerIp, agentIp, new Agent.Agent());

	        Console.ReadLine();
        }
    }
}
