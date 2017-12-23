using System;
using System.Messaging;
using MsmqAdapters;
using NUnit.Framework;

namespace Agent.Tests
{
	[TestFixture]
	class MsmqRequestorAdapterTest
	{
		private readonly string managerIp = "192.168.0.101";
		private readonly string agentIp = "192.168.0.101";

		private Message SendTestMessage(string start, string finish, string[] hashArr)
		{
			var requestAdapter = new MsmqRequestorAdapter();

			requestAdapter.Send(start, finish, hashArr);
			return requestAdapter.ReceiveSync();
		}

		/// <summary>
		/// Для тестирования необходимо изменить путь к файлу конфига.
		/// </summary>
		[Test]
		public void ValidMessage()
		{
			// ReSharper disable once UnusedVariable
			// нужен неявно, так как в конструкторе подписывается на событие прихода сообщения в очередь и обрабатывает его.
			var replierAdapter = new MsmqReplierAdapter(managerIp, agentIp, new Agent());

			string start = "0";
			string finish = "1000";

			string password = "zs";
			var hash = new Agent().Md5Hash(password);
			string[] hashArr = { hash };

			var message = SendTestMessage(start, finish, hashArr);
			
			Assert.That(hash + " " + password, Is.EqualTo(message.Body.ToString()));
		}
	}
}
